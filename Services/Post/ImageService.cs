using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using web.Data;

namespace web.Services.Post
{
    public class ImageService : IDisposable
    {
        #region Fields

        private readonly IDataContext _ctx;

        #endregion

        #region Constructors

        public ImageService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Methods

        public void Dispose() => _ctx?.Dispose();

        public async Task<Stream> GetImageStreamAsync(int postId)
        {
            var conn = _ctx.GetConnection();
            var cmd = conn.CreateCommand();
            DbDataReader rdr = null;
            StreamWrapper stream = null;
            try
            {
                await conn.OpenAsync();
                cmd.CommandText = "select [Data] from [PostPictures] where [PostId] = @postId";
                cmd.Parameters.Add(new SqlParameter(nameof(postId), postId));
                rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

                if (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    stream = new StreamWrapper(rdr.GetStream(0), rdr, cmd, conn);
                }
            }
            finally
            {
                if (stream == null)
                {
                    rdr?.Dispose();
                    cmd?.Dispose();
                    conn?.Dispose();
                }
            }
            return stream;
        }

        public async Task UpdateImageAsync(int postId, Stream stream)
        {
            var conn = _ctx.GetConnection();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update [PostPictures] set [Data] = @stream where [PostId] = @postId";
                cmd.Parameters.Add(new SqlParameter(nameof(postId), postId));
                cmd.Parameters.Add(new SqlParameter(nameof(stream), stream));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        #endregion
    }
}