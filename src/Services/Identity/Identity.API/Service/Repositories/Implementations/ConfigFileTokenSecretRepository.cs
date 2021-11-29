using OLM.Services.Identity.API.Service.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLM.Services.Identity.API.Service.Repositories.Implementations
{
    public class ConfigFileTokenSecretRepository : ISecretRepository
    {
        private IConfiguration _config;

        public ConfigFileTokenSecretRepository(IConfiguration config)
        {
            _config = config;
        }

        public string GetTokenSecret()
            => _config.GetValue<string>("Secret");

        public byte[] GetTokenSecretInBytes() =>
            Encoding.UTF8.GetBytes(GetTokenSecret());
    }
}
