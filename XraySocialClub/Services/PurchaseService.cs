using Microsoft.EntityFrameworkCore;
using XraySocialClub.Areas.Administration.Models.Purchase;
using XraySocialClub.Data;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Services
{
    public class PurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context, OrganisationService organisationService)
        {
            _context = context;
        }

        public async Task <IEnumerable<Purchase>> GetAllPurchaseRecords()
        {
            var purchases = await _context.Purchases.ToListAsync();
            return purchases;
        }

        //TODO: Create a method to create a purchase record.
        public async Task<Purchase> CreatePurchaseRecord(PurchaseViewModel m)
        {
            var purchaseRecord = new Purchase(m.Description!, m.TotalPrice!.Value, m.ReceiptNumber!, m.DatePurchased!.Value);

            if (purchaseRecord != null)
            {
                _context.Purchases.Add(purchaseRecord);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException("There was an error creating the purchase record. Please try again.");
            }
            return purchaseRecord;
        }

        //TODO: Create a method to get a purchase record details by ID.
    }
}
