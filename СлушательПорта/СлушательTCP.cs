using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ядро;

namespace СлушательПорта
{
	class СлушательTcp : IСлушательTcp
	{
		static readonly int НомерПорта = 80;
		readonly IОбработчикСобытий ОбработчикСобытий;

		public СлушательTcp(IОбработчикСобытий обработчикСобытий)
		{
			ОбработчикСобытий = обработчикСобытий ?? throw new ArgumentNullException(nameof(обработчикСобытий));
		}

		public async Task НачатьСлушать()
		{
			var tcpListener = TcpListener.Create(НомерПорта);
			tcpListener.Start();

			while (true)
			{
				var tcpClient = await tcpListener.AcceptTcpClientAsync();
				var событие = "Подключился клиент: " + tcpClient;
				ОбработчикСобытий.ОбработатьСобытие(событие);
				using (var reader = new StreamReader(tcpClient.GetStream()))
					событие = await reader.ReadToEndAsync();
				ОбработчикСобытий.ОбработатьСобытие(событие);
			}
		}
	}
}
