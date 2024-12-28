using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {


        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;  


        public ProductController(IUnitOfWork UnitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }





        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.Product.GetAll(includeProp:"Category").ToList();
            //IEnumerable<SelectListItem> Categorylist = _UnitOfWork.Category.GetAll()
            //    .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

            return View(objProductList);
        }




       

        //[HttpGet]
        //public IActionResult Delete(int? id) {
        //    if (id == null) return NotFound();
        //    Product? productFromDb = _UnitOfWork.Product.Get(u => u.Id == id);
        //    if (productFromDb == null) return NotFound();
        //    return View(productFromDb);
        //}




        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Product? obj = _UnitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null) return NotFound();
        //    _UnitOfWork.Product.Remove(obj);
        //    _UnitOfWork.Save();
        //    TempData["success"] = "Product Deleted Successfully!";
        //    return RedirectToAction("Index");
        //}



        [HttpGet]
        public IActionResult UpSert(int? id) {
            ProductVM obj = new()
            {
                Product = new Product(),
                CategoryList = _UnitOfWork.Category.GetAll().
                Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                )
            };

            if (id == null || id == 0)
            {
                return View(obj);
            }
            else {
                obj.Product = _UnitOfWork.Product.Get(u => u.Id == id);
                return View(obj);
            }



        }


        [HttpPost]
        public IActionResult UpSert(ProductVM obj , IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Give unique name for the image
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //Path to save the Image
                    string productPath = Path.Combine(wwwRootPath, @"images\product");


                    //Remove old image if exist
                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl)) {
                        var oldPath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath)) {
                             System.IO.File.Delete(oldPath);
                        }
                    }






                    using (var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create)) { 
                        file.CopyTo(filestream);
                    }

                    //save URL in DataBase
                    obj.Product.ImageUrl = @"\images\product\" + filename;
                }


                if (obj.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "Product Created Successfully!";
                }
                else
                {
                    _UnitOfWork.Product.Update(obj.Product);
                    TempData["success"] = "Product Updated Successfully!";
                }

                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _UnitOfWork.Category.GetAll().
                Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                );
                return View(obj);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            List<Product> objProductList = _UnitOfWork.Product.GetAll(includeProp: "Category").ToList();
            return Json(new { data = objProductList } );
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Product? productFromDb = _UnitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null) return Json( new { success = false, message = "Error while deleting" });

            //Remove old image if exist
            if (!string.IsNullOrEmpty(productFromDb.ImageUrl))
            {
                var oldPath = Path.Combine(_WebHostEnvironment.WebRootPath, productFromDb.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            _UnitOfWork.Product.Remove(productFromDb);
            _UnitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
