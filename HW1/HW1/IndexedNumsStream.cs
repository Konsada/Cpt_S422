using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CS422
{
	public class IndexedNumsStream : Stream
	{
		private long currentPosition;
		private long streamLength;

		public IndexedNumsStream(long length)
		{
			if(length < 0)
				streamLength = 0;
			else
				streamLength = length;
			currentPosition = 0;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override long Length
		{
			get { return streamLength; }
		}

		public override long Position
		{
			get { return currentPosition; }

			set
			{
				if (value >= 0)
				{
					if (value > streamLength)
					{
						currentPosition = streamLength;
					}
					else
					{
						currentPosition = value;
					}
				}
				else
					currentPosition = 0;
			}
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count < 0)
				count = 0;
			if (offset < 0)
				offset = 0;
			if ((count + offset) > streamLength)
				throw new ArgumentException ("Exceeded streamLength");

			int i = 0;
			for (i = 0; i < count; i++)
			{
				buffer[i+offset] = (byte)(i % 256);
				currentPosition++;
			}
			return i;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if(SeekOrigin.Begin == origin) // Seek origin passed is the begining of the stream
			{
				if(offset > streamLength)
				{
					return 0;
				}
				else
				{
					if(offset < 0)
					{
						return 0;
					}
					else
					{
						currentPosition = offset;
					}
				}
			}
			else if(SeekOrigin.End == origin) // seek origin passed is the end of the stream
			{
				if(offset > 0)
				{
					return 0;
				}
				else if((offset + streamLength) < 0)
				{
					return 0;
				}
				else
				{
					currentPosition = streamLength + offset;
					return currentPosition;
				}
			}
			else // see origin passed is current posisiton of the stream
			{
				if((offset + currentPosition) < 0)
				{
					return 0;
				}
				else if((offset + currentPosition) >= streamLength)
				{
					return 0;
				}
				else
				{
					currentPosition = currentPosition + offset;
				}
			}
			return currentPosition;

		}

		public override void SetLength(long value)
		{
			if (value < 0)
				streamLength = 0;
			else
				streamLength = value;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
		}
	}
}