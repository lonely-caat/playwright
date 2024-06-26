﻿using Polly;
using Polly.Contrib.Simmy;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Chaos
{
    [ExcludeFromCodeCoverage]
    public static class SimmyExtensions
    {
        private static OperationChaosSetting GetOperationChaosSettings(this Context context) => context.GetChaosSettings()?.GetSettingsFor(context.OperationKey);

        private static readonly Task<bool> NotEnabled = Task.FromResult(false);
        private static readonly Task<double> NoInjectionRate = Task.FromResult<double>(0);
        private static readonly Task<Exception> NoExceptionResult = Task.FromResult<Exception>(null);
        private static readonly Task<HttpResponseMessage> NoHttpResponse = Task.FromResult<HttpResponseMessage>(null);
        private static readonly Task<TimeSpan> NoLatency = Task.FromResult(TimeSpan.Zero);

        /// <summary>
        /// Add chaos-injection policies to every policy returning <see cref="IAsyncPolicy{HttpResponseMessage}"/>
        /// in the supplied <paramref name="registry"/>.
        /// </summary>
        /// <param name="registry">The <see cref="IPolicyRegistry{String}"/> whose policies should be decorated with chaos policies.</param>
        /// <returns>The policy registry.</returns>
        public static IPolicyRegistry<string> AddChaosInjectors(this IPolicyRegistry<string> registry)
        {
            foreach (KeyValuePair<string, IsPolicy> policyEntry in registry)
            {
                if (policyEntry.Value is IAsyncPolicy<HttpResponseMessage> policy)
                {
                    registry[policyEntry.Key] = policy
                            .WrapAsync(MonkeyPolicy.InjectFaultAsync<HttpResponseMessage>(
                                (ctx, ct) => GetException(ctx),
                                GetInjectionRate,
                                GetEnabled))
                            .WrapAsync(MonkeyPolicy.InjectFaultAsync<HttpResponseMessage>(
                                (ctx, ct) => GetHttpResponseMessage(ctx),
                                GetInjectionRate,
                                GetHttpResponseEnabled))
                            .WrapAsync(MonkeyPolicy.InjectLatencyAsync<HttpResponseMessage>(
                                GetLatency,
                                GetInjectionRate,
                                GetEnabled))
                        ;
                }
            }

            return registry;
        }

        private static Task<bool> GetEnabled(Context context)
        {
            OperationChaosSetting chaosSettings = context.GetOperationChaosSettings();
            if (chaosSettings == null)
            {
                return NotEnabled;
            }

            return Task.FromResult(chaosSettings.Enabled);
        }

        private static Task<double> GetInjectionRate(Context context)
        {
            OperationChaosSetting chaosSettings = context.GetOperationChaosSettings();
            if (chaosSettings == null)
            {
                return NoInjectionRate;
            }

            return Task.FromResult(chaosSettings.InjectionRate);
        }

        private static Task<Exception> GetException(Context context)
        {
            OperationChaosSetting chaosSettings = context.GetOperationChaosSettings();
            if (chaosSettings == null)
            {
                return NoExceptionResult;
            }

            string exceptionName = chaosSettings.Exception;
            if (string.IsNullOrWhiteSpace(exceptionName))
            {
                return NoExceptionResult;
            }

            try
            {
                Type exceptionType = Type.GetType(exceptionName);
                if (exceptionType == null)
                {
                    return NoExceptionResult;
                }

                if (!typeof(Exception).IsAssignableFrom(exceptionType))
                {
                    return NoExceptionResult;
                }

                var instance = Activator.CreateInstance(exceptionType);
                return Task.FromResult(instance as Exception);
            }
            catch
            {
                return NoExceptionResult;
            }
        }

        private static Task<bool> GetHttpResponseEnabled(Context context)
        {
            if (GetHttpResponseMessage(context) == NoHttpResponse)
            {
                return NotEnabled;
            }

            return GetEnabled(context);
        }

        private static Task<HttpResponseMessage> GetHttpResponseMessage(Context context)
        {
            OperationChaosSetting chaosSettings = context.GetOperationChaosSettings();
            if (chaosSettings == null)
            {
                return NoHttpResponse;
            }

            int statusCode = chaosSettings.StatusCode;
            if (statusCode < 200)
            {
                return NoHttpResponse;
            }

            try
            {
                return Task.FromResult(new HttpResponseMessage((HttpStatusCode)statusCode));
            }
            catch
            {
                return NoHttpResponse;
            }
        }

        private static Task<TimeSpan> GetLatency(Context context, CancellationToken token)
        {
            OperationChaosSetting chaosSettings = context.GetOperationChaosSettings();
            if (chaosSettings == null)
            {
                return NoLatency;
            }

            int milliseconds = chaosSettings.LatencyMs;
            if (milliseconds <= 0)
            {
                return NoLatency;
            }

            return Task.FromResult(TimeSpan.FromMilliseconds(milliseconds));
        }

    }
}
