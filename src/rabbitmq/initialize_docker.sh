
docker build -t rabbit_image . ;
echo 'INFO: Image rabbit_image created!'
docker run -d -it -p 15672:15672 --name=rabbit_container rabbit_image;
echo 'INFO: Started container'