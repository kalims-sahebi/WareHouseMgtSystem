using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;

namespace WareHouseMgtSystem.ViewModels
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerModel> Customer { get; set; }

        [Required(ErrorMessage = "جنس را انتخاب کنید")]
        public int ItemId { get; set; }
        [Display(Name = "نام جنس")]
        public IEnumerable<SelectListItem> Item { get; set; }
        [Required(ErrorMessage = "تعداد را مشخص کنید")]
        [Display(Name = "تعداد")]
        public int Qty { get; set; }
        [Display(Name = "تفصیلات")]
        public string Description { get; set; }

    }
}
