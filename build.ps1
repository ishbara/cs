dotnet restore CartWebAPI
dotnet publish -c Release -o ../publish/CartWebAPI CartWebAPI/CartWebAPI.csproj
copy docker-conf/redisSettings.json -Destination publish/CartWebAPI/redisSettings.json
docker build -t cart-api publish/CartWebAPI