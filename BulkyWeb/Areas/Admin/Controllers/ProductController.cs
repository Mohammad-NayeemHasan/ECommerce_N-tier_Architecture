using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        IUnitofOfWork _unitOfWork;
        public ProductController(IUnitofOfWork unitofOfWork)
        {
            _unitOfWork = unitofOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.productRepo.GetAll().ToList();
            return View(objProductList);
        }
    }
}
