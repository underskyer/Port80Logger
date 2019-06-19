using System.Activities;

namespace ПотокОбработкиСобытийПорта
{
	public sealed class ОжиданиеСобытия : NativeActivity<string>
	{
		[RequiredArgument]
		public InArgument<string> Закладка { get; set; }

		protected override void Execute(NativeActivityContext контекст)
		{
			string названиеЗакладки = Закладка.Get(контекст);
			контекст.CreateBookmark(названиеЗакладки, (cont, _, state) => Result.Set(cont, state?.ToString()));
		}

		protected override bool CanInduceIdle => true;
	}
}
