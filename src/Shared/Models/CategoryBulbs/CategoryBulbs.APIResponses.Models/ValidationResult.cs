using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Models.CategoryBulbs.APIResponses
{
    public class ValidationResult
    {
        public ValidationResult() { }

        public string Description { get; set; }

        public bool ValidationSucceded { get; set; }

        public string Name { get; set; }
    }
}
