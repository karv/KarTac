namespace KarTac.Personajes
{
	public struct Empuje
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
	}
}