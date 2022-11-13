using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Model
{
    public class WildBoarIotData
    {
        [BsonId]
        public string id { get; set; }
        public int weights { get; set; }
        public bool occupancy { get; set; }
    }
}