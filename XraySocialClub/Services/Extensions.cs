namespace XraySocialClub.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddOrganisationServices(this IServiceCollection services)
        {
            services.AddScoped<OrganisationService>();

            return services;
        }
    }
}