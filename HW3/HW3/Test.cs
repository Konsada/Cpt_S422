using NUnit.Framework;
using System;
using System.Net;
using System.Net.Sockets;

namespace CS422
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			
		}
	}
	public class TestConnection
	{
		TcpClient client;
		byte[] data;
		string defaultInput = "GET http://github.com HTTP/1.1\r\n\r\n";
		int bufferLength = 4096;

		TestConnection()
		{
			client = new TcpClient ("localhost", 4220);
			data = System.Text.Encoding.ASCII.GetBytes (defaultInput);
			bufferLength = data.Length;
		}
		TestConnection(int bufLength)
		{
			client = new TcpClient ("localhost", 4220);
			data = System.Text.Encoding.ASCII.GetBytes (defaultInput);
			bufferLength = bufLength;
		}
		TestConnection(int port)
		{
			client = new TcpClient ("localhost", port);
			data = System.Text.Encoding.ASCII.GetBytes (defaultInput);
			bufferLength = data.Length;
		}
		TestConnection(byte[] buffer, int port)
		{
			client = new TcpClient ("localhost", port);
			data = buffer;
			bufferLength = data.Length;
		}
		TestConnection(string input)
		{
			client = new TcpClient ("localhost", port);
			data = System.Text.Encoding.ASCII.GetBytes (input);
			bufferLength = data.Length;
		}
		TestConnection(int port, string input)
		{
			client = new TcpClient ("localhost", 4220);
			data = System.Text.Encoding.ASCII.GetBytes (input);
			bufferLength = data.Length;
		}
		public bool EstablishConnection()
		{
			try
			{
			NetworkStream ns = client.GetStream ();
				int offset = 0;
				do
				{
					ns.Write (data, offset, bufferLength);
					offset+=bufferLength;
				}while(offset < data.Length);

			
			}
			catch(Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}
	}
}

