namespace KarTac.Personajes
{
	public struct ModificadorAtributo : IModificador
	{
		public ModificadorAtributo (string atributo, float delta)
		{
			Atributo = atributo;
			Delta = delta;
		}

		public string Atributo { get; }

		public float Delta { get; }

		public override string ToString ()
		{
			return string.Format ("{0}: +{1}", Atributo, Delta);
		}
	}
}