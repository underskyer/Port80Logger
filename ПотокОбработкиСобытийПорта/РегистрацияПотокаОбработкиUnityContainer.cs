using Unity;
using Ядро;

namespace ПотокОбработкиСобытийПорта
{
	public static class РегистрацияПотокаОбработкиUnityContainer
	{
		public static IUnityContainer ЗарегистрироватьПотокОбработки(this IUnityContainer контейнер)
			=> контейнер.RegisterType<IОбработчикСобытий, ПотоковыйОбработчикСобытий>();
	}
}
