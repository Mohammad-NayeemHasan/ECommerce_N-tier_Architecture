using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofOfWork _unitofOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitofOfWork unitofOfWork)
        {
            _logger = logger;
            _unitofOfWork = unitofOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            Product product = _unitofOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
