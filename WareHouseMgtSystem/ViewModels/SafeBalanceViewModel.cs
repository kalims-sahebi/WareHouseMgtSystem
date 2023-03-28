using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouseMgtSystem.ViewModels
{
    public class SafeBalanceViewModel
    {

        public int SafeBalancingId { get; set; }
        public int Amount { get; set; }
        public int inout { get; set; }
        public int Total { get; set; }

    }
}
