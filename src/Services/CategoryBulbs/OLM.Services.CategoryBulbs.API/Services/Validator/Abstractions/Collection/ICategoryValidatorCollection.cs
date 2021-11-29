using OLM.Services.CategoryBulbs.API.Services.Validator.Implementations.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Validator.Abstractions.Collection
{
    public interface ICategoryValidatorCollection : IList<ICategoryValidator>
    {
        IEnumerable<ValidationResult> ValidateAll();
    }
}
