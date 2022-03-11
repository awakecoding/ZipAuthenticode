using System;
using System.IO;
using System.Runtime.InteropServices;

using Devolutions.Authenticode;
using Devolutions.ZipAuthenticode;

namespace Devolutions.ZipAuthenticode
{
    class Program
    {
        static void TestZipAuthenticode()
        {
            Console.WriteLine("ZipAuthenticode!");

            string unsignedZipFileName = "../data/test-unsigned.zip";
            ZipFile unsignedZipFile = new ZipFile(unsignedZipFileName);
            string unsignedZipDigest = unsignedZipFile.GetDigestString();
            Console.WriteLine("unsigned zip digest: {0}", unsignedZipDigest);

            string inputSignatureFileName = "../data/test.sig.ps1";

            string sigDigest = string.Empty;
            string sigBlock = string.Empty;
            string sigCommentLine = ZipFile.LoadSignatureFile(inputSignatureFileName, out sigDigest, out sigBlock);

            Console.WriteLine(sigCommentLine);
            ZipFile.SplitSignatureCommentLine(sigCommentLine, out sigDigest, out sigBlock);
            string outputSignatureFileName = "../data/output.sig.ps1";
            ZipFile.SaveSignatureFile(outputSignatureFileName, sigCommentLine);

            string signedZipFileName = "../data/test-signed.zip";
            ZipFile signedZipFile = new ZipFile(signedZipFileName);
            string signedZipDigest = signedZipFile.GetDigestString();
            Console.WriteLine("signed zip digest: {0}", signedZipDigest);

            signedZipFile.ExportSignatureFile(outputSignatureFileName);
            string sig = signedZipFile.GetSignatureFileData() ?? String.Empty;

            Signature signature = SignatureHelper.GetSignature("zip.sig.ps1", sig);

            Console.WriteLine("Signature StatusMessage: {0}",
                signature.StatusMessage);

            Console.WriteLine("Signature Issuer: {0}",
                 signature.SignerCertificate.Issuer);
        }

        static void Main(string[] args)
        {
            TestZipAuthenticode();
        }
    }
}
