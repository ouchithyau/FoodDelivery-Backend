# Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
COPY ["Support.API/Support.API.csproj", "Support.API/"]
COPY ["Support.Application/Support.Application.csproj", "Support.Application/"]
COPY ["Support.Domain/Support.Domain.csproj", "Support.Domain/"]
COPY ["Support.Infrastructure/Support.Infrastructure.csproj", "Support.Infrastructure/"]
RUN dotnet restore "Support.API/Support.API.csproj"

# Copy the rest of the code and build
COPY . .
WORKDIR "/src/Support.API"
RUN dotnet build "Support.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Support.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Image Setup
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Support.API.dll"]