# Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY ["FoodDelivery.ApiGateway.csproj", "./"]
RUN dotnet restore "FoodDelivery.ApiGateway.csproj"

# Copy the rest and build
COPY . .
WORKDIR "/src/"
RUN dotnet build "FoodDelivery.ApiGateway.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "FoodDelivery.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodDelivery.ApiGateway.dll"]