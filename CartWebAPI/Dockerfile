FROM microsoft/dotnet:aspnetcore-runtime
COPY ./ /app
ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /app
ENTRYPOINT ["dotnet", "CartWebAPI.dll"]