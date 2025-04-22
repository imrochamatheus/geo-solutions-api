FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY GeoSolucoesAPI/*.csproj ./GeoSolucoesAPI/
RUN dotnet restore ./GeoSolucoesAPI/GeoSolucoesAPI.csproj

COPY . .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet build ./GeoSolucoesAPI/GeoSolucoesAPI.csproj -c Release
RUN dotnet publish ./GeoSolucoesAPI/GeoSolucoesAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Docker

ENTRYPOINT ["dotnet", "GeoSolucoesAPI.dll"]
