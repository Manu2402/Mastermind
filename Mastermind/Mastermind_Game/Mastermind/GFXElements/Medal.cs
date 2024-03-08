using OpenTK;

namespace Mastermind
{
    internal class Medal : GameObject //Unlocked after 100 wins
    {
        private byte winsQuest = 100;

        public Medal(string texturePath) : base(texturePath, DrawLayer.Playground)
        {
            IsActive = Game.WinsConfig.GetWins() >= winsQuest; //100 wins

            sprite.pivot = Vector2.Zero;
            Position = new Vector2(Game.PixelsToUnits(407), Game.PixelsToUnits(677));
        }

        public override void Update()
        {
            IsActive = Game.WinsConfig.GetWins() >= winsQuest; //100 wins
        }

    }
}
