FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ContosoService.csproj", "."]
RUN dotnet restore
COPY [".", "."]
RUN dotnet build "ContosoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContosoService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContosoService.dll"]