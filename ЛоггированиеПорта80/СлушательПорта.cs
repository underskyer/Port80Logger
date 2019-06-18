using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ЛоггированиеПорта80
{
	class СлушательПорта
	{
		public ManualResetEvent allDone = new ManualResetEvent(false);
		public event EventHandler СобытиеВПорту;
		public void Слушать()
		{
			var ipHostInfo = Dns.Resolve(Dns.GetHostName());
			var ipAddress = ipHostInfo.AddressList.First(adr => adr.AddressFamily == AddressFamily.InterNetwork);
			var localEndPoint = new IPEndPoint(ipAddress, 11000);

			using (var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true)
				{
					// Set the event to nonsignaled state.  
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.  
					allDone.WaitOne();
				}
			}
		}

		public void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.  
			allDone.Set();

			// Get the socket that handles the client request.  
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar)
		{
			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			var state = (StateObject)ar.AsyncState;
			var handler = state.workSocket;

			// Read data from the client socket.   
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There  might be more data, so store the data received so far.  
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				// Check for end-of-file tag. If it is not there, read   
				// more data.  
				var content = state.sb.ToString();
				if (content.IndexOf("<EOF>") > -1)
					СобытиеВПорту?.Invoke(this, EventArgs.Empty);
				else
					handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
			}
		}

		//private static void Send(Socket handler, String data)
		//{
		//	// Convert the string data to byte data using ASCII encoding.  
		//	byte[] byteData = Encoding.ASCII.GetBytes(data);

		//	// Begin sending the data to the remote device.  
		//	handler.BeginSend(byteData, 0, byteData.Length, 0,
		//		new AsyncCallback(SendCallback), handler);
		//}

	}

}
