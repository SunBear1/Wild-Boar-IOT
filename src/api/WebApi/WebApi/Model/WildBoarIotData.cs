using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Model
{
    public class WildBoarIotData
    {
        [BsonId]
        public string id { get; set; }
        public int weight { get; set; }
        public bool occupied { get; set; }
        public DateTime date { get; set; }
    }
}