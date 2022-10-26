import pika

USERNAME = "admin"
PASSWORD = "1234"
VHOST = "/vhost1"
PORT = 5672
HOST = "message_broker"


class RabbitMQClient:
    credentials = pika.credentials.PlainCredentials(username=USERNAME, password=PASSWORD)
    connection = None

    @classmethod
    def create_connection(cls):
        cls.connection = pika.BlockingConnection(
            pika.ConnectionParameters(host=HOST, port=PORT, credentials=cls.credentials, virtual_host=VHOST))

    @classmethod
    def send_data_to_queue(cls, queue_name: str, payload):
        channel = cls.connection.channel()
        channel.queue_declare(queue=queue_name)
        channel.basic_publish(exchange='',
                              routing_key=queue_name,
                              body=payload)

    @classmethod
    def close_connection(cls):
        cls.connection.close()
