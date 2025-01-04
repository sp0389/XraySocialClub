using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Models.Payment;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganisationService _organisationService;

        public PaymentService(ApplicationDbContext context, OrganisationService organisationService)
        {
            _context = context;
            _organisationService = organisationService;
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

        public async Task<IEnumerable<LottoPayment>> GetLottoPaymentsAsync()
        {
            var lottoPayments = await _context.Payments.OfType<LottoPayment>()
                .ToListAsync();
            return lottoPayments;
            //TODO: Error handling
        }

        public async Task<IEnumerable<Payment>> GetPaymentRecordForMemberAsync(string id)
        {
            var member = await _organisationService.GetMemberByIdAsync(id);

            if (member.Role == Role.Lotto)
            {
                var paymentRecord = await _context.Payments.OfType<LottoPayment>()
                    .ToListAsync() ?? throw new ApplicationException("No payment records were found for this user.");
                return paymentRecord;
            }

            else if (member.Role == Role.Lotto && member.Role == Role.Social)
            {
                var paymentRecord = await _context.Payments.OfType<SocialPayment>()
                    .OfType<LottoPayment>()
                    .ToListAsync() ?? throw new ApplicationException("No payment records were found for this user.");
                return paymentRecord;
            }

            else
            {
                var paymentRecord = await _context.Payments.OfType<SocialPayment>()
                    .ToListAsync() ?? throw new ApplicationException("No payment records were found for this user.");
                return paymentRecord;
            }
        }

        public async Task<Payment>CreatePaymentRecordForMemberAsync(string id, PaymentViewModel m)
        {
            var member = await _organisationService.GetMemberByIdAsync(id)
                ?? throw new ApplicationException("No user was found with that ID.");

            if (m.RolePaymentType == RolePaymentType.Lotto)
            {
                var lottoPayment = member.NewLottoPayment(m.Amount!.Value, m.DatePaid!.Value, m.Type!.Value, m.Notes!);
                await _context.Payments.AddAsync(lottoPayment);
                await _context.SaveChangesAsync();
                return lottoPayment;
            }

            else
            {
                var socialPayment = member.NewSocialPayment(m.Amount!.Value, m.DatePaid!.Value, m.Type!.Value, m.Notes!);
                await _context.Payments.AddAsync(socialPayment);
                await _context.SaveChangesAsync();
                return socialPayment;
            }
        }
    }
}