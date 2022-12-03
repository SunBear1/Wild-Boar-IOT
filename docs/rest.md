# REST API

## GET /api/v1/api/WildBoarIotData
Endpoint response payload should follow schema described below:

```json
[
  {
    "id": 11,
    "type": "BICEPS_MACHINE",
    "weight": 30,
    "occupied": false,
    "date": "2022-11-04T15:21:47Z"
  }
]
```

Whole payload is a list of objects\
Data types:

- id: Long
- type: String
- weight: Int
- occupancy: Bool
- date: DateTime

---

### Parameters

#### Sorting

- /api/WildBoarIotData?sort=id
- /api/WildBoarIotData?sort=type
- /api/WildBoarIotData?sort=weight
- /api/WildBoarIotData?sort=occupied
- /api/WildBoarIotData?sort=date

### Filters

- /api/WildBoarIotData?type={value: String}
- /api/WildBoarIotData?weight={value: Int}
- /api/WildBoarIotData?occupied={value: Bool}
- /api/WildBoarIotData?date_start={value: DateTime}&date_end={value: DateTime}

### Format

- /api/WildBoarIotData?format=json - default doesn't have to be provided
- /api/WildBoarIotData?format=csv - alternative mode of formatting the request payload