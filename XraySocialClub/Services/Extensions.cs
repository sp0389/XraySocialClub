namespace XraySocialClub.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddOrganisationServices(this IServiceCollection services)
        {
            services.AddScoped<OrganisationService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<TicketService>();
            services.AddScoped<PurchaseService>();
            services.AddScoped<AnnouncementService>();

            return services;
        }
    }
}