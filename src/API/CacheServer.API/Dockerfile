FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API/CacheServer.API/CacheServer.API.csproj", "API/CacheServer.API/"]
RUN dotnet restore "API/CacheServer.API/CacheServer.API.csproj"
COPY . .
WORKDIR "/src/API/CacheServer.API"
RUN dotnet build "CacheServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CacheServer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CacheServer.API.dll"]