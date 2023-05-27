using FleecyBook.DataAccess;
using FleecyBook.DataAccess.Repository.IRepository;
using FleecyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FleecyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;        // now replaced Dbcontext with Repository  ( //don't need to create objects   // _8
                                                     //controller will do this, to work with DB )

    //to write constructor --> ctor tabtab
    public CategoryController(IUnitOfWork unitOfWork) //whatever written inside containers, you can access that (we've registered application DBC=context (program.cs))


    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()     //Right_Click > addview > RazorView      _6
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll(); // SO SIMPLE => you don't have to write, SELECT statements to retrieve all the tables
        return View(objCategoryList);  // display the categories 
    }

    //GET
    public IActionResult Create()         //Right_Click>addview>RazorView      _17
    {
        return View();          //if you gonna add/create somthing new by user, stay feilds/parameters empty here.
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)                 //add dumy categories _19
    {
        if (obj.Name == obj.DisplayOrder.ToString())   //Comparison two fields _22
        {
            ModelState.AddModelError("name", "budhu write right");
        }

        if (ModelState.IsValid)    // put validations _20
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully"; /* _31 give noti's to Create/Edite/Delete*/
            return RedirectToAction("Index");
        }
        return View();
    }

    //GET
    public IActionResult Edit(int? id)     // Edit Category     _25
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "budhu write right");
        }

        if (ModelState.IsValid)    // put validations _20
        {
            _unitOfWork.Category.Update(obj);   /* for Update db _28 */
            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)     // Delete Category     _29
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var categoryFromDb = _db.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");

    }

}
