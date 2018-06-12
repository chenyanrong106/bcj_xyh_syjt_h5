using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SPACRM.Extension;

namespace SPACRM.WebApp.assets.app.report
{
    public partial class financialrep :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string beg = DateTime.Now.ToString("yyyy-MM-dd");
            string end = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string stores = MyContext.CurrentLoginUser.STORE_ID.ToString();
            string STORE = "";
            string REGION = "";
            string md = "";//门店
            string dq = "";//地区
            if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "1")
            {
                beg = DateTime.Parse(Request.QueryString["beg"].ToString()).ToString("yyyy-MM-dd");
                end = DateTime.Parse(Request.QueryString["end"].ToString()).ToString("yyyy-MM-dd");
                stores = Request.QueryString["stores"].ToString();
                if (stores.Contains(","))
                    stores = stores.Substring(0, stores.Length - 1);
                STORE = Request.QueryString["STORE"].ToString();
                REGION = Request.QueryString["REGION"].ToString();
                md = Request.QueryString["md"].ToString();
                dq = Request.QueryString["dq"].ToString();
                Xml1.TransformSource = "financialrep.xslt";
                DataSet ds = new SPACRM.Business.ServiceImpl.ReportService().GetOrder(STORE == "" ? stores : STORE, beg, end, MyContext.CurrentLoginUser.ORG_ID).ToDataSet();
                ds.Tables[0].Columns.Add("beg");
                ds.Tables[0].Columns.Add("end");
                //ds.Tables[0].Columns.Add("md");
                //ds.Tables[0].Columns.Add("dq");
                if (MyContext.CurrentLoginUser.USER_TYPE == "2")//门店用户
                {
                    md = MyContext.CurrentLoginUser.STORE_NAME;
                }
                else if (md!="请选择门店名称")
                {
                   // md = "所有门店";
                }
                 else if (dq != "根据地区查询")
                 {
                     md = "所有" + dq + "门店";
                 }
                 else
                 {
                     md = "所有门店";
                 }
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     ds.Tables[0].Rows[0]["beg"] = beg;
                     ds.Tables[0].Rows[0]["end"] = end;
                     ds.Tables[0].Rows[0]["md"] = md;
                     ds.Tables[0].Rows[0]["dq"] = dq;
                 }
                 else
                 {
                     DataRow dr = ds.Tables[0].NewRow();
                     dr["beg"] = beg;
                     dr["end"] = end;
                     dr["md"] = md;
                     dr["dq"] = dq;
                     ds.Tables[0].Rows.Add(dr);
                 }
                Xml1.DocumentContent = ds.GetXml();
            }
            else if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "2")
            {
                beg = DateTime.Parse(Request.QueryString["beg"].ToString()).ToString();
                end = DateTime.Parse(Request.QueryString["end"].ToString()).ToString();
                stores = Request.QueryString["stores"].ToString();
                if (stores.Contains(","))
                    stores = stores.Substring(0, stores.Length - 1);
                STORE = Request.QueryString["STORE"].ToString();
                Xml1.TransformSource = "financialrep.xslt"; ;
                DataSet ds = new SPACRM.Business.ServiceImpl.ReportService().GetOrder(STORE == "" ? stores : STORE, beg, end, MyContext.CurrentLoginUser.ORG_ID).ToDataSet();
                ds.Tables[0].Columns["date"].ColumnName = "日期";
                ds.Tables[0].Columns["xj"].ColumnName = "现金";
                ds.Tables[0].Columns["ylk"].ColumnName = "银联卡";
                ds.Tables[0].Columns["wk"].ColumnName = "外卡";
                ds.Tables[0].Columns["wxzf"].ColumnName = "微信支付";
                ds.Tables[0].Columns["zfb"].ColumnName = "支付宝";
                ds.Tables[0].Columns["dzdp"].ColumnName = "大众点评团购";
                ds.Tables[0].Columns["dzdpsh"].ColumnName = "大众点评闪惠";
                ds.Tables[0].Columns["mt"].ColumnName = "美团";
                ds.Tables[0].Columns["nm"].ColumnName = "糯米";
                ds.Tables[0].Columns["emily"].ColumnName = "Emily";
                ds.Tables[0].Columns["woyouli"].ColumnName = "我有礼";
                ds.Tables[0].Columns["zouqi"].ColumnName = "走起";
                ds.Tables[0].Columns["xjsum"].ColumnName = "现金流总计";
                ds.Tables[0].Columns["hykkk"].ColumnName = "会员卡扣款";
                ds.Tables[0].Columns["hyksjkk"].ColumnName = "会员卡实际扣款";
                ds.Tables[0].Columns["xkcz"].ColumnName = "新客充值";
                ds.Tables[0].Columns["lkxc"].ColumnName = "老客续充";
                ds.Tables[0].Columns["zsjl"].ColumnName = "赠送奖励";
                ds.Tables[0].Columns["lckgm"].ColumnName = "疗程卡购买";
                ds.Tables[0].Columns["lckxh"].ColumnName = "疗程卡消耗";
                ds.Tables[0].Columns["xjq"].ColumnName = "现金券/按摩券";
                ds.Tables[0].Columns["xfq"].ColumnName = "消费券";
                ds.Tables[0].Columns["fwsjyye"].ColumnName = "服务实际营业额";
                ds.Tables[0].Columns["xjl"].ColumnName = "现金流";
                ds.Tables[0].Columns["hy"].ColumnName = "会员（客人数量）";
                ds.Tables[0].Columns["lk"].ColumnName = "老客（客人数量）";
                ds.Tables[0].Columns["xk"].ColumnName = "新客（客人数量）";
                ds.Tables[0].Columns["sk"].ColumnName = "散客（客人数量）";
                ds.Tables[0].Columns["zj"].ColumnName = "总计（客人数量）";
                ds.Tables[0].Columns["kdj"].ColumnName = "客单价";
                ds.Tables[0].Columns["amqsl"].ColumnName = "数量（按摩券/现金券）";
                ds.Tables[0].Columns["amqje"].ColumnName = "金额（按摩券/现金券）";
                ds.Tables[0].Columns["swcpsl"].ColumnName = "数量（实物产品）";
                ds.Tables[0].Columns["swcpje"].ColumnName = "金额（实物产品）";
                ds.Tables[0].Columns.Remove("md");
                ds.Tables[0].Columns.Remove("dq");
                CreateExcel(ds, "excel");
            }
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="FileName"></param>
        public void CreateExcel(DataSet ds, string FileName)
        {
            System.Web.UI.WebControls.DataGrid dgExport = null;
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (ds != null)
            {
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                curContext.Response.Charset = "UTF-8";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(FileName)) + ".xls");

                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                dgExport = new System.Web.UI.WebControls.DataGrid();
                dgExport.DataSource = ds.Tables[0].DefaultView;
                dgExport.AllowPaging = false;
                dgExport.DataBind();

                dgExport.RenderControl(htmlWriter);
                curContext.Response.Write(strWriter.ToString());
                curContext.Response.End();
            }
        }
    }



    static class Extensions
    {
        internal static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return ds;
        }
    }

    /// <summary>
    /// 实体转Xml，Xml转实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlHelper<T> where T : new()
    {
        #region 实体类转成Xml
        /// <summary>
        /// 对象实例转成xml
        /// </summary>
        /// <param name="item">对象实例</param>
        /// <returns></returns>
        public static string EntityToXml(T item)
        {
            IList<T> items = new List<T>();
            items.Add(item);
            return EntityToXml(items);
        }

        /// <summary>
        /// 对象实例集转成xml
        /// </summary>
        /// <param name="items">对象实例集</param>
        /// <returns></returns>
        public static string EntityToXml(IList<T> items)
        {
            //创建XmlDocument文档
            XmlDocument doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(typeof(T).Name + "s");
            //添加根元素的子元素集
            foreach (T item in items)
            {
                EntityToXml(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);

            return doc.InnerXml;
        }

        private static void EntityToXml(XmlDocument doc, XmlElement root, T item)
        {
            //创建元素
            XmlElement xmlItem = doc.CreateElement(typeof(T).Name);
            //对象的属性集

            System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);



            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    if (pinfo.GetValue(item, null) != null)
                        value = pinfo.GetValue(item, null).ToString();//获取对象属性值
                    //设置元素的属性值
                    xmlItem.SetAttribute(name, value);
                }
            }
            //向根添加子元素
            root.AppendChild(xmlItem);
        }


        #endregion

        #region Xml转成实体类

        /// <summary>
        /// Xml转成对象实例
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static T XmlToEntity(string xml)
        {
            IList<T> items = XmlToEntityList(xml);
            if (items != null && items.Count > 0)
                return items[0];
            else return default(T);
        }

        /// <summary>
        /// Xml转成对象实例集
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static IList<T> XmlToEntityList(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch
            {
                return null;
            }
            if (doc.ChildNodes.Count != 1)
                return null;
            if (doc.ChildNodes[0].Name.ToLower() != typeof(T).Name.ToLower() + "s")
                return null;

            XmlNode node = doc.ChildNodes[0];

            IList<T> items = new List<T>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == typeof(T).Name.ToLower())
                    items.Add(XmlNodeToEntity(child));
            }

            return items;
        }

        private static T XmlNodeToEntity(XmlNode node)
        {
            T item = new T();

            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement element = (XmlElement)node;

                System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

                foreach (XmlAttribute attr in element.Attributes)
                {
                    string attrName = attr.Name.ToLower();
                    string attrValue = attr.Value.ToString();
                    foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                    {
                        if (pinfo != null)
                        {
                            string name = pinfo.Name.ToLower();
                            Type dbType = pinfo.PropertyType;
                            if (name == attrName)
                            {
                                if (String.IsNullOrEmpty(attrValue))
                                    continue;
                                switch (dbType.ToString())
                                {
                                    case "System.Int32":
                                        pinfo.SetValue(item, Convert.ToInt32(attrValue), null);
                                        break;
                                    case "System.Boolean":
                                        pinfo.SetValue(item, Convert.ToBoolean(attrValue), null);
                                        break;
                                    case "System.DateTime":
                                        pinfo.SetValue(item, Convert.ToDateTime(attrValue), null);
                                        break;
                                    case "System.Decimal":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Double":
                                        pinfo.SetValue(item, Convert.ToDouble(attrValue), null);
                                        break;
                                    default:
                                        pinfo.SetValue(item, attrValue, null);
                                        break;
                                }
                                continue;
                            }
                        }
                    }
                }
            }
            return item;
        }

        #endregion
    }
}