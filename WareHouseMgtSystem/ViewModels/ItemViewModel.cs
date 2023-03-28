using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;

namespace WareHouseMgtSystem.ViewModels
{
    public class ItemViewModel
    {
        public IEnumerable<ItemModel> ItemList { get; set; }
    }
}
