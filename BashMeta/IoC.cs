using System.ComponentModel.Design;
using Microsoft.Extensions.DependencyInjection;

namespace BashMeta
{
    public static class IoC
    {
        private static readonly IServiceCollection collection = new ServiceCollection();
        private static ServiceProvider provider;

        public static TService Get<TService>()
        {
            if (provider == null)
                throw new ArgumentNullException("Provider is not initialized. Run IoC.Build method before");

            return provider.GetService<TService>();
        }

        public static IServiceCollection Services => collection;

        public static ServiceProvider Build()
        {
            provider = collection.BuildServiceProvider();

            return provider;
        }
    }
}