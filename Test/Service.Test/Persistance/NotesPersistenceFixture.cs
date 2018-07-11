using System.Threading.Tasks;
using PipServices.Commons.Data;
using Service.Persistance;
using Service.Test.Data;
using Xunit;

namespace Service.Test.Persistance
{
    public class NotesPersistenceFixture
    {
        private readonly INotesPersistence _persistence;

        public NotesPersistenceFixture(INotesPersistence persistence)
        {
            _persistence = persistence;
            _persistence.ClearAsync(); // required for database persistence to have clean environment
        }

        public async Task TestCreateNote()
        {
            // arrange 
            var note = NoteTestModelV1.GenerateRandomNote();

            // act
            var result = await _persistence.CreateNoteAsync(null, note);

            // assert
            NoteTestModelV1.AssertEqual(note, result);
        }

        public async Task TestUpdateNote()
        {
            // arrange 
            var note = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            // act
            note.Title = "T";
            note.Description = "holiday";

            var result = await _persistence.UpdateNoteAsync(null, note);

            // assert
            NoteTestModelV1.AssertEqual(note, result);
        }

        public async Task TestGetNoteById()
        {
            // arrange 
            var note = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            // act
            var result = await _persistence.GetNoteByIdAsync(null, note.Id);

            // assert
            NoteTestModelV1.AssertEqual(note, result);
        }

        public async Task TestGetNoteByIdsFilter()
        {
            // arrange 
            var note1 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());
            var note2 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());
            var note3 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            var filter = FilterParams.FromTuples(
                "ids", $"{note2.Id}"
            );

            // act
            var result = await _persistence.GetNotesAsync(null, filter, null);

            // assert
            Assert.NotNull(result);
            Assert.Single(result.Data);
            NoteTestModelV1.AssertEqual(note2, result.Data[0]);
        }

        public async Task TestGetNotesByComplexFilter()
        {
            // arrange 
            var note1 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());
            var note2 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());
            var note3 = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            var filter = FilterParams.FromTuples(
                "ids", $"{note2.Id},{note1.Id}",
                "Ids", $"{note1.Id}",
                "type", note1.Title,
                "code", note1.Description
            );

            // act
            var result = await _persistence.GetNotesAsync(null, filter, null);

            // assert
            Assert.NotNull(result);
            Assert.Single(result.Data);
            NoteTestModelV1.AssertEqual(note1, result.Data[0]);
        }

        public async Task TestDeleteNote()
        {
            // arrange 
            var note = await _persistence.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            // act
            var deletedNotes = await _persistence.DeleteNoteByIdAsync(null, note.Id);
            var result = await _persistence.GetNoteByIdAsync(null, note.Id);

            // assert
            NoteTestModelV1.AssertEqual(note, deletedNotes);
            Assert.Null(result);
        }
    }
}