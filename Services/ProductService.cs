
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using web_blazor.Models;
public class ProductService
{
    public List<ProductVM> lstProduct = new List<ProductVM>();
    public ProductDetailVM prodDetail = new ProductDetailVM();
    public List<ProductVM> lstProductSearch = new List<ProductVM>();

    public HttpClient _httpClient;
    public ProductService(HttpClient http)
    {
        _httpClient = http;
    }
    public async void GetAllProductApi()
    {
        // URL gốc của API
        var url = "https://apistore.cybersoft.edu.vn/api/Product";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<List<ProductVM>>>(url);
        lstProduct = res.content;
        SetStateHasChange();
    }
    public async void GetAllProductByKeywordApi(string keyword="")
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product?keyword={keyword}";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<List<ProductVM>>>(url);
        lstProductSearch = res.content;
        SetStateHasChange();
    }

    public async Task<string> AddNewProduct(ProductAddNew model)
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/addNew";
        var res = await _httpClient.PostAsJsonAsync(url,  model);
        
        var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        SetStateHasChange();

        return response.content;

    }
    
    public async void GetProductByIdApi(string idProduct)
    {
        if (!string.IsNullOrEmpty(idProduct))
        {
            // URL gốc của API
            var url = $"https://apistore.cybersoft.edu.vn/api/Product/getid?id={idProduct}";
            var res = await _httpClient.GetFromJsonAsync<HTTPResponse<ProductDetailVM>>(url);
            prodDetail = res.content;
        }
        SetStateHasChange();
    }



    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();


}