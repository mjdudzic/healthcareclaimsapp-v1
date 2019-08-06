FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ClaimsSubmission.Api/ClaimsSubmission.Api.csproj", "ClaimsSubmission.Api/"]
RUN dotnet restore "ClaimsSubmission.Api/ClaimsSubmission.Api.csproj"
COPY . .
WORKDIR "/src/ClaimsSubmission.Api"
RUN dotnet build "ClaimsSubmission.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClaimsSubmission.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClaimsSubmission.Api.dll"]