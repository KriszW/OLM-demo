using System;
using System.Collections.Generic;
using System.Text;

namespace OLM.Services.SharedBases.Abstractions
{
    interface IServiceBase<TModel> : IBase
        
    {
        TModel Model { get; set; }
    }
}
