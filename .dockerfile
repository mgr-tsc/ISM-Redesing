# Use the official .NET SDK image as a build environment.
# This generates a build artifact that we can then copy into a runtime image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory to /app
WORKDIR /app

# Copy the csproj file and restore any dependencies (via NuGet)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as the base image for the runtime environment.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory to /app
WORKDIR /app

# Copy the build artifact from the build environment into the runtime environment
COPY --from=build-env /app/out .

# Expose port 80 for the application to listen on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "ISM_Redesing.dll"]
