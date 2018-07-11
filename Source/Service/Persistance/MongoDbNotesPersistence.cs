using System;
using System.Threading.Tasks;
using Interface.Data.Version1;
using MongoDB.Driver;
using PipServices.Commons.Data;
using PipServices.Commons.Data.Mapper;
using PipServices.Oss.MongoDb;

namespace Service.Persistance
{
    public class MongoDbNotesPersistence : IdentifiableMongoDbPersistence<NoteV1MongoDbSchema, string>, INotesPersistence
    {
        public MongoDbNotesPersistence() : base("notes")
        {
        }

        public async void ClearAsync()
        {
            await ClearAsync(null);
        }

        public async Task<NoteV1> CreateNoteAsync(string correlationId, NoteV1 note)
        {
            var result = await CreateAsync(correlationId, FromPublic(note));

            return ToPublic(result);
        }

        public async Task<NoteV1> DeleteNoteByIdAsync(string correlationId, string id)
        {
            var result = await DeleteByIdAsync(correlationId, id);

            return ToPublic(result);
        }

        public async Task<DataPage<NoteV1>> GetNotesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var result = await GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll(ToPublic);

            return new DataPage<NoteV1>
            {
                Data = data,
                Total = data.Count
            };
        }

        public async Task<NoteV1> GetNoteByIdAsync(string correlationId, string id)
        {
            var result = await GetOneByIdAsync(correlationId, id);

            return ToPublic(result);
        }

        public async Task<NoteV1> UpdateNoteAsync(string correlationId, NoteV1 note)
        {
            var result = await UpdateAsync(correlationId, FromPublic(note));

            return ToPublic(result);
        }

        
        private static NoteV1 ToPublic(NoteV1MongoDbSchema value)
        {
            return value == null ? null : ObjectMapper.MapTo<NoteV1>(value);
        }

        private static NoteV1MongoDbSchema FromPublic(NoteV1 value)
        {
            return ObjectMapper.MapTo<NoteV1MongoDbSchema>(value);
        }

        private FilterDefinition<NoteV1MongoDbSchema> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<NoteV1MongoDbSchema>.Filter;
            var filter = builder.Empty;

            foreach (var filterKey in filterParams.Keys)
            {
                if (filterKey.Equals("ids"))
                {
                    filter &= builder.In(s => s.Id, ToStringArray(filterParams.GetAsNullableString("ids")));
                    continue;
                }

                filter &= builder.Eq(filterKey, filterParams[filterKey]);
            }

            return filter;
        }

        private static string[] ToStringArray(string value)
        {
            string[] items = value?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return items?.Length > 0 ? items : null;
        }
    }
}