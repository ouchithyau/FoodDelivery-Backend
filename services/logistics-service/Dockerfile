# Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
COPY ["Logistics.API/Logistics.API.csproj", "Logistics.API/"]
COPY ["Logistics.Application/Logistics.Application.csproj", "Logistics.Application/"]
COPY ["Logistics.Domain/Logistics.Domain.csproj", "Logistics.Domain/"]
COPY ["Logistics.Infrastructure/Logistics.Infrastructure.csproj", "Logistics.Infrastructure/"]
RUN dotnet restore "Logistics.API/Logistics.API.csproj"

# Copy the rest of the code and build
COPY . .
WORKDIR "/src/Logistics.API"
RUN dotnet build "Logistics.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Logistics.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Image Setup
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logistics.API.dll"]