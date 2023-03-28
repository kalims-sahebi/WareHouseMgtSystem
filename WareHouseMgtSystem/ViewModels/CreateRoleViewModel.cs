using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.ViewModels
{
    public class CreateRoleViewModel
    {   [Required(ErrorMessage ="نام رول ضروری است")]
        [Display(Name ="نام رول جدید")]
        public string RoleName { get; set; }
    }
}
