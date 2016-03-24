---
uid: AntiPatterns
---

Anti-Patterns
======================

* **The Liar**
	* An entire unit test that passes all of the test cases it has and appears valid, but upon closer inspection it is discovered that it doesn’t really test the intended target at all.
* **Excessive Setup**
	* A test that requires a lot of work setting up in order to even begin testing. Sometimes several hundred lines of code is used to setup the environment for one test, with several objects involved, which can make it difficult to really ascertain what is tested due to the “noise” of all of the setup going on.
* **The Giant**
	* A unit test that, although it is validly testing the object under test, can span thousands of lines and contain many test cases. This can be an indicator that the system under test is a God Object.
* **The Mockery**
	* A unit test contains so many mocks, stubs and/or fakes that the system under test isn’t even being tested at all, instead data returned from mocks is what is being tested.
* **The Inspector**
	* A unit test that violates encapsulation in an effort to achieve 100% code coverage, but knows so much about what is going on in the object that any attempt to refactor will break the existing test and require any change to be reflected in the unit test.
* **Wet Floor**
	* A test that fails to clean up after itself, leaving data persisted somewhere that causes another test, or the same test on subsequent runs, to fail.
* **Generous Leftovers**
	* An instance where one unit test creates data that is persisted somewhere, and another test reuses the data for its own devious purposes. If the “generator” is run afterword, or not at all, the test using the data will outright fail.
* **The Local Hero**
	* A test case that is dependent on something specific to the development environment it was written on in order to run. The result is the test passes on development boxes, but fails when someone attempts to run it elsewhere.
* **The Nitpicker**
	* A unit test which compares a complete output when it’s really only interested in a small part of it, so the test has to continually be kept in line with otherwise unimportant details.
* **The Secret Catcher**
	* A test that at first glance appears to be doing no testing due to the absence of assertions, but as they say, “the devil is in the details.” The test is really relying on an exception to be thrown when a mishap occurs, and is expecting the testing framework to capture the exception and report it to the user as a failure.
* **The Silent Catcher**
	* A test that passes if an exception is thrown, even if the exception that actually occurs is one that is different from the one the developer intended.
* **The Dodger**
	* A unit test which has lots of tests for minor (and presumably easy to test) side effects, but never tests the core desired behavior. Sometimes you may find this in database access related tests, where a method is called, then the test selects from the database and runs assertions against the result.
* **The Loudmouth**
	* A unit test (or test suite) that clutters up the console with diagnostic messages, logging messages, and other miscellaneous chatter, even when tests are passing. Sometimes during test creation there was a desire to manually see output, but even though it’s no longer needed, it was left behind.
* **The Greedy Catcher**
	* A unit test which catches exceptions and swallows the stack trace, sometimes replacing it with a less informative failure message, but sometimes even just logging (c.f. Loudmouth) and letting the test pass.
* **The Sequencer**
	* A unit test that depends on items in an unordered list appearing in the same order during assertions.
* **Hidden Dependency**
	* A close cousin of the Local Hero, a unit test that requires existing data to have been populated somewhere before the test runs. If that data wasn’t populated, the test will fail and leave little indication to the developer what it wanted, or why… forcing them to dig through acres of code to find out where the data it was using was supposed to come from.
* **The Enumerator**
	* A unit test where each test case method name is only an enumeration, i.e. test1, test2, test3. As a result, the intention of the test case is unclear, and the only way to be sure is to read the test case code and pray for clarity.
* **The Stranger/Distant relative**
	* A test case that doesn’t even belong in the unit test it is part of. It’s really testing a separate object, most likely an object that is used by the object under test, but the test case has gone and tested that object directly without relying on the output from the object under test making use of that object for its own behavior.
* **The Operating System Evangelist**
	* A unit test that relies on a specific operating system environment to be in place in order to work. A good example would be a test case that uses the newline sequence for Windows in an assertion, only to break when run on Linux.
* **Success Against All Odds**
	* A test that was written to pass first rather than fail first. As an unfortunate side effect, the test case happens to always pass even though the test should fail.
* **The Free Ride/Piggyback**
	* Rather than write a new test case method to test another feature or functionality, a new assertion rides along in an existing test case.
* **The One/Silver Bullet**
	* A combination of several patterns, particularly The Free Ride and The Giant, a unit test that contains only one test method which tests the entire set of functionality an object has. A common indicator is that the test method name is often the same as the unit test name, and contains multiple lines of setup and assertions.
* **The Peeping Tom/The Uninvited Guest**
	* A test that, due to the shared resources, can see the result data of another test, and may cause the test to fail even though the system under test is perfectly valid. This has been seen commonly in Fitnesse, where the use of static member variables to hold collections aren’t properly cleaned after test execution, often popping up unexpectedly in other test runs.
* **The Slow Poke**
	* A unit test that runs incredibly slow. When developer kick it off, they have time to go to the bathroom, grab a smoke, or worse, go home for the day.
* **Second Class Citizens**
	* Test code isn’t as well refactored/structured as production code, containing a lot of duplicated code, making it hard to maintain tests.
* **Happy Path**
	* The test “stays on the happy path” (i.e. expected results) without testing for boundaries and exceptions.
* **Chain Gang**
	* Two or more tests that must run in a certain order, i.e. one test changes the global state of the system and the next test(s) depend on it.
* **The Test With No Name**
	* The test that gets added to reproduce a specific bug in the bug tracker and whose author thinks does not warrant a name of its own. Instead of enhancing an existing, lacking test, a new test is created called TestForBug123.
* **Wait and See**
	* A test that runs some setup code and the needs to “wait” a specific amount of time before it can “see” if the code under test functioned as expected. A test method that uses Sleep() or equivalent is most certainly a “Wait and See” test.
* **The Sleeper/Mount Vesuvius**
	* A test that is destined to fail at some specific time and date in the future. This is often caused by incorrect bounds checking when testing code which uses a Date or Calendar object. Sometimes, the test may fail if run at a very specific time of day, such as midnight.
* **The Flickering Test**
	* A test which just occasionally fails, and is generally due to race conditions within the test. Typically occurs when testing something that is asynchronous. Possibly a superset of the Wait and See and The Sleeper anti-patterns.
* **The Cuckoo/Inappropriately Shared Fixture/The Mother Hen**
	* Several test cases in the test fixture do not even use or need the setup/teardown shared code. Partly due to developer inertia to create a new test fixture… easier to just add one or more test cases to the pile.
* **I’ll believe it when I see some flashing GUIs**
	* An unhealthy fixation/obsession with testing the app via its GUI, “just like a real user.”
* **The Forty Foot Pole Test**
	* Afraid of getting too close to the class they are trying to test, these tests act at a distance, separated by countless layers of abstraction and thousands of lines of code from the logic they are checking. As such they are extremely brittle, and susceptible to all sorts of side-effects that happen on the epic journey to and from the class interest.
* **Doppelgänger**
	* In order to test something, you have to copy parts of the code under test into a new class with the same name and package and you have to use classpath magic or custom classloader to make sure it is visible first (so your copy is picked up). This pattern indicates an unhealthy amount of hidden dependencies which you can’t control from a test.
* **The Ugly Mirror**
	* Test code that looks exactly like the code under test. Example, a SUT that formats a string tested by code that formats the expected string result in the exact same manner.
