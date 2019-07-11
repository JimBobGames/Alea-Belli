using Alea_Belli.Core.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.XNA;
using Alea_Belli.Core.Network;

namespace Alea_Belli.Rendering
{
    public class GameRenderer
    {
        /// <summary>
        /// Holds the regimental textures
        /// </summary>
        private static Dictionary<int, RenderTarget2D> regimentTextures = new Dictionary<int, RenderTarget2D>();

        public static float ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (float) (radians);
        }

        public static void DrawRegimentToTexture(Regiment r, SpriteBatch spriteBatch, SpriteFont counterFont)
        {
            if (r == null)
            {
                return;
            }
            switch (r.RegimentType)
            {
                case RegimentType.LineInfantry:
                    DrawLineRegimentToTexture(r, spriteBatch, counterFont);
                    break;
                case RegimentType.HeavyCavalry:
                    DrawCavalryRegimentToTexture(r, spriteBatch, counterFont);
                    break;
                case RegimentType.LightCavalry:
                    DrawCavalryRegimentToTexture(r, spriteBatch, counterFont);
                    break;

            }
        }


        public static void DrawArtilleryRegimentToTexture(Regiment r, SpriteBatch spriteBatch, SpriteFont counterFont)
        {

        }


        public static void DrawCavalryRegimentToTexture(Regiment r, SpriteBatch spriteBatch, SpriteFont counterFont)
        {
            int width = r.CurrentWidth;
            int height = r.CurrentHeight;

            Point topleft = new Point(0, 0);
            Point bottomleft = new Point(0, 0 + 60);

            Point size = new Point(width, height + 20);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(topleft, size), Color.White);

            size = new Point(width, height);
            Primitives2D.FillTriangle(spriteBatch, new Rectangle(topleft, size), Color.Red, 0);

            Vector2 textLoc = new Vector2(0, 100);
            spriteBatch.DrawString(counterFont, r.ShortName, textLoc, Color.Black);

        }

        public static void DrawLineRegimentToTexture(Regiment r, SpriteBatch spriteBatch, SpriteFont counterFont)
        {
            int width = r.CurrentWidth;
            int height = r.CurrentHeight;

            Point topleft = new Point(0, 0);
            Point bottomleft = new Point(0, 0 + 60);

            Point size = new Point(width, height + 20);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(topleft, size), Color.White);


            size = new Point(width, height);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(topleft, size), Color.Red);

            Vector2 textLoc = new Vector2(0, 100);

            spriteBatch.DrawString(counterFont, r.ShortName, textLoc, Color.Black);
        }

        /// <summary>
        /// For each regiment ensure a texture exists and is up to date
        /// </summary>
        /// <param name="abgame"></param>
        internal static void UpdateAllRegimentalTextures(IAleaBelliGame abgame, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch, SpriteFont CounterFont)
        {
            foreach(Regiment r in abgame.AllRegiments)
            {
                RenderTarget2D renderTarget;
                // does the texture exists or is the regiment 'dirty'
                if (regimentTextures.TryGetValue(r.RegimentId, out renderTarget) == false)
                {
                    // create the texture
                    renderTarget = new RenderTarget2D(
                    GraphicsDevice,
                    200,
                    200,
                    false,
                    GraphicsDevice.PresentationParameters.BackBufferFormat,
                    DepthFormat.Depth24);

                    // add to the structure
                    regimentTextures[r.RegimentId] = renderTarget;

                    // redraw
                    r.IsDirty = false;

                    UpdateRegimentalTexture(r, renderTarget, GraphicsDevice, spriteBatch, CounterFont);

                }
                else if (r.IsDirty)
                {
                    // redraw
                    r.IsDirty = false;

                    UpdateRegimentalTexture(r, renderTarget, GraphicsDevice, spriteBatch, CounterFont);
                }
            }
        }

        /// <summary>
        /// Draws the entire scene in the given render target.
        /// </summary>
        /// <returns>A texture2D with the scene drawn in it.</returns>
        protected static void UpdateRegimentalTexture(Regiment r, RenderTarget2D renderTarget, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch, SpriteFont CounterFont)
        {
            // Set the render target
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            // Draw the scene
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //DrawModel(model, world, view, projection);


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, DepthStencilState.Default,
                RasterizerState.CullNone);

            GameRenderer.DrawRegimentToTexture(r, spriteBatch, CounterFont);
            spriteBatch.End();

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
        }

        internal static void DrawAllRegiments(SinglePlayerAleaBelliGame abgame, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            foreach (Regiment r in abgame.AllRegiments)
            {
                RenderTarget2D renderTarget;
                // does the texture exists or is the regiment 'dirty'
                if (regimentTextures.TryGetValue(r.RegimentId, out renderTarget))
                {
                    GameRenderer.DrawRegimentFromTexture(r, spriteBatch, renderTarget);
                }
            }
        }

        internal static void DrawRegimentFromTexture(Regiment r, SpriteBatch spriteBatch, RenderTarget2D renderTarget)
        {
            int width = r.CurrentWidth;
            int height = r.CurrentHeight + 20;

            spriteBatch.Draw(renderTarget, new Rectangle(r.MapX, r.MapY, width, height), new Rectangle(0, 0, width, height),
                Color.White,
                GameRenderer.ConvertDegreesToRadians(r.FacingInDegrees),
                Vector2.Zero,
                SpriteEffects.None,
                0
                );
        }
    }
}
