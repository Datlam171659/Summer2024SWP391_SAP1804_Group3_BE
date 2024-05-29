using JewelleryShop.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryShop.API.Controllers
{
    public class DiscountController : Controller
    {
        private readonly JewelleryDBContext _context;

        public DiscountController(JewelleryDBContext context)
        {
            _context = context;
        }
    }
}
