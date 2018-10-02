FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MSP_Demo.Api/MSP_Demo.Api.csproj MSP_Demo.Api/
RUN dotnet restore MSP_Demo.Api/MSP_Demo.Api.csproj
COPY . .
WORKDIR /src/MSP_Demo.Api
RUN dotnet build MSP_Demo.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MSP_Demo.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MSP_Demo.Api.dll"]
