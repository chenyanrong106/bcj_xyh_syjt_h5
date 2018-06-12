using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity
{
    public enum ResponseCode : int
    {
        Continue = 100,// 初始的请求已经接受，客户应当继续发送请求的其余部分。（HTTP 1.1新）
        Switching_Protocols = 101, //服务器将遵从客户的请求转换到另外一种协议（HTTP 1.1新）
        OK = 200, //一切正常，对GET和POST请求的应答文档跟在后面。
        Created = 201, // 服务器已经创建了文档，Location头给出了它的URL。
        Accepted = 202, //已经接受请求，但处理尚未完成。
        Moved_Permanently = 301, //客户请求的文档在其他地方，新的URL在Location头中给出，浏览器应该自动地访问新的URL。
        Bad_Request = 400, //Bad Request 请求出现语法错误。
        Unauthorized = 401, //客户试图未经授权访问受密码保护的页面。应答中会包含一个WWW-Authenticate头，浏览器据此显示用户名字/密码对话框，然后在填写合适的Authorization头后再次发出请求。
        Forbidden = 403, // 资源不可用。服务器理解客户的请求，但拒绝处理它。通常由于服务器上文件或目录的权限设置导致。
        Not_Found = 404, //无法找到指定位置的资源。这也是一个常用的应答。
        Request_Timeout = 408, //在服务器许可的等待时间内，客户一直没有发出任何请求。客户可以在以后重复同一请求。（HTTP 1.1新）
        Internal_Server_Error = 500, //Internal Server Error 服务器遇到了意料不到的情况，不能完成客户的请求。
        Not_Implemented = 501, //服务器不支持实现请求所需要的功能。例如，客户发出了一个服务器不支持的PUT请求。
        Bad_Gateway = 502, //服务器作为网关或者代理时，为了完成请求访问下一个服务器，但该服务器返回了非法的应答。
        Service_Unavailable = 503 //服务器由于维护或者负载过重未能应答。例如，Servlet可能在数据库连接池已满的情况下返回503。服务器返回503时可以提供一个Retry-After头。  
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus : int
    {
        //0--新建
        //1--已支付
        //2--红冲
        //9--已取消
        //8--已退款

        New = 0,
        Payed = 1,
        HongChong = 2,
        Canceled = 9,
        Refund = 8
    }

    /// <summary>
    /// 产品类别
    /// </summary>
    public enum ProductType : int
    {
        /// <summary>
        /// 商品
        /// </summary>
        Goods = 0,

        /// <summary>
        /// 卡
        /// </summary>
        Card = 1,

        /// <summary>
        /// 服务项目
        /// </summary>
        Service = 2,
    }

    /// <summary>
    /// 付款方式
    /// </summary>
    public enum PaymentType : int
    {
        //0	现金
        //1	银联POS
        //2	应收账款
        //3	代金券
        //5	会员卡
        //6 电子券
        //999	疗程卡
        Cash = 0,
        BankUnion = 1,
        Receivable = 2,
        Voucher = 3,
        VipCard = 5,
        dzq=6,
        TreatmentCard = 999,
    }

    /// <summary>
    /// 短信类型
    /// </summary>
    public enum SmsType : int
    {
        //自动生成生日信息	1
        Birthday = 1,
        //自动生成消费信息	2
        Consume = 2,
        //卡券到期信息	3
        CardExpired = 3,
        //自动生成积分提醒信息	4
        Point = 4,
        //自动生成预约提醒信息	5
        Booking = 5,
        //自动生成存款信息	6
        BuyCard = 6,
    }

    /// <summary>
    /// 预约状态
    /// </summary>
    public enum BookingStatus : int
    {
        //0--新预约,1--已开单,2--已结帐,7--未约进,8--已爽约,9--已取消
        New = 0,
        HasOrder = 1,
        HasBuy = 2,
        UnArrived = 7,
        BrokenPromise = 8,
        Canceled = 9,
    }

    /// <summary>
    /// 卡类型
    /// </summary>
    public enum CardType
    {
        SavingCard = 1, //储值卡

        LCCard = 2, //疗程卡

        ItemVoucher = 5, //项目券

        CashVoucher = 6,    //现金券
    }
}
