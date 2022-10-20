## Developers guide to deploy WildBoar-IOT platform locally with docker swarm
All commands must be run from `/deployment` directory
### Building platform from scratch
1. Set environment variables: \
   This environment variables will be used for tagging docker images. If you want you can set these variables to your own values.\
`export WEB_UI_IMAGE_TAG=frontend/vue-app`\
`export API_IMAGE_TAG=backend/dotnet-core`\
`export RABBITMQ_IMAGE_TAG=message-broker/rabbitmq`\
`export MONGODB_IMAGE_TAG=database/mongodb`
2. Run script `./build_images.sh` (you can confirm that every image was built by running command `docker images`)
3. Run `sudo -E docker-compose up`

### Rebuilding image
Let's say that you want to rebuild single image and see how platform is working with your brand new changes
1. Locally remove docker image by running command `docker image rm $TAG_OF_YOUR_IMAGE -f `\
*example: `docker image rm $WEB_UI_IMAGE_TAG -f`*
2. Run script `./build_images.sh`
3. Run `sudo -E docker-compose up`