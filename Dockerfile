FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
EXPOSE 80
COPY Peitho.csproj .
RUN dotnet restore "Peitho.csproj"
COPY . .
RUN dotnet build "Peitho.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Peitho.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf