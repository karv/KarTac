#if DEBUG
using NUnit.Framework;
using KarTac.Recursos;
using System;
using KarTac.Batalla.Objetos;
using Microsoft.Xna.Framework;

namespace KarTac.Test
{
	[TestFixture]
	public class Pruebas
	{
		[Test]
		public void TestRecHP ()
		{
			/*
			HP hp;
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
			*/
		}

		[Test]
		public void TestPared ()
		{
			var p = new Pared (
				        new Vector2 (5, 5),
				        new Vector2 (6, 5));

			var n = p.Normal (new Vector2 (0, 5));
			Console.WriteLine ((n));

			Assert.IsTrue (p.Corta (new Segmento (
				new Vector2 (5.5f, 0),
				new Vector2 (
					0,
					10))));

			Assert.IsTrue (!p.Corta (new Segmento (
				new Vector2 (5.5f, 0),
				new Vector2 (
					0,
					1))));
		}
	}
}

#endif