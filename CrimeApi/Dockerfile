#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CrimeApi/CrimeApi.csproj", "CrimeApi/"]
COPY ["RepCrimeCommon/RepCrimeCommon.csproj", "RepCrimeCommon/"]
RUN dotnet restore "CrimeApi/CrimeApi.csproj"
COPY . .
WORKDIR "/src/CrimeApi"
RUN dotnet build "CrimeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrimeApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrimeApi.dll"]
