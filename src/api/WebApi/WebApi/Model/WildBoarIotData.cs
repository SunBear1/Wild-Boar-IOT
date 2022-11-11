using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Model
{
    public class WildBoarIotData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int weights { get; set; }
        public bool occupancy { get; set; }
    }
}