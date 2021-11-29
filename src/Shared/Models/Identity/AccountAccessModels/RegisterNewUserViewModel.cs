using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Shared.Models.Identity.AccountAccessModels
{
    public class RegisterNewUserViewModel
    {
        public RegisterNewUserViewModel() { }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
