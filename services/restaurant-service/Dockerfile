# Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
COPY ["Restaurant.API/Restaurant.API.csproj", "Restaurant.API/"]
COPY ["Restaurant.Application/Restaurant.Application.csproj", "Restaurant.Application/"]
COPY ["Restaurant.Domain/Restaurant.Domain.csproj", "Restaurant.Domain/"]
COPY ["Restaurant.Infrastructure/Restaurant.Infrastructure.csproj", "Restaurant.Infrastructure/"]
RUN dotnet restore "Restaurant.API/Restaurant.API.csproj"

# Copy the rest of the code and build
COPY . .
WORKDIR "/src/Restaurant.API"
RUN dotnet build "Restaurant.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Restaurant.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Image Setup
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.API.dll"]