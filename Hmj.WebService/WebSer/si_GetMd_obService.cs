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


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "si_GetMd_obBinding", Namespace = "http://jahwa.com/pos/ecc")]
    public partial class si_GetMd_obService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback si_GetMd_obOperationCompleted;

        /// <remarks/>
        public si_GetMd_obService()
        {
            this.Url = "http://jhpi.jahwa.com.cn:443/XISOAPAdapter/MessageServlet?channel=:bs_gfpos:cc" +
                "_GetMd_ob&version=3.0&Sender.Service=bs_gfpos&Interface=http%3A%2F%2Fjahwa.com%2" +
                "Fpos%2Fecc%5Esi_GetMd_ob";
            this.Credentials = new System.Net.NetworkCredential("ZWX_BCPOS", "d43967651");

        }

        /// <remarks/>
        public event si_GetMd_obCompletedEventHandler si_GetMd_obCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("mt_GetMd_res", Namespace = "http://jahwa.com/pos/ecc")]
        public dt_GetMd_res si_GetMd_ob([System.Xml.Serialization.XmlElementAttribute(Namespace = "http://jahwa.com/pos/ecc")] dt_GetMd_req mt_GetMd_req)
        {
            object[] results = this.Invoke("si_GetMd_ob", new object[] {
                        mt_GetMd_req});
            return ((dt_GetMd_res)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginsi_GetMd_ob(dt_GetMd_req mt_GetMd_req, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("si_GetMd_ob", new object[] {
                        mt_GetMd_req}, callback, asyncState);
        }

        /// <remarks/>
        public dt_GetMd_res Endsi_GetMd_ob(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((dt_GetMd_res)(results[0]));
        }

        /// <remarks/>
        public void si_GetMd_obAsync(dt_GetMd_req mt_GetMd_req)
        {
            this.si_GetMd_obAsync(mt_GetMd_req, null);
        }

        /// <remarks/>
        public void si_GetMd_obAsync(dt_GetMd_req mt_GetMd_req, object userState)
        {
            if ((this.si_GetMd_obOperationCompleted == null))
            {
                this.si_GetMd_obOperationCompleted = new System.Threading.SendOrPostCallback(this.Onsi_GetMd_obOperationCompleted);
            }
            this.InvokeAsync("si_GetMd_ob", new object[] {
                        mt_GetMd_req}, this.si_GetMd_obOperationCompleted, userState);
        }

        private void Onsi_GetMd_obOperationCompleted(object arg)
        {
            if ((this.si_GetMd_obCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.si_GetMd_obCompleted(this, new si_GetMd_obCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jahwa.com/pos/ecc")]
    public partial class dt_GetMd_req
    {

        private TVKO[] zTVKOField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public TVKO[] ZTVKO
        {
            get
            {
                return this.zTVKOField;
            }
            set
            {
                this.zTVKOField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jahwa.com/pos/ecc")]
    public partial class TVKO
    {

        private string vKORGField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VKORG
        {
            get
            {
                return this.vKORGField;
            }
            set
            {
                this.vKORGField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jahwa.com/pos/ecc")]
    public partial class ZSAL_MD
    {

        private string mANDTField;

        private string vKORGField;

        private string wERKSField;

        private string wERKS_CField;

        private string kUNNRField;

        private string nUMField;

        private string kUNNR_SHField;

        private string zMD_IDField;

        private string zMD_MCField;

        private string zMD_JCField;

        private string zMD_TYPEField;

        private string zMD_ATTRField;

        private string vTWEGField;

        private string zMD_UPIDField;

        private string zKUNNR_SPField;

        private string zKUNNR_SHField;

        private string nAME3Field;

        private string tELF1Field;

        private string mOB_NUMBERField;

        private string tELFXField;

        private string sMTP_ADDRField;

        private string sTRASField;

        private string pOST_CODE1Field;

        private string cITY_CODEField;

        private System.DateTime zDATE_CRField;

        private bool zDATE_CRFieldSpecified;

        private string zSTATUSField;

        private System.DateTime zDATE_DEField;

        private bool zDATE_DEFieldSpecified;

        private string zREC_STAField;

        private System.DateTime dATUMField;

        private bool dATUMFieldSpecified;

        private System.DateTime uZEITField;

        private bool uZEITFieldSpecified;

        private string uNAMEField;

        private System.DateTime dATUM_CRField;

        private bool dATUM_CRFieldSpecified;

        private System.DateTime uZEIT_CRField;

        private bool uZEIT_CRFieldSpecified;

        private string uNAME_CRField;

        private System.DateTime dATUM_QYField;

        private bool dATUM_QYFieldSpecified;

        private string fLAG_QYField;

        private string zMD_ATTR2Field;

        private string zCITY_TYPEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MANDT
        {
            get
            {
                return this.mANDTField;
            }
            set
            {
                this.mANDTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VKORG
        {
            get
            {
                return this.vKORGField;
            }
            set
            {
                this.vKORGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WERKS
        {
            get
            {
                return this.wERKSField;
            }
            set
            {
                this.wERKSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WERKS_C
        {
            get
            {
                return this.wERKS_CField;
            }
            set
            {
                this.wERKS_CField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KUNNR
        {
            get
            {
                return this.kUNNRField;
            }
            set
            {
                this.kUNNRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NUM
        {
            get
            {
                return this.nUMField;
            }
            set
            {
                this.nUMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KUNNR_SH
        {
            get
            {
                return this.kUNNR_SHField;
            }
            set
            {
                this.kUNNR_SHField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_ID
        {
            get
            {
                return this.zMD_IDField;
            }
            set
            {
                this.zMD_IDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_MC
        {
            get
            {
                return this.zMD_MCField;
            }
            set
            {
                this.zMD_MCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_JC
        {
            get
            {
                return this.zMD_JCField;
            }
            set
            {
                this.zMD_JCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_TYPE
        {
            get
            {
                return this.zMD_TYPEField;
            }
            set
            {
                this.zMD_TYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_ATTR
        {
            get
            {
                return this.zMD_ATTRField;
            }
            set
            {
                this.zMD_ATTRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VTWEG
        {
            get
            {
                return this.vTWEGField;
            }
            set
            {
                this.vTWEGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_UPID
        {
            get
            {
                return this.zMD_UPIDField;
            }
            set
            {
                this.zMD_UPIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZKUNNR_SP
        {
            get
            {
                return this.zKUNNR_SPField;
            }
            set
            {
                this.zKUNNR_SPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZKUNNR_SH
        {
            get
            {
                return this.zKUNNR_SHField;
            }
            set
            {
                this.zKUNNR_SHField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME3
        {
            get
            {
                return this.nAME3Field;
            }
            set
            {
                this.nAME3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TELF1
        {
            get
            {
                return this.tELF1Field;
            }
            set
            {
                this.tELF1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOB_NUMBER
        {
            get
            {
                return this.mOB_NUMBERField;
            }
            set
            {
                this.mOB_NUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TELFX
        {
            get
            {
                return this.tELFXField;
            }
            set
            {
                this.tELFXField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SMTP_ADDR
        {
            get
            {
                return this.sMTP_ADDRField;
            }
            set
            {
                this.sMTP_ADDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STRAS
        {
            get
            {
                return this.sTRASField;
            }
            set
            {
                this.sTRASField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POST_CODE1
        {
            get
            {
                return this.pOST_CODE1Field;
            }
            set
            {
                this.pOST_CODE1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CITY_CODE
        {
            get
            {
                return this.cITY_CODEField;
            }
            set
            {
                this.cITY_CODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ZDATE_CR
        {
            get
            {
                return this.zDATE_CRField;
            }
            set
            {
                this.zDATE_CRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ZDATE_CRSpecified
        {
            get
            {
                return this.zDATE_CRFieldSpecified;
            }
            set
            {
                this.zDATE_CRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZSTATUS
        {
            get
            {
                return this.zSTATUSField;
            }
            set
            {
                this.zSTATUSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ZDATE_DE
        {
            get
            {
                return this.zDATE_DEField;
            }
            set
            {
                this.zDATE_DEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ZDATE_DESpecified
        {
            get
            {
                return this.zDATE_DEFieldSpecified;
            }
            set
            {
                this.zDATE_DEFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZREC_STA
        {
            get
            {
                return this.zREC_STAField;
            }
            set
            {
                this.zREC_STAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DATUM
        {
            get
            {
                return this.dATUMField;
            }
            set
            {
                this.dATUMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DATUMSpecified
        {
            get
            {
                return this.dATUMFieldSpecified;
            }
            set
            {
                this.dATUMFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "time")]
        public System.DateTime UZEIT
        {
            get
            {
                return this.uZEITField;
            }
            set
            {
                this.uZEITField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UZEITSpecified
        {
            get
            {
                return this.uZEITFieldSpecified;
            }
            set
            {
                this.uZEITFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UNAME
        {
            get
            {
                return this.uNAMEField;
            }
            set
            {
                this.uNAMEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DATUM_CR
        {
            get
            {
                return this.dATUM_CRField;
            }
            set
            {
                this.dATUM_CRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DATUM_CRSpecified
        {
            get
            {
                return this.dATUM_CRFieldSpecified;
            }
            set
            {
                this.dATUM_CRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "time")]
        public System.DateTime UZEIT_CR
        {
            get
            {
                return this.uZEIT_CRField;
            }
            set
            {
                this.uZEIT_CRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UZEIT_CRSpecified
        {
            get
            {
                return this.uZEIT_CRFieldSpecified;
            }
            set
            {
                this.uZEIT_CRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UNAME_CR
        {
            get
            {
                return this.uNAME_CRField;
            }
            set
            {
                this.uNAME_CRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DATUM_QY
        {
            get
            {
                return this.dATUM_QYField;
            }
            set
            {
                this.dATUM_QYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DATUM_QYSpecified
        {
            get
            {
                return this.dATUM_QYFieldSpecified;
            }
            set
            {
                this.dATUM_QYFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLAG_QY
        {
            get
            {
                return this.fLAG_QYField;
            }
            set
            {
                this.fLAG_QYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZMD_ATTR2
        {
            get
            {
                return this.zMD_ATTR2Field;
            }
            set
            {
                this.zMD_ATTR2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZCITY_TYPE
        {
            get
            {
                return this.zCITY_TYPEField;
            }
            set
            {
                this.zCITY_TYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jahwa.com/pos/ecc")]
    public partial class dt_GetMd_res
    {

        private ZSAL_MD[] iNFO_MDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ZSAL_MD[] INFO_MD
        {
            get
            {
                return this.iNFO_MDField;
            }
            set
            {
                this.iNFO_MDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void si_GetMd_obCompletedEventHandler(object sender, si_GetMd_obCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class si_GetMd_obCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal si_GetMd_obCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public dt_GetMd_res Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((dt_GetMd_res)(this.results[0]));
            }
        }
    }
}
