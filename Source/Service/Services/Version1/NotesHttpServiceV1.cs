using PipServices.Commons.Refer;
using PipServices.Net.Rest;

namespace Service.Services.Version1
{
    public class NotesHttpServiceV1 : CommandableHttpService
    {
        public NotesHttpServiceV1(): base("v1/notes")
        {
            _dependencyResolver.Put("controller", new Descriptor("shl-services-notes", "controller", "*", "*", "1.0"));
        }
    }
}