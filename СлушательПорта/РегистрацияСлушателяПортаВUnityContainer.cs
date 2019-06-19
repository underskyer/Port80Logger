using Unity;
using Ядро;

namespace СлушательПорта
{
	public static class РегистрацияСлушателяПортаВUnityContainer
	{
		public static IUnityContainer ЗарегистрироватьСлушателяПорта(this IUnityContainer контейнер)
			=> контейнер.RegisterType<IСлушательTcp, СлушательTcp>();
	}
}
