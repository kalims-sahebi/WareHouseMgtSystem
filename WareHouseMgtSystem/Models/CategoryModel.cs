using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="نام ضروریست")]
        [Display(Name = "نام")]
        public string Name { get; set; }
    }
}
