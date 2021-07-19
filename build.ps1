docker build -t service.user . ;
docker tag service.user mateusz9090/user:local ;
docker push mateusz9090/user:local