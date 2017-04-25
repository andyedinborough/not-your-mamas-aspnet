using System;

namespace web.Data.Entities
{
    public class PostPicture
    {
        #region Properties

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public virtual Post Post { get; set; }

        public int PostId { get; set; }

        #endregion
    }
}