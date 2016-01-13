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
			public readonly Func <IItem> Objeto;
			public int Cantidad;
			public int Precio;
			public string NombreMostrar;

			public Entrada (Func <IItem> objetoCtor,
			                int cantidad,
			                int precio,
			                string nombre)
			{
				Objeto = objetoCtor;
				Cantidad = cantidad;
				Precio = precio;
				NombreMostrar = nombre;
			}

			public override string ToString ()
			{
				return NombreMostrar;
			}
		}

		readonly List<Entrada> artículos = new List<Entrada> ();

		/// <summary>
		/// Devuelve la lista de artículos ofrecidos
		/// </summary>
		/// <value>The ofrecidos.</value>
		public ICollection<Func<IItem>> Ofrecidos
		{
			get
			{
				var ret = new List<Func<IItem>> ();
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
				return artículos;
			}
		}

		/// <summary>
		/// Devuelve la primera entrada de un objeto en esta tienda.
		/// Si no existe devuelve null
		/// </summary>
		/// <param name="key">Objeto</param>
		public Entrada? this [Func <IItem> key]
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
		public struct EntradaUnificada
		{
			readonly Tienda.Entrada baseEntrada;

			public int Marcadas { get; }

			public string Nombre
			{
				get
				{
					return baseEntrada.NombreMostrar;
				}
			}

			public int PrecioUnitario
			{
				get
				{
					return baseEntrada.Precio;
				}
			}

			public int Precio
			{
				get
				{
					return PrecioUnitario * Marcadas;
				}
			}

			public Func<IItem> Objeto
			{
				get
				{
					return baseEntrada.Objeto;
				}
			}

			public EntradaUnificada (Tienda.Entrada baseEntrada, int marcadas)
			{
				this.baseEntrada = baseEntrada;
				Marcadas = marcadas;
			}
		}

		public Compras (Tienda tienda, InventarioClan inv)
		{
			Tienda = tienda;
			InvTransferencia = inv;
			comprasMarcadas = new Dictionary<Func<IItem>, int> ();
		}

		public Tienda Tienda { get; }

		/// <summary>
		/// A dónde se van las compras
		/// </summary>
		public InventarioClan InvTransferencia { get; }

		Dictionary<Func <IItem>, int> comprasMarcadas { get; }

		/// <summary>
		/// Objetos marcados para comprar
		/// </summary>
		public ReadOnlyDictionary<Func <IItem>, int> ComprasMarcadas
		{
			get
			{
				var ret = new Dictionary<Func<IItem>, int> ();
				foreach (var x in Tienda.Ofrecidos)
				{
					int num;
					comprasMarcadas.TryGetValue (x, out num);
					ret.Add (x, num);
				}
				return new ReadOnlyDictionary<Func<IItem>, int> (ret);
			}
		}

		public IReadOnlyCollection<EntradaUnificada> MisCompras
		{
			get
			{
				var ret = new List<EntradaUnificada> ();
				foreach (var x in ComprasMarcadas)
				{
					var entrada = Tienda [x.Key];
					if (entrada.HasValue)
						ret.Add (new EntradaUnificada (
							entrada.Value, x.Value));
					else
						throw new Exception ();
				}
				return ret.AsReadOnly ();
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
		public void Add (Func <IItem> item, int cantidad = 1)
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
		public int MáximoComprable (Func <IItem> item)
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
					InvTransferencia.Add (x.Key ());
				}
			}
			comprasMarcadas.Clear ();
			InvTransferencia.Dinero = dineroRestante;
		}
	}
}