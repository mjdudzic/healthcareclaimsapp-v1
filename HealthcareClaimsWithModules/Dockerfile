FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../HealthcareClaimsWithModules/HealthcareClaimsWithModules.csproj", "../HealthcareClaimsWithModules/"]
RUN dotnet restore "../HealthcareClaimsWithModules/HealthcareClaimsWithModules.csproj"
COPY . .
WORKDIR "/src/../HealthcareClaimsWithModules"
RUN dotnet build "HealthcareClaimsWithModules.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HealthcareClaimsWithModules.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HealthcareClaimsWithModules.dll"]