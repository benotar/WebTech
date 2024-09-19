FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ARG JWT_SECRET
ARG POSTGRES_DB_USERNAME
ARG POSTGRES_DB_PASSWORD

ENV JWT__SecretKey=$JWT_SECRET
ENV DATABASE__USERNAME=$POSTGRES_DB_USERNAME
ENV DATABASE__PASSWORD=$POSTGRES_DB_PASSWORD
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebTech.Domain/WebTech.Domain.csproj", "WebTech.Domain/WebTech.Domain.csproj"]
COPY ["WebTech.Application/WebTech.Application.csproj", "WebTech.Application/WebTech.Application.csproj"]
COPY ["WebTech.Persistence/WebTech.Persistence.csproj", "WebTech.Persistence/WebTech.Persistence.csproj"]
COPY ["WebTech.WebApi/WebTech.WebApi.csproj", "WebTech.WebApi/WebTech.WebApi.csproj"]
RUN dotnet restore "WebTech.WebApi/WebTech.WebApi.csproj"
COPY . ./
WORKDIR "/src/WebTech.WebApi"
RUN dotnet build "WebTech.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebTech.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "WebTech.WebApi.dll"]