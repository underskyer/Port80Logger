using System.Threading.Tasks;

namespace ЛоггированиеПорта80
{
	internal interface IСлушательTcp
	{
		Task НачатьСлушать();
	}
}