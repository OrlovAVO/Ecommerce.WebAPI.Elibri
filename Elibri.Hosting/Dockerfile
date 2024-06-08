# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the main solution file and all project files
COPY *.sln .
COPY Elibri.Hosting/Elibri.Hosting.csproj Elibri.Hosting/
COPY Elibri.Api/Elibri.Api.csproj Elibri.Api/
COPY Elibri.Authorization/Elibri.Authorization.csproj Elibri.Authorization/
COPY Elibri.EF/Elibri.EF.csproj Elibri.EF/
COPY Elibri.Core/Elibri.Core.csproj Elibri.Core/

# Restore dependencies
RUN dotnet restore

# Install Entity Framework Core tools locally
RUN dotnet tool install --global dotnet-ef

# Copy remaining files and build the project
COPY . .
WORKDIR /src/Elibri.Hosting
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install PostgreSQL client
RUN apt-get update && apt-get install -y postgresql-client

# Copy the published files and the startup script
COPY --from=build /app/publish .
COPY run-container.sh /app/
RUN chmod +x /app/run-container.sh

# Start the application
ENTRYPOINT ["sh", "./run-container.sh"]