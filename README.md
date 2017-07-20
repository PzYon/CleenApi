# CleenApi

Provides a generic, unified approach for building a REST API using ASP.NET WebApi and Entity Framework.
- The `Library`-project defines the common interfaces and base classes that can be used when building a Cleen API
- The `Library.Tests`-project contains unit tests for the `Library`-project
- The `ExampleApi`-project provides a simple implementation example

## Philosophy
- Unified approach for users of the API by providing the same operators on all entities (top, take, order by, filter, etc.)
- Unified approach for developers by providing interfaces and base classes that handle redundancies
- Even though CleenApi provides a standardized approach, developers still have the possibility to take another path when handling specific resources
- Treat entities as resources and hence force developers to think from a REST-persective
- Controller logic is minimal and basically only handles the routes to the resources
- Provide typed interfaces for adding/updating entities
