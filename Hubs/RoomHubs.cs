using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class RoomHubs : Hub
{
    public RoomService _roomService;
    public ProductService _productService;
    public RoomHubs(RoomService roomService, ProductService prodService)
    {
        _roomService = roomService;
        _productService = prodService;
    }
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"connected client-id: {Context.ConnectionId}");
        await base.OnConnectedAsync();
        //Gọi api = service
        await Task.Run(() => _productService.GetAllProductApi());
        await Clients.All.SendAsync("GetAllProduct", _productService.lstProduct);
        //Sau khi connect thì gửi dữ liệu cho tất cả client danh sách phòng
        List<RoomVM> lstRoom = RoomService.lstRoom;
        await Clients.All.SendAsync("GetAllRoom", lstRoom);
    }
    //Tạo ra 1 api hub trả về danh sách các phòng
    public async Task AddRoom()
    {
        //Tạo 1 room mới
        RoomVM newRoom = new RoomVM();
        RoomService.lstRoom.Add(newRoom); //Thêm 1 room vào list
        //Phát lại toàn client lst room mới
        await Clients.All.SendAsync("GetAllRoom", RoomService.lstRoom);
    }
    public async Task<string> AddNewProd(ProductAddNew prod)
    {
        string responseMessage = await _productService.AddNewProduct(prod);
        if (!string.IsNullOrEmpty(responseMessage))
        {
            await Task.Run(() => _productService.GetAllProductApi());
            await Clients.All.SendAsync("GetAllProduct", _productService.lstProduct);
        }
        return responseMessage;
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        Console.WriteLine($"disconnected client-id: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(ex);
    }
}