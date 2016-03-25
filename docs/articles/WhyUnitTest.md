---
uid: WhyUnitTest
---

* ~~Find Bugs~~
* ~~Prove Code Correct~~
* Allow refactoring with confidence
* API design (TDD)
* Executable API documentation for developers

## ~~Find Bugs~~

It's a common misconception that the reason to write unit tests is to find bugs. While it's true you can find bugs while writing unit tests this really isn't that common. It's said that unit tests don't find bugs, they find regressions.

## ~~Prove Code Correct~~

It's actually a pretty simple thing to write code you know isn't correct and then intentionally create a lot of unit tests that pass despite the incorrectness. If you can do this intentionally, it should be obvious that it's also possible to do this unintentionally. This means unit tests can't really prove that code is correct.

## Allow refactoring with confidence

If you have a thorough suite of unit tests you can have confidence when you refactor the production code you won't cause any regressions in the (tested) behavior. In fact, you really shouldn't refactor code unless it's covered by unit tests.

## API Design (TDD)

Often it can be difficult to fully understand how an API functions until you've used it. Unit tests can help you to flesh out an API because you have to actually use it in the tests. This is why the third D in TDD stands for design.

## Executable API documentation for developers

Reading the unit tests is a great way to see how to use an unfamiliar API. Unlike other forms of documentation, unit tests are guaranteed to be compilable and you can even execute and debug the tests to learn more.