//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Threading.Tasks;

//namespace WebMatrix
//{
//  public class ErrorHandlerMiddleware
//  {
//    private readonly RequestDelegate _next;

//    public ErrorHandlerMiddleware(RequestDelegate next)
//    {
//      _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//      try
//      {
//        await _next(context);
//      }
//      catch (Exception error)
//      {
//        HttpResponse response = context.Response;
//        response.ContentType = "application/json";

//        response.StatusCode = error switch
//        {
//          AppException _ => (int)HttpStatusCode.BadRequest,// custom application error
//          KeyNotFoundException _ => (int)HttpStatusCode.NotFound,// not found error
//          _ => (int)HttpStatusCode.InternalServerError// unhandled error
//        };

//        using StreamWriter w = File.AppendText("Error.log");
//        w.WriteLine(error.ToString());

//        //await response.WriteAsync(error.ToString());
//      }
//    }
//  }
//}