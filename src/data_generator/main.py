import json

import pika
from fastapi import FastAPI
from fastapi.responses import PlainTextResponse, Response

from generator import generate_machine_weight_data, generate_machine_occupancy, generated_data

app = FastAPI()

generate_machine_weight_data()
generate_machine_occupancy()

# connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
# channel = connection.channel()
# channel.queue_declare(queue='weights')
# channel.basic_publish(exchange='',
#                       routing_key='weights',
#                       body='Hello World!')
# print(" [x] Sent 'Hello World!'")


@app.get("/")
async def root():
    return PlainTextResponse(content="Data generator for WildBoarIOT", status_code=200)


@app.get("/data")
async def data():
    return Response(content=json.dumps(generated_data), media_type="application/json", status_code=200)
