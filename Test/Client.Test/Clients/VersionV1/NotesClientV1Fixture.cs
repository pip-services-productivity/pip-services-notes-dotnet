using System.Threading.Tasks;
using Client.Clients.Version1;
using Service.Test.Data;
using Xunit;

namespace Client.Test.Clients.VersionV1
{
    public class NotesClientV1Fixture
    {
        private readonly INotesHttpClientV1 _httpClient;

        public NotesClientV1Fixture(INotesHttpClientV1 httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task TestClientCrudOperationsAsync()
        {
            var note1 = await _httpClient.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());
            var note2 = await _httpClient.CreateNoteAsync(null, NoteTestModelV1.GenerateRandomNote());

            //dynamic projectionResult = await _httpClient.GetNotesAsync(null, null, null);
            //Assert.Equal(note1.Title, projectionResult.Data[0].Title);
            //Assert.Equal(note2.Title, projectionResult.Data[1].Title);

            var page = await _httpClient.GetNotesAsync(null, null, null);
            Assert.Equal(2, page.Data.Count);

            await _httpClient.DeleteNoteByIdAsync(null, note1.Id);

            //projectionResult = await _httpClient.GetNoteByIdAsync(null, note2.Id);
            //Assert.Equal(note2.Id, projectionResult.Id);
            
            var result = await _httpClient.GetNoteByIdAsync(null, note2.Id);
            Assert.NotNull(result);

            await _httpClient.DeleteNoteByIdAsync(null, note2.Id);
            page = await _httpClient.GetNotesAsync(null, null, null);
            Assert.Empty(page.Data);
        }
    }
}
