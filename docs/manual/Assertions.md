---
uid: Assertions
---

Assertions
==========

Assertions are a corner stone of any unit test framework. Testify provides a framework of assertions that can replace
the assertions provided by your unit test framework. These assertions are designed to be discoverable through
IntelliSense, readable due to their fluent syntax and extensible using extension methods.

To use Testify assertions include the following using statements in your unit test file.

[!code-csharp[Inclusion](..\..\src\Examples\Testify.Examples\Inclusion.cs#L1-L2)]

```csharp
using Testify;
using static Testify.Assertions;
```

> [!IMPORTANT]
> The static using for `Testify.Assertions` is used to allow you to use the static methods on the `Testify.Assertions` class
> without having to include the class name on every call. If you're using C# 5 or older or just prefer to not use static
> usings you can either fully specify the call, or add an `Assert` helper method to your unit test class, possibly through
> a base class.

> [!NOTE]
> `Testify.Assertions` and several other types exposed by Testify is decorated with a `EditorBrowsableAttribute` that hides
> the type from IntelliSense. `Testify.Assertions` is considered an advanced type that's usually not used directly (it's
> used indirectly using the static using), so it's hidden to improve IntelliSense usability.

With the using statements in place you can add `Assert(result).` to any unit test, at which point IntelliSense will provide
you with a list of valid assertions based on the type of `result`. For instance, if `result` is a `List<int>` the assertions
you'd be presented with in IntelliSense would include `AllItemsAreUnique`. Another common assertion for collections,
`AllItemsAreNotNull`, would not be presented in IntelliSense because `int` is a value type and can't be `null`. This strong
typing and IntelliSense support is one of the key benefits to Testify over the built-in assertions of your unit test
framework of choice.

## Compound Assertions

Testify also supports the concept of a compound assertion. A compound assertion is an assertion composed of multiple other
assertions. All of the assertions that fail in a compound assertion are reported, ensuring that no relevant information about
a test failure is lost. Normally, compound assertions are used to compose ad hoc "logical assertions". In other words, the
intent is to make a single logical assertion by making multiple assertions. If you have a logical assertion that is likely
to be made frequently it is probably better that you write an extension to add that assertion to the framework rather than
using an ad hoc compound assertion.

Here's an example of a compound assertion.

[!code-csharp[Compound Assertion](..\..\src\Examples\Testify.Examples\Assertions\CompoundExample.cs)]

> [!NOTE]
> It's always a good idea to include assertion messages, but it's even more important when composing compound logical
> assertions using `AssertAll`. Failing to do so is very likely to result in test failure messages that are difficult
> to understand.

## Custom Assertions

It's easy to add assertions when using Testify. `Assertions.Assert` returns an `ActualValue<T>` which can be used in
extension methods to add custom assertions. By using strong typing for `T` or using generic constraints you can expose
assertions that are valid and present in IntelliSense only for specific types. Inside your custom assertion method you
can use `Assertions.Throw` to report failures, which helps to ensure the exception message is formatted in a consistent
manner.

> [!NOTE]
> Both `Assertions` and `Assertions.Throw` are decorated with `EditorBrowsableAttribute` to hide them from IntelliSense.

Here's an example of a custom assertion.

[!code-csharp[Custom Assertion](..\..\src\Examples\Testify.Examples\Assertions\CustomAssertionExample.cs)]

> [!NOTE]
> Custom assertions, like the assertions provided by Testify, should include overloads with and without a user message
> and user arguments. The example above only shows the full overload and not the simpler overloads that should be included
> as well.

## AssertionException

Testify assertions all throw an `AssertionException` on failure. This type is derived from `AggregateException`
allowing for multiple inner exceptions. This is how compound assertions are able to report multiple failures. This
is also how contract verifiers can run multiple tests and report all of the failures.

## Provided Assertions

See the API documentation for the following classes.

* @Testify.StandardAssertions
* @Testify.CollectionAssertions
* @Testify.ExceptionAssertions
* @Testify.StringAssertions
* @Testify.TaskAssertions