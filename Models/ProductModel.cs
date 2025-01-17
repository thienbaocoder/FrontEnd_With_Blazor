namespace web_blazor.Models;
using System;
using System.Text.Json.Serialization;
public class ProductModel
{
    public string? id { get; set; }

    public string? name { get; set; }

    public string price { get; set; }

    public string? img { get; set; }

}