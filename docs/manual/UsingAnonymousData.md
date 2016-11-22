---
uid: UsingAnonymousData
---

# Using AnonymousData

It's vital that unit tests be written to make what is being tested as completely comprehensible as possible.
Extra "noise" in tests make it harder to understand the test "at a glance". When code is modified that results
in failing unit tests, any impediment to understanding what is being tested and why it failed is very detrimental
to the development workflow. If a developer has to spend too much time figuring out how to fix their code or
update a test to make it pass again they are likely to give up in frustration and make incorrect test changes
or worse, disable the test. So, if unit tests are to be seen as beneficial instead of detrimental to developer
productivity, tests have to be easily understood.

One of the biggest factors in making a unit test difficult to understand is lengthy and complex setup (arrange
in AAA) code. It's not uncommon for a test to need to create lots of objects and set many properties before
the test's Act phase. Often, most of this setup is just ceremony, and not really relevant to what's being
tested. For example, to test a Search method on a custom collection you'd have to first create the collection
and populate it with items. Ensuring the collection contains a specific item to search for is relevant here,
but the rest of the collection's items really aren't. They are necessary, but what items are added doesn't
make any real difference to the outcome of the test. This is a very simple example, but you can imagine many
other more complex scenarios where the majority of the test setup is necessary but not directly related to
the behavior of the test.

This is where @Testify.AnonymousData comes in. `AnonymousData` is a general purpose object factory specfically
designed to create "random" objects for unit testing. Random may sound like a bad thing. After all, you don't
want random behavior in your tests. However, the random objects created here are very specifically for things
that are needed to execute a test, but that won't effect the outcome of the test. This actually provides
another benefit, by clearly stating in the test what portions of the setup are relevant and what portions are
necessary but do not effect the outcome of the test.

Here's a typical usage example:

[!code-csharp[Simple AnonymousData Example](..\..\src\Examples\Testify.Examples\UsingAnonymousData\SimpleExample.cs)]

## Registering Factories

`AnonymousData` knows how to create random primitive values, as well as several other common data types such
as `DateTime`. For types that it doesn't know about it will try and create a random instance by using the
type's public constructor with the fewest parameters, and creating random values for those parameters.
This means that most types can be randomly created by `AnonymousData` "out of the box". However, some types
may not be creatable using this simple strategy, or you may need more control over how the object is created.
In these scenarios it's possible to register a factory callback with the `AnonymousData` instance that
will be used any time you need an instance of that type.

Here's an example:

[!code-csharp[Register Example](..\..\src\Examples\Testify.Examples\UsingAnonymousData\RegisterExample.cs)]

In addition to registering factories with a specific instance you may also register default factories to use
with all instances of `AnonymousData`, by using the static `RegisterDefault` method.

[!code-csharp[Register Example](..\..\src\Examples\Testify.Examples\UsingAnonymousData\RegisterDefaultExample.cs)]

It's also often useful to have multiple ways to create an anonymous object. For instance, there are ways
to control the minimum and maximum values or random `Int32` instances created by calling the `AnyInt32`
extension method. You can define your own extension methods for your specific types as well. It's always
a good practice to register one of your extension methods as a factory callback as well. When creating
an object that has no registered factory it's always done by using registered factories to create any
data necessary during construction: extension methods won't be called unless they are registered. Only
one such factory may be registered at one time, with the last callback registered being the only callback
registered.

[!code-csharp[Register Example](..\..\src\Examples\Testify.Examples\UsingAnonymousData\AnonymousPerson.cs)]

In general, if only one test needs it, register the factory callback on an `AnonymousData` instance
created in that test. If multiple tests in a single test class will need it, either use a helper
method to create the instance, or create a member instance using your test frameworks test class
setup functionality, and register the factory there. If more than one test class will need to use
the factory, register it in your test framework's test assembly setup, and use `RegisterDefault`
to register it for all `AnonymousData` instances.

## Populating instances

In some scenarios you may need to do more than just create random instances. You may need to populate
all the public properties of those instances with random values as well. This can be done either
shallowly (only the properties on the instance) or deeply (properties on the instance and properties
those values, all the way down the property chain). There are overloads of `Any` that take a
`PopulationOption` enumeration value to create and populate an instance in one call, as well as `Populate`
overloads you can call to populate an existing object instance.

Sometimes you need control over how a type is populated, so there are `Register` and `RegisterDefault`
methods that allow you to specify how a specific property is to be populated.

> [!NOTE]
> While most types can be created by `AnonymousData` out of the box, you will likely find that many
> cannot be successfully populated requiring more configuration to specify how specific properties
> need to be populated.

Here's an example of using `Populate`:

[!code-csharp[Register Example](..\..\src\Examples\Testify.Examples\UsingAnonymousData\PopulateExample.cs)]
