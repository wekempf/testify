---
uid: GoodUnitTests
---

# Good Unit Tests

A basic unit test:

* is automated
* is repeatable
* does not depend on the environment
* does not depend on other unit tests
* does not depend on external data
* does not have side effects

A good unit test:

* asserts the results of your code
* tests a single unit of work
* covers all the paths of the code you want to test
* cleans up after itself

A better unit test:

* tests and asserts edge cases and different ranges of data
* runs fast
* is well-factored
* reveals intention
* is short

## A Basic Unit Test

These traits should exist in all unit tests. Any unit test that is missing any of these traits can be considered a bad unit test and should be fixed.

### Is Automated

You get this one for free when using a unit test framework, obviously. However, it still should be said that it's necessary to automate unit tests. Doing "manual developer testing" does not count as unit testing, and when you find yourself doing this you should consider writing an automated unit test instead.

### Is Repeatable

You need to be able to run a unit test over and over again, obtaining the same results every time. A unit test that produces random results each time it is run is not a useful unit test.

### Does not Depend on the Environment

This means it shouldn't rely on environment variables, use specific hardware, rely on the file system or external services, require specific accounts or any other environmental aspect that would prevent the unit test from being run by anyone on any machine.

### Does not Depend on Other Unit Tests

Each unit test needs to be executable in isolation. This allows individual tests to be run instead of requiring the entire suite to be run, as well as ensures changes to the suite don't cause false failures.

### Does not Depend on External Data

It's important that unit tests be entirely self contained. This trait doesn't mean you can't have data driven unit tests, rather it means the data needs to be a part of the test, and not obtained from some external source.

### Does not Have Side Effects

 A unit test with side effects is not likely to be repeatable and may well interfere with other tests.

## A Good Unit Test

These traits elevate a test from basic status to being considered good unit tests.

### Asserts the Results of Your Code

In other words, they test the observable behavior of your code, rather than test the code's implementation. A good unit test simply tests that for a given input you obtain a given output. Note, however, that by input and output we are not limiting ourselves to method parameters and return values. If it's vital that some method creates a specific log message, that is a an output that can (and should) be tested.

### Tests a Single Unit of Work

A good unit test should not test multiple things at the same time. This means more than just testing for a single result, but rather means what's being tested should be tested in isolation. For instance, if testing the behavior of adding an item to a shopping cart object, the test should isolate that object from any persistence object that may be used to store the item on disk. These are unit tests, not integration tests.

### Covers all the Paths of Code You Want to Test

In other words, you have high code coverage. However, it should be noted that the arbitrary goal of having 100% code coverage in order to ensure quality is a fallacy. You can have 100% coverage and still have numerous bugs, while conversely you can have 50% coverage and have reasonable coverage with no bugs. You should know when your tests are sufficient without the need for arbitrary coverage water marks.

### Cleans Up After Itself

If the test makes any changes that could be observable from other tests, or from another run of this test, then those changes should be rolled back before the test finishes.

## A Better Unit Test

Really good unit tests go even further and exhibit the following traits.

### Tests and Asserts Edge Cases and Different Ranges of Data

Most errors occur at the boundaries. Good unit tests will test for usage with null values for reference types and minimum and maximum values for comparable types.

### Runs Fast

A general rule of thumb for fast is less than 100 ms, but the faster the better. Tests are not generally run in isolation, but rather run as a part of a test suite. A test that takes a second might not seem like a big deal, but since most test suites will grow to be in the thousands it doesn't take much for these slower tests to result in test runs that take far too long to be useful, and are likely to lead developers to remove the tests are stopped testing entirely.

### Is Well-Factored

This means the test is highly readable and doesn't hide information. Note, however, that test code is different from production code and some factorings that would be appropriate in production code are not appropriate in test code. Everything relevant to the test needs to be clearly visible within the test method and not factored out to other methods or abstractions. Non-relevant but necessary code can (and should) be factored out, however.

### Reveals Intention

Good unit tests make it obvious what the test is doing and why it exists. This is important for the developers who have to fix a failing test after a regression is introduced during a refactoring of the production code. Further, unit tests should also be a form of documentation, and thus need to be revealing.

### Is Short

A unit test should be less than a dozen lines of code, and the fewer lines of code the better. Longer tests are likely less revealing and are much harder to maintain.