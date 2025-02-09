
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using web_blazor.Models;
public class ProductService
{
    public List<ProductVM> lstProduct = new List<ProductVM>();
    public ProductDetailVM prodDetail = new ProductDetailVM();
    public List<ProductVM> lstProductSearch = new List<ProductVM>();
    public ProductEditVM productEdit = new ProductEditVM();

    public HttpClient _httpClient;
    public ProductService(HttpClient http)
    {
        _httpClient = http;
    }
    public async Task GetAllProductApi()
    {
        // URL gốc của API
        var url = "https://apistore.cybersoft.edu.vn/api/Product";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<List<ProductVM>>>(url);
        lstProduct = res.content;
        SetStateHasChange();
    }
    public async void GetAllProductByKeywordApi(string keyword = "")
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product?keyword={keyword}";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<List<ProductVM>>>(url);
        lstProductSearch = res.content;
        SetStateHasChange();
    }
    public async void GetProductByIdApi(string id)
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/getid?id={id}";
        var res = await _httpClient.GetFromJsonAsync<HTTPResponse<ProductDetailVM>>(url);
        prodDetail = res.content;
        SetStateHasChange();
    }

    public async Task<string> AddNewProduct(ProductAddNew model)
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/addNew";
        var res = await _httpClient.PostAsJsonAsync(url, model);

        var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        SetStateHasChange();

        return response.content;

    }
    public async Task<string> DeleteProductByIdApi(string id)
    {
        List<string> lstId = new List<string>();
        lstId.Add(id);
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(lstId),
            Encoding.UTF8,
            "application.json"
        );
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("https://apistore.cybersoft.edu.vn/api/Product"),
            Content = jsonContent
        };
        var res = _httpClient.SendAsync(request);
        var response = await res.Result.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        return response.content;
    }

    public async Task GetProductByIdEditApi(string idProduct)
    {
        if (!string.IsNullOrEmpty(idProduct))
        {
            // URL gốc của API
            var url = $"https://apistore.cybersoft.edu.vn/api/Product/getid?id={idProduct}";
            var res = await _httpClient.GetFromJsonAsync<HTTPResponse<ProductEditVM>>(url);
            productEdit = res.content;
        }
        SetStateHasChange();
    }
    public async Task<string> UpdateProductApi()
    {
        // URL gốc của API
        var url = $"https://apistore.cybersoft.edu.vn/api/Product/updateProduct";
        var res = await _httpClient.PutAsJsonAsync(url, productEdit);

        var response = await res.Content.ReadFromJsonAsync<HTTPResponse<string>>();
        SetStateHasChange();
        //Sau khi xóa dữ liệu thành công thì gọi lại api getAll để cập nhật lại prodService.lstProduct
        GetAllProductApi();
        return response.content;
    }


    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();


}