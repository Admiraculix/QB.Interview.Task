#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["QB.API/QB.API.csproj", "QB.API/"]
COPY ["QB.External.Rest.Service/QB.External.Rest.Service.csproj", "QB.External.Rest.Service/"]
COPY ["QB.Application/QB.Application.csproj", "QB.Application/"]
COPY ["QB.Domain/QB.Domain.csproj", "QB.Domain/"]
COPY ["Persistance.Abstractions/Persistance.Abstractions.csproj", "Persistance.Abstractions/"]
COPY ["EqualityComparer.Abstractions/EqualityComparer.Abstractions.csproj", "EqualityComparer.Abstractions/"]
COPY ["QB.Persistence/QB.Persistence.csproj", "QB.Persistence/"]
COPY ["QB.Persistence.Sqlite/QB.Persistence.Sqlite.csproj", "QB.Persistence.Sqlite/"]
RUN dotnet restore "QB.API/QB.API.csproj"
COPY . .
WORKDIR "/src/QB.API"
RUN dotnet build "QB.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QB.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QB.API.dll"]