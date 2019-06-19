using NLog;
using System.Activities;

namespace ПотокОбработкиСобытийПорта
{
	public sealed class ЛогированиеСобытия : NativeActivity
	{
		[RequiredArgument]
		public InArgument<string> Закладка { get; set; }

		protected override void Execute(NativeActivityContext контекст)
		{
			var событие = контекст.GetValue(Закладка);
			LogManager.GetCurrentClassLogger().Debug(событие);
		}

		protected override bool CanInduceIdle => true;

	}
}
