using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.CategoryBulbs.BackgroundTasks.Updater.Models
{
    public class SourceModel
    {
        public string BundleID { get; set; }
        public DateTime FinishedDate { get; set; }
        public string Itemnumber { get; set; }
    }
}
