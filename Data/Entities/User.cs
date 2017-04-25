using System;
using System.ComponentModel.DataAnnotations;

namespace web.Data.Entities
{
    public class User
    {
        #region Properties

        [StringLength(100)]
        public string Email { get; set; }

        public DateTimeOffset EnteredAt { get; set; } = DateTimeOffset.Now;

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        #endregion
    }
}