namespace Mastermind
{
    interface IDrawable //Drawable Objects
    {
        DrawLayer Layer { get; }

        void Draw();
    }
}
