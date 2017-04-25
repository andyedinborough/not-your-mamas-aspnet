using System;

namespace web.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Caption { get; set; }

        public DateTimeOffset EnteredAt { get; set; } = DateTimeOffset.Now;

        public virtual User EnteredBy { get; set; }

        public int EnteredById { get; set; }

        public byte[] Picture { get; set; }
    }
}