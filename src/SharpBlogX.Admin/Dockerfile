FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/SharpBlogX.Admin/SharpBlogX.Admin.csproj", "src/SharpBlogX.Admin/"]
COPY ["src/SharpBlogX.Response/SharpBlogX.Response.csproj", "src/SharpBlogX.Response/"]
RUN dotnet restore "src/SharpBlogX.Admin/SharpBlogX.Admin.csproj"
COPY . .
WORKDIR "/src/src/SharpBlogX.Admin"
RUN dotnet build "SharpBlogX.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpBlogX.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharpBlogX.Admin.dll"]