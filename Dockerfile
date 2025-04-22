FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./GeoSolucoesAPI.csproj ./
RUN dotnet restore ./GeoSolucoesAPI.csproj

COPY . .

RUN dotnet build ./GeoSolucoesAPI.csproj -c Release
RUN dotnet publish ./GeoSolucoesAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GeoSolucoesAPI.dll"]
