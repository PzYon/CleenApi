# CleenApi

Provides a generic, unified approach for building a REST API using ASP.NET WebApi and Entity Framework.
- The `Library`-project defines the main the interfaces and base classes that can be using when building a Cleen API
- The `WebApi`- provides a simple implementation example

## Philosophy
- Unified approach for users of the API by providing the same operators on all entities (top, take, order by, filter, etc.)
- Unified apporach for developers by providing interfaces and base classes that handle redundancies
- Even though CleenApi provides a standardized approach, developers still have the possibility to take another path when handling specific resources
- Treat entities as resources and hence force developers to think from a REST-persective
- Controller logic is minimal and basically only handles the routes to the resources
- Provide typed interfaces for adding/updating entities

## Next Steps
- HATEOAS
- Further improvements in queries (e.g. number comparison, groups [and/or], etc.)
- ...
