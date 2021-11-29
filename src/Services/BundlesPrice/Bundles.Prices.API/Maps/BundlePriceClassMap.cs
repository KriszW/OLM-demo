using CsvHelper.Configuration;
using OLM.Services.Bundles.Prices.API.Models;
using System.Globalization;

namespace OLM.Services.Bundles.Prices.API.Maps
{
    public class BundlePriceClassMap : ClassMap<BundlePriceModel>
    {
        public BundlePriceClassMap()
        {
            Map(m => m.ID).Constant(null);


            Map(m => m.RawMaterialItemNumber).Name("Material");
            Map(m => m.VendorID).Name("Vendor");
            Map(m => m.Currency).Name("Unit");
            Map(m => m.Price).Convert(row =>
            {
                var text = row.Row.GetField<string>("AmountPrice");

                return decimal.Parse(text, NumberStyles.Any);
            });
        }
    }
}
