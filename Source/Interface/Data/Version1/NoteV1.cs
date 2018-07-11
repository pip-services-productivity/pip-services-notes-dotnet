using System.Runtime.Serialization;
using PipServices.Commons.Data;

namespace Interface.Data.Version1
{
    [DataContract]
    public class NoteV1 : IStringIdentifiable
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name="title")]
        public string Title { get; set; }
        [DataMember(Name="description")]
        public string Description { get; set; }
        //[DataMember(Name="contentBlockV1")]
        //public ContentBlockV1[] Content { get; set; }
    }
}
