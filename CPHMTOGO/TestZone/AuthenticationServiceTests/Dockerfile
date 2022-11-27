#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestZone/AuthenticationServiceTests/AuthenticationServiceTests.csproj", "TestZone/AuthenticationServiceTests/"]
COPY ["Authentication Service/AuthenticationService.csproj", "Authentication Service/"]
RUN dotnet restore "TestZone/AuthenticationServiceTests/AuthenticationServiceTests.csproj"
COPY . .
WORKDIR "/src/TestZone/AuthenticationServiceTests"
RUN dotnet build "AuthenticationServiceTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationServiceTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationServiceTests.dll"]