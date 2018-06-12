using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.Entities;

namespace SPACRM.Interface
{
    public interface ICommonService
    {
        //FILES UploadFile(string ext,string contentType,byte[] data,string url);

        //FILES UploadFile(string ext, string contentType, byte[] data, string url,string remark);

        //FILES GetUploadFile(int id);

        int DeleteFiles(int id);

    }
}
