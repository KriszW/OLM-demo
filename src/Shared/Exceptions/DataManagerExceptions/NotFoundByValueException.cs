using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Exceptions.DataManagerExceptions
{
    public class NotFoundByValueException<TColumn> : ContainsKeyAndValueBaseException<TColumn>
    {
        public NotFoundByValueException(TColumn colValue, string colName) : base(colValue, colName) { }

        public NotFoundByValueException(TColumn colValue, string colName, string message) : base(colValue, colName, message) { }
    }
}
