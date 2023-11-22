#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FS.WebAPI/FS.WebAPI.csproj", "FS.WebAPI/"]
COPY ["FS.BLL/FS.BLL.csproj", "FS.BLL/"]
COPY ["FS.DAL/FS.DAL.csproj", "FS.DAL/"]
RUN dotnet restore "./FS.WebAPI/./FS.WebAPI.csproj"
COPY . .
WORKDIR "/src/FS.WebAPI"
RUN dotnet build "./FS.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FS.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FS.WebAPI.dll"]