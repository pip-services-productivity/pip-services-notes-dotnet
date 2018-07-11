# Build stage
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY Source/Interface/Interface.csproj ./Source/Interface/
RUN dotnet restore Source/Interface/Interface.csproj
COPY Source/Service/Service.csproj ./Source/Service/
RUN dotnet restore Source/Service/Service.csproj
COPY Source/Client/Client.csproj ./Source/Client/
RUN dotnet restore Source/Client/Client.csproj
COPY Source/Process/Process.csproj ./Source/Process/
RUN dotnet restore Source/Process/Process.csproj

# Copy src
COPY . .

# Publish
RUN dotnet publish Source/Process/Process.csproj -o /obj

