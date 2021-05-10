FROM node:16-buster-slim AS node_base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY --from=node_base . .
WORKDIR /source
COPY . .
WORKDIR "/source/src/SharpBlogX.Admin"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 44382
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SharpBlogX.Admin.dll"]