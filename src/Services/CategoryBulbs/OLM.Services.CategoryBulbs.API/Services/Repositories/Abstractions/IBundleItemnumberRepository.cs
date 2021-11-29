using OLM.Services.CategoryBulbs.API.Services.Validator.Implementations.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions
{
    public interface IBundleItemnumberRepository
    {
        Task<ValidationResult> HasCategory(string bundleID, string category);
    }
}
