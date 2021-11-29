using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Shared.Exceptions.DataManagerExceptions
{
    public class UniqueDataAlreadyExistsException<TColumnType> : ContainsKeyAndValueBaseException<TColumnType>
    {
        public UniqueDataAlreadyExistsException(TColumnType colValue, string colName) : base(colValue, colName) { }

        public UniqueDataAlreadyExistsException(TColumnType colValue, string colName, string message) : base(colValue, colName, message) { }
    }
}
