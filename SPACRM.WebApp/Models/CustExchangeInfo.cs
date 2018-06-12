using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPACRM.WebApp.Models
{
    public class CustExchangeVoucherInfo
    {
        public int CustID { get; set; }

        public int CardID { get; set; }

        public string VoucherNO {get;set;}

        public decimal Balance { get; set; }

    }
}