import random
import time
from datetime import datetime

from faker import Faker

from rabbitmq_client import RabbitMQClient

fake = Faker()

RabbitMQClient.create_connection()
FREQUENCY = 7.5
MACHINES = ["CHEST_MACHINE", "BICEPS_MACHINE", "TREADMILL"]

start_time = time.time()
message_id = 1

rand_date_begin = datetime.strptime('1/1/2022 1:00 AM', '%m/%d/%Y %I:%M %p')
rand_date_end = datetime.strptime('12/31/2022 1:00 AM', '%m/%d/%Y %I:%M %p')

while True:
    random_payload = {
        "id": message_id,
        "type": random.choice(MACHINES),
        "weight": random.randrange(5, 200, 5),
        "occupied": str(random.choice([True, False])).lower(),
        "date": fake.date_time_between(start_date=rand_date_begin, end_date=rand_date_end).strftime(
            "%m/%d/%Y, %H:%M:%S")
    }
    print(str(random_payload))
    RabbitMQClient.send_data_to_queue(queue_name="WildBoarQueue", payload=str(random_payload))
    time.sleep(FREQUENCY - ((time.time() - start_time) % FREQUENCY))
    message_id += 1
