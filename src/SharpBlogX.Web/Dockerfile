FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/SharpBlogX.Web/SharpBlogX.Web.csproj", "src/SharpBlogX.Web/"]
COPY ["src/SharpBlogX.Response/SharpBlogX.Response.csproj", "src/SharpBlogX.Response/"]
RUN dotnet restore "src/SharpBlogX.Web/SharpBlogX.Web.csproj"
COPY . .
WORKDIR "/src/src/SharpBlogX.Web"
RUN dotnet build "SharpBlogX.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpBlogX.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharpBlogX.Web.dll"]