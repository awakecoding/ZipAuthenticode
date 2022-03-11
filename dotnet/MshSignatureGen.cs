﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a dotnet run from src\ResGen folder.
//     To add or remove a member, edit your .resx file then rerun src\ResGen.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Reflection;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]

internal class MshSignature
{

    private static global::System.Resources.ResourceManager resourceMan;

    private static global::System.Globalization.CultureInfo resourceCulture;

    /// <summary>constructor</summary>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal MshSignature()
    {
    }

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Resources.ResourceManager ResourceManager
    {
        get
        {
            if (resourceMan is null)
            {
                //global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("System.Management.Automation.resources.MshSignature", typeof(MshSignature).Assembly);
                //resourceMan = temp;
            }

            return resourceMan;
        }
    }

    /// <summary>
    ///   Overrides the current threads CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Globalization.CultureInfo Culture
    {
        get
        {
            return resourceCulture;
        }

        set
        {
            resourceCulture = value;
        }
    }

    internal static string GetResourceString(string name, System.Globalization.CultureInfo Culture)
    {
        return name;
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Signature verified.
    ///  
    /// </summary>
    internal static string MshSignature_Valid
    {
        get
        {
            return GetResourceString("MshSignature_Valid", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    The file {0} is not digitally signed. You cannot run this script on the current system. For more information about running scripts and setting execution policy, see about_Execution_Policies at https://go.microsoft.com/fwlink/?LinkID=135170
    ///  
    /// </summary>
    internal static string MshSignature_NotSigned
    {
        get
        {
            return GetResourceString("MshSignature_NotSigned", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    The contents of file {0} might have been changed by an unauthorized user or process, because the hash of the file does not match the hash stored in the digital signature. The script cannot run on the specified system. For more information, run Get-Help about_Signing.
    ///  
    /// </summary>
    internal static string MshSignature_HashMismatch
    {
        get
        {
            return GetResourceString("MshSignature_HashMismatch", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} is signed but the signer is not trusted on this system.
    ///  
    /// </summary>
    internal static string MshSignature_NotTrusted
    {
        get
        {
            return GetResourceString("MshSignature_NotTrusted", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Cannot sign the file because the system does not support signing operations on {0} files.
    ///  
    /// </summary>
    internal static string MshSignature_NotSupportedFileFormat
    {
        get
        {
            return GetResourceString("MshSignature_NotSupportedFileFormat", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Cannot sign the file because the system does not support signing operations on files that do not have a file name extension.
    ///  
    /// </summary>
    internal static string MshSignature_NotSupportedFileFormat_NoExtension
    {
        get
        {
            return GetResourceString("MshSignature_NotSupportedFileFormat_NoExtension", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    The signature cannot be verified because it is incompatible with the current system.
    ///  
    /// </summary>
    internal static string MshSignature_Incompatible
    {
        get
        {
            return GetResourceString("MshSignature_Incompatible", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    The signature cannot be verified because it is incompatible with the current system. The hash algorithm is not valid.
    ///  
    /// </summary>
    internal static string MshSignature_Incompatible_HashAlgorithm
    {
        get
        {
            return GetResourceString("MshSignature_Incompatible_HashAlgorithm", resourceCulture);
        }
    }

}

