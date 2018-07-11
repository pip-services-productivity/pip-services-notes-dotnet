using System.Threading.Tasks;
using Interface.Data.Version1;
using PipServices.Commons.Data;
using PipServices.Net.Rest;

namespace Client.Clients.Version1
{
    public class NotesHttpHttpClientV1 : CommandableHttpClient, INotesHttpClientV1
    {
        public NotesHttpHttpClientV1() : base("v1/notes")
        {

        }

        public async Task<DataPage<NoteV1>> GetNotesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await CallCommandAsync<DataPage<NoteV1>>(
                "get_notes",
                correlationId,
                new
                {
                    filter = filter,
                    paging = paging
                }
            );
        }
        
        public async Task<NoteV1> GetNoteByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<NoteV1>(
                "get_note_by_id",
                correlationId,
                new
                {
                    id = id
                }
            );
        }


        public async Task<NoteV1> CreateNoteAsync(string correlationId, NoteV1 note)
        {
            return await CallCommandAsync<NoteV1>(
                "create_note",
                correlationId,
                new
                {
                    note = note
                }
            );
        }


        public async Task<NoteV1> UpdateNoteAsync(string correlationId, NoteV1 note)
        {
            return await CallCommandAsync<NoteV1>(
                "update_note",
                correlationId,
                new
                {
                    note = note
                }
            );
        }

        public async Task<NoteV1> DeleteNoteByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<NoteV1>(
                "delete_note_by_id",
                correlationId,
                new
                {
                    id = id
                }
            );
        }
    }
}
