using System.Threading.Tasks;

namespace ЛоггированиеПорта80
{
	public interface IОбработчикСобытий
	{
		void ОбработатьСобытие(string событие);
	}
}