using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Devolutions.ZipAuthenticode
{
    public class ZipFile
    {
        // https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT

        public const uint ZipLocalFileHeaderSignature = 0x04034b50;
        public const uint ZipLocalFileHeaderSize = 30;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 30)]
        public struct ZipLocalFileHeader
        {
            public uint signature;
            public ushort version;
            public ushort bitflags;
            public ushort compressionMethod;
            public ushort lastModFileTime;
            public ushort lastModFileDate;
            public uint crc32;
            public uint compressedSize;
            public uint uncompressedSize;
            public ushort fileNameLength;
            public ushort extraFieldLength;
        }

        public const uint ZipCentralFileHeaderSignature = 0x02014b50;
        public const uint ZipCentralFileHeaderSize = 46;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 46)]
        public struct ZipCentralFileHeader
        {
            public uint signature;
            public ushort versionUsed;
            public ushort versionRequired;
            public ushort bitflags;
            public ushort compressionMethod;
            public ushort lastModeFileTime;
            public ushort lastModFileDate;
            public uint crc32;
            public uint compressedSize;
            public uint uncompressedSize;
            public ushort fileNameLength;
            public ushort extraFieldLength;
            public ushort fileCommentLength;
            public ushort diskNumberStart;
            public ushort internalFileAttributes;
            public uint externalFileAttributes;
            public uint relativeOffsetOfLocalHeader;
        }

        public const uint ZipEndOfCentralDirHeaderSignature = 0x06054b50;
        public const uint ZipEndOfCentralDirHeaderSize = 22;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 22)]
        public struct ZipEndOfCentralDirHeader
        {
            public uint signature;
            public ushort diskNumberCurrent;
            public ushort diskNumberCentral;
            public ushort diskEntryCountCurrent;
            public ushort diskEntryCountCentral;
            public uint centralDirSize;
            public uint centralDirOffset;
            public ushort fileCommentLength;
        }

        public byte[] data;

        private long GetLocalFileRecordSize(byte[] data, long offset)
        {
            long size = data.Length;
            long headerSize = ZipLocalFileHeaderSize;

            unsafe
            {
                if ((offset + headerSize) > size)
                {
                    return -1;
                }

                fixed (byte* ptr = data)
                {
                    ZipLocalFileHeader* hdr = (ZipLocalFileHeader*) &ptr[offset];
                    long recordSize = sizeof(ZipLocalFileHeader) + hdr->compressedSize + hdr->fileNameLength + hdr->extraFieldLength;

                    if ((offset + recordSize) > size)
                    {
                        return -1;
                    }

                    return recordSize;
                }
            }
        }

        private long GetCentralFileRecordSize(byte[] data, long offset)
        {
            long size = data.Length;
            long headerSize = ZipCentralFileHeaderSize;

            unsafe
            {
                if ((offset + headerSize) > size)
                {
                    return -1;
                }

                fixed (byte* ptr = data)
                {
                    ZipCentralFileHeader* hdr = (ZipCentralFileHeader*) &ptr[offset];
                    long recordSize = sizeof(ZipCentralFileHeader) + hdr->fileNameLength + hdr->extraFieldLength;

                    if ((offset + recordSize) > size)
                    {
                        return -1;
                    }

                    return recordSize;
                }
            }
        }

        private long GetEndOfCentralDirRecordSize(byte[] data, long offset)
        {
            long size = data.Length;
            long headerSize = ZipEndOfCentralDirHeaderSize;

            unsafe
            {
                if ((offset + headerSize) > size)
                {
                    return -1;
                }

                fixed (byte* ptr = data)
                {
                    ZipEndOfCentralDirHeader* hdr = (ZipEndOfCentralDirHeader*)&ptr[offset];
                    long recordSize = sizeof(ZipEndOfCentralDirHeader) + hdr->fileCommentLength;

                    if ((offset + recordSize) > size)
                    {
                        return -1;
                    }

                    return recordSize;
                }
            }
        }

        private long FindZipFooterOffset(byte[] data)
        {
            unsafe
            {
                fixed (byte* ptr = data)
                {
                    long offset = 0;
                    long size = data.Length;
                    long recordSize = 0;

                    while (offset < (size - 4))
                    {
                        uint hdrSignature = *((uint*)&ptr[offset]);

                        if (hdrSignature == ZipLocalFileHeaderSignature)
                        {
                            recordSize = GetLocalFileRecordSize(data, offset);

                            if (recordSize < 0)
                            {
                                throw new InvalidDataException("Invalid zip local file record");
                            }

                            offset += recordSize;
                        }
                        else if (hdrSignature == ZipCentralFileHeaderSignature)
                        {
                            recordSize = GetCentralFileRecordSize(data, offset);

                            if (recordSize < 0)
                            {
                                throw new InvalidDataException("Invalid zip central file record");
                            }

                            offset += recordSize;
                        }
                        else if (hdrSignature == ZipEndOfCentralDirHeaderSignature)
                        {
                            recordSize = GetEndOfCentralDirRecordSize(data, offset);

                            if (recordSize < 0)
                            {
                                throw new InvalidDataException("Invalid end of zip central dir record");
                            }

                            return offset;
                        }
                        else
                        {
                            string message = String.Format("Unhandled zip record header 0x{0:X}", hdrSignature);
                            throw new InvalidDataException(message);
                        }
                    }

                    throw new InvalidDataException("Count not parse zip file");
                }
            }
        }

        private string? GetFileComment(byte[] data)
        {
            unsafe
            {
                fixed (byte* ptr = data)
                {
                    long offset = FindZipFooterOffset(data);

                    if (offset < 0)
                        return null;

                    ZipEndOfCentralDirHeader* hdr = (ZipEndOfCentralDirHeader*)&ptr[offset];

                    if (hdr->signature != ZipEndOfCentralDirHeaderSignature)
                        return null;

                    if (hdr->fileCommentLength < 0)
                        return null;

                    offset += sizeof(ZipEndOfCentralDirHeader);

                    return Encoding.UTF8.GetString(&ptr[offset], hdr->fileCommentLength);
                }
            }
        }

        public string? SetFileComment(string? comment)
        {
            comment = comment ?? String.Empty;
            string oldComment = String.Empty;

            long fileCommentOffset = 0;
            byte[] newCommentBytes = Encoding.UTF8.GetBytes(comment);
            ushort newCommentLength = (ushort)newCommentBytes.Length;

            unsafe
            {
                fixed (byte* ptr = data)
                {
                    long offset = FindZipFooterOffset(data);

                    if (offset < 0)
                        return null;

                    ZipEndOfCentralDirHeader* hdr = (ZipEndOfCentralDirHeader*)&ptr[offset];

                    if (hdr->signature != ZipEndOfCentralDirHeaderSignature)
                        return null;

                    offset += sizeof(ZipEndOfCentralDirHeader);

                    fileCommentOffset = offset;

                    if (hdr->fileCommentLength > 0)
                    {
                        oldComment = Encoding.UTF8.GetString(&ptr[fileCommentOffset], (int) hdr->fileCommentLength);
                    }

                    hdr->fileCommentLength = newCommentLength;
                }
            }

            int newFileSize = (int)fileCommentOffset + newCommentBytes.Length;

            if (data.Length != newFileSize)
            {
                Array.Resize(ref data, newFileSize);
            }

            Array.Copy(newCommentBytes, 0, data, fileCommentOffset, newCommentLength);

            return oldComment;
        }

        public string GetDigestString()
        {
            // https://github.com/opencontainers/image-spec/blob/v1.0.1/descriptor.md#digests

            using (SHA256 sha256 = SHA256.Create())
            {
                long offset = FindZipFooterOffset(data);
                offset += ZipEndOfCentralDirHeaderSize;
                byte[] tbsData = new byte[offset];
                Array.Copy(data, 0, tbsData, 0, offset);
                tbsData[offset - 1] = 0;
                tbsData[offset - 2] = 0;
                byte[] result = sha256.ComputeHash(tbsData);
                return "sha256:" + BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
            }
        }

        public void Save(string filename)
        {
            File.WriteAllBytes(filename, data);
        }

        public ZipFile(string filename)
        {
            data = File.ReadAllBytes(filename);
            string comment = GetFileComment(data) ?? string.Empty;
            Console.WriteLine("ZipFileComment: {0}", comment);
        }

        public static string GetZipDigestString(string filename)
        {
            ZipFile zipFile = new ZipFile(filename);
            return zipFile.GetDigestString();
        }
    }
}
