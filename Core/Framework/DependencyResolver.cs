
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace Laren.E2ETests.Core.Framework
{
    public class DependencyResolver
    {
        private static IServiceProvider _provider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                if (_provider == null)
                {
                    _provider = value;
                }
            }
        }        
    }
}
