FROM rabbitmq:latest as build-stage

WORKDIR /rabbitmq_data

RUN rabbitmq-plugins enable rabbitmq_management
RUN rabbitmq-plugins enable rabbitmq_federation
RUN rabbitmq-plugins enable rabbitmq_federation_management

ADD rabbitmq.config /etc/rabbitmq/
ADD definitions.json /etc/rabbitmq/

RUN chown rabbitmq:rabbitmq /etc/rabbitmq/rabbitmq.config /etc/rabbitmq/definitions.json

CMD ["rabbitmq-server"]

EXPOSE 5672
EXPOSE 15672
