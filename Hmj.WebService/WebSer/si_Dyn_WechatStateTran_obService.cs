﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl 自动生成, Version=4.6.1055.0。
// 
namespace Hmj.WebService
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Configuration;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="si_Dyn_WechatStateTran_obBinding", Namespace="http://jahwa.com/pos/crm")]
    public partial class si_Dyn_WechatStateTran_obService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback si_Dyn_WechatStateTran_obOperationCompleted;
        
        /// <remarks/>
        public si_Dyn_WechatStateTran_obService() {
            this.Url = ConfigurationManager.AppSettings["WebServerURLTest"] + "/sap/xi/engine?type=entry&version=3.0&Sender.Service=bs_pos&Int" +
                "erface=http%3A%2F%2Fjahwa.com%2Fpos%2Fcrm%5Esi_Dyn_WechatStateTran_ob";

            this.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["WebServerUserTest"],
            ConfigurationManager.AppSettings["WebServerPassTest"]);
        }
        
        /// <remarks/>
        public event si_Dyn_WechatStateTran_obCompletedEventHandler si_Dyn_WechatStateTran_obCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("mt_Dyn_WechatStateTran_res", Namespace="http://jahwa.com/pos/crm")]
        public dt_Dyn_WechatStateTran_res si_Dyn_WechatStateTran_ob([System.Xml.Serialization.XmlElementAttribute(Namespace="http://jahwa.com/pos/crm")]
        dt_Dyn_WechatStateTran_req mt_Dyn_WechatStateTran_req) {
            
            object[] results = this.Invoke("si_Dyn_WechatStateTran_ob", new object[] {
                        mt_Dyn_WechatStateTran_req});
            

            return ((dt_Dyn_WechatStateTran_res)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult Beginsi_Dyn_WechatStateTran_ob(dt_Dyn_WechatStateTran_req mt_Dyn_WechatStateTran_req, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("si_Dyn_WechatStateTran_ob", new object[] {
                        mt_Dyn_WechatStateTran_req}, callback, asyncState);
        }
        
        /// <remarks/>
        public dt_Dyn_WechatStateTran_res Endsi_Dyn_WechatStateTran_ob(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((dt_Dyn_WechatStateTran_res)(results[0]));
        }
        
        /// <remarks/>
        public void si_Dyn_WechatStateTran_obAsync(dt_Dyn_WechatStateTran_req mt_Dyn_WechatStateTran_req) {
            this.si_Dyn_WechatStateTran_obAsync(mt_Dyn_WechatStateTran_req, null);
        }
        
        /// <remarks/>
        public void si_Dyn_WechatStateTran_obAsync(dt_Dyn_WechatStateTran_req mt_Dyn_WechatStateTran_req, object userState) {
            if ((this.si_Dyn_WechatStateTran_obOperationCompleted == null)) {
                this.si_Dyn_WechatStateTran_obOperationCompleted = new System.Threading.SendOrPostCallback(this.Onsi_Dyn_WechatStateTran_obOperationCompleted);
            }
            this.InvokeAsync("si_Dyn_WechatStateTran_ob", new object[] {
                        mt_Dyn_WechatStateTran_req}, this.si_Dyn_WechatStateTran_obOperationCompleted, userState);
        }
        
        private void Onsi_Dyn_WechatStateTran_obOperationCompleted(object arg) {
            if ((this.si_Dyn_WechatStateTran_obCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.si_Dyn_WechatStateTran_obCompleted(this, new si_Dyn_WechatStateTran_obCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://jahwa.com/pos/crm")]
    public partial class dt_Dyn_WechatStateTran_req {
        
        private string oPENIDField;
        
        private string nEW_STATEField;
        
        private string vGROUPField;
        
        private string dATA_SOURCEField;
        
        private string sOURCE_SYSTEMField;
        
        private string lOYALTY_BRANDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPENID {
            get {
                return this.oPENIDField;
            }
            set {
                this.oPENIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NEW_STATE {
            get {
                return this.nEW_STATEField;
            }
            set {
                this.nEW_STATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VGROUP {
            get {
                return this.vGROUPField;
            }
            set {
                this.vGROUPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DATA_SOURCE {
            get {
                return this.dATA_SOURCEField;
            }
            set {
                this.dATA_SOURCEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SOURCE_SYSTEM {
            get {
                return this.sOURCE_SYSTEMField;
            }
            set {
                this.sOURCE_SYSTEMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOYALTY_BRAND {
            get {
                return this.lOYALTY_BRANDField;
            }
            set {
                this.lOYALTY_BRANDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://jahwa.com/pos/crm")]
    public partial class dt_Dyn_WechatStateTran_res {
        
        private string zRETURNField;
        
        private string mESSAGEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZRETURN {
            get {
                return this.zRETURNField;
            }
            set {
                this.zRETURNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE {
            get {
                return this.mESSAGEField;
            }
            set {
                this.mESSAGEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void si_Dyn_WechatStateTran_obCompletedEventHandler(object sender, si_Dyn_WechatStateTran_obCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class si_Dyn_WechatStateTran_obCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal si_Dyn_WechatStateTran_obCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public dt_Dyn_WechatStateTran_res Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((dt_Dyn_WechatStateTran_res)(this.results[0]));
            }
        }
    }
}
