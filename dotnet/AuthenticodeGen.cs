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

internal class Authenticode
{

    private static global::System.Resources.ResourceManager resourceMan;

    private static global::System.Globalization.CultureInfo resourceCulture;

    /// <summary>constructor</summary>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal Authenticode()
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
                //global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("System.Management.Automation.resources.Authenticode", typeof(Authenticode).Assembly);
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
    ///    File {0} cannot be loaded because you opted not to run this software now.
    ///  
    /// </summary>
    internal static string Reason_DoNotRun
    {
        get
        {
            return GetResourceString("Reason_DoNotRun", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} cannot be loaded because you opted never to run software from this publisher.
    ///  
    /// </summary>
    internal static string Reason_NeverRun
    {
        get
        {
            return GetResourceString("Reason_NeverRun", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} is published by {1}. This publisher is explicitly not trusted on your system. The script will not run on the system. For more information, run the command "get-help about_signing".
    ///  
    /// </summary>
    internal static string Reason_NotTrusted
    {
        get
        {
            return GetResourceString("Reason_NotTrusted", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} cannot be loaded because running scripts is disabled on this system. For more information, see about_Execution_Policies at https://go.microsoft.com/fwlink/?LinkID=135170.
    ///  
    /// </summary>
    internal static string Reason_RestrictedMode
    {
        get
        {
            return GetResourceString("Reason_RestrictedMode", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} cannot be loaded. {1}.
    ///  
    /// </summary>
    internal static string Reason_Unknown
    {
        get
        {
            return GetResourceString("Reason_Unknown", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} cannot be loaded because its operation is blocked by software restriction policies, such as those created by using Group Policy.
    ///  
    /// </summary>
    internal static string Reason_DisallowedBySafer
    {
        get
        {
            return GetResourceString("Reason_DisallowedBySafer", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} cannot be loaded because its content could not be read.
    ///  
    /// </summary>
    internal static string Reason_FileContentUnavailable
    {
        get
        {
            return GetResourceString("Reason_FileContentUnavailable", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Cannot sign code. The specified certificate is not suitable for code signing.
    ///  
    /// </summary>
    internal static string CertNotGoodForSigning
    {
        get
        {
            return GetResourceString("CertNotGoodForSigning", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Cannot sign code. The TimeStamp server URL must be fully qualified, and in the format http://<server url>.
    ///  
    /// </summary>
    internal static string TimeStampUrlRequired
    {
        get
        {
            return GetResourceString("TimeStampUrlRequired", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Cannot sign code. The hash algorithm is not supported.
    ///  
    /// </summary>
    internal static string InvalidHashAlgorithm
    {
        get
        {
            return GetResourceString("InvalidHashAlgorithm", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Do you want to run software from this untrusted publisher?
    ///  
    /// </summary>
    internal static string AuthenticodePromptCaption
    {
        get
        {
            return GetResourceString("AuthenticodePromptCaption", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    File {0} is published by {1} and is not trusted on your system. Only run scripts from trusted publishers.
    ///  
    /// </summary>
    internal static string AuthenticodePromptText
    {
        get
        {
            return GetResourceString("AuthenticodePromptText", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Software {0} is published by an unknown publisher. It is recommended that you do not run this software.
    ///  
    /// </summary>
    internal static string AuthenticodePromptText_UnknownPublisher
    {
        get
        {
            return GetResourceString("AuthenticodePromptText_UnknownPublisher", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Security warning
    ///  
    /// </summary>
    internal static string RemoteFilePromptCaption
    {
        get
        {
            return GetResourceString("RemoteFilePromptCaption", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Run only scripts that you trust. While scripts from the internet can be useful, this script can potentially harm your computer. If you trust this script, use the Unblock-File cmdlet to allow the script to run without this warning message. Do you want to run {0}?
    ///  
    /// </summary>
    internal static string RemoteFilePromptText
    {
        get
        {
            return GetResourceString("RemoteFilePromptText", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Ne&ver run
    ///  
    /// </summary>
    internal static string Choice_NeverRun
    {
        get
        {
            return GetResourceString("Choice_NeverRun", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Do not run the script from this publisher now, and do not prompt me to run this script in future. Future attempts to run this script will result in a silent failure.
    ///  
    /// </summary>
    internal static string Choice_NeverRun_Help
    {
        get
        {
            return GetResourceString("Choice_NeverRun_Help", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    &Do not run
    ///  
    /// </summary>
    internal static string Choice_DoNotRun
    {
        get
        {
            return GetResourceString("Choice_DoNotRun", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Do not run the script from this publisher now, and continue to prompt me to run this script in the future.
    ///  
    /// </summary>
    internal static string Choice_DoNotRun_Help
    {
        get
        {
            return GetResourceString("Choice_DoNotRun_Help", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    &Run once
    ///  
    /// </summary>
    internal static string Choice_RunOnce
    {
        get
        {
            return GetResourceString("Choice_RunOnce", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Run the script from this publisher now, and continue to prompt me to run this script in the future.
    ///  
    /// </summary>
    internal static string Choice_RunOnce_Help
    {
        get
        {
            return GetResourceString("Choice_RunOnce_Help", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    &Always run
    ///  
    /// </summary>
    internal static string Choice_AlwaysRun
    {
        get
        {
            return GetResourceString("Choice_AlwaysRun", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Run the script from this publisher now, and do not prompt me to run this script in the future.
    ///  
    /// </summary>
    internal static string Choice_AlwaysRun_Help
    {
        get
        {
            return GetResourceString("Choice_AlwaysRun_Help", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    &Suspend
    ///  
    /// </summary>
    internal static string Choice_Suspend
    {
        get
        {
            return GetResourceString("Choice_Suspend", resourceCulture);
        }
    }


    /// <summary>
    ///   Looks up a localized string similar to 
    ///    Pause the current pipeline and return to the command prompt. Type exit to resume operation when you are done.
    ///  
    /// </summary>
    internal static string Choice_Suspend_Help
    {
        get
        {
            return GetResourceString("Choice_Suspend_Help", resourceCulture);
        }
    }

}
