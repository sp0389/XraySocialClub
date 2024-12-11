using Microsoft.AspNetCore.Mvc;
using XraySocialClub.Data.Core;

namespace XraySocialClub.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ApplicationDbContext context, ILogger<BaseController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}