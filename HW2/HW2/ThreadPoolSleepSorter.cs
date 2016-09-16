using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;

namespace CS422
{
	internal class ThreadSleepSort
	{
		public void Sort (TextWriter output)
		{
			Thread.Sleep ((Data) * 1000);
			output.WriteLine (Data);
		}

		public byte Data { get; set; }
	}

	public class ThreadPoolSleepSorter : IDisposable
	{
		TextWriter outwriter;
		ushort numThreads;
		BlockingCollection<ThreadSleepSort> collection;

		/// <summary>
		/// output: used to display the numbers after sleeping
		/// threadCount: indicates the number of threads to create
		/// </summary>
		/// <param name="output">Output.</param>
		/// <param name="threadCount">Thread count.</param>
		public ThreadPoolSleepSorter (TextWriter output, ushort threadCount)
		{
			if (output == null)
				throw new ArgumentNullException ("output is empty!");

			outwriter = output;

			if (threadCount <= 0)
				threadCount = 64;
			numThreads = threadCount;
			collection = new BlockingCollection<ThreadSleepSort> ();
			try
			{
			// adds/takes and starts threads in collection
			for (int i = 0; i < numThreads; i++) {
				Thread t = new Thread (() => {
					while (true) {
						ThreadSleepSort task = collection.Take ();
						if (null == task)
							break;
						task.Sort (outwriter);
					}
				});
				t.Start ();
			}
			}
			catch(Exception ex){
				ex.ToString ();
			}
		}
		/*public void SleepSortTask(){
			ThreadSleepSort task = collection.Take ();
			if (null == task)
				break;
			
		}*/
		public void Sort (byte[] values)
		{
			if (values == null)
				throw new ArgumentNullException ("values is empty!");
			foreach (var v in values) {
				
				collection.Add (new ThreadSleepSort{ Data = v });
			}
		}
		// dispose of collection
		public void Dispose ()
		{
			try {
				collection.Dispose ();
			} catch {
				throw new ObjectDisposedException ("cannot dispose collection!");
			}
		}


	}
}

