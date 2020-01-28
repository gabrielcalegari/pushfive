#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Catalog/PushFive.Catalog.WebApi/PushFive.Catalog.WebApi.csproj", "Catalog/PushFive.Catalog.WebApi/"]
COPY ["src/Services/Catalog/PushFive.Catalog.Data/PushFive.Catalog.Data.csproj", "Catalog/PushFive.Catalog.Data/"]
COPY ["src/Services/Catalog/PushFive.Catalog.Domain/PushFive.Catalog.Domain.csproj", "Catalog/PushFive.Catalog.Domain/"]
COPY ["src/Services/Core/PushFive.Core/PushFive.Core.csproj", "Core/PushFive.Core/"]
RUN dotnet restore "Catalog/PushFive.Catalog.WebApi/PushFive.Catalog.WebApi.csproj"
COPY src/Services/. .
WORKDIR "/src/Catalog/PushFive.Catalog.WebApi"
RUN dotnet build "PushFive.Catalog.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PushFive.Catalog.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PushFive.Catalog.WebApi.dll"]