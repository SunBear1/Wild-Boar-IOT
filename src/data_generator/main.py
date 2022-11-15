import random
import time
from datetime import datetime

from faker import Faker

from src.data_generator.rabbitmq_client import RabbitMQClient

fake = Faker()

RabbitMQClient.create_connection()
FREQUENCY = 7.5

start_time = time.time()
message_id = 1

rand_date_begin = datetime.strptime('1/1/2021 1:00 AM', '%m/%d/%Y %I:%M %p')
rand_date_end = datetime.strptime('1/1/2022 1:00 AM', '%m/%d/%Y %I:%M %p')

while True:
    random_payload = {
        "id": message_id,
        "weight": random.randrange(5, 130, 5),
        "occupied": str(random.choice([True, False])).lower(),
        "date": fake.date_time_between(start_date='-45d', end_date='now').strftime("%d/%m/%Y, %H:%M:%S")
    }
    print(str(random_payload))
    RabbitMQClient.send_data_to_queue(queue_name="WildBoarQueue", payload=str(random_payload))
    time.sleep(5.0 - ((time.time() - start_time) % 5.0))
    message_id += 1
