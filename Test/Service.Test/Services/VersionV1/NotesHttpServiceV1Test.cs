using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interface.Data.Version1;
using PipServices.Commons.Config;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using Service.Logic;
using Service.Persistance;
using Service.Services.Version1;
using Service.Test.Data;
using Xunit;

namespace Service.Test.Services.VersionV1
{
    public class NotesHttpServiceV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", "8080"
        );

        private readonly NotesMemoryPersistance _persistence;
        private readonly NotesController _controller;
        private readonly NotesHttpServiceV1 _service;

        public NotesHttpServiceV1Test()
        {
            _persistence = new NotesMemoryPersistance();
            _controller = new NotesController();
            _service = new NotesHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("shl-services-notes", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("shl-services-notes", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("shl-services-notes", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);
            _service.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task IT_Should_Test_Crud_Operations()
        {
            var expectedNote1 = NoteTestModelV1.GenerateRandomNote();
            var Note1 = await Invoke<NoteV1>("create_note", new { Note = expectedNote1 });
            NoteTestModelV1.AssertEqual(expectedNote1, Note1);

            var expectedNote2 = NoteTestModelV1.GenerateRandomNote();
            var Note2 = await Invoke<NoteV1>("create_note", new { Note = expectedNote2 });
            NoteTestModelV1.AssertEqual(expectedNote2, Note2);

            var page = await Invoke<DataPage<NoteV1>>("get_notes", new { });
            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            Note1.Title = "Updated Type";
            Note1.Description = "Updated Code";

            var Note = await Invoke<NoteV1>("update_note", new { Note = Note1 });
            NoteTestModelV1.AssertEqual(Note1, Note);

            Note = await Invoke<NoteV1>("delete_note_by_id", new { id = Note1.Id });
            Assert.NotNull(Note);
            Assert.Equal(Note1.Id, Note.Id);

            Note = await Invoke<NoteV1>("delete_note_by_id", new { id = Note1.Id });
            Assert.Null(Note);

            Note = await Invoke<NoteV1>("delete_note_by_id", new { id = Note2.Id });
            Assert.NotNull(Note);
            Assert.Equal(Note2.Id, Note.Id);

            Note = await Invoke<NoteV1>("get_note_by_id", new { id = Note2.Id });
            Assert.Null(Note);
        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:8080/v1/notes/" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
        }
    }
}
