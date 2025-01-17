using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace web_blazor.Services;
public class CryptoService
{

    public HttpClient _httpClient;
    public List<Coin> lstAllCoin = new List<Coin>();
    public List<Coin> lstFavoritesCoin = new List<Coin>();
    public CryptoService(HttpClient http)
    {
        _httpClient = http;

    }
    public async void GetAllCoinAsync()
    {
         // URL gốc của API
        var url = "https://api.coingecko.com/api/v3/coins/markets";
        // Các tham số query cần thiết cho API
        var parameters = new Dictionary<string, string>
        {
            { "vs_currency", "usd" },
            { "order", "market_cap_desc" },
            { "per_page", "50" },
            { "page", "1" },
            { "sparkline", "false" }
        };
        // Tạo URI hoàn chỉnh với query string
        var uri = QueryHelpers.AddQueryString(url, parameters);
        // Tạo yêu cầu HTTP và thêm header User-Agent
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("User-Agent", "YourAppName/1.0"); // Thay YourAppName bằng tên app của bạn
        // Gửi yêu cầu và chờ phản hồi
        var response = await _httpClient.SendAsync(request);
        // Đảm bảo phản hồi thành công, nếu không sẽ ném ngoại lệ
        // response.EnsureSuccessStatusCode();
        //Lấy giá trị từ api trả về gán cho thuộc tính lstAllCoint
        lstAllCoin = await response.Content.ReadFromJsonAsync<List<Coin>>();
        SetStateHasChange();
    }

    public void AddFavorites(Coin newCoin){
        var item = lstFavoritesCoin.Find(x => x.id == newCoin.id);
        if(item == null){
            lstFavoritesCoin.Add(newCoin);
        }
        SetStateHasChange();
    }

    public void RemoveFavorite(string idCoin){

        lstFavoritesCoin = lstFavoritesCoin.Where(item=>item.id != idCoin).ToList();
        SetStateHasChange();

    }
    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();

    



}