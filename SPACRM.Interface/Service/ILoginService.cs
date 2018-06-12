using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity.Entities;
using SPACRM.Entity;

namespace SPACRM.Interface
{
    public interface ILoginService
    {
        USER_INFO_EX LoginUser(string loginName, string pwd);
        USER_INFO_EX GetUserById(int id);
        USER_INFO_EX WXLoginUser(string loginName, string pwd);
        USER_INFO_EX WXGetUserById(int id);
        ORG_INFO GetMerchants(int id);
        ORG_INFO GetMerchants(string ToUserName);
        int UpdateMerchants(ORG_INFO m);
        USER_INFO_EX LoginSYSUser(string loginName, string pwd);
        int ValidSecretKey(string key,int orgid);
    }
}
