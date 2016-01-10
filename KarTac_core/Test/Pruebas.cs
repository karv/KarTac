#if DEBUG
using NUnit.Framework;
using KarTac.Recursos;
using System;

namespace KarTac.Test
{
	[TestFixture]
	public class Pruebas
	{
		[Test]
		public void TestRecHP ()
		{
			var hp = new HP ();
			hp.Max = 100;
			hp.Valor = 100;

			hp.AlCambiarValor += delegate
			{
				Console.WriteLine ("Cambio valor HP a " + hp.Valor);
			};

			hp.AlValorCero += () => Console.WriteLine ("Cero HP");

			hp.Valor = 150;
			Assert.AreEqual (100, hp.Valor);
			Assert.Throws<ArgumentOutOfRangeException> (new TestDelegate (() => hp.Max = -100));
			hp.Valor = 30;
			Assert.AreEqual (30, hp.Valor);
			hp.Valor = -10;
			Assert.AreEqual (0, hp.Valor);
		}
	}
}

#endif