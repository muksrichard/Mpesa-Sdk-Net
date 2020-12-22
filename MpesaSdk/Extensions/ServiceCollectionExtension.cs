﻿using Microsoft.Extensions.DependencyInjection;
using MpesaSdk.Interfaces;
using Polly;
using System;
using System.Net;
using System.Net.Http;

namespace MpesaSdk.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Creating a mpesa service collection to be used in projects that support dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddMpesaService(this IServiceCollection services)
        {          
            Random jitterer = new Random();
            services.AddHttpClient<IMpesaClient, MpesaClient>(options =>
            {
#if DEBUG
                options.BaseAddress = MpesaRequestEndpoint.SandboxBaseAddress;
                options.Timeout = TimeSpan.FromMinutes(10);
#else
                options.BaseAddress = MpesaRequestEndpoint.LiveBaseAddress;
                options.Timeout = TimeSpan.FromMinutes(10);
#endif
            }).ConfigurePrimaryHttpMessageHandler(messageHandler =>
            {
                var handler = new HttpClientHandler();

                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                }

                return handler;
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(1, retryAttempt =>
            {
                return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
            }));
        }
    }
}