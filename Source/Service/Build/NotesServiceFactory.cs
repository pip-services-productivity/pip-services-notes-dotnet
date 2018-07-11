using PipServices.Commons.Build;
using PipServices.Commons.Refer;
using PipServices.Net.Rest;
using Service.Logic;
using Service.Persistance;
using Service.Services.Version1;

namespace Service.Build
{
    public class NotesServiceFactory : Factory
    {

        public static Descriptor MongoDbPersistenceDescriptor = 
            new Descriptor("shl-services-notes", "persistence", "mongodb", "default", "1.0");

        public static Descriptor MemoryPersistenceDescriptor = 
            new Descriptor("shl-services-notes", "persistence", "memory", "default", "1.0");
                            
        public static Descriptor ControllerDescriptor =
            new Descriptor("shl-services-notes", "controller", "default", "default", "1.0");
        
        public static Descriptor HttpServiceDescriptor =
            new Descriptor("shl-services-notes", "service", "http", "default", "1.0");

        public static Descriptor HttpEndpointDescriptor = 
            new Descriptor("pip-services", "endpoint", "http", "*", "1.0");

        public NotesServiceFactory()
        {
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(MongoDbNotesPersistence));
            RegisterAsType(MemoryPersistenceDescriptor, typeof(NotesMemoryPersistance));
            RegisterAsType(ControllerDescriptor, typeof(NotesController));
            RegisterAsType(HttpServiceDescriptor, typeof(NotesHttpServiceV1));
            RegisterAsType(HttpEndpointDescriptor, typeof(HttpEndpoint));
        }
    }
}
