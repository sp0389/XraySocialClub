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
        }

        public async Task<IEnumerable<SocialPayment>> GetSocialPaymentsAsync()
        {
            var socialPayments = await _context.Payments.OfType<SocialPayment>()
                .ToListAsync();
            return socialPayments;
        }

        public async Task<IEnumerable<LottoPayment>> GetLottoPaymentsAsync()
        {
            var lottoPayments = await _context.Payments.OfType<LottoPayment>()
                .ToListAsync();
            return lottoPayments;
        }

        public async Task<IEnumerable<Payment>> GetPaymentRecordForMemberAsync(string id)
        {
            var member = await _organisationService.GetMemberByIdAsync(id);
            var memberRoles = await _organisationService.GetUserRolesAsync(member);

            if (memberRoles.Contains("Social") && memberRoles.Contains("Lotto"))
            {
                var paymentRecord = await _context.Payments
                    .Where(m => m.MemberId == member.Id && (m is LottoPayment || m is SocialPayment)).ToListAsync();
                return paymentRecord;
            }

            else if (memberRoles.Contains("Lotto"))
            {
                var paymentRecord = await _context.Payments.OfType<LottoPayment>()
                    .Where(m => m.MemberId == member.Id)
                    .ToListAsync();
                return paymentRecord;
            }

            else
            {
                var paymentRecord = await _context.Payments.OfType<SocialPayment>()
                    .Where(m => m.MemberId == member.Id)
                    .ToListAsync();
                return paymentRecord;
            }
        }

        public async Task<Payment>CreatePaymentRecordForMemberAsync(string id, PaymentViewModel m)
        {
            var member = await _organisationService.GetMemberByIdAsync(id)
                ?? throw new ApplicationException("No user was found with that ID.");

            if (m.RolePaymentType == RolePaymentType.Lotto && member.UserRoles.Contains(Role.Lotto))
            {
                var lottoPayment = member.NewLottoPayment(m.Amount!.Value, m.DatePaid!.Value, m.Type!.Value, m.Notes!);
                await _context.Payments.AddAsync(lottoPayment);
                await _context.SaveChangesAsync();
                return lottoPayment;
            }

            else if (m.RolePaymentType == RolePaymentType.Social && member.UserRoles.Contains(Role.Social))
            {
                var socialPayment = member.NewSocialPayment(m.Amount!.Value, m.DatePaid!.Value, m.Type!.Value, m.Notes!);
                await _context.Payments.AddAsync(socialPayment);
                await _context.SaveChangesAsync();
                return socialPayment;
            }

            else
            {
                throw new ApplicationException("This member does not have that role payment type. Please add the member to the role.");
            }
        }
    }
}