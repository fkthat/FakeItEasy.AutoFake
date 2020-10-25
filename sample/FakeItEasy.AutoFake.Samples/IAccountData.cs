using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake.Samples
{
    public interface IAccountData
    {
        decimal GetValue();

        void SetValue(decimal value);
    }
}
