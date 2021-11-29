using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Auth.Abstractions
{
    public interface IAuthenticationService
    {
        Task Login(string token);
        Task Logout();
    }
}
