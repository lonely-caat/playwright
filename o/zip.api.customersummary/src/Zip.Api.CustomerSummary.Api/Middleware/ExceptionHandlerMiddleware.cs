using System;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Api.Models;
using Zip.Api.CustomerSummary.Domain.Enum;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Api.Exceptions;
using Polly;
using Polly.Timeout;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var (httpStatusCode, customErrorCode) = GetStatusAndErrorCode(exception);
            var message = string.IsNullOrEmpty(exception.Message) ? customErrorCode.ToString() : exception.Message;

            var result = JsonConvert.SerializeObject(new
            {
                success = false,
                error = new ExceptionResponse
                {
                    Code = customErrorCode.ToString(),
                    Message = $"{exception.GetType().Name}: {message}"
                }
            }, Formatting.Indented);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            return httpStatusCode == HttpStatusCode.NoContent
                       ? httpContext.Response.CompleteAsync()
                       : httpContext.Response.WriteAsync(result);
        }

        private static (HttpStatusCode, CustomErrorCode) GetStatusAndErrorCode(Exception exception)
        {
            HttpStatusCode statusCode;
            CustomErrorCode errorCode;

            switch (exception)
            {
                case ValidationException _:
                case HeaderMissingException _:
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = CustomErrorCode.InvalidRequest;
                    break;

                case AccountsApiException _:
                case CustomerCoreApiException _:
                case CoreGraphException _:
                case CoreApiException _:
                case MerchantDashboardApiException _:
                case MfaApiException _:
                case BeamApiException _:
                    statusCode = HttpStatusCode.FailedDependency;
                    errorCode = CustomErrorCode.ExternalCall;
                    break;

                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = CustomErrorCode.AccessDenied;
                    break;

                case ExecutionRejectedException e when (e is TimeoutRejectedException):
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    errorCode = CustomErrorCode.ServerError;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = CustomErrorCode.ServerError;
                    break;
            }

            return (statusCode, errorCode);
        }
    }
}
