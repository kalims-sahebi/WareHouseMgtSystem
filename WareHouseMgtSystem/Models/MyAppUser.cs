using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class MyAppUser: IdentityUser
    {
        
        public string FullName { get; set; }
    }
}
