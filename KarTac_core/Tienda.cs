using KarTac.Equipamento;
using System.Collections.Generic;

namespace KarTac
{
	/// <summary>
	/// Una tienda para el jugador
	/// </summary>
	public class Tienda
	{
		public struct Entrada
		{
			public IItem Objeto;
			public int Cantidad;
			public float Precio;

			public Entrada (IItem objeto, int cantidad, float precio)
			{
				Objeto = objeto;
				Cantidad = cantidad;
				Precio = precio;
			}
		}

		readonly List<Entrada> artículos = new List<Entrada> ();

		/// <summary>
		/// Devuelve la lista de artículos ofrecidos
		/// </summary>
		/// <value>The ofrecidos.</value>
		public ICollection<IItem> Ofrecidos
		{
			get
			{
				var ret = new List<IItem> ();
				foreach (var item in artículos)
				{
					if (!ret.Contains (item.Objeto))
						ret.Add (item.Objeto);
				}
				return ret.AsReadOnly ();
			}
		}

		/// <summary>
		/// Devuelve una colección sus artículos
		/// </summary>
		/// <value>The artículos.</value>
		public ICollection<Entrada> Artículos
		{
			get
			{
				return artículos.AsReadOnly ();
			}
		}

		public Entrada? this [IItem key]
		{
			get
			{
				return artículos.Find (x => key.Equals (x.Objeto));
			}
		}
	}

	/// <summary>
	/// Administra lo marcado para comprar
	/// </summary>
	public class Compras
	{
		public Tienda Tienda { get; }

	}
}