﻿#if NETCOREAPP2_1
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Tests
{
    using System;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public class DependencyContextResolutionTests
    {
        private readonly IServiceProvider _provider;

        public DependencyContextResolutionTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(new Logger());
            services.AddMediatR();
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldResolveMediator()
        {
            _provider.GetService<IMediator>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveRequestHandler()
        {
            _provider.GetService<IRequestHandler<Ping, Pong>>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveNotificationHandlers()
        {
            _provider.GetServices<INotificationHandler<Pinged>>().Count().ShouldBe(3);
        }
    }
}
#endif