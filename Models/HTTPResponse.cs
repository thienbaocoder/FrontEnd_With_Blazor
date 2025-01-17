using System;

public class HTTPResponse<T> 
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public T content { get; set; }
    public DateTime dataTime { get; set; }

}