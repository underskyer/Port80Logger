using System;
using System.Linq;
using Unity;
using ПотокОбработкиСобытийПорта;
using NLog.Config;
using NLog.Targets;
using NLog;
using System.IO;
using System.Reflection;
using Ядро;
using СлушательПорта;

namespace ЗапускаемыйМодуль
{
	class Программа
	{
		static readonly IUnityContainer Контейнер = new UnityContainer();
		static void Main()
		{
			СконфигурироватьЛогирование();

			Контейнер
				.ЗарегистрироватьПотокОбработки()
				.ЗарегистрироватьСлушателяПорта();

			var слушатель = Контейнер.Resolve<IСлушательTcp>();
			слушатель.НачатьСлушать().Wait();
		}

		static void СконфигурироватьЛогирование()
		{
			var названиеПродукта = Assembly.GetEntryAssembly().GetCustomAttributes(false)
				.OfType<AssemblyProductAttribute>()
				.FirstOrDefault()
				?.Product;

			var конфигурация = new LoggingConfiguration();
			var файловыйЛог = new FileTarget("логфайл")
			{
				FileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					названиеПродукта,
					"Logs",
					"log.txt")
			};
			var консольныйЛог = new ConsoleTarget("консольный лог");
			Directory.CreateDirectory(Path.GetDirectoryName(файловыйЛог.FileName.ToString().Trim('\'')));
			конфигурация.AddTarget(файловыйЛог);
			конфигурация.AddTarget(консольныйЛог);
			конфигурация.AddRuleForAllLevels(файловыйЛог);
			конфигурация.AddRuleForAllLevels(консольныйЛог);

			LogManager.Configuration = конфигурация;
		}
	}
}
