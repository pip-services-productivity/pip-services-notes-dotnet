using PipServices.Container;
using Service.Build;

namespace Service.Container
{
    public class NotesProcess : ProcessContainer
    {
        public NotesProcess()
            : base("Description", "Description microservice")
        {
            _factories.Add(new NotesServiceFactory());
        }
    }
}