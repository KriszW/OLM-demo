using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.SharedBases.Abstractions.Request
{
    interface IRequestBase : IBase
    {
        DateTime? CreationDate { get; set; }
    }
}
