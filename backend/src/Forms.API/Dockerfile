FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Forms.API/Forms.API.csproj", "src/Forms.API/"]
COPY ["src/Forms.Application/Forms.Application.csproj", "src/Forms.Application/"]
COPY ["src/Forms.Domain/Forms.Domain.csproj", "src/Forms.Domain/"]
COPY ["src/Forms.Infrastructure/Forms.Infrastructure.csproj", "src/Forms.Infrastructure/"]
RUN dotnet restore "src/Forms.API/Forms.API.csproj"
COPY . .
WORKDIR "/src/src/Forms.API"
RUN dotnet build "Forms.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Forms.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forms.API.dll"]
