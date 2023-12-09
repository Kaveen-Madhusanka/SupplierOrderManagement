FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductService/SOM.ProductServiceApi/SOM.ProductServiceApi.csproj", "ProductService/SOM.ProductServiceApi/"]
COPY ["Shared/SOM.Shared/SOM.Shared.csproj", "Shared/SOM.Shared/"]
COPY ["ProductService/SOM.ProductService.Infrastructure/SOM.ProductService.Infrastructure.csproj", "ProductService/SOM.ProductService.Infrastructure/"]
COPY ["ProductService/SOM.ProductService.Application/SOM.ProductService.Application.csproj", "SupplierService/SOM.ProductService.Application/"]
COPY ["ProductService/SOM.ProductService.Domain/SOM.ProductService.Domain.csproj", "ProductService/SOM.ProductService.Domain/"]
RUN dotnet restore "SupplierService/SOM.ProductServiceApi/SOM.ProductServiceApi.csproj"
COPY . .
WORKDIR "/src/ProductService/SOM.ProductServiceApi"
RUN dotnet build "SOM.ProductServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOM.ProductServiceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOM.ProductServiceApi.dll"]