using System;

namespace CS422
{
	public class PCQueue
	{
		Node F, B, E;

		public PCQueue ()
		{
			F = B = E = new Node (true); // only emptyNode exists at start
		}
		// add to front of queue
		public bool Dequeue (ref int out_value)
		{
			//empty
			if (object.ReferenceEquals (B, E))
				return false;
			//not empty
			else {
				out_value = F.pNext.data;
				E = F = F.pNext;
				return true;
			}
		}
		// add to back of queue
		public void Enqueue (int dataValue)
		{
			B.pNext = new Node (dataValue);
			B = B.pNext;
		}

		public class Node
		{
			public int data;
			public Node pNext;
			public bool emptyNode;

			public Node (bool isEmpty)
			{
				isEmpty = true;
			}

			public Node (int dataValue)
			{
				data = dataValue;
				emptyNode = false;
			}

		}
	}
}

