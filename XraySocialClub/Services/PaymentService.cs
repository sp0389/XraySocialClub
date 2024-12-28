using Microsoft.EntityFrameworkCore;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments.ToListAsync();
            return payments;
            //TODO: Error handling
        }

        public async Task<IEnumerable<SocialPayment>> GetSocialPaymentsAsync()
        {
            var socialPayments = await _context.Payments.OfType<SocialPayment>()
                .ToListAsync();
            return socialPayments;
            //TODO: Error handling
        }

        public async Task <IEnumerable<LottoPayment>> GetLottoPaymentsAsync()
        {
            var lottoPayments = await _context.Payments.OfType<LottoPayment>()
                .ToListAsync();
            return lottoPayments;
            //TODO: Error handling
        }
    }
}
