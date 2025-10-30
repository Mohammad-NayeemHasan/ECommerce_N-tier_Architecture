using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IUnitofOfWork _unitOfWork;
        public ProductController(IUnitofOfWork unitofOfWork)
        {
            _unitOfWork = unitofOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product
                .GetAllProductsWithCategory().ToList();

            return View(objProductList);
        }
       
     
        public IActionResult UpSert( int? id)  // Upsert means (update + insert)
        {
            var ProductVM = new ProductVM()
            {
                PVM = new Product(),
                CategoryList = _unitOfWork.categoryRepo.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == null || id == 0)
            {
                //create product
                return View(ProductVM);
            }
            else
            {
                //update product
                ProductVM.PVM = _unitOfWork.Product.Get(u => u.Id == id);
                return View(ProductVM);
            }

        }
        [HttpPost]
        public IActionResult UpSert(ProductVM pvm , IFormFile? file)
        {
            if (ModelState.IsValid)
            {
               // _unitOfWork.Product.Add(pvm);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
            }
            return RedirectToAction("Index");
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product Product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product Updated Successfully";
        //    }
        //    return RedirectToAction("Index");
        //}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product ProductDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (ProductDb == null)
            {
                return NotFound();
            }
            return View(ProductDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product Product = _unitOfWork.Product.Get(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
