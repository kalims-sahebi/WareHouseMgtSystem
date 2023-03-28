using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WareHouseMgtSystem.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "نام جنس ضروریست!")]
        [Display(Name ="نام جنس")]
        public string Name { get; set; }

        [Required(ErrorMessage = "کد جنس ضروریست!")]
        [Display(Name = "کد جنس")]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "قیمت جنس ضروریست!")]
        [Display(Name = "قیمت واحد")]
        public int Price { get; set; }

        public string User { get; set; }

        [Required(ErrorMessage = "تفصیلات جنس ضروریست!")]
        [Display(Name = "تفصیلات")]
        public string Description { get; set; }

       
        public IEnumerable<SelectListItem> Category { get; set; }

        [Required(ErrorMessage = "کتگوری جنس ضروریست!")]
        [Display(Name = "کتگوری")]
        public int CategoryId { get; set; }

        
        public IEnumerable<SelectListItem> Supplier { get; set; }

        [Required(ErrorMessage = "توضیع کننده جنس ضروریست!")]
        [Display(Name = "توضیع کننده")]
        public int SupplierId { get; set; }

        
        public IEnumerable<SelectListItem> Measurement { get; set; }

        [Required(ErrorMessage = "واحد اندازه گیری جنس ضروریست!")]
        [Display(Name = "واحد اندازه گیری")]
        public int MeasurementId { get; set; }

        [Required(ErrorMessage = "تعداد جنس ضروریست!")]
        [Display(Name = "تعداد")]
        public int Qty { get; set; }



    }
}
