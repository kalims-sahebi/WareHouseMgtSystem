using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class BillModel
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }
}
