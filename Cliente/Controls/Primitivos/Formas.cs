/* Clase proporcionada por http://gamedev.stackexchange.com/users/4568/ken
* en http://gamedev.stackexchange.com/questions/44015/how-can-i-draw-a-simple-2d-line-in-xna-without-using-3d-primitives-and-shders
*/
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace KarTac.Cliente.Controls.Primitivos
{
	public static class Formas
	{
		/// <summary>
		/// Devuelve una textura 1x1 blanco
		/// </summary>
		public static Texture2D PixelTexture (GraphicsDevice dev)
		{
			var ret = new Texture2D (dev, 1, 1);
			ret.SetData<Color> (
				new [] { Color.White });
			return ret;
		}


		public static void DrawLine (SpriteBatch sb, Vector2 start, Vector2 end, Color color, Texture2D text)
		{
			Vector2 edge = end - start;
			// calculate angle to rotate line
			float angle = (float)Math.Atan2 (edge.Y, edge.X);

			sb.Draw (text,
			         start,			         			   
			         new Rectangle (
				         (int)start.X,
				         (int)start.Y,
				         (int)edge.Length (),
				         1),
			         color,
			         angle,
			         Vector2.Zero,
			         1.0f,
			         SpriteEffects.None,
			         0);

		}

		/// <summary>
		/// Dibuja un rectángulo en un SpriteBatch
		/// </summary>
		public static void DrawRectangle (SpriteBatch bat, Rectangle rect, Color color, Texture2D textura)
		{
			var tl = new Vector2 (rect.Left, rect.Top);
			var tr = new Vector2 (rect.Right, rect.Top);
			var bl = new Vector2 (rect.Left, rect.Bottom);
			var br = new Vector2 (rect.Right, rect.Bottom);

			DrawLine (bat, tl, tr, color, textura);
			DrawLine (bat, tr, br, color, textura);
			DrawLine (bat, br, bl, color, textura);
			DrawLine (bat, bl, tl, color, textura);

		}
	}
}