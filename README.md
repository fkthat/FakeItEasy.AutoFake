# FakeItEasy.AutoFake

A simple autofaking container for FakeItEasy.

## Usage

### Simple usage

```csharp
var faker = new AutoFaker();

// Get<> returns a fake to be autoinjected
var accountData = faker.Get<IAccountData>();

A.CallTo(() => accountData.GetValue()).Returns(42);

// CreateInstance<> creates an instance of the system under test
var accountService = faker.CreateInstance<AccountService>();
accountService.Deposite(69);

A.CallTo(() => accountData.SetValue(111)).MustHaveHappened();
```

### Usage with a predefined instance of the injectable service

```csharp
// the predefined instance of the injectable service
var accountData = new AccountData(42);

var faker = new AutoFaker(configure => 
    // Use<> configures faker to use an instance
    configure.Use<IAccountData>(accountData));

// CreateInstance<> creates an instance of the system under test
var accountService = faker.CreateInstance<AccountService>();
accountService.Deposite(69);

accountData.GetValue().Should().Be(111);
```
