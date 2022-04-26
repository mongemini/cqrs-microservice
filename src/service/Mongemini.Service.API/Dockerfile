#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/service/Mongemini.Service.API/Mongemini.Service.API.csproj", "src/service/Mongemini.Service.API/"]
COPY ["src/service/Mongemini.Service.Infrastructure/Mongemini.Service.Infrastructure.csproj", "src/service/Mongemini.Service.Infrastructure/"]
COPY ["src/libs/Mongemini.Persistence.Implementations/Mongemini.Persistence.Implementations.csproj", "src/libs/Mongemini.Persistence.Implementations/"]
COPY ["src/libs/Mongemini.Persistence.Contracts/Mongemini.Persistence.Contracts.csproj", "src/libs/Mongemini.Persistence.Contracts/"]
COPY ["src/service/Mongemini.Service.Domain/Mongemini.Service.Domain.csproj", "src/service/Mongemini.Service.Domain/"]
COPY ["src/libs/Mongemini.Domain/Mongemini.Domain.csproj", "src/libs/Mongemini.Domain/"]
COPY ["src/service/Mongemini.Service.Application/Mongemini.Service.Application.csproj", "src/service/Mongemini.Service.Application/"]
COPY ["src/libs/Mongemini.Application.Core/Mongemini.Application.Core.csproj", "src/libs/Mongemini.Application.Core/"]
RUN dotnet restore "src/service/Mongemini.Service.API/Mongemini.Service.API.csproj"
COPY . .
WORKDIR "/src/src/service/Mongemini.Service.API"
RUN dotnet build "Mongemini.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mongemini.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mongemini.Service.API.dll"]
