using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //ApplicationDbContext _db;
        //ICategoryRepository _categoryRepository;
        //public CategoryController(ICategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}
        IUnitofOfWork _unitOfWork;
        public CategoryController(IUnitofOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepo.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepo.Update(category);
               _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryDb = _unitOfWork.categoryRepo.Get(u => u.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category category = _unitOfWork.categoryRepo.Get(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.categoryRepo.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
