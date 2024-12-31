using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public IActionResult Index()
        {
            IEnumerable<Company> CompnyList = _unitOfWork.Company.GetAll().ToList();
            return View(CompnyList);

        }


        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            Company obj = new Company();
            
            if (id == null || id == 0)
            {
                return View(obj);
            }
            else
            {
                obj = _unitOfWork.Company.Get(u => u.id == id);
                return View(obj);
            }
        }



        //[HttpPost]
        //public IActionResult UpSert(Company obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (obj.id == 0 || obj.id == null) {
        //            _unitOfWork.Company.Add(obj);
                  
        //            TempData["success"] = "Comapny Created Successfully!";
        //        }
        //        else{
        //            _unitOfWork.Company.Update(obj);
        //            TempData["success"] = "Comapny Updated Successfully!";
        //        }
        //        _unitOfWork.Save();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        




        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company? obj = _unitOfWork.Company.Get(u => u.id == id);
            if (obj == null) return  Json(new { success = false, message = "Sorry Delete Unsuccessful!" });
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
          
            return Json(new { success = true, message = "Delete Successful!" });
        }





        //[HttpGet]
        //public IActionResult UpSert(int id) { 
        //    Company obj = new Company();
        //    return Json()
            
        //}






        #endregion




    }
}


