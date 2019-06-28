using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DealerTrackSalesFile.Models
{
    public class VehicleSale
    {
        public int DealNumber { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }
        public decimal Price { get; set; }
        public string Date { get; set;}

        public static VehicleSale FromTextLine(string dataline)
        {
            if (string.IsNullOrWhiteSpace(dataline)) return null;


            var vs = new VehicleSale();


            var regexObj = new Regex(@"""[^""]+""|'[^']+'|[^,]+");
            var columns = new List<string>();

            foreach (Match m in regexObj.Matches(dataline))
            {
                columns.Add(m.Value);
            }


            for (var i = 0; i < columns.Count; i++)
            {
                var val = columns[i].Trim();

                switch(i)
                {
                    case 0:
                        int dnum;
                        Int32.TryParse(val, out dnum);
                        vs.DealNumber = dnum;
                              
                        break;
                    case 1:
                        vs.CustomerName = val;
                        break;
                    case 2:
                        vs.DealershipName = val;
                        break;
                    case 3:
                        vs.Vehicle = val;
                        break;
                    case 4:
                        val = val.Trim('"').Replace(",", "");
                        decimal pr;
                        decimal.TryParse(val, out pr);
                        vs.Price = pr;
                        break;
                    case 5:
                        vs.Date = val;
                        break;

                }
            }

            return vs;
        }
    }
}
