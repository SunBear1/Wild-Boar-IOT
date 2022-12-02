echo "Building web_ui image..."
cd ../src/web_ui; docker build -t malajski/wildboar-iot-gym:frontend .;
cd ..
echo "Finished building web_ui image"

echo "Building rabbitMQ image..."
cd ../src/rabbitmq; docker build -t malajski/wildboar-iot-gym:message-broker .;
cd ..
echo "Finished building rabbitMQ image"

echo "Building mongoDB image..."
cd ../src/mongodb; docker build -t malajski/wildboar-iot-gym:database .;
cd ..
echo "Finished building mongoDB image"

echo "Building data_generator image..."
cd ../src/data_generator; docker build -t malajski/wildboar-iot-gym:data-generator .;
cd ..
echo "Finished building data_generator image"

echo "Building api image..."
cd ../src/api/WebApi; docker build -t malajski/wildboar-iot-gym:backend .;
cd ..
echo "Finished building api image"
