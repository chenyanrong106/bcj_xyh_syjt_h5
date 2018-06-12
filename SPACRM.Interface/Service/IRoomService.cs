using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    /// <summary>
    /// 房间服务接口
    /// by Levin
    /// 2014-05-18
    /// </summary>
   public interface IRoomService
    {
        /// <summary>+房间列表信息
       /// </summary>
       /// <param name="search"></param>
       /// <param name="view"></param>
       /// <returns></returns>
       PagedList<STORE_ROOM_EX> QueryRoomList(RoomSearch search, PageView view);

       //ipad 房间视图接口
       List<STORE_ROOM_EX> QueryRoomList(int storeid);

       /// <summary>+根据房间编号得到房间信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       STORE_ROOM_EX GetRoom(int id);

       /// <summary>
       /// 保存房间信息
       /// </summary>
       /// <param name="room"></param>
       /// <returns></returns>
       int SaveRoom(STORE_ROOM room);

       /// <summary>+删除房间信息
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       int DeleteRoom(int id);

    }
}
