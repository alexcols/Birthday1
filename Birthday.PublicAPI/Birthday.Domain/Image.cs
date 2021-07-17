using Birthday.Domain.shared;
using System;

namespace Birthday.Domain
{
    public sealed class Image:Entity<int>
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Person Person { get; set; }
        public byte[] Content { get; set; }
    }
}