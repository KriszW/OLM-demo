using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.CategoryBulbs.APIResponses.Manager
{
    public class CategoryBulbItemNumberSettingsViewModel
    {
        public CategoryBulbItemNumberSettingsViewModel() { }

        public int? ID { get; set; }
        public string Itemnumber { get; set; }
        public string CategoryType { get; set; }
    }
}
