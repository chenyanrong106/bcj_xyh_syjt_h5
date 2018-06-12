using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface ICustomMenuService
    {
        WXCustomMenu AddCustomMenu(WXCustomMenu cm);

        bool DeleteMenu(int id);

        int SaveMenu(ref WXCustomMenu cm);

        //int SaveMenu(CustomMenu cm);

        WXCustomMenu GetCustomMenu(int id);

        WXCustomMenu GetRootMenu(int merchants_ID);

        List<WXCustomMenu> GetCustomMenuList(int merchants_ID);

        //CustomMenu AddCustomMenu(string name, int? type, int? orderNum, string content, int? graphic_ID, string url, int merchants_ID, int parentID);
        //CustomMenu SaveMenu(CustomMenu cm, int merchants_ID);
    }
}
