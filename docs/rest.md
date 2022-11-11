# REST API

### All sensor data endpoint
#### GET /api/v1/data
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

Whole payload is a list of objects\
Data types:

- id: String
- weight: Int
- occupancy: Bool

### Status endpoint

#### GET /api/v1/status

Endpoint response payload should follow schema described below:

```
API is listening
```

Whole payload is a simple string