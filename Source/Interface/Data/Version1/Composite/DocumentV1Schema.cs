using System;
using PipServices.Commons.Validate;

namespace Interface.Data.Version1.Composite
{
    public class DocumentV1Schema : ObjectSchema
    {
        public DocumentV1Schema() : base()
        {
            this.WithOptionalProperty("FileId", TypeCode.String);
            this.WithOptionalProperty("FileName", TypeCode.Boolean);
        }
    }
}
