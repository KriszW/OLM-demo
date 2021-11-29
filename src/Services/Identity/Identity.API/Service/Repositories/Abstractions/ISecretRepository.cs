using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Service.Repositories.Abstractions
{
    public interface ISecretRepository
    {
        string GetTokenSecret();
        byte[] GetTokenSecretInBytes();
    }
}
