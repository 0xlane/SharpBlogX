FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY . .
WORKDIR "/source/src/SharpBlogX.Api"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 44380
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SharpBlogX.Api.dll"]