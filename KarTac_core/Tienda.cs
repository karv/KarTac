using KarTac.Equipamento;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System;

namespace KarTac
{
	/// <summary>
	/// Una tienda para el jugador
	/// </summary>
	public class Tienda
	{
		public struct Entrada
		{
			public readonly IItem Objeto;
			public int Cantidad;
			public int Precio;

			public Entrada (IItem objeto, int cantidad, int precio)
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

		/// <summary>
		/// Devuelve la primera entrada de un objeto en esta tienda.
		/// Si no existe devuelve null
		/// </summary>
		/// <param name="key">Objeto</param>
		public Entrada? this [IItem key]
		{
			get
			{
				var pret = artículos.Where (x => key.Equals (x.Objeto)).Cast<Entrada?> ();
				return pret.FirstOrDefault ();
			}
		}
	}

	/// <summary>
	/// Administra lo marcado para comprar
	/// </summary>
	public class Compras
	{
		public Tienda Tienda { get; }

		/// <summary>
		/// A dónde se van las compras
		/// </summary>
		public InventarioClan InvTransferencia { get; }

		Dictionary<IItem, int> comprasMarcadas { get; }

		/// <summary>
		/// Objetos marcados para comprar
		/// </summary>
		public ReadOnlyDictionary<IItem, int> ComprasMarcadas
		{
			get
			{
				return new ReadOnlyDictionary<IItem, int> (comprasMarcadas);
			}
		}

		/// <summary>
		/// Devuelve el costo total de las compras marcadas
		/// </summary>
		public int PorPagar
		{
			get
			{
				int ret = 0;
				foreach (var x in ComprasMarcadas)
				{
					ret += x.Value * Tienda [x.Key].Value.Precio;
				}
				return ret;
			}
		}

		/// <summary>
		/// Agrega items al carro
		/// </summary>
		public void Add (IItem item, int cantidad = 1)
		{
			if (Tienda [item].Value.Precio * cantidad > DineroDisponible)
				throw new Exception ("Dinero disponible < Petición de compra.");
			if (!comprasMarcadas.ContainsKey (item))
				comprasMarcadas.Add (item, 0);
			comprasMarcadas [item] += cantidad;
			if (comprasMarcadas [item] == 0)
				comprasMarcadas.Remove (item);
		}

		/// <summary>
		/// Devuelve el máximo número comprable para un objeto
		/// Toma en cuenta el inventario de la tienda y el dinero restante.
		/// </summary>
		public int MáximoComprable (IItem item)
		{
			var entrada = Tienda [item].Value;
			return Math.Min (entrada.Cantidad, DineroDisponible / entrada.Precio);
		}

		/// <summary>
		/// Devuelve el dinero disponible para más compras
		/// </summary>
		/// <value>The dinero disponible.</value>
		public int DineroDisponible
		{
			get
			{
				return InvTransferencia.Dinero - PorPagar;
			}
		}

		/// <summary>
		/// Ejecuta las compras en un inventorio, y vacía el carro
		/// </summary>
		public void Commit ()
		{
			var dineroRestante = DineroDisponible;
			foreach (var x in ComprasMarcadas)
			{
				for (int i = 0; i < x.Value; i++)
				{
					InvTransferencia.Add (x.Key.Duplicar ());
				}
			}
			InvTransferencia.Dinero = dineroRestante;
		}
	}
}