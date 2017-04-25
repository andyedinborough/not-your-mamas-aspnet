using System;
using System.ComponentModel.DataAnnotations;

namespace web.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(160)]
        public string Caption { get; set; }

        public DateTimeOffset EnteredAt { get; set; } = DateTimeOffset.Now;

        public virtual User EnteredBy { get; set; }

        public int EnteredById { get; set; }

    }
}