using KarTac.Cliente.Controls.Screens;
using Microsoft.Xna.Framework;
using KarTac.Cliente.Controls.Primitivos;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace KarTac.Cliente.Controls
{
	public class Lista : SBC
	{
		public Lista (IScreen screen)
			: base (screen)
		{
			Objetos = new List<string> ();
		}

		public override void Dibujar (GameTime gameTime)
		{
			// Dibujar el rectángulo
			var bat = Screen.Batch;
			var tl = new Vector2 (Bounds.Left, Bounds.Top);
			var tr = new Vector2 (Bounds.Right, Bounds.Top);
			var br = new Vector2 (Bounds.Right, Bounds.Bottom);
			var bl = new Vector2 (Bounds.Left, Bounds.Bottom);
			Formas.DrawLine (bat, tl, tr, Color.White, Screen.Device);
			Formas.DrawLine (bat, tr, br, Color.White, Screen.Device);
			Formas.DrawLine (bat, br, bl, Color.White, Screen.Device);
			Formas.DrawLine (bat, bl, tl, Color.White, Screen.Device);

			// TODO: Que no se me salga el texto.
			var currY = tl;
			for (int i = 0; i < Objetos.Count; i++)
			{
				var x = Objetos [i];
				if (i == cursorIndex)
				{
					var rect = Fuente.GetStringRectangle (x, currY);
					bat.Draw (bgCursor, rect, Color.White * 0.5f);
				}
				bat.DrawString (Fuente, x, currY, Color.White);
				currY.Y += Fuente.LineHeight;
			}
		}

		public List<string> Objetos { get; }

		//public int LíneasVisibles = 3;
		int cursorIndex = 0;

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

		Texture2D bgCursor { get; set; }

		public Rectangle Bounds { get; set; }

		public override Rectangle GetBounds ()
		{
			return Bounds;
		}

		public bool InterceptarTeclado = true;

		public override void LoadContent ()
		{
			Fuente = Screen.Content.Load<BitmapFont> ("fonts");
			bgCursor = Screen.Content.Load<Texture2D> ("Rect");
		}

		public Keys AbajoKey = Keys.Down;
		public Keys ArribaKey = Keys.Up;

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			if (InterceptarTeclado)
			{
				var kb = Keyboard.GetState ();
				var lastkb = Screen.LastKeyboardState;

				if (kb.IsKeyDown (AbajoKey) && lastkb.IsKeyUp (AbajoKey))
					CursorIndex++;
				if (kb.IsKeyDown (ArribaKey) && lastkb.IsKeyUp (ArribaKey))
					CursorIndex--;
			}
		}

		#region Eventos

		public event Action AlMoverCursor;

		#endregion
	}
}