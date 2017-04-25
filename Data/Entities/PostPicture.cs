using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Data.Entities
{
    public class PostPicture
    {
        #region Properties

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public virtual Post Post { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        #endregion
    }
}