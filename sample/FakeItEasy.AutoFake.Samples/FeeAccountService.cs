using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake.Samples
{
    public class FeeAccountService
    {
        private readonly IAccountData _accountData;
        private readonly int _depositeFee;

        public FeeAccountService(IAccountData accountData, int depositeFee)
        {
            _accountData = accountData;
            _depositeFee = depositeFee;
        }

        public void Deposite(decimal amount) =>
            _accountData.SetValue(_accountData.GetValue() +
                amount * (1 - _depositeFee / 100m));
    }
}
