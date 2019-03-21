FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/AiWand.Api/AiWand.Api.csproj", "src/AiWand.Api/"]
COPY ["src/AiWand.Core/AiWand.Core.csproj", "src/AiWand.Core/"]
COPY ["src/AiWand.Service/AiWand.Service.csproj", "src/AiWand.Service/"]
COPY ["src/AiWand.Data/AiWand.Data.csproj", "src/AiWand.Data/"]
RUN dotnet restore "src/AiWand.Api/AiWand.Api.csproj"
COPY . .
WORKDIR "/src/src/AiWand.Api"
RUN dotnet build "AiWand.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AiWand.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AiWand.Api.dll"]