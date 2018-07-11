using System.Threading.Tasks;
using Client.Clients.Version1;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using Xunit;

namespace Client.Test.Clients.VersionV1
{
    public class NotesHttpClientV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "serv",
            "connection.port", 8080
        );

        private readonly NotesClientV1Fixture _fixture;

        public NotesHttpClientV1Test()
        {
            var httpClient = new NotesHttpHttpClientV1();

            IReferences references = References.FromTuples(
                new Descriptor("shl-services-notes", "client", "http", "default", "1.0"), httpClient
            );

            httpClient.Configure(HttpConfig);
            httpClient.SetReferences(references);

            _fixture = new NotesClientV1Fixture(httpClient);
            
            httpClient.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestHttpClientCrudOperationsAsync()
        {
            await _fixture.TestClientCrudOperationsAsync();
        }
    }
}
