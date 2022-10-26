import random
import time

from rabbitmq_client import RabbitMQClient

RabbitMQClient.create_connection()
FREQUENCY = 7.5

start_time = time.time()
while True:
    RabbitMQClient.send_data_to_queue(queue_name="weights", payload=str(random.randrange(5, 130, 5)))
    RabbitMQClient.send_data_to_queue(queue_name="occupancy", payload=str(random.choice([True, False])))
    time.sleep(5.0 - ((time.time() - start_time) % 5.0))
