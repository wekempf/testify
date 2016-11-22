---
uid: BestPractices
---

# Best Practices

1. Test Structure Should Follow Production Code
1. Test Name Should Clearly Describe Test
1. Follow AAA or Four Phase Test Structure
1. Reduce Coupling to Non-Relevant Production Code
1. Ensure Test's Intent is Clearly Understandable
1. Don't "Hide" Important Test Information
1. Use Only a Single "Logical" Assertion
1. Ensure the Assertion Message is Clear
1. Ensure Tests are Quick
1. Avoid Anti-Patterns
1. Use Attributes that Make a Unit Test Good

## Test Structure Should Follow Production Code

It's important that you can easily navigate from production code to the related tests and from tests to the related
production code. The best way to ensure this can be done is to structure the tests as a mirror of the production
code. If you have a production assembly called `Util` you should have a unit test assembly called `Util.Tests`. If
there's a class called `Util.StringUtilities` there should be a test class called `Util.StringUtilitiesTests`. The
folder structures of the production project and the test project should match as well.

> [!NOTE]
> The names given here are for example. It doesn't matter what naming scheme you use, so long as it's logical and
> consistent.

## Test Name Should Clearly Describe Test

Names are always important, and naming is always hard. There's the old joke:

> There are only two hard things in Computer Science: cache invalidation, naming things, and off-by-one errors.
>
> \- Phil Karlton

Joke as much as you like, though, the fact is naming is really hard. It's always important to ensure your names
are meaningful, but it's extra important for your unit test methods. When a refactoring in the production code
leads to a broken test it is extremely important that the developer is able to figure out what the test is doing
very quickly. Any friction here is likely to lead the developer to make inappropriate changes to make the test
pass, or worse, to disable the test, just so they can "get on with coding." If you're tests are going to be a
benefit to your project, and not a source of frustration and pain, then the tests **must be** understandable.
The first thing that will help achieve that goal is to have a meaningful name.

There's another reason your test names have to be meaningful. The name and the test result are the only things
that appear in test reports and logs. It's important that you can tell what's broken, at a high level at least,
just by reading these reports. That's only possible if your names are meaningful.

Test names should clearly indicate what's being tested, the conditions under which the test is being run, and
the expected results of the test. There have been several suggested naming conventions proposed by various sources.
One of the more popular of these follows `MethodName_StateUnderTest_ExpectedBehavior`. For example,
`Withdraw_InvalidAccount_ExceptionThrown`. Note how this convention includes all the information just stated as
needing to be clearly indicated?

> [!NOTE]
> It's not being suggested that this naming scheme is the best or even the one you should use. It's only important
> that you have a naming scheme that's consistent and clearly indicates what's being tested, the conditions under
> which the test is being run, and the expected results of the test.

## Follow AAA or Four Phase Test Structure

There's a well known pattern for structuring unit tests known as AAA, or triple-A. This stands for
"Arrange-Act-Assert", and describes the structure of the unit test. First you arrange everything necessary to
perform the test, such as creating objects and data and configuring the environment. Then you act, performing the
test operation. Finally you assert what the results of the test are expected to be.

There's another well known pattern called Four Phase testing. This breaks a test into four "phases": setup, exercise,
verify and tear down. These two patterns are nearly identical, except Four Phase includes a step for tear down. You
should be familiar with both patterns, but in conversation most people only talk about AAA, as the name helps you to
remember the steps involved and most unit tests don't require any tear down.

> [!NOTE]
> It's far more likely for tear down to be necessary when doing integration testing than it is when doing unit testing.
> If you think a unit test requires a tear down step that's an indication that you might not have fully isolated the
> code to a single "unit" for testing.

These patterns lead to more consistent and easier to understand tests, as well as avoiding the common anti-pattern of
interspersing assertions throughout the test.

> [!IMPORTANT]
> Having easy to understand tests is the most important thing to achieve in order to ensure your tests are a benefit to
> your project and not a source of friction and pain.

## Reduce Coupling to Non-Relevant Production Code

It can be very frustrating when modifying some production code leads to several unit tests failing. It's even more
frustrating to realize many of those tests aren't even testing the code you modified. The frustration caused by having
to fix all of these tests "for no good reason" can lead to a team avoiding or abandoning testing altogether. So, it's
very important that you decouple your unit test from non-relevant production code as much as possible, in order to avoid
changes leading to broken tests that aren't testing the code that was modified.

This is the first best practice where `Testify` can be of great help. The @Testify.AnonymousData class can create objects,
data and even mock objects for you, removing the coupling to any constructors in your production code. For more information
on using this class, see @UsingAnonymousData.

## Ensure Test's Intent is Clearly Understandable

Like naming, it's always important that your code is understandable: after all, you read code far more often than you write
it. Also like naming, this is doubly important for unit tests. When a unit test fails after modifying some production code
it's very important that a developer is able to easily understand the test code in order to fix either the test or the
production code quickly. If it's not easy to understand and fix the code quickly then, again, there's a tendency to just do
anything to "fix" the test, including disabling it, rather than making the appropriate code change.

Following AAA or Four Phase testing helps here. Keeping the Arrange phase short is obviously the most important thing here.
This is another area where the @Testify.AnonymousData class can be of help. It's ability to create objects and data can often
reduce several lines of arrange code to a single method call. Further, when using @Testify.AnonymousData you're clearly
declaring what parts of the arrangement are actually relevant to the test and what parts are merely necessary to be able to
Act. This can lead to much clearer test code, with the intent clearly visible. For more information on using the class, see
@UsingAnonymousData.

## Don't "Hide" Important Test Information

Keeping your production code DRY (Don't Repeat Yourself) is very important. Many people will tell you that test code is not
any different, but this is not entirely true. While keeping your test code DRY is often useful and important, you can take
that too far and "hide" important information about the test in other methods or abstractions. This leads to the intent of
the test becoming less understandable. If there's any information to be found in a bit of code that is important to
understanding the test, no matter how many times you repeat this bit of code in other tests you should **not** follow DRY
principals and factor it out into a method or other reusable abstraction. By all means, factor out non-relevant and repeated
code, just don't factor out the relevant code.

> [!IMPORTANT]
> @Testify.AnonymousData is a very useful abstraction for keeping your tests DRY. Just be sure you don't use it in ways that
> hide relevant information about what's being tested.

## Use Only a Single "Logical" Assertion

Many experts and books recommend that a unit test have only one assertion. There are a couple of reasons for this.

1. Code after an assertion fails is not run, since the assertion throws an exception. This can lead to missing or misinformation.
1. It can make the tests more complicated. Multiple assertions usually mean you're actually performing multiple tests, which
   makes the test more complicated and potentially harder to understand.

However, Roy Osherove has this to say.

> My guideline is usually that you test one logical *CONCEPT* per test. You can have multiple asserts on the same \*object\*.
> They will usually be the same concept being tested.
> 
> - Roy Osherove

Testify considers this idea to be a "logical" assertion.

Let's think about this at a higher level. Let's consider unit testing the `Push` method on a `Stack` class. We want to have a
unit test `Push_Value_AddsValueToStack`. This means there's one "logical" assertion of "value was added." To actually assert this,
though, we'd need to assert several observable states: `stack.Count` is incremented by one and `stack.Contains(value)` is `true`.
Testify includes support for "compound assertions" for these scenarios. For more information see @Assertions.

## Ensure the Assertion Message is Clear

It's best practice to not rely on the default messages provided by your assertion framework. When a test fails it's not very useful
to read a message along the lines of "Expected: '1'. Actual: '2'." This tells you absolutely nothing about why the test fails. Most
frameworks, Testify included, allow you to add a more meaningful message to assertion failures, such as "Count was not incremented
after adding a new value. Expected: '1'. Actual: '2'." It's best practice to always provide such messages with your assertions.

## Ensure Tests are Quick

This can't be emphasized enough. Test suites tend to grow very large, and even moderately slow tests can cause the suite itself to
take far too long to run. Remember, your unit tests are work best when run after every compilation, so you want the entire suite
to run in seconds. If this takes too long developers will run the tests less frequently, if at all.

## Avoid Anti-Patterns

The last bit of advice that can be given is to avoid well known anti-patterns. For more information see @AntiPatterns.

## Use Attributes that Make a Unit Test Good

There are several attributes that make a unit test good that you can read about in @GoodUnitTests.