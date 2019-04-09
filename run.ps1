docker pull redis
docker run --name cart-redis -p 6379:6379 -d redis
docker run --name cart-api -p 5000:5000 -d cart-api