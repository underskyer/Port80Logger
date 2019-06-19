using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ЛоггированиеПорта80
{
	public class СлушательTcp : IСлушательTcp
	{
		readonly IОбработчикСобытий ОбработчикСобытий;

		public СлушательTcp(IОбработчикСобытий обработчикСобытий)
		{
			ОбработчикСобытий = обработчикСобытий ?? throw new ArgumentNullException(nameof(обработчикСобытий));
		}

		public async Task НачатьСлушать()
		{
			var tcpListener = TcpListener.Create(11000);
			tcpListener.Start();

			while (true)
			{
				var tcpClient = await tcpListener.AcceptTcpClientAsync();
				var событие = (string)null;
				using (var reader = new StreamReader(tcpClient.GetStream()))
					событие = await reader.ReadToEndAsync();
				ОбработчикСобытий.ОбработатьСобытие(событие);
			}
		}
	}
}
