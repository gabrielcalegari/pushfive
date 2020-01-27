# PushFive

PushFive is an example web application with voting system. It shows a catalog of songs, and a user has to choose 5 favorite songs.

## Architecture

PushFive is composed of 1 website and 2 microsservices:

- Website: presentation layer
- PushFive.Catalog.WebApi: catalog service
- PushFive.Voting.WebApi: voting service

## Domain modeling

![Domain modeling of each one microsservice](https://imagizer.imageshack.com/img921/662/ylagTi.png)

## References
I used some references to build this web app:

- https://github.com/dotnet-architecture/eShopOnContainers
- https://www.eduardopires.net.br/
- https://docs.docker.com/compose/
