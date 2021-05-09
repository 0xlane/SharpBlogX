FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /source
COPY . .
WORKDIR "/source/src/SharpBlogX.Web"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build /app .
EXPOSE 44381
ENTRYPOINT ["dotnet", "SharpBlogX.Web.dll"]