#build and publish project
dotnet restore CartWebAPI
dotnet publish -c Release -o ../publish/CartWebAPI CartWebAPI/CartWebAPI.csproj

#prepare docker images
copy docker-conf/redisSettings.json -Destination publish/CartWebAPI/redisSettings.json
docker build -t cart-api publish/CartWebAPI
docker pull redis

#create docker instances
docker create --name cart-redis -p 6379:6379 redis
docker create --name cart-api -p 5000:5000 cart-api