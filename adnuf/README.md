# Adnuf

Web API that provides processed property info from Funda.

![Demo](assets/demo.gif)

## Prerequisites

* Ensure [`dotnet`](https://github.com/dotnet/cli) 3.1+ is installed and available on PATH.

## Getting started

Start the web service:

```sh
dotnet run -p src/Adnuf.WebAPI token=<funda_token>
```

List top 10 agents with most properties on sale. Note that initial request can take a long time
because the service needs to traverse through all properties but is being rate limited:

```sh
curl localhost:5000/agent/top_by_properties?city=amsterdam&limit=10
```

List top 10 agents with most properties with a garden on sale:

```sh
curl localhost:5000/agent/top_by_properties?city=amsterdam&limit=10&extras=garden

```

Run tests:

```sh
dotnet test
```
