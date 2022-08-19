﻿using employee.service.Configurations;
using employee.service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Http.Filters;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;
using IExceptionFilter = Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter;

namespace employee.service.Filters
{
    //public class CustomExceptionFilter : IExceptionFilter
    //{
    //    private readonly IAppSettings _apiSettings;
    //    private readonly ILogger _logger;
    //    public CustomExceptionFilter(IAppSettings apiSettings, ILoggerFactory loggerFactory) : base()
    //    {
    //        _apiSettings = apiSettings;
    //        _logger = loggerFactory.CreateLogger("ServiceLogFilter");
    //    }

    //    public void OnException(ExceptionContext context)
    //    {
    //        var transactionName = "Core_Service";
    //        if (context.ActionDescriptor!=null && context.ActionDescriptor.AttributeRouteInfo!=null)
    //         transactionName = context.ActionDescriptor.AttributeRouteInfo.Template;         
    //        HttpResponse response = context.HttpContext.Response;
    //        response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //        response.ContentType = "application/json";
    //        var request = context.HttpContext.Request;
    //        var requestMethod = (request != null) ? request.Method : string.Empty;
    //        var message = context.Exception.Message + " StackTrace : " + context.Exception.StackTrace;
    //        Helpers.Helper.NewRelicLogging(transactionName, request.Path, requestMethod, string.Empty, response.StatusCode.ToString(), context.Exception, "Some Error has occured");


    //        byte[] byteArray = Encoding.UTF8.GetBytes(message);

    //        using (var messageStream = new MemoryStream(byteArray))
    //        {

    //            using (var stream = new StreamReader(messageStream))
    //            {
    //                var responseBody = stream.ReadToEnd();
    //                responseBody = responseBody.Replace(Environment.NewLine, "");
    //                _logger.LogError($"@timestamp: {DateTime.Now},@site: {"core-service"}, @level: error, @threadid: {Thread.CurrentThread.ManagedThreadId}, @message: response -{responseBody},  requestUrl: {request.Path},requestMethod: {requestMethod}, responseCode: { response.StatusCode}");
    //            }
    //        }

    //        var errorResponse = Helpers.Helper.ConvertToErrorResponse(context.Exception.Message, ErrorsType.UnhandledError.ToString(), ErrorMessageType.Error.ToString());
    //        if (!_apiSettings.IsProd)
    //        {                
    //            errorResponse.StackTrace = context.Exception.StackTrace;
    //        }

    //        context.Result = new ObjectResult(errorResponse);
    //    }
    //}




    public class CustomExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly AppSettings _apiSettings;
        public CustomExceptionFilter(AppSettings apiSettings) : base()
        {
            _apiSettings = apiSettings;
        }

        public void OnException(ExceptionContext context)
        {
            var transactionName = "Core_Service";
            if (context.ActionDescriptor != null && context.ActionDescriptor.AttributeRouteInfo != null)
                transactionName = context.ActionDescriptor.AttributeRouteInfo.Template;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            var request = context.HttpContext.Request;
            var requestMethod = (request != null) ? request.Method : string.Empty;
            var message = context.Exception.Message + " StackTrace : " + context.Exception.StackTrace;
            Helpers.Helper.NewRelicLogging(transactionName, request.Path, requestMethod, string.Empty, response.StatusCode.ToString(), context.Exception, "Some Error has occured");


            byte[] byteArray = Encoding.UTF8.GetBytes(message);

            using (var messageStream = new MemoryStream(byteArray))
            {

                using (var stream = new StreamReader(messageStream))
                {
                    var responseBody = stream.ReadToEnd();
                    responseBody = responseBody.Replace(Environment.NewLine, "");
                    //Log.Error($"@timestamp: {DateTime.Now},@site: {"core-service"}, @level: error, @threadid: {Thread.CurrentThread.ManagedThreadId}, @message: response -{responseBody},  requestUrl: {request.Path},requestMethod: {requestMethod}, responseCode: { response.StatusCode}");
                    Log.Error(context.Exception.Message);
                }
            }

            var errorResponse = Helpers.Helper.ConvertToErrorResponse(context.Exception.Message, ErrorsType.UnhandledError.ToString(), ErrorMessageType.Error.ToString());
            if (!_apiSettings.IsProd)
            {
                errorResponse.StackTrace = context.Exception.StackTrace;
            }

            context.Result = new ObjectResult(errorResponse);
        }
    }
}

