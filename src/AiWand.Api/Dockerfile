FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY bin/publish /app

ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=${ENVIRONMENT} ASPNETCORE_URLS="http://*:5000"

EXPOSE 5000
ENTRYPOINT ["dotnet", "AiWand.Api.dll"]