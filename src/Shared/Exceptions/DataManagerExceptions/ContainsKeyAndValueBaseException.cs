using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Exceptions.DataManagerExceptions
{
    public class ContainsKeyAndValueBaseException<TColType> : Exception
    {
        public ContainsKeyAndValueBaseException(TColType colValue, string colName, string message) : base(message)
        {
            ColumnValue = colValue;
            ColumnName = colName;
        }

        public ContainsKeyAndValueBaseException(TColType colValue, string colName)
        {
            ColumnValue = colValue;
            ColumnName = colName;
        }

        public string ColumnName { get; set; }
        public TColType ColumnValue { get; set; }
    }
}
