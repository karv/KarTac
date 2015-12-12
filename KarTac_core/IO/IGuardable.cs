using System.IO;

namespace KarTac.IO
{
	public interface IGuardable
	{
		void Guardar (BinaryWriter writer);

		void Cargar (BinaryWriter writer);
	}
}