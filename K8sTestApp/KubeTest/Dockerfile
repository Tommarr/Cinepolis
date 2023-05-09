#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:443;http://+:80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KubeTest/KubeTest.csproj", "KubeTest/"]
RUN dotnet restore "KubeTest/KubeTest.csproj"
COPY . .
WORKDIR "/src/KubeTest"
RUN dotnet build "KubeTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KubeTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KubeTest.dll"]