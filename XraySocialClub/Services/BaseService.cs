using XraySocialClub.Data.Core;

namespace XraySocialClub.Areas.Administration.Controllers
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;
        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}