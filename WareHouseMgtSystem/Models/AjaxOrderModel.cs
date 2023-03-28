using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.Models
{
    public class AjaxOrderModel
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public int Printed { get; set; }

    }
}
