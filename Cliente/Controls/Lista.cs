﻿using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Primitivos;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;
using System;
using OpenTK.Input;

namespace KarTac.Cliente.Controls
{
	
	public class Lista<TObj> : SBC, IList<TObj>
	{
		public struct Entrada
		{
			public TObj Objeto;
			public Color Color;

			public Entrada (TObj obj)
				: this (obj, Color.White)
			{
			}

			public Entrada (TObj obj, Color color)
			{
				Objeto = obj;
				Color = color;
			}
		}

		public Lista (IScreen screen)
			: base (screen)
		{
			Objetos = new List<Entrada> ();
			ColorBG = Color.Blue * 0.3f;
		}

		public override void Dibujar (GameTime gameTime)
		{
			// Dibujar el rectángulo
			var bat = Screen.Batch;

			Formas.DrawRectangle (bat, Bounds, Color.White, noTexture);

			// Background
			bat.Draw (noTexture, Bounds, ColorBG);

			// TODO: Que no se me salga el texto.
			var currY = Bounds.Location.ToVector2 ();
			for (int i = 0; i < Objetos.Count; i++)
			{
				var x = Objetos [i];
				var strTxt = Stringificación (x.Objeto);
				if (i == cursorIndex)
				{
					var rect = Fuente.GetStringRectangle (strTxt, currY);
					bat.Draw (noTexture, rect, Color.White * 0.5f);
				}
				bat.DrawString (Fuente, strTxt, currY, x.Color);
				currY.Y += Fuente.LineHeight;
			}
		}

		public List<Entrada> Objetos { get; }

		public Func<TObj, string> Stringificación { get; set; }

		int cursorIndex;

		/// <summary>
		/// El índice del cursor
		/// </summary>
		public int CursorIndex
		{
			get
			{
				return cursorIndex;
			}
			set
			{
				cursorIndex = Math.Max (Math.Min (Objetos.Count - 1, value), 0);
				AlMoverCursor?.Invoke ();
			}
		}

		public BitmapFont Fuente { get; set; }

		Texture2D noTexture { get; set; }

		public Color ColorBG { get; set; }

		public Rectangle Bounds { get; set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Devuelve o establece si este control puede interactuar por sí mismo con el teclado
		/// </summary>
		public bool InterceptarTeclado = true;

		public override void LoadContent ()
		{
			Fuente = Screen.Content.Load<BitmapFont> ("fonts");
			noTexture = Screen.Content.Load<Texture2D> ("Rect");
		}

		public Key AbajoKey = Key.Down;
		public Key ArribaKey = Key.Up;

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InterceptarTeclado)
			{

				if (InputManager.FuePresionado (AbajoKey))
					CursorIndex++;
				if (InputManager.FuePresionado (ArribaKey))
					CursorIndex--;
			}
		}

		#region IList

		public int IndexOf (TObj item)
		{
			throw new NotImplementedException ();
		}

		public void Insert (int index, TObj item)
		{
			throw new NotImplementedException ();
		}

		public void RemoveAt (int index)
		{
			throw new NotImplementedException ();
		}

		public void Add (TObj item)
		{
			Add (item, Color.White);
		}

		public void Add (TObj item, Color color)
		{
			Add (new Entrada (item, color));
		}

		public void Add (Entrada entrada)
		{
			Objetos.Add (entrada);
		}

		public void Clear ()
		{
			throw new NotImplementedException ();
		}

		public bool Contains (TObj item)
		{
			throw new NotImplementedException ();
		}

		public void CopyTo (TObj[] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (TObj item)
		{
			throw new NotImplementedException ();
		}

		public IEnumerator<TObj> GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		public TObj this [int index]
		{
			get
			{
				throw new NotImplementedException ();
			}
			set
			{
				throw new NotImplementedException ();
			}
		}

		public int Count
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		#endregion

		#region Eventos

		public event Action AlMoverCursor;

		#endregion
	}
}