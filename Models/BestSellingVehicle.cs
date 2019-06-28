using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrackSalesFile.Models
{
    public class BestSellingVehicle
    {
        public string Vehicle { get; set; }
        public int NumberSold { get; set; }
        public decimal SalesTotal { get; set; }

        public decimal AveragePrice { get; set; }
    }
}
