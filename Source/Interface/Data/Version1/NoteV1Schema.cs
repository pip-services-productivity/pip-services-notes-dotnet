using PipServices.Commons.Convert;
using PipServices.Commons.Validate;

namespace Interface.Data.Version1
{
    public class NoteV1Schema: ObjectSchema
    {
        public NoteV1Schema()
        {
            WithOptionalProperty("id", TypeCode.String);
            WithOptionalProperty("title", TypeCode.String);
            WithOptionalProperty("description", TypeCode.String);
            //..
        }
    }
}