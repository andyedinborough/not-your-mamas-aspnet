using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace web.Data
{
    public class StreamWrapper : Stream, IDisposable
    {
        #region Fields

        private readonly List<IDisposable> _disposable;
        private readonly Stream _stream;

        #endregion

        #region Constructors

        public StreamWrapper(Stream stream, params IDisposable[] disposable)
        {
            _stream = stream;
            _disposable = disposable.ToList();
        }

        #endregion

        #region Properties

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position
        {
            get => _stream.Position;
            set
            {
                if (!CanSeek || _stream.Position == value) return;
                _stream.Position = value;
            }
        }

        #endregion

        #region Methods

        public override void Flush() => _stream.Flush();

        public override int Read(byte[] buffer, int offset, int count) => _stream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin) => _stream.Seek(offset, origin);

        public override void SetLength(long value) => _stream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) => _stream.Write(buffer, offset, count);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _stream.Dispose();
            if (disposing)
            {
                _disposable.ForEach(Dispose);
            }
        }

        private static void Dispose(IDisposable disposable) => disposable?.Dispose();

        #endregion
    }
}