﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShanDian.UIShell.Framework.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UISResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UISResources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ShanDian.UIShell.Framework.Properties.UISResources", typeof(UISResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
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
        ///   查找类似 善点主机已安装在其他设备，请返回安装分机；如果一定要在本机重新安装善点主机，请联系客服（40080-51780）解绑后，再继续安装 的本地化字符串。
        /// </summary>
        internal static string Error_ExistDriver {
            get {
                return ResourceManager.GetString("Error_ExistDriver", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 账号未授权，请联系门店管理员 的本地化字符串。
        /// </summary>
        internal static string Error_InValidAuth {
            get {
                return ResourceManager.GetString("Error_InValidAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 账号或密码错误 的本地化字符串。
        /// </summary>
        internal static string Error_InValidUsername {
            get {
                return ResourceManager.GetString("Error_InValidUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该账号无任何权限，请联系管理员 的本地化字符串。
        /// </summary>
        internal static string Error_NoAction {
            get {
                return ResourceManager.GetString("Error_NoAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 系统繁忙, 请稍后重试 的本地化字符串。
        /// </summary>
        internal static string Error_OtherError {
            get {
                return ResourceManager.GetString("Error_OtherError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务端通信异常，请检查重启善点服务 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_BadData {
            get {
                return ResourceManager.GetString("HttpAccessor_BadData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 找不到相关的身分验证 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_NotFoundTokenInfo {
            get {
                return ResourceManager.GetString("HttpAccessor_NotFoundTokenInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 找不到相关的用户信息 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_NotFoundUserInfo {
            get {
                return ResourceManager.GetString("HttpAccessor_NotFoundUserInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 打不到相关的用户权限 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_NotFoundUserPermission {
            get {
                return ResourceManager.GetString("HttpAccessor_NotFoundUserPermission", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该账号无任何权限, 请联系管理员 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_NotPermissions {
            get {
                return ResourceManager.GetString("HttpAccessor_NotPermissions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 系统繁忙, 请稍后重试 的本地化字符串。
        /// </summary>
        internal static string HttpAccessor_SystemBusy {
            get {
                return ResourceManager.GetString("HttpAccessor_SystemBusy", resourceCulture);
            }
        }
    }
}