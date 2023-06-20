using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentMate_Service.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services.Stripe
{
    public static class StripeInfrastructure
    {
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
                .AddScoped<PaymentIntentService>()
                .AddScoped<IStripeAppService, StripeAppService>();
        }
    }
}
