#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Voting/PushFive.Voting.WebApi/PushFive.Voting.WebApi.csproj", "Voting/PushFive.Voting.WebApi/"]
COPY ["src/Services/Voting/PushFive.Voting.Data/PushFive.Voting.Data.csproj", "Voting/PushFive.Voting.Data/"]
COPY ["src/Services/Voting/PushFive.Voting.Domain/PushFive.Voting.Domain.csproj", "Voting/PushFive.Voting.Domain/"]
COPY ["src/Services/Core/PushFive.Core/PushFive.Core.csproj", "Core/PushFive.Core/"]
RUN dotnet restore "Voting/PushFive.Voting.WebApi/PushFive.Voting.WebApi.csproj"
COPY src/Services/. .
WORKDIR "/src/Voting/PushFive.Voting.WebApi"
RUN dotnet build "PushFive.Voting.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PushFive.Voting.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PushFive.Voting.WebApi.dll"]