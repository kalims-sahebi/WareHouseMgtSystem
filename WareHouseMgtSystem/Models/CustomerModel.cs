using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "نام کامل ضروریست")]
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "آدرس  ضروریست")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Required(ErrorMessage = "شماره تماس  ضروریست")]
        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }
        [Display(Name = "حساب")]
        public int Balance { get; set; }
        public int Printed { get; set; }
        public string User { get; set; }
    }
}
