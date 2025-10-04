FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FIAP.CloudGames.sln ./
COPY src/FIAP.CloudGames.API/FIAP.CloudGames.API.csproj src/FIAP.CloudGames.API/
COPY src/FIAP.CloudGames.Application/FIAP.CloudGames.Application.csproj src/FIAP.CloudGames.Application/
COPY src/FIAP.CloudGames.Domain/FIAP.CloudGames.Domain.csproj src/FIAP.CloudGames.Domain/
COPY src/FIAP.CloudGames.Infrastructure/FIAP.CloudGames.Infrastructure.csproj src/FIAP.CloudGames.Infrastructure/
COPY . .

RUN dotnet restore src/FIAP.CloudGames.API/FIAP.CloudGames.API.csproj

RUN dotnet publish src/FIAP.CloudGames.API/FIAP.CloudGames.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FIAP.CloudGames.API.dll"]
