#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CDBCalculator.Api/CDBCalculator.Api.csproj", "src/CDBCalculator.Api/"]
COPY ["src/CDBCalculator.Application/CDBCalculator.Application.csproj", "src/CDBCalculator.Application/"]
COPY ["src/CDBCalculator.Domain/CDBCalculator.Domain.csproj", "src/CDBCalculator.Domain/"]
COPY ["src/CDBCalculator.Infrastructure/CDBCalculator.Infrastructure.csproj", "src/CDBCalculator.Infrastructure/"]
RUN dotnet restore "./src/CDBCalculator.Api/CDBCalculator.Api.csproj"
COPY . .
WORKDIR "/src/src/CDBCalculator.Api"
RUN dotnet build "./CDBCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CDBCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDBCalculator.Api.dll"]