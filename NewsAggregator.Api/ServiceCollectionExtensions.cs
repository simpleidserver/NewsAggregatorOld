// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using NewsAggregator.Api.DataSources;
using NewsAggregator.Api.Feeds;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorApi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));
            services.AddTransient<IDataSourceService, DataSourceService>();
            services.AddTransient<IFeedService, FeedService>();
            return services;
        }
    }
}