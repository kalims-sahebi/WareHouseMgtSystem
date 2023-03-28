using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class MeasurementModel
    {
        public int MeasurementId { get; set; }
        [Required(ErrorMessage = "نام ضروریست")]
        [Display(Name = "نام")]
        public string Measure { get; set; }
    }
}
