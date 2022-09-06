using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Extensions;
using Zip.Core.Serilog;

namespace Zip.Api.CustomerSummary.Application
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected ILogger Logger { get; }

        protected IMapper Mapper { get; }

        protected abstract string HandlerName { get; }
        

        protected BaseRequestHandler(ILogger logger)
        {
            Logger = logger;
        }

        protected BaseRequestHandler(ILogger logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected void LogInformation(string details, string method = nameof(Handle))
        {
            Logger.LogInformation($"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}",
                                  HandlerName,
                                  method,
                                  details);
        }

        protected void LogError(Exception exception, string details, string method = nameof(Handle))
        {
            Logger.LogError(exception,
                            $"{Constants.ClassName}::{Constants.MethodName}::{Constants.Detail}::{Constants.StackTrace}",
                            HandlerName,
                            method,
                            details,
                            exception.GetErrorLinesInfo());
        }

        protected void LogError(string details, string method = nameof(Handle))
        {
            Logger.LogError($"{Constants.ClassName}::{Constants.MethodName}::{SerilogProperty.Detail}",
                            HandlerName,
                            method,
                            details);
        }

        protected void LogWarning(string detail, string method = nameof(Handle))
        {
            Logger.LogWarning($"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Detail}",
                              HandlerName,
                              method,
                              detail);
        }
    }
}