---
uid: Intro
---

# Intro

Testify is a .NET unit test assertions, test data creation and contract verification framework. It's not
dependent on any specific unit testing framework, and can be used with the unit test framework of your choice.

Testify @Assertions use a fluent syntax that makes it easy to discover what assertions are valid for the actual
value being tested through strong IntelliSense support. In addition, because of the fluent syntax, it is easy to
add your own custom assertions.

Testify's @Testify.AnonymousData allows you to create test data and objects that are necessary for the test to run but
that aren't otherwise of interest. This helps to decouple the test from non-relevant production code (making the tests
less "brittle"), as well as helps to clarify what's actually being tested. For more information see @UsingAnonymousData.

Testify @ContractVerifiers make it easy to test that a type adheres to a contract by running a suite of tests against
the type.

If you're new to unit testing or want to know how best to apply Testify in your unit tests, see the @WhyUnitTest and
@BestPractices documentation.