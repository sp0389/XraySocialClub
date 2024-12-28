namespace XraySocialClub.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddOrganisationServices(this IServiceCollection services)
        {
            services.AddScoped<OrganisationService>();
            services.AddScoped<PaymentService>();

            return services;
        }
    }
}