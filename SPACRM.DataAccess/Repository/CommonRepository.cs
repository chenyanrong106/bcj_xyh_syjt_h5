using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess.Repository
{
    public class CommonRepository : BaseRepository
    {
        //public Entity.FILES GetFILES(int id)
        //{
        //    return base.Get<Entity.FILES>("SELECT * FROM FILES WHERE ID=@ID", new { ID = id });
        //}

        public int DeleteFiles(int id)
        {
            return Excute("UPDATE dbo.FILES SET Type='del' WHERE id=@id", new { id = id });
            //return Excute("delete FILES where id=@id", new { id = id });
        }
    }
}
