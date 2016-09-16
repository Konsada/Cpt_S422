using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CS422
{
	public class WebServer
	{
		byte[] buf;
		byte[] rbytes;
		string requestLine;

		public WebServer ()
		{
			
		}

		public static bool Start (int port, string responseTemplate)
		{
			TcpListener listener = null;
			if (port < 0 || port > 65535) {
				throw new ArgumentOutOfRangeException ();
			}
			if (null == responseTemplate) {
				throw new ArgumentNullException ();
			}

			try {
				listener = new TcpListener (IPAddress.Any, port);
				listener.Start ();
				TcpClient client = listener.AcceptTcpClient ();
				NetworkStream ns = client.GetStream ();
				ns.ReadTimeout = 5 * 1000;

				if(!TestConnection(ns))
					return false;
				
				rbytes = Encoding.ASCII.GetBytes (string.Format (responseTemplate, "11169575", DateTime.Now.ToString (), getRequestURI (requestLine)));
				ns.Write (rbytes, 0, rbytes.Length);
				ns.Dispose ();
				ns.Close();
			} catch (HttpListenerException ex) {
				ex.ToString ();
			}
			return true;
		}
		protected bool TestConnection(NetworkStream ns)
		{
			int streamOffset = 0;
			byte[] buf = new byte[4096];
			int read = 0;

			do {
				try {
					read = ns.Read (buf, streamOffset, buf.Length);
				} catch (Exception ex) {
					ex.ToString ();
				}
				streamOffset = read;
				Array.Resize(buf, buf.Length+4096);
				if (read + streamOffset <= 0) {
					ns.Close ();
					return false;
				}
			} while(ns.DataAvailable);

			string request = Encoding.ASCII.GetString (buf, 0, buf.Length);
			requestLine = getRequestLine (request);

			if (!validateRequest (requestLine))
				return false;
			else
				return true;

		}
		protected bool validateRequest(string input)
		{
			if ((getRequestMethod (input) != "GET") && (getRequestVersion (input) != "HTTP/1.1")) {
				return false;
			}
		}
		private static string getRequestLine (string s)
		{
			string[] requestLine = new string[2];
			requestLine = s.Split (new string[]{ "\r\n" }, 2, StringSplitOptions.None);
			return requestLine [0];
		}

		private static string getHeader (string s)
		{
			string[] header = new string[2];
			header = s.Split (new string[]{ "\r\n\r\n" }, 2, StringSplitOptions.None);
			return header [0];
		}

		private static string getRequestMethod (string requestLine)
		{
			string[] method = new string[3];
			method = requestLine.Split (new string[]{ " " }, 3, StringSplitOptions.None);
			return method [0];
		}

		private static string getRequestURI (string requestLine)
		{
			string[] uri = new string[3];
			uri = requestLine.Split (new string[]{ " " }, 3, StringSplitOptions.None);
			return uri [1];
		}

		private static string getRequestVersion (string requestLine)
		{
			string[] version = new string[3];
			version = requestLine.Split (new string[]{ " " }, 3, StringSplitOptions.None);
			return version [2];
		}
	}
}

