using OLM.Services.TCO.BackgroundTasks.Updater.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.TCO.BackgroundTasks.Updater.Extensions
{
    public static class TCODataSourceExtensions
    {
        public static TCODataDestinationModel ConvertToDestinationModel(this TCODataSourceModel model)
            => new TCODataDestinationModel() 
            {
                BundleID = model.BundleID,
                ID = null,
                Primary = model.Primary,
                Secondary = model.Secondary,
                RawMaterialItemNumber = model.RawMaterialItemNumber, 
                Volume = model.Volume,
                VendorID = model.VendorID
            };
    }
}
