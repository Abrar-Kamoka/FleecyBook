using FleecyBook.DataAccess;
using FleecyBook.DataAccess.Repository.IRepository;
using FleecyBook.Models;
using FleecyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace FleecyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;




    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)

    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    //GET
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {

                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverTypes.GetAll().Select(i => new SelectListItem
            {

                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id == null || id == 0)
        {
            //create Product
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);
        }
        else
        {
            // update Product
            productVM.Product = _unitOfWork.Products.GetFirstOrDefault(i => i.Id == id);
            return View(productVM);
        }
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {                                              //to upload an image
            string wwwRootpath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootpath, @"Images\products");
                var extension = Path.GetExtension(file.FileName);
                //update image
                if(obj.Product.ImageUrl != null)
                {                                //delete image
                    var oldImagePath = Path.Combine(wwwRootpath, obj.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"\Images\products\" + filename + extension;

            }

            if (obj.Product.Id == 0) 
            { 
                 _unitOfWork.Products.Add(obj.Product);
            }

            else 
            {

                _unitOfWork.Products.Update(obj.Product);
            }

            _unitOfWork.Save();
            
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

  

    #region API CALL
    [HttpGet]

    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Products.GetAll(includeProperties: "Category,CoverType");
        return Json(new { data = productList });
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Products.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new {success = false, message = "Error While Deleting"});
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Products.Remove(obj);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Deleted Successfully" });

    }

    #endregion

}
