using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;

namespace WareHouseMgtSystem.ViewModels
{
    public class CatListViewModel
    {
        public IEnumerable<CategoryModel> CategoryList { get; set; }
    }
}
