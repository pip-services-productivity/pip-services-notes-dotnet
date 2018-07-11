using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interface.Data.Version1;
using PipServices.Commons.Data;

namespace Service.Persistance
{
    public class NotesMemoryPersistance : INotesPersistence
    {
        private const int MaxPageSize = 100;
        private readonly object _lock = new object();
        private readonly Dictionary<string, NoteV1> _notes = new Dictionary<string, NoteV1>();

        
        public async Task<NoteV1> CreateNoteAsync(string correlationId, NoteV1 note)
        {
            note.Id = note.Id ?? IdGenerator.NextLong();

            lock (_lock)
            {
                _notes[note.Id] = note;
            }

            return await Task.FromResult(note);
        }

        public async Task<NoteV1> DeleteNoteByIdAsync(string correlationId, string id)
        {
            NoteV1 result = null;

            lock (_lock)
            {
                _notes.TryGetValue(id, out result);
                if (result != null)
                {
                    _notes.Remove(id);
                }
            }

            return await Task.FromResult(result);
        }

        public Task<DataPage<NoteV1>> GetNotesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            filter = filter ?? new FilterParams();

            lock (_lock)
            {
                var foundNotes = new List<NoteV1>();

                foreach (var notes in _notes.Values)
                {
                    foundNotes.Add(notes);
                }

                paging = paging ?? new PagingParams();
                var skip = paging.GetSkip(0);
                var take = paging.GetTake(MaxPageSize);
                var page = foundNotes.Skip((int)skip).Take((int)take).ToList();
                var total = foundNotes.Count;

                return Task.FromResult(new DataPage<NoteV1>(page, total));
            }
        }

        public async Task<NoteV1> GetNoteByIdAsync(string correlationId, string id)
        {
            NoteV1 result = null;

            lock (_lock)
            {
                //_Notes.TryGetValue(id, out result);
                result = _notes.FirstOrDefault(t => t.Value.Id == id).Value;
            }

            return await Task.FromResult(result);
        }

        public async Task<NoteV1> UpdateNoteAsync(string correlationId, NoteV1 note)
        {
            lock (_lock)
            {
                _notes[note.Id] = note;
            }

            return await Task.FromResult(note);
        }

        public async void ClearAsync()
        {
            lock (_lock)
            {
                _notes.Clear();
            }

            await Task.Delay(0);
        }
    }
}