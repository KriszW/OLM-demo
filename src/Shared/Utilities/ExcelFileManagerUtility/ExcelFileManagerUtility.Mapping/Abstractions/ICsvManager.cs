using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Shared.Utilities.ExcelFileManagerUtility.Mapping.Abstractions
{
    public interface ICsvManager : ICsvReader, ICsvWriter
    {
    }
}
