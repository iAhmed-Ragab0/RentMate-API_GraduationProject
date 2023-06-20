using RentMate_Domain.Records.Stripe;
using RentMate_Service.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services.Stripe
{
    public class StripeAppService : IStripeAppService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly PaymentIntentService _paymentIntentService;

        public StripeAppService(
            ChargeService chargeService,
            CustomerService customerService,
            PaymentIntentService paymentIntentService,
            TokenService tokenService)
        {
            _chargeService = chargeService;
            _paymentIntentService = paymentIntentService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken ct)
        {
            // Set Stripe Token options based on customer data
            //TokenCreateOptions tokenOptions = new TokenCreateOptions
            //{

            //};

            // Create new Stripe Token
            //Token stripeToken = await _tokenService.CreateAsync(/*tokenOptions,*/  ct)/*;*/

            // Set Customer options using
            CustomerCreateOptions customerOptions = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email,
                //Source = stripeToken.Id,
                PaymentMethod = "pm_card_visa"
            };

            // Create customer at Stripe
            Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            // Return the created customer at stripe
            return new StripeCustomer(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }


        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            // Set the options for the payment we would like to create at Stripe
            var options = new PaymentIntentCreateOptions
            {

                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount,
                PaymentMethod = "pm_card_visa",
                CaptureMethod = "automatic",
                Confirm = true


            };

            // Create the payment
            var createdPayment = await _paymentIntentService.CreateAsync(options, null, ct);

            // Return the payment to requesting method
            return new StripePayment(
              createdPayment.CustomerId,
              createdPayment.ReceiptEmail,
              createdPayment.Description,
              createdPayment.Currency,
              createdPayment.Amount,
              createdPayment.Id);
        }
    }
}
