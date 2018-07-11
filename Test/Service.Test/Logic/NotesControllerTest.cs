using System.Collections.Generic;
using Interface.Data.Version1;
using Moq;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using Service.Logic;
using Service.Persistance;
using Service.Test.Data;
using Xunit;

namespace Service.Test.Logic
{
    public class NotesControllerTest
    {
        private readonly NotesController _controller;

        private readonly INotesPersistence _persistence;
        private readonly Mock<INotesPersistence> _moqPersistence;

        public NotesControllerTest()
        {
            var references = new References();
            _controller = new NotesController();

            _moqPersistence = new Mock<INotesPersistence>();
            _persistence = _moqPersistence.Object;

            references.Put(new Descriptor("shl-services-notes", "persistence", "memory", "*", "1.0"), _persistence);
            references.Put(new Descriptor("shl-services-notes", "controller", "default", "*", "1.0"), _controller);

            _controller.SetReferences(references);
        }

        [Fact]
        public async void It_Should_Create_Note_Async()
        {
            var note = NoteTestModelV1.GenerateRandomNote();

            _moqPersistence.Setup(p => p.CreateNoteAsync(null, note)).ReturnsAsync(note);

            var result = await _controller.CreateNoteAsync(null, note);

            Assert.Equal(note.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Update_Note_Async()
        {
            var note = NoteTestModelV1.GenerateRandomNote();

            _moqPersistence.Setup(p => p.UpdateNoteAsync(null, note)).ReturnsAsync(note);

            var result = await _controller.UpdateNoteAsync(null, note);

            Assert.Equal(note.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Delete_Note_Async()
        {
            var note = NoteTestModelV1.GenerateRandomNote();

            _moqPersistence.Setup(p => p.DeleteNoteByIdAsync(null, note.Id)).ReturnsAsync(note);

            var result = await _controller.DeleteNoteByIdAsync(null, note.Id);

            Assert.Equal(note.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Get_Note_Async()
        {
            var note = NoteTestModelV1.GenerateRandomNote();

            _moqPersistence.Setup(p => p.GetNoteByIdAsync(null, note.Id)).ReturnsAsync(note);

            var result = await _controller.GetNoteByIdAsync(null, note.Id);

            Assert.Equal(note.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Get_Notes_Async()
        {
            var notes = new DataPage<NoteV1>
            {
                Data = new List<NoteV1> { NoteTestModelV1.GenerateRandomNote(), NoteTestModelV1.GenerateRandomNote(), NoteTestModelV1.GenerateRandomNote() },
                Total = 3
            };
            var filter = new FilterParams();
            var paging = new PagingParams();

            _moqPersistence.Setup(p => p.GetNotesAsync(null, filter, paging)).ReturnsAsync(notes);

            var result = await _controller.GetNotesAsync(null, filter, paging);

            Assert.Equal(notes.Total, result.Total);
        }
    }
}
