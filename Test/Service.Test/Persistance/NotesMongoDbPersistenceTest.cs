using System.Threading.Tasks;
using PipServices.Commons.Config;
using Service.Persistance;
using Xunit;

namespace Service.Test.Persistance
{
    public class NotesMongoDbPersistenceTest
    {
        public MongoDbNotesPersistence Persistence { get; set; }
        public NotesPersistenceFixture Fixture { get; set; }

        public NotesMongoDbPersistenceTest()
        {
            ConfigParams config = ConfigParams.FromTuples(
                "collection", "Notes",
                "connection.uri", "mongodb://mongo:27017/test"
                );

            Persistence = new MongoDbNotesPersistence();
            Persistence.Configure(config);
            Persistence.OpenAsync(null).Wait();
            Persistence.ClearAsync(null).Wait();
            Fixture = new NotesPersistenceFixture(Persistence);
        }

        [Fact]
        public async Task IT_Should_Create_Note()
        {
            await Fixture.TestCreateNote();
        }

        [Fact]
        public async Task IT_Should_Update_Note()
        {
            await Fixture.TestUpdateNote();
        }

        [Fact]
        public async Task IT_Should_Get_Note_By_Id()
        {
            await Fixture.TestGetNoteById();
        }

        [Fact]
        public async Task IT_Should_Delete_Note()
        {
            await Fixture.TestDeleteNote();
        }
    }
}
