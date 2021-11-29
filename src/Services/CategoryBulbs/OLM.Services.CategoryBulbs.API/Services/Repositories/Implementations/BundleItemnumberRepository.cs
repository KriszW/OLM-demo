using Microsoft.EntityFrameworkCore;
using OLM.Services.CategoryBulbs.API.Data;
using OLM.Services.CategoryBulbs.API.Services.Repositories.Abstractions;
using OLM.Services.CategoryBulbs.API.Services.Validator.Implementations.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Repositories.Implementations
{
    public class BundleItemnumberRepository : IBundleItemnumberRepository
    {
        private CategoryBulbsDbContext _dbContext;

        public BundleItemnumberRepository(CategoryBulbsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ValidationResult> HasCategory(string bundleID, string category)
        {
            var bundle = await _dbContext.Bundles
                                            .Include(b => b.Models)
                                            .FirstOrDefaultAsync(b => b.BundleID == bundleID);

            if (bundle == default)
            {
                return new ValidationResult(default, false, $"A {bundleID} rakat nem létezik az adatbázisban");
            }

            var itemNums = bundle.Models.Select(m => m.Itemnumber);
            if (itemNums.Any() == false)
            {
                return new ValidationResult(default, false, $"A {bundleID} rakathoz nincsen cikk feltöltve");
            }

            var res = await _dbContext.Categories.AnyAsync(c => itemNums.Any(i => i == c.Itemnumber) && c.CategoryType == category);

            return new ValidationResult(default, res, $"A {bundleID} rakathoz {( res == true ? "van" : "nincsen")} cikk a {category} kategoriában");
        }
    }
}
