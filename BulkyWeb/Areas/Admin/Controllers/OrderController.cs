using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe.Climate;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {


        public readonly IUnitOfWork _unitOfWork;
        
        [BindProperty]
        public OrderVM OrderVM { get; set; }




        public OrderController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index(List<string>? selectedStatuses)
        {
            
            PaymentStatusFilterVM filterVM = new PaymentStatusFilterVM();



            return View(filterVM);
        }







        public IActionResult Detail(int Id) {

            OrderVM = new OrderVM();
            OrderVM.OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == Id, includeProp: "ApplicationUser");
            OrderVM.OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.Id == Id, includeProp: "Product");

          return View(OrderVM);
        }



        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;    
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;

            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier)) { 
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber)) { 
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Order Details Updated Successfully!";

            return RedirectToAction(nameof(Detail) , new { Id = orderHeaderFromDb.Id});
        }



        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult StartProcessing()
        {
            OrderVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id , SD.StatusInProcess);
            _unitOfWork.Save();

            TempData["success"] = "Order Status Updated Successfully!";

            return RedirectToAction(nameof(Detail), new { Id = OrderVM.OrderHeader.Id });
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult ShipOrder()
        {

            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier=OrderVM.OrderHeader.Carrier;
            orderHeader.OrderStatus=SD.StatusShipped;
            orderHeader.ShippingDate=DateTime.Now;
            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment) {
                orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }




            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();


            TempData["success"] = "Order Shipped Successfully!";

            return RedirectToAction(nameof(Detail), new { Id = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult CancelOrder()
        {

            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader.OrderStatus == SD.StatusApproved
                && orderHeader.PaymentStatus == SD.StatusApproved)
            {
                //refund logic

                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
                TempData["success"] = "Order Canceld Successfully!";
                _unitOfWork.Save();
            }
            else if (orderHeader.OrderStatus == SD.StatusApproved
              && orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled);
                TempData["success"] = "Order Canceld Successfully!";
                _unitOfWork.Save();
            }

            else if (orderHeader.OrderStatus == SD.StatusPending)
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled);
                TempData["success"] = "Order Canceld Successfully!";
                _unitOfWork.Save();
            }
            else {
                TempData["error"] = "Order Can't Be Canceld!";
            }

            return RedirectToAction(nameof(Detail), new { Id = OrderVM.OrderHeader.Id });
        }




        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {

            if (!User.Identity.IsAuthenticated) {
                return Json(null);
            }

            IEnumerable<OrderHeader> OrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                OrderHeaders = _unitOfWork.OrderHeader.GetAll().ToList();
            }
            else {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                OrderHeaders = _unitOfWork.OrderHeader.GetAll(x=> x.ApplicationUserId == id).ToList();
            }
           

            switch (status)
            {
                case "pending":
                    OrderHeaders = OrderHeaders.Where(x=> x.OrderStatus == SD.StatusPending);
                    break;
                case "inprocess":
                    OrderHeaders = OrderHeaders.Where(x => x.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    OrderHeaders = OrderHeaders.Where(x => x.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    OrderHeaders = OrderHeaders.Where(x => x.OrderStatus == SD.StatusApproved);
                    break;
                case "cancelld":
                    OrderHeaders = OrderHeaders.Where(x => x.OrderStatus == SD.StatusCancelled);
                    break;


            }






            return Json(new{data = OrderHeaders });
        }


        #endregion



    }
}
