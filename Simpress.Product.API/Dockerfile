#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Simpress.Product.API/Simpress.Product.API.csproj", "Simpress.Product.API/"]
RUN dotnet restore "Simpress.Product.API/Simpress.Product.API.csproj"
COPY . .
WORKDIR "/src/Simpress.Product.API"
RUN dotnet build "Simpress.Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simpress.Product.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simpress.Product.API.dll"]