using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace CS422
{
	public class NumberedTextWriter: TextWriter
	{
		private int currentLineNumber;
		private TextWriter _wrapThis;

		public NumberedTextWriter(TextWriter wrapThis)
		{
			currentLineNumber = 1;
			_wrapThis = wrapThis;
		}
		public NumberedTextWriter(TextWriter wrapThis, int startingLineNumber)
		{
			if (startingLineNumber < 0)
				currentLineNumber = 1;
			else
				currentLineNumber = startingLineNumber;
			_wrapThis = wrapThis;
		}
		public override Encoding Encoding
		{
			get{return _wrapThis.Encoding;}
		}
		public override void WriteLine(string value)
		{
			if(!value.GetType().Equals(typeof(string)))
				_wrapThis.WriteLine(currentLineNumber.ToString() + ": ");
			else
				_wrapThis.WriteLine(currentLineNumber.ToString() + ": " + value);
			currentLineNumber++;
		}
	}
}