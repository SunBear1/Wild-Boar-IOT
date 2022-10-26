import random
import threading

from rabbitmq_client import RabbitMQClient

generated_data = []
FREQUENCY = 7.5


def generate_machine_weight_data():
    threading.Timer(FREQUENCY, generate_machine_weight_data).start()
    RabbitMQClient.create_connection()
    RabbitMQClient.send_data_to_queue(queue_name="weights", payload=str(random.randrange(5, 130, 5)))
    RabbitMQClient.close_connection()


def generate_machine_occupancy():
    threading.Timer(FREQUENCY, generate_machine_occupancy).start()
    RabbitMQClient.create_connection()
    RabbitMQClient.send_data_to_queue(queue_name="occupancy", payload=str(random.choice([True, False])))
    RabbitMQClient.close_connection()
