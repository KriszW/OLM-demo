using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Exceptions.DataManagerExceptions
{
    public class DataNotFoundWithSpecifiedColumnException<TColumn> : ContainsKeyAndValueBaseException<TColumn>
    {
        public DataNotFoundWithSpecifiedColumnException(TColumn colValue, string colName, string message) : base(colValue, colName, message) { }

        public DataNotFoundWithSpecifiedColumnException(TColumn colValue, string colName) :base(colValue, colName) { }
    }
}
