﻿using KarTac.Cliente.Controls.Screens;
using KarTac.Batalla.Shape;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KarTac.Cliente.Controls.Primitivos
{
	public class Forma : SBC
	{
		IShape shape;

		public Forma (IScreen screen, IShape shape)
			: base (screen)
		{
			this.shape = shape;
			Prioridad = 1;
			Color = Color.White;
		}

		Texture2D texture;
		string textureString;

		Rectangle bounds;

		public Color Color { get; set; }

		void construirTextura ()
		{
			var círculo = shape as Círculo;
			if (círculo != null)
			{
				textureString = @"Shapes/Círculo"; // TODO: agregar este Content
				bounds = círculo.MínimoRectángulo ();
				return;
			}
			var rect = shape as Rectángulo;
			if (rect != null)
			{
				textureString = "Rect"; // TODO: cambiar este Content
				bounds = rect;
				return;
			}
		}

		public override void LoadContent ()
		{
			texture = Screen.Content.Load<Texture2D> (textureString);
		}

		public override void Dibujar (GameTime gameTime)
		{
			var bat = Screen.Batch;
			bat.Draw (texture, bounds, Color);
		}

		public override Rectangle GetBounds ()
		{
			return bounds;
		}
	}
}