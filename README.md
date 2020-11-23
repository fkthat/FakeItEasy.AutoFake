[![Build Status](https://dev.azure.com/FkThat/FakeItEasy.AutoFake/_apis/build/status/CI?branchName=develop)](https://dev.azure.com/FkThat/FakeItEasy.AutoFake/_build/latest?definitionId=24&branchName=develop)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/FkThat/FakeItEasy.AutoFake/24)

# FakeItEasy.AutoFake ![Nuget](https://img.shields.io/nuget/v/FakeItEasy.AutoFake)

An auto-mocking container that generates mocks using FakeItEasy.

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

### Usage with parameters

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

### Multiple constructors support

Since version 3.0.0 AutoFake supports classes with multiple constructors.

CreateInstance selects the constructor with the maximum number of parameters all of which it
can resolve.

Parameters are resolved in the following order:

* Parameter list provided with CreateInstance(...)
* Predefined injectables configured with Use(...)
* Dynamically created fakes

