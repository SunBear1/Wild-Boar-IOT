# RabbitMQ Setup instruction
In order to start RabbitMQ service script `initialize_docker.sh` builds docker image and runs docker container.
In order to see if the docker has successfully started open any browser and go to `http://localhost:15672/`.
As you can see app is running on port `15672`.
If u want to login please check the login and password in `definitions.json` file.
---

# Files structure
Files needed to execute the script:
- `definitions.json` -> Base user (login and password) is initialized in the definitions file. It is also possible to add 
queues, parameters and many other options
- `dockerfile` -> downloads latest RabbitMQ and binds definitions.json with RabbitMQ configurations so that when we start a new image we still have the same basic settings
- `initialize_docker.sh` -> script that initializes `docker image` and `docker container`
- `rabbitmq.config` -> main RabbitMQ config file, any changes needed may be implemented here
---

# RabbitMQ + .net - Example
In the directory `DotNet_UsageExample`, I have placed basic usage of RabbitMQ in C#
In case anyone is curious how we will communicate between C# and RabbitMQ, please head to this directory