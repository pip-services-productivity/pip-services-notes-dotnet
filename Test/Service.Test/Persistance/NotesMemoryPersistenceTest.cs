using System.Threading.Tasks;
using Service.Persistance;
using Xunit;

namespace Service.Test.Persistance
{
    public class NotesMemoryPersistenceTest
    {
        public NotesMemoryPersistance Persistence { get; set; }
        public NotesPersistenceFixture Fixture { get; set; }

        public NotesMemoryPersistenceTest()
        {
            Persistence = new NotesMemoryPersistance();
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
