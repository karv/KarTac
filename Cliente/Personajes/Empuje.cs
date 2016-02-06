using KarTac.IO;

namespace KarTac.Personajes
{
	public struct Empuje : IGuardable
	{
		public float HaciaEnemigo;
		public float HaciaAliado;
		public float Masa;

		public Empuje (float haciaEnemigo, float haciaAliado, float masa)
		{
			HaciaEnemigo = haciaEnemigo;
			HaciaAliado = haciaAliado;
			Masa = masa;
		}

		public void Guardar (System.IO.BinaryWriter writer)
		{
			writer.Write (HaciaEnemigo);
			writer.Write (HaciaAliado);
			writer.Write (Masa);
		}

		public void Cargar (System.IO.BinaryReader reader)
		{
			HaciaEnemigo = reader.ReadSingle ();
			HaciaAliado = reader.ReadSingle ();
			Masa = reader.ReadSingle ();
		}
	}
}