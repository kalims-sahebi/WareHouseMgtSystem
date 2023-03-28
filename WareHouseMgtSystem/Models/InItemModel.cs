using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class InItemModel
    {
        public int InItemId { get; set; }
        [Required(ErrorMessage = "تعداد جنس ضروریست!")]
        [Display(Name = "تعداد جنس")]
        public int Qty { get; set; }
        [Required(ErrorMessage = "قیمت مجموعی جنس ضروریست!")]
        [Display(Name = "قیمت مجموعی جنس")]
        public int TotalPrice { get; set; }
        [Required(ErrorMessage = "نام جنس ضروریست!")]
        [Display(Name = "نام جنس")]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = " تاریخ ورود ضروریست!")]
        [Display(Name = "تاریخ ورود جنس")]
        public string DateString { get; set; }
        public string User { get; set; }
        public string Measure { get; set; }
    }
}
