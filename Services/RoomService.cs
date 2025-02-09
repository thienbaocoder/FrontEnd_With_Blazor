using System.Collections.Generic;
using System.Linq;

public class RoomService
{
    public static List<RoomVM> lstRoom = new List<RoomVM>();
    static RoomService()
    {
        for (int i = 0; i <= 10; i++)
        {
            RoomVM room = new RoomVM();
            lstRoom.Add(room);
        }
    }
    public void NewRoom()
    {
        RoomVM room = new RoomVM();
        lstRoom.Add(room);
    }
    public void RemoveRoom(string id)
    {
        lstRoom = lstRoom.Where(item => item.roomId != id).ToList();
    }
}