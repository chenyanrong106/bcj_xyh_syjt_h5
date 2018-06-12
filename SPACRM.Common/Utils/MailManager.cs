using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SPACRM.Common.Utils
{
    public class MailManager : IDisposable
    {
        private string server;
        private string userName;
        private string password;
        private int port = 25;
        private bool isSsl;
        private bool network = true;
        private bool ishtml = true;
        private string subject;
        private string body;
        private string from;
        private string to;
        private string errortext;
        private string[] attachement;
        private Encoding bodyencoding = Encoding.Default;
        private Encoding subjectencoding = Encoding.Default;
        private Component conponent = new Component();
        private bool disposed;
        private string[] cc;
        private string[] bcc;

        /// <summary>
        /// 根据配置文件，初始化邮件传输协议的参数
        /// </summary>
        public MailManager()
        {
            //IDictionary mail = (IDictionary)ConfigurationManager.GetSection("mail");
            Dictionary<string, string> mail = new Dictionary<string, string>();
            mail.Add("server", "smtp.exmail.qq.com");
            mail.Add("port", "25");
            mail.Add("username", "likui.liu@puman.com");
            mail.Add("password", "puman123");
            mail.Add("ssl", "false");

            this.server = (string)mail["server"];
            int.TryParse((string)mail["port"], out this.port);
            this.userName = (string)mail["username"];
            this.password = (string)mail["password"];
            this.isSsl = (string)mail["ssl"] == "true";

            this.subjectencoding = Encoding.UTF8;
            this.bodyencoding = Encoding.UTF8;
            this.ishtml = true;
        }

        /// <summary>
        /// 初始化邮件传输协议
        /// </summary>
        /// <param name="servername">邮件服务器地址</param>
        //public MailManager(string serverName)
        //    : this(serverName, 25)
        //{
        //}
        /// <summary>
        /// 初始化邮件传输协议
        /// </summary>
        /// <param name="servername">邮件服务器地址</param>
        /// <param name="port">端口</param>
        //public MailManager(string serverName, int port)
        //    : this(serverName, port, null, null)
        //{
        //}
        /// <summary>
        /// 初始化邮件传输协议
        /// </summary>
        /// <param name="servername">邮件服务器地址</param>
        /// <param name="port">端口</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        //public MailManager(string serverName, int port, string userName, string password)
        //{
        //    this.ServerName = serverName;
        //    this.port = port;
        //    this.UserName = userName;
        //    this.PassWord = password;
        //}

        ~MailManager()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposed)
        {
            if (!this.disposed && disposed)
            {
                conponent.Dispose();
            }
            this.disposed = true;
        }
        /// <summary>
        /// 发送
        /// </summary>
        public bool Send()
        {
            SmtpClient _client;
            if (this.ServerName == null)
            {
                this.errortext = "(SMTP)服务器未定义";
                return false;
            }
            _client = new SmtpClient(this.server, this.port);
            _client.UseDefaultCredentials = true;
            _client.EnableSsl = this.isSsl;
            if ((this.userName == null) || (this.password == null))
            {
                _client.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                _client.Credentials = new NetworkCredential(this.userName, this.password);
            }
            if (this.network)
            {
                _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            }
            else
            {
                _client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            }
            if (this.from == null)
            {
                this.errortext = "发件人地址未定义!";
                return false;
            }
            if (this.to == null)
            {
                this.errortext = "收件人人地址未定义!";
                return false;
            }
            MailAddress maFrom = new MailAddress(this.from);
            MailAddress maTo = new MailAddress(this.to);
            MailMessage maMsg = new MailMessage(maFrom, maTo);
            if (this.subject == null)
            {
                this.errortext = "邮件主题未定义!";
                return false;
            }
            maMsg.Subject = this.subject;
            maMsg.Body = this.body;
            if (this.cc != null)
            {
                if (this.cc.Length > 0)
                {
                    foreach (string cc in this.cc)
                    {
                        maMsg.CC.Add(cc);
                    }
                }
            }
            //maMsg.Bcc.Add("deo@lanxigua.com");
            if (this.bcc != null)
            {
                if (this.bcc.Length > 0)
                {
                    foreach (string bcc in this.bcc)
                    {
                        maMsg.Bcc.Add(bcc);
                    }
                }
            }
            if (this.attachement != null)
            {
                if (this.attachement.Length > 0)
                {
                    foreach (string att in this.attachement)
                    {
                        maMsg.Attachments.Add(new Attachment(att));
                    }
                }
            }
            maMsg.BodyEncoding = this.bodyencoding;
            maMsg.SubjectEncoding = this.subjectencoding;
            maMsg.IsBodyHtml = this.ishtml;
            try
            {
                _client.Send(maMsg);
                return true;
            }
            catch (Exception e)
            {
                this.errortext = e.Message;
                return false;
            }
            finally
            {
                maMsg.Dispose();
            }
        }
        #region 属性
        /// <summary>
        /// 获取或设置 SMTP 服务器地址
        /// </summary>
        public string ServerName
        {
            get { return this.server; }
            set { this.server = value; }
        }
        /// <summary>
        /// 获取或设置登陆 SMTP 服务器的用户名
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        /// <summary>
        /// 获取或设置登陆 SMTP 服务器的密码
        /// </summary>
        public string PassWord
        {
            get { return this.password; }
            set { this.password = value; }
        }
        /// <summary>
        /// 获取或设置 SMTP 端口
        /// </summary>
        //public int Port
        //{
        //    get { return this.port; }
        //    set { this.port = value; }
        //}

        /// <summary>
        /// 是否启用安全连接
        /// </summary>
        public bool IsSsl
        {
            get { return this.isSsl; }
            set { this.isSsl = value; }
        }
        /// <summary>
        /// 获取或设置是否通过网络 SMTP 服务器发送
        /// </summary>
        public bool IsNetwork
        {
            get { return this.network; }
            set { this.network = value; }
        }
        /// <summary>
        /// 获取或设置邮件内容是否以 HTML 格式发送
        /// </summary>
        public bool IsHtml
        {
            get { return this.ishtml; }
            set { this.ishtml = value; }
        }
        /// <summary>
        /// 获取或设置邮件内容
        /// </summary>
        public string Body
        {
            get { return this.body; }
            set { this.body = value; }
        }
        /// <summary>
        /// 获取或设置邮件主题行
        /// </summary>
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
        /// <summary>
        /// 获取或设置邮件发送人
        /// </summary>
        public string From
        {
            get { return this.from; }
            set { this.from = value; }
        }
        /// <summary>
        /// 获取或设置邮件接收人
        /// </summary>
        public string To
        {
            get { return this.to; }
            set { this.to = value; }
        }
        /// <summary>
        /// 获取邮件发送失败后的信息描述
        /// </summary>
        public string ErrorText
        {
            get { return this.errortext; }
        }
        /// <summary>
        /// 获取或设置邮件附件
        /// </summary>
        public string[] Attachement
        {
            get { return this.attachement; }
            set { this.attachement = value; }
        }
        /// <summary>
        /// 获取或设置邮件抄送地址
        /// </summary>
        public string[] CC
        {
            get { return this.cc; }
            set { this.cc = value; }
        }
        /// <summary>
        /// 获取或设置邮件密送地址
        /// </summary>
        public string[] Bcc
        {
            get { return this.bcc; }
            set { this.bcc = value; }
        }
        /// <summary>
        /// 获取或设置邮件内容编码格式
        /// </summary>
        public Encoding BodyEncoding
        {
            get { return this.bodyencoding; }
            set { this.bodyencoding = value; }
        }
        /// <summary>
        /// 获取或设置邮件主题编码格式
        /// </summary>
        public Encoding SubjectEncoding
        {
            get { return this.subjectencoding; }
            set { this.subjectencoding = value; }
        }
        #endregion
        /// <summary>
        /// 释放该对象非托管资源
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
