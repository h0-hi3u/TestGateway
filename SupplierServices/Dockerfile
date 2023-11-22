#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SupplierServices/SupplierServices.csproj", "SupplierServices/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "SupplierServices/SupplierServices.csproj"
COPY . .
WORKDIR "/src/SupplierServices"
RUN dotnet build "SupplierServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupplierServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupplierServices.dll"]