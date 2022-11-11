# REST API

### All sensor data endpoint

#### /api/v1/data

Endpoint response payload should follow schema described below:

```json
[
  {
    "id": "Biceps-Machine",
    "weight": 80,
    "occupancy": "True"
  },
  {
    "id": "Klata-Machine",
    "weight": 140,
    "occupancy": "False"
  }
]
```

Please keep this in mind when creating API endpoint and request in GUI