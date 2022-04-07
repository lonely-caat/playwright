# 2. Use xUnit for tests

Date: 2019-06-20

## Status

Accepted

## Context

To develop and maintain a high quality code base we need a comprehensive suite
of tests, preferably written test-first. We require a unit testing tool that
makes use of the language features of C# and .NET Core to keep tests simple and
easy to maintain.

## Decision

We will use xUnit for writing all unit tests.

## Consequences

By selecting xUnit we are armed with a unit testing tool that has many of the
lessons learned in earlier test frameworks (MSTest and nUnit) built into the
framework itself. xUnit makes use of many of the newer language features of the
.NET framework to help us develop tests that are easier to understand.

xUnit has good support from test runners in all environments, making it easier
to use TDD from within a developer's preferred IDE.

