using System;

namespace Interface.Data.Version1.Composite
{
    class ContentBlockV1
    {
        public string Type;
        public string  Text;
        public ChecklistItemV1[] Checklist;
        public string LocName;
        public object LocPos; // GeoJSON
        public DateTime Start;
        public DateTime End;
        public bool AllDay;
        public string[] PicIds;
        public DocumentV1[] Docs;
        public string EmbedType;
        public string EmbedUri;
        public object Custom;
    }
}
