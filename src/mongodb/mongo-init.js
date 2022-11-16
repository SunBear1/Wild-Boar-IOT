// db = db.getSiblingDB('sample_db');
//
// db.createCollection('sample_collection');
//
// db.sample_collection.insertMany([
//     {
//         org: 'helpdev',
//         filter: 'EVENT_A',
//         addrs: 'http://rest_client_1:8080/wh'
//     },
//     {
//         org: 'helpdev',
//         filter: 'EVENT_B',
//         addrs: 'http://rest_client_2:8081/wh'
//     },
//     {
//         org: 'github',
//         filter: 'EVENT_C',
//         addrs: 'http://rest_client_3:8082/wh'
//     }
// ]);

db.auth("admin", "admin");

db.createUser({
    user: 'root',
    pwd: 'toor',
    roles: [
        {
            role: 'readWrite',
            db: 'testDB',
        },
    ],
});

db = new Mongo().getDB("testDB");

db.createCollection('users', {capped: false});
db.createCollection('test', {capped: false});

db.test.insert([
    {"item": 1},
    {"item": 2},
    {"item": 3},
    {"item": 4},
    {"item": 5}
]);
