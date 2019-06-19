using System;
using System.Activities;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using ПотокОбработкиСобытийПорта;

namespace ЛоггированиеПорта80
{
	class Программа
	{
		static async Task Main(string[] args)
		{
			СоздатьИСконфигурироватьКонтейнер();
			var слушатель = Контейнер.Resolve<IСлушательTcp>();

			await слушатель.НачатьСлушать();

			//var handles = new [] { syncEvent, idleEvent };
			//while (WaitHandle.WaitAny(handles) != 0)
			//{
			//	int guess;
			//	while (!int.TryParse(Console.ReadLine(), out guess))
			//		Console.WriteLine("Please enter an integer.");

			//	wfApp.ResumeBookmark("EnterGuess", guess);
			//}



				//var port = 80;
				//var localAddr = IPAddress.Parse("127.0.0.1");

				//// TcpListener server = new TcpListener(port);
				//var server = new TcpListener(localAddr, port);

				//// Start listening for client requests.
				//server.Start();




				//new СлушательПорта().Слушать();
			}
	}
}
