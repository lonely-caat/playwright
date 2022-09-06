using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Extensions;

namespace Zip.Api.CustomerSummary.Infrastructure
{
    public abstract class BaseService<TService> where TService : class
    {
        protected ILogger<TService> Logger { get; }

        protected IMapper Mapper { get; }

        protected BaseService(ILogger<TService> logger)
        {
            Logger = logger;
        }

        protected BaseService(ILogger<TService> logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        protected void LogInformation(string detail, string methodName)
        {
            Logger.LogInformation($"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}",
                                  typeof(TService).Name,
                                  methodName,
                                  detail);
        }

        protected void LogWarning(string methodName, string detail)
        {
            Logger.LogWarning($"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}",
                              typeof(TService).Name,
                              methodName,
                              detail);
        }
        
        protected  void LogError(string methodName, string detail)
        {
            Logger.LogError($"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}",
                            typeof(TService).Name,
                            methodName,
                            detail);
        }

        protected void LogError(Exception exception, string methodName, string detail)
        {
            Logger.LogError(exception,
                            $"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}::{Constants.StackTrace}",
                            typeof(TService).Name,
                            methodName,
                            detail,
                            exception.GetErrorLinesInfo());
        }
    }
}
