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
    [System.Web.Services.WebServiceBindingAttribute(Name="si_SMSInsert_real_obBinding", Namespace="http://jahwa.com/sms")]
    public partial class si_SMSInsert_real_obService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback si_SMSInsert_real_obOperationCompleted;
        
        /// <remarks/>
        public si_SMSInsert_real_obService() {
            this.Url = ConfigurationManager.AppSettings["WebServerURL"] + "/sap/xi/engine?type=entry&version=3.0&Sender.Service=bs_webservi" +
                "ce&Interface=http%3A%2F%2Fjahwa.com%2Fsms%5Esi_SMSInsert_real_ob";

            this.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["WebServerUser"],
            ConfigurationManager.AppSettings["WebServerPass"]);
        }
        
        /// <remarks/>
        public event si_SMSInsert_real_obCompletedEventHandler si_SMSInsert_real_obCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("mt_SMSInsert_res", Namespace="http://jahwa.com/sms")]
        public dt_SMSInsert_res si_SMSInsert_real_ob([System.Xml.Serialization.XmlElementAttribute(Namespace="http://jahwa.com/sms")] dt_SMSInsert_req mt_SMSInsert_req) {
            object[] results = this.Invoke("si_SMSInsert_real_ob", new object[] {
                        mt_SMSInsert_req});
            return ((dt_SMSInsert_res)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult Beginsi_SMSInsert_real_ob(dt_SMSInsert_req mt_SMSInsert_req, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("si_SMSInsert_real_ob", new object[] {
                        mt_SMSInsert_req}, callback, asyncState);
        }
        
        /// <remarks/>
        public dt_SMSInsert_res Endsi_SMSInsert_real_ob(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((dt_SMSInsert_res)(results[0]));
        }
        
        /// <remarks/>
        public void si_SMSInsert_real_obAsync(dt_SMSInsert_req mt_SMSInsert_req) {
            this.si_SMSInsert_real_obAsync(mt_SMSInsert_req, null);
        }
        
        /// <remarks/>
        public void si_SMSInsert_real_obAsync(dt_SMSInsert_req mt_SMSInsert_req, object userState) {
            if ((this.si_SMSInsert_real_obOperationCompleted == null)) {
                this.si_SMSInsert_real_obOperationCompleted = new System.Threading.SendOrPostCallback(this.Onsi_SMSInsert_real_obOperationCompleted);
            }
            this.InvokeAsync("si_SMSInsert_real_ob", new object[] {
                        mt_SMSInsert_req}, this.si_SMSInsert_real_obOperationCompleted, userState);
        }
        
        private void Onsi_SMSInsert_real_obOperationCompleted(object arg) {
            if ((this.si_SMSInsert_real_obCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.si_SMSInsert_real_obCompleted(this, new si_SMSInsert_real_obCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://jahwa.com/sms")]
    public partial class dt_SMSInsert_req {
        
        private SMS_ITEM[] sMS_ITEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public SMS_ITEM[] SMS_ITEM {
            get {
                return this.sMS_ITEMField;
            }
            set {
                this.sMS_ITEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://jahwa.com/sms")]
    public partial class SMS_ITEM {
        
        private string mESSAGEIDField;
        
        private string mESSAGETYPEField;
        
        private string sRCNUMBERField;
        
        private string mSGFORMATField;
        
        private string mOBILEField;
        
        private string cONTENTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGEID {
            get {
                return this.mESSAGEIDField;
            }
            set {
                this.mESSAGEIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGETYPE {
            get {
                return this.mESSAGETYPEField;
            }
            set {
                this.mESSAGETYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SRCNUMBER {
            get {
                return this.sRCNUMBERField;
            }
            set {
                this.sRCNUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MSGFORMAT {
            get {
                return this.mSGFORMATField;
            }
            set {
                this.mSGFORMATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBILE {
            get {
                return this.mOBILEField;
            }
            set {
                this.mOBILEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTENT {
            get {
                return this.cONTENTField;
            }
            set {
                this.cONTENTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://jahwa.com/sms")]
    public partial class dt_SMSInsert_res {
        
        private string zstatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string zstatus {
            get {
                return this.zstatusField;
            }
            set {
                this.zstatusField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void si_SMSInsert_real_obCompletedEventHandler(object sender, si_SMSInsert_real_obCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class si_SMSInsert_real_obCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal si_SMSInsert_real_obCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public dt_SMSInsert_res Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((dt_SMSInsert_res)(this.results[0]));
            }
        }
    }
}
