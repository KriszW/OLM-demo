using OLM.Services.CategoryBulbs.API.Services.Validator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Validator.Implementations.Collection
{
    public class ValidationResult
    {
        public ValidationResult(ICategoryValidator validator, bool validationSucceded, string message)
        {
            Validator = validator;
            ValidationSucceded = validationSucceded;
            Message = message;
        }

        public ICategoryValidator Validator { get; set; }

        public bool ValidationSucceded { get; set; }
        public string Message { get; set; }
    }
}
