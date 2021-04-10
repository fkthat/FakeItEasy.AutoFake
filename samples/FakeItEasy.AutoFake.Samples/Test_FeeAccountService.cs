using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Samples
{
    public class Test_FeeAccountService
    {
        [Fact]
        public void Deposite_ShouldAddAccountValue()
        {
            var faker = new AutoFaker();

            var accountData = faker.Get<IAccountData>();

            A.CallTo(() => accountData.GetValue()).Returns(42);

            var accountService = faker.CreateInstance<FeeAccountService>(
                new NamedParameter("depositeFee", 10));

            accountService.Deposite(100);

            // 42 + (100 - 10%) = 132
            A.CallTo(() => accountData.SetValue(132)).MustHaveHappened();
        }

        [Fact]
        public void Deposite_ShouldAddAccountValue_2()
        {
            var accountData = new AccountData(42);
            var faker = new AutoFaker().Use<IAccountData>(accountData);

            var accountService = faker.CreateInstance<FeeAccountService>(
                new NamedParameter("depositeFee", 10));

            accountService.Deposite(100);
            accountData.GetValue().Should().Be(132);
        }

        [Fact]
        public void Deposite_ShouldAddAccountValue_3()
        {
            var faker = new AutoFaker();
            var accountData = faker.Get<IAccountData>();
            A.CallTo(() => accountData.GetValue()).Returns(42);

            var accountService = faker.CreateInstance<FeeAccountService>(
                new TypedParameter<int>(10));

            accountService.Deposite(100);
            // 42 + (100 - 10%) = 132
            A.CallTo(() => accountData.SetValue(132)).MustHaveHappened();
        }

        [Fact]
        public void Deposite_ShouldAddAccountValue_4()
        {
            var accountData = new AccountData(42);
            var faker = new AutoFaker().Use<IAccountData>(accountData);

            var accountService = faker.CreateInstance<FeeAccountService>(
                new TypedParameter<int>(10));

            accountService.Deposite(100);
            accountData.GetValue().Should().Be(132);
        }
    }
}
