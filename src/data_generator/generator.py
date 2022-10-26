import threading
import random

generated_data = []


def generate_machine_weight_data():
    threading.Timer(5.0, generate_machine_weight_data).start()
    generated_data.append({"weight": random.randrange(5, 130, 5)})


def generate_machine_occupancy():
    threading.Timer(10.0, generate_machine_occupancy).start()
    r = random.randrange(0, 1)
    occupied = False
    if r == 1:
        occupied = True
    generated_data.append({"occupancy": occupied})
