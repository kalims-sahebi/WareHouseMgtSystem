using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;

namespace WareHouseMgtSystem.ViewModels
{
    public class OutItemViewModel
    {
        public IEnumerable<OutItemModel> OutItemList { get; set; }
    }
}
