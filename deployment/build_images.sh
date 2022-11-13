echo "Building web_ui image..."
cd ../src/web_ui; docker build -t $WEB_UI_IMAGE_TAG .;
cd ..
echo "Finished building web_ui image"

echo "Building rabbitMQ image..."
cd ../src/rabbitmq; docker build -t $RABBITMQ_IMAGE_TAG .;
cd ..
echo "Finished building rabbitMQ image"

echo "Building mongoDB image..."
cd ../src/mongodb; docker build -t $MONGODB_IMAGE_TAG .;
cd ..
echo "Finished building mongoDB image"

echo "Building data_generator image..."
cd ../src/data_generator; docker build -t $DATA_GENERATOR_IMAGE_TAG .;
cd ..
echo "Finished building data_generator image"

echo "Building api image..."
cd ../src/api/WebApi; docker build -t $API_IMAGE_TAG .;
cd ..
echo "Finished building api image"
