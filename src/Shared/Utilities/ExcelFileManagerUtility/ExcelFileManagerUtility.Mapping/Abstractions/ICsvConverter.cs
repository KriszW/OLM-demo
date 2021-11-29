using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions
{
    public interface ICsvConverter<T>
        where T : class
    {
        T ToCsv();
        T FromCsv();
    }
}
