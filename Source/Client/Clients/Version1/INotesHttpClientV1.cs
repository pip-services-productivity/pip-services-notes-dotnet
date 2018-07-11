using System.Threading.Tasks;
using Interface.Data.Version1;
using PipServices.Commons.Data;

namespace Client.Clients.Version1
{
    public interface INotesHttpClientV1
    {
        Task<DataPage<NoteV1>> GetNotesAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<NoteV1> GetNoteByIdAsync(string correlationId, string id);
        Task<NoteV1> CreateNoteAsync(string correlationId, NoteV1 note);
        Task<NoteV1> UpdateNoteAsync(string correlationId, NoteV1 note);
        Task<NoteV1> DeleteNoteByIdAsync(string correlationId, string id);
    }
}