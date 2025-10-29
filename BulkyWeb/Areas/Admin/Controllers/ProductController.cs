using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitofOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // 🔹 Display all products
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product
                .GetAllProductsWithCategory().ToList();

            return View(objProductList);
        }

        // 🔹 Create or Update Product Page
        public IActionResult Upsert(int? id)  // Upsert = Update + Insert
        {
            var productVM = new ProductVM()
            {
                PVM = new Product(),
                CategoryList = _unitOfWork.categoryRepo.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                // Create Product
                return View(productVM);
            }
            else
            {
                // Update Product
                productVM.PVM = _unitOfWork.Product.Get(u => u.Id == id);
                if (productVM.PVM == null)
                    return NotFound();

                return View(productVM);
            }
        }

        // 🔹 Create or Update Product (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Handle Image Upload (optional)
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!Directory.Exists(productPath))
                        Directory.CreateDirectory(productPath);

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.PVM.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.PVM.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.PVM);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.PVM);
                    TempData["success"] = "Product Updated Successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            // reload category list if model invalid
            productVM.CategoryList = _unitOfWork.categoryRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(productVM);
        }

        // 🔹 Delete Product (GET)
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product productDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productDb == null)
            {
                return NotFound();
            }

            return View(productDb);
        }

        // 🔹 Delete Product (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
