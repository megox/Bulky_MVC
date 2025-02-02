using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class PaymentStatusFilterVM
    {
        public List<string> SelectedStatuses { get; set; }
        public List<SelectListItem> AvailableStatuses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Approved", Text = "Approved" },
            new SelectListItem { Value = "Pending", Text = "Pending" },
            new SelectListItem { Value = "Rejected", Text = "Rejected" }
        };
    }
}
