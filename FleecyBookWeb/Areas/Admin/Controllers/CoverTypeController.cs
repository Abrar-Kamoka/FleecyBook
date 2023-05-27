using FleecyBook.DataAccess;
using FleecyBook.DataAccess.Repository.IRepository;
using FleecyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FleecyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;



    public CoverTypeController(IUnitOfWork unitOfWork)


    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverTypes.GetAll();
        return View(objCoverTypeList);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverTypes.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Created Successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var CoverTypeFromDbFirst = _unitOfWork.CoverTypes.GetFirstOrDefault(u => u.Id == id);

        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverTypes.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Edited Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var CoverTypeFromDbFirst = _unitOfWork.CoverTypes.GetFirstOrDefault(u => u.Id == id);

        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverTypes.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverTypes.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType Deleted Successfully";
        return RedirectToAction("Index");

    }

}
