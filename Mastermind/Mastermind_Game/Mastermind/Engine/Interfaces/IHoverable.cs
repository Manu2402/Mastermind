namespace Mastermind
{
    internal interface IHoverable //Interface for all objs that have "hover effect" with mouse cursor
    {
        bool IsHover { get; set; }

        void Hover();
    }
}
