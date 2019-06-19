using System;
using System.Activities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ЛоггированиеПорта80
{
	class ПотоковыйОбработчикСобытий : IОбработчикСобытий
	{
		readonly WorkflowApplication ПотокОбработки;
		public void ОбработатьСобытие(string событие)
		{
			ПотокОбработки.ResumeBookmark("ПолученоСобытие", событие);
		}
	}
}
