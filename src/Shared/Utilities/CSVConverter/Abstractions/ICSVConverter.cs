using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.CSVConverter.Abstractions
{
    public interface ICSVConverter<TModel>
        where TModel : class
    {
        Task<IEnumerable<TModel>> ConvertAsync(IEnumerable<string> lines);
    }
}
