using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models
{
    public class DestinationBundleModel
    {
        public int? ID { get; set; }
        public string BundleID { get; set; }

        public IEnumerable<DestinationItemnumberModel> Itemnumbers { get; set; }
    }
}
