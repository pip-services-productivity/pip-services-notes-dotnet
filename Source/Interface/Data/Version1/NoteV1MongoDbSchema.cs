using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using PipServices.Commons.Data;

namespace Interface.Data.Version1
{
    [BsonIgnoreExtraElements]
    public class NoteV1MongoDbSchema : IStringIdentifiable
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("href")]
        public string Href { get { return $@"/notes/{Id}"; } set { } }
        
        //[DataMember(Name="contentBlockV1")]
        //public ContentBlockV1[] Content { get; set; }
    }
}
