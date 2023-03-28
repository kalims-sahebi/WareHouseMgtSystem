using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouseMgtSystem.Models;

namespace WareHouseMgtSystem.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<ItemListModel> ItemList { get; set; }
    }
}
