using Microsoft.Xna.Framework.Graphics;

namespace MonoManicMiner.Spectrum
{
    class ScoreRenderer : SpectrumFont
    {
        public ScoreRenderer(Texture2D texture)
            : base(texture)
        {
            UpdateScore(0, 0);
        }

        public void UpdateScore(int score, int hiScore)
        {
            Text = $"`{FontColor.Yellow}High Score {hiScore:000000}   Score {score:000000}";
        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            base.Draw(spriteBatch, scale);
        }
    }
}
