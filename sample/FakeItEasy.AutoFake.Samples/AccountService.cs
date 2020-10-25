using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake.Samples
{
    public class AccountService
    {
        private readonly IAccountData _accountData;

        public AccountService(IAccountData accountData)
        {
            _accountData = accountData;
        }

        public void Deposite(decimal amount) =>
            _accountData.SetValue(_accountData.GetValue() + amount);
    }
}
