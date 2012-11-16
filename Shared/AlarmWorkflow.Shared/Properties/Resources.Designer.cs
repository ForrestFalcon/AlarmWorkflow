﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlarmWorkflow.Shared.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AlarmWorkflow.Shared.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Address book scan finished. Entries found: {0}..
        /// </summary>
        internal static string AddressBook_FinishScanMessage {
            get {
                return ResourceManager.GetString("AddressBook_FinishScanMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Begin scanning address book contents....
        /// </summary>
        internal static string AddressBook_StartScanMessage {
            get {
                return ResourceManager.GetString("AddressBook_StartScanMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file path must be in absolute format!.
        /// </summary>
        internal static string FileNameMustBeAbsolute {
            get {
                return ResourceManager.GetString("FileNameMustBeAbsolute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No setting identifier with the name &apos;{0}&apos; has been found!.
        /// </summary>
        internal static string SettingIdentifierNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("SettingIdentifierNotFoundExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; has no type defined! Skipping setting..
        /// </summary>
        internal static string SettingItemEmptyType {
            get {
                return ResourceManager.GetString("SettingItemEmptyType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encountered setting with no name! Setting index (one-based): {0}. Skipping setting..
        /// </summary>
        internal static string SettingItemInvalidName {
            get {
                return ResourceManager.GetString("SettingItemInvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; has an invalid type! Supported types are: {1}. Skipping setting..
        /// </summary>
        internal static string SettingItemInvalidType {
            get {
                return ResourceManager.GetString("SettingItemInvalidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No setting with the name &apos;{0}&apos; has been found!.
        /// </summary>
        internal static string SettingNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("SettingNotFoundExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Encountered not supported node type of &apos;{0}&apos; in setting &apos;{1}&apos;. Only plain values and CDATA-nodes are currently supported!.
        /// </summary>
        internal static string SettingsConfigurationEmbResInvalidValueContent {
            get {
                return ResourceManager.GetString("SettingsConfigurationEmbResInvalidValueContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loaded settings configuration from assembly &apos;{0}&apos;..
        /// </summary>
        internal static string SettingsConfigurationEmbResLoaded {
            get {
                return ResourceManager.GetString("SettingsConfigurationEmbResLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsing of settings configuration file from assembly &apos;{0}&apos; failed. The file may contain invalid or missing information that is expected to be present in the specified configuration version..
        /// </summary>
        internal static string SettingsConfigurationEmbResParseFailed {
            get {
                return ResourceManager.GetString("SettingsConfigurationEmbResParseFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The settings configuration file found in assembly &apos;{0}&apos; contains invalid XML data! The error message was: {1}.
        /// </summary>
        internal static string SettingsConfigurationEmbResXmlException {
            get {
                return ResourceManager.GetString("SettingsConfigurationEmbResXmlException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loaded settings display configuration from assembly &apos;{0}&apos;..
        /// </summary>
        internal static string SettingsDisplayConfigurationEmbResLoaded {
            get {
                return ResourceManager.GetString("SettingsDisplayConfigurationEmbResLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The settings display configuration file found in assembly &apos;{0}&apos; contains invalid XML data! The error message was: {1}.
        /// </summary>
        internal static string SettingsDisplayConfigurationEmbResXmlException {
            get {
                return ResourceManager.GetString("SettingsDisplayConfigurationEmbResXmlException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was no SettingsDisplayConfiguration found in this instance. Perhaps this instance was not initialized with &quot;IncludeDisplayConfiguration&quot; or it was and the configuration is not available at this point..
        /// </summary>
        internal static string SettingsDisplayConfigurationNotFoundException {
            get {
                return ResourceManager.GetString("SettingsDisplayConfigurationNotFoundException", resourceCulture);
            }
        }
    }
}
