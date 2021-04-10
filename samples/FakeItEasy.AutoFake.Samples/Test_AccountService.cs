using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Samples
{
    public class Test_AccountService
    {
        [Fact]
        public void Deposite_ShouldAddAccountValue()
        {
            var faker = new AutoFaker();
            var accountData = faker.Get<IAccountData>();
            A.CallTo(() => accountData.GetValue()).Returns(42);
            var accountService = faker.CreateInstance<AccountService>();
            accountService.Deposite(69);
            A.CallTo(() => accountData.SetValue(111)).MustHaveHappened();
        }

        [Fact]
        public void Deposite_ShouldAddAccountValue_2()
        {
            var accountData = new AccountData(42);
            var faker = new AutoFaker().Use<IAccountData>(accountData);
            var accountService = faker.CreateInstance<AccountService>();
            accountService.Deposite(69);
            accountData.GetValue().Should().Be(111);
        }
    }
}
