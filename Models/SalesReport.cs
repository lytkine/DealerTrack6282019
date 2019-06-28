using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrackSalesFile.Models
{
    public class SalesReportModel
    {
        public string ReportName { get; set; }
        public List<VehicleSale> Sales { get; private set; }

        public BestSellingVehicle Best { get;set;}

        public static SalesReportModel CreateFromFile(string title, string raw)
        {
            var r = new SalesReportModel() { ReportName = title};

            if (!string.IsNullOrEmpty(raw))
            {
                r.ReadSalesFromText(raw);
                r.ProcessSaleData();
            }
            return r;
        }

        public void ReadSalesFromText(string raw)
        {
            var lines = raw.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2) return;


            Sales = new List<VehicleSale>(lines.Length - 1);

            for(var i = 1; i < lines.Length; i++)
            {
                var sale = VehicleSale.FromTextLine(lines[i]);

                if (sale != null)
                {
                    Sales.Add(sale);
                }
            }

        }

        public void ProcessSaleData()
        {
            if ((Sales == null) || (Sales.Count < 1))
            {
                Best = null;
                return;
            }



            var bestVehicleGroup = Sales.GroupBy(s => s.Vehicle).OrderByDescending(gr => gr.Count()).First().ToList<VehicleSale>();


            Best = new BestSellingVehicle()
            {
                Vehicle = bestVehicleGroup.FirstOrDefault().Vehicle,
                NumberSold = bestVehicleGroup.Count(),
                SalesTotal = bestVehicleGroup.Sum(x => x.Price),
            };

            if (Best.NumberSold > 0)
            {
                Best.AveragePrice = Math.Round(Best.SalesTotal / Best.NumberSold, 2);
            }


            return;
        }

       
    }
}
