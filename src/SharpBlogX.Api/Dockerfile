FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/SharpBlogX.Api/SharpBlogX.Api.csproj", "src/SharpBlogX.Api/"]
COPY ["src/SharpBlogX.BackgroundWorkers/SharpBlogX.BackgroundWorkers.csproj", "src/SharpBlogX.BackgroundWorkers/"]
COPY ["src/SharpBlogX.Core/SharpBlogX.Core.csproj", "src/SharpBlogX.Core/"]
COPY ["src/SharpBlogX.Response/SharpBlogX.Response.csproj", "src/SharpBlogX.Response/"]
COPY ["src/SharpBlogX.Application/SharpBlogX.Application.csproj", "src/SharpBlogX.Application/"]
COPY ["src/SharpBlogX.MongoDb/SharpBlogX.MongoDb.csproj", "src/SharpBlogX.MongoDb/"]
RUN dotnet restore "src/SharpBlogX.Api/SharpBlogX.Api.csproj"
COPY . .
WORKDIR "/src/src/SharpBlogX.Api"
RUN dotnet build "SharpBlogX.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpBlogX.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharpBlogX.Api.dll"]