using System.Activities;
using Ядро;

namespace ПотокОбработкиСобытийПорта
{
	class ПотоковыйОбработчикСобытий : IОбработчикСобытий
	{
		readonly WorkflowApplication ПотокОбработки;

		public ПотоковыйОбработчикСобытий()
		{
			ПотокОбработки = new WorkflowApplication(new ПотокОбработки());
		}

		public void ОбработатьСобытие(string событие)
		{
			ПотокОбработки.ResumeBookmark("ПолученоСобытие", событие);
		}
	}
}
