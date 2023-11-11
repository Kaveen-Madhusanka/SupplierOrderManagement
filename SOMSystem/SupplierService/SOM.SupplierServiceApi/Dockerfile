#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SupplierService/SOM.SupplierServiceApi/SOM.SupplierServiceApi.csproj", "SupplierService/SOM.SupplierServiceApi/"]
COPY ["Shared/SOM.Shared/SOM.Shared.csproj", "Shared/SOM.Shared/"]
COPY ["SupplierService/SOM.SupplierService.Infrastructure/SOM.SupplierService.Infrastructure.csproj", "SupplierService/SOM.SupplierService.Infrastructure/"]
COPY ["SupplierService/SOM.SupplierService.Application/SOM.SupplierService.Application.csproj", "SupplierService/SOM.SupplierService.Application/"]
COPY ["SupplierService/SOM.SupplierService.Domain/SOM.SupplierService.Domain.csproj", "SupplierService/SOM.SupplierService.Domain/"]
RUN dotnet restore "SupplierService/SOM.SupplierServiceApi/SOM.SupplierServiceApi.csproj"
COPY . .
WORKDIR "/src/SupplierService/SOM.SupplierServiceApi"
RUN dotnet build "SOM.SupplierServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOM.SupplierServiceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOM.SupplierServiceApi.dll"]