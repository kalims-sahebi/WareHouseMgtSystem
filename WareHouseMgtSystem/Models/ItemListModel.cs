using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class ItemListModel
    {
        public int ItemId { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public string Measure { get; set; }
        public string SupplierName { get; set; }
    }
}
