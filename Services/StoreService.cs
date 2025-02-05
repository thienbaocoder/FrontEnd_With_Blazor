using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
public class StoreService
{
    public List<StoreVM> lstStore = new List<StoreVM>();
    public StoreAddNew storeEdit = new StoreAddNew();
    public HttpClient _httpClient;
    public StoreService(HttpClient http)
    {
        _httpClient = http;
    }
    public async void GetAllStoreApi()
    {
        // URL gốc của API
        var url = "https://apistore.cybersoft.edu.vn/api/Store/getAll";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<List<StoreVM>>>(url);
        lstStore = res.content;
        SetStateHasChange();
    }

    public async Task<string> AddNewStore(StoreAddNew model)
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Store";
        var res = await _httpClient.PostAsJsonAsync(url, model);

        var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        SetStateHasChange();

        return response.content;

    }


    public async Task GetStoreByIdEditApi(string idStore)
    {
        if (!string.IsNullOrEmpty(idStore))
        {
            // URL gốc của API
            var url = $"https://apistore.cybersoft.edu.vn/api/Store/getbyid?id={idStore}";
            var res = await _httpClient.GetFromJsonAsync<HTTPResponse<StoreAddNew>>(url);
            storeEdit = res.content;
        }
        SetStateHasChange();
    }
    public async Task<string> UpdateStoreApi()
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Store";
        var res = await _httpClient.PutAsJsonAsync(url, storeEdit);

        var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        SetStateHasChange();
        GetAllStoreApi();
        return response.content;
    }
    public async Task<string> DeleteStoreByIdApi(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return "ID sản phẩm không hợp lệ.";
        }

        // URL API để xóa sản phẩm theo ID
        var url = $"https://apistore.cybersoft.edu.vn/api/Store/{id}";

        try
        {
            // Gửi yêu cầu DELETE tới API
            var res = await _httpClient.DeleteAsync(url);

            // Kiểm tra phản hồi
            if (res.IsSuccessStatusCode)
            {
                var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
                GetAllStoreApi();

                SetStateHasChange();
                return response.content;
            }
            else
            {
                return "Lỗi khi xóa sản phẩm.";
            }
        }
        catch (Exception ex)
        {
            return $"Đã xảy ra lỗi: {ex.Message}";
        }
    }




    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();

}