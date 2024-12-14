using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Category? category { get; set; }


        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }


        public void OnGet(int? id)
        {
            if (id != null) {
                category = _db.Categories.Find(id);
            }
                       
        }

        public IActionResult OnPost() {
            if (category != null) { 
               _db.Categories.Remove(category);
               _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToPage("Index");
            }
            else return NotFound();
        }
    }
}
