FROM python:3.8-slim as build_stage

WORKDIR /code

COPY requirements.txt requirements.txt
RUN pip install --upgrade pip
RUN pip install --no-cache-dir --upgrade -r requirements.txt
COPY . .

CMD ["python3", "main.py"]
