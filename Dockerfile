FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
EXPOSE 80
COPY Minerva.Client.csproj .
RUN dotnet restore "Minerva.Client.csproj"
COPY . .
RUN dotnet build "Minerva.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Minerva.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf