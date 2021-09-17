---
uid: ContractVerifiers
---

# Contract Verifiers

Contract verifiers can be thought of as test suites that can be run to verify that a type conforms to a logical contract. As an example, if your type is intended to be equality comparable you can use the @Testify.EquatableVerifier`1 to run a bunch of tests to ensure the type really conforms to that contract. This will test things like is `GetHashCode` overridden, are `==` and `!=` operators defined, etc?

Contract verifiers make it really simple to test your types for very complex behavior that is shared by many other types as well, reducing the amount of test code you have to write significantly. Testify includes a few built-in contract verifiers, but it also provides a very simple framework that allows you to create your own.

## Using Contract Verifiers

A contract verifier is a class that derives from @Testify.ContractVerifier. This abstract class provides a single method, `Verify`, that is called to verify the contract. Concrete verifier classes are usually generic types with a single generic parameter used to specify the type to verify that it conforms to the contract. Usually these verifiers will have properties that need to be set to configure how the verifier will run. For example, the @Testify.EquatableVerifier`1 has a `UniqueItemsFactory` property that you need to set to provide a factory for creating unique items to use in the equality tests run by the verifier. So, a typical usage for using contract verifiers is to create a unit test that creates an instance of the verifier, configures it's properties and then calls `Verify`.

`Verify` leverages @Testify.AssertionException to report multiple test failures. So, while you'll have a single unit test reported in the test runner, failures for that test will include all of the failures found when running the verifier.

## Creating Contract Verifiers

To create your own contract verifier you create a class that derives from @Testify.ContractVerifier and overrides @Testify.ContractVerifier.VerifyConfiguration to verify the user configured the verifier properly and @Testify.ContractVerifier.GetTests to provide an array of test methods to run when verifying the contract. These test methods are written much as you'd write any other unit test method, minus any test framework attributes.

## Provided Contract Verifiers

Testify provides the following contract verifiers.

* @Testify.EquatableVerifier`1
* @Testify.ComparableVerifier`1