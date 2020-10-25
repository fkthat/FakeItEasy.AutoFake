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

### Usage with non-injectable parameters

#### Named parameter

```csharp
var faker = new AutoFaker();

var accountData = faker.Get<IAccountData>();

A.CallTo(() => accountData.GetValue()).Returns(42);

var accountService = faker.CreateInstance<FeeAccountService>(
    new NamedParameter("depositeFee", 10));

accountService.Deposite(100);

// 42 + (100 - 10%) = 132
A.CallTo(() => accountData.SetValue(132)).MustHaveHappened();
```

#### Typed parameter

```csharp
var faker = new AutoFaker();

var accountData = faker.Get<IAccountData>();

A.CallTo(() => accountData.GetValue()).Returns(42);

var accountService = faker.CreateInstance<FeeAccountService>(
    new TypedParameter<int>(10)); // or TypedParameter(typeof(int), 10)

accountService.Deposite(100);

// 42 + (100 - 10%) = 132
A.CallTo(() => accountData.SetValue(132)).MustHaveHappened();
```
