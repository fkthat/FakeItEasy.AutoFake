using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake.Samples
{
    public class AccountData : IAccountData
    {
        private decimal _value;

        public AccountData(decimal value)
        {
            _value = value;
        }

        public decimal GetValue() => _value;

        public void SetValue(decimal value)
        {
            _value = value;
        }
    }
}
