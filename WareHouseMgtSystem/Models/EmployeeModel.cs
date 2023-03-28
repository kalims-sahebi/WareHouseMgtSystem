using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace WareHouseMgtSystem.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage = "نام کامل ضروریست")]
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "آدرس کامل ضروریست")]
        [Display(Name = "آدرس کامل")]
        public string Address { get; set; }

        [Required(ErrorMessage = "  اندازه معاش ضروریست")]
        [Display(Name = "معاش")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "   شماره تماس  ضروریست")]
        [Display(Name = "شماره تماس")]
        public string  Phone { get; set; }
        public string User { get; set; }

        [Required(ErrorMessage = "وظیفه را انتخاب کنید")]
        [Display(Name = "وظیفه")]
        public int PositionId { get; set; }

        public string PositionTitle { get; set; }

        public IEnumerable<SelectListItem> Position { get; set; }
    }
}
