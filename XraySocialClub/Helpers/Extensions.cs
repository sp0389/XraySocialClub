using XraySocialClub.Services;

namespace XraySocialClub.Helpers
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
            services.AddScoped<ImageService>();

            return services;
        }

        public static string AsTimeAgo(this DateTime dateTime)
        {
            DateTime convertedDateTime = dateTime.ToLocalTime();
            TimeSpan timeSpan = DateTime.Now.Subtract(convertedDateTime);

            return timeSpan.TotalSeconds switch
            {
                <= 60 => $"{timeSpan.Seconds} seconds ago",

                _ => timeSpan.TotalMinutes switch
                {
                    <= 1 => "about a minute ago",
                    < 60 => $"about {timeSpan.Minutes} minutes ago",
                    _ => timeSpan.TotalHours switch
                    {
                        <= 1 => "about an hour ago",
                        < 24 => $"about {timeSpan.Hours} hours ago",
                        _ => timeSpan.TotalDays switch
                        {
                            <= 1 => "yesterday",
                            <= 30 => $"about {timeSpan.Days} days ago",

                            <= 60 => "about a month ago",
                            < 365 => $"about {timeSpan.Days / 30} months ago",

                            <= 365 * 2 => "about a year ago",
                            _ => $"about {timeSpan.Days / 365} years ago"
                        }
                    }
                }
            };
        }
    }
}