using System;

namespace ADProv_Parconsil.Models.Result
{
    public class EDrive
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string WebViewLink { get; set; }
        public string WebContentLink { get; set; }
        public string ThumbnailLink { get; set; }

    }
}
