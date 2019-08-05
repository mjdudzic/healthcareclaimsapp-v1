FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["HealthcareClaims/HealthcareClaims.csproj", "HealthcareClaims/"]
RUN dotnet restore "HealtcareClaims/HealthcareClaims.csproj"
COPY . .
WORKDIR "/src/HealthcareClaims"
RUN dotnet build "HealthcareClaims.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HealthcareClaims.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HealthcareClaims.dll"]
