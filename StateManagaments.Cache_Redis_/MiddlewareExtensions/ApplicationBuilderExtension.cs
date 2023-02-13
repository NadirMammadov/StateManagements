using StateManagaments.Cache_Redis_.SubscribeTableDependencies;

namespace StateManagaments.Cache_Redis_.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseFoodTableDependency(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeFoodTableDependency>();
            service.SubscribeTableDependency();
        }
    }
}
