# PushFive

PushFive is an example web application with voting system. It shows a catalog of songs, and a user has to choose 5 favorite songs.

## Architecture

PushFive is composed of 2 websites and 2 microsservices:

- PushFive.Voting.Website: presentation layer for voting
- PushFive.Result.Website: presentation layer for voting result
- PushFive.Catalog.WebApi: catalog service
- PushFive.Voting.WebApi: voting service

Each one of the microsservices has own SQL Server database.

![Microsservice architecture](https://imagizer.imageshack.com/img922/8066/Y0VNMn.png)

## Domain modeling

![Domain modeling of each one microsservice](https://imagizer.imageshack.com/img921/662/ylagTi.png)

## How to run?

PushFive is containerized through Docker. Ensure you have Docker installed in your machine and follow the steps:

1. Clone the repository
1. Open the root folder (where is located docker-compose.yml) via console 
1. Type docker-compose up
1. Ready! The Website will be available on http://localhost:8080

## References
I used some references to build this web app:

- https://github.com/dotnet-architecture/eShopOnContainers
- https://www.eduardopires.net.br/
- https://docs.docker.com/compose/
