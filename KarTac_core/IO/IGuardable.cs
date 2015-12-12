using System.IO;

namespace KarTac.IO
{
	public interface IGuardable
	{
		void Guardar (BinaryWriter writer);

		TObj Cargar<TObj> (BinaryReader reader);
	}
}