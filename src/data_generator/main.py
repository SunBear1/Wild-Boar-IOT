import random
import time

from rabbitmq_client import RabbitMQClient

RabbitMQClient.create_connection()
FREQUENCY = 7.5

start_time = time.time()
while True:
    RabbitMQClient.send_data_to_queue(queue_name="WildBoarQueue", payload=str(random.randrange(1, 10000000, 1)),body="id")
    RabbitMQClient.send_data_to_queue(queue_name="WildBoarQueue", payload=str(random.randrange(5, 130, 5)),body="weights")
    RabbitMQClient.send_data_to_queue(queue_name="WildBoarQueue", payload=str(random.choice([True, False])),body="occupancy")
    time.sleep(5.0 - ((time.time() - start_time) % 5.0))
