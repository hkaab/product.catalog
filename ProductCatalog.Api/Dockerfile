#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductCatalog.Api/ProductCatalog.Api.csproj", "ProductCatalog.Api/"]
RUN dotnet restore "ProductCatalog.Api/ProductCatalog.Api.csproj"
COPY . .
WORKDIR "/src/ProductCatalog.Api"
RUN dotnet build "ProductCatalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCatalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.Api.dll"]