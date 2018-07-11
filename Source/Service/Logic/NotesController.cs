using System.Threading.Tasks;
using Interface.Data.Version1;
using Interface.Logic;
using PipServices.Commons.Commands;
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Commons.Logic;
using PipServices.Commons.Refer;
using Service.Persistance;

namespace Service.Logic
{
    public class NotesController : AbstractController, ICommandable, INotesController
    {
        private INotesPersistence _persistence;
        private NotesCommandSet _commandSet;

        public override void Configure(ConfigParams config) { }

        public NotesController()
        {
            //_dependencyResolver =
            //    new DependencyResolver(ConfigParams.FromTuples("dependencies.persistence", "shl-services-notes:persistence:memory:*:1.0"));
        }

        public override void SetReferences(IReferences references)
        {
            base.SetReferences(references);
       
            //_persistence = _dependencyResolver.GetOneRequired<INotesPersistence>("persistence");
            _persistence =
                references.GetOneRequired<INotesPersistence>(new Descriptor("shl-services-notes", "persistence", "memory", "*", "1.0"));
        }

        public override string Component { get; }

        public CommandSet GetCommandSet()
        {
            return _commandSet ?? (_commandSet = new NotesCommandSet(this));
        }

        public async Task<NoteV1> CreateNoteAsync(string correlationId, NoteV1 note)
        {
            return await SafeInvokeAsync(correlationId, "CreateNoteAsync", 
                () => _persistence.CreateNoteAsync(correlationId, note));
        }

        public async Task<NoteV1> UpdateNoteAsync(string correlationId, NoteV1 note)
        {
            return await SafeInvokeAsync(correlationId, "UpdateNoteAsync", 
                () => _persistence.UpdateNoteAsync(correlationId, note));
        }

        public async Task<NoteV1> DeleteNoteByIdAsync(string correlationId, string id)
        {
            return await SafeInvokeAsync(correlationId, "DeleteNoteByIdAsync", 
                () => _persistence.DeleteNoteByIdAsync(correlationId, id));
        }

        public async Task<NoteV1> GetNoteByIdAsync(string correlationId, string id)
        {
            return await SafeInvokeAsync(correlationId, "GetNoteByIdAsync",
                () =>  _persistence.GetNoteByIdAsync(correlationId, id));
        }

        public async Task<DataPage<NoteV1>> GetNotesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await SafeInvokeAsync(correlationId, "GetNotesAsync", 
                () => _persistence.GetNotesAsync(correlationId, filter, paging));
        }
    }
}
