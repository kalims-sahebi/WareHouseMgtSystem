using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        [Required(ErrorMessage ="نام ضروریست")]
        [Display(Name ="نام")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "نام برند")]
        [Display(Name = "برند")]
        public string Brand { get; set; }

    }
}
