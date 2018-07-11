using PipServices.Commons.Convert;
using PipServices.Commons.Validate;

namespace Interface.Data.Version1.Composite
{
    public class ChecklistItemV1Schema : ObjectSchema
    {
        public ChecklistItemV1Schema() : base()
        {
            this.WithOptionalProperty("Text", TypeCode.String);
            this.WithOptionalProperty("Checked", TypeCode.Boolean);
        }
    }
}
