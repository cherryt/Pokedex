# Pokedex

## Prerequisites

[.NET 5](https://dotnet.microsoft.com/download) needs to be installed to run this project

## Running tests

```
dotnet test
```

## Running the project

```
dotnet build
dotnet run --project Pokedex
```

The project will be running on port 5001:

https://localhost:5001


Example endpoints:

- https://localhost:5001/pokemon/bulbasaur
- https://localhost:5001/pokemon/translated/zubat


#### TODO:
- Move API urls to app settings
- End to end/integration test
- Verify fields in json response from external APIs
