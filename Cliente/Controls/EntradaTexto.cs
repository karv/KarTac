﻿using KarTac.Cliente.Controls.Screens;
using KarTac.Cliente.Controls.Primitivos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using OpenTK.Input;
using System.Collections.Generic;

namespace KarTac.Cliente.Controls
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : SBC
	{
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			Texto = "";

			TeclasPermitidas = new Dictionary<Key, string> ();

			// Construir teclas permitidas
			for (Key i = Key.A; i <= Key.Z; i++)
			{
				TeclasPermitidas.Add (i, i.ToString ());
			}
			for (Key i = Key.Number0; i <= Key.Number9; i++)
			{
				TeclasPermitidas.Add (i, ((int)i - 109).ToString ());
			}
		}

		#region Estado

		public string Texto { get; set; }

		/// <summary>
		/// Si el control debe responder a el estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		#endregion

		public Point Pos
		{
			get
			{
				return Bounds.Location;
			}
			set
			{
				Bounds = new Rectangle (value, Bounds.Size);
			}
		}

		/// <summary>
		/// Color del contorno
		/// </summary>
		public Color ColorContorno = Color.White;
		/// <summary>
		/// Color del texto
		/// </summary>
		public Color ColorTexto = Color.White;
		Texture2D contornoTexture;
		BitmapFont fontTexture;

		/// <summary>
		/// Límites de el control
		/// </summary>
		public Rectangle Bounds { get; set; }

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			Formas.DrawRectangle (bat, GetBounds (), ColorContorno, contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos.ToVector2 (), ColorTexto);
		}

		public override void LoadContent ()
		{
			contornoTexture = Screen.Content.Load<Texture2D> ("Rect");
			fontTexture = Screen.Content.Load<BitmapFont> ("fonts");
		}

		public IDictionary<Key, string> TeclasPermitidas { get; set; }

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

			if (CatchKeys)
			{
				foreach (var k in TeclasPermitidas)
				{
					if (InputManager.FuePresionado (k.Key))
					{
						var tx = k.Value;
						if (!InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftLeft) && !InputManager.EstadoActualTeclado.IsKeyDown (Key.ShiftRight))
						{
							tx = tx.ToLower ();
						}
						Texto += tx;
					}
				}
				if (InputManager.FuePresionado (Key.Space))
				{
					Texto += " ";
				}
				if (InputManager.FuePresionado (Key.Back))
				{
					if (Texto.Length > 0)
						Texto = Texto.Remove (Texto.Length - 1);
				}
			}
		}

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}
	}
}