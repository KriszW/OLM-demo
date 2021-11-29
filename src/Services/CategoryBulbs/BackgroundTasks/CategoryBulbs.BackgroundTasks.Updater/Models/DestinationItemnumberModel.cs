using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models
{
    public class DestinationItemnumberModel
    {
        public int? ID { get; set; }
        public string Itemnumber { get; set; }
        public int BundleItemnumbersModelID { get; set; }
    }
}
