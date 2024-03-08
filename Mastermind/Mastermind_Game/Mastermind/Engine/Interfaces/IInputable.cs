namespace Mastermind
{
    internal interface IInputable //Objects that are interactable like buttons and slots ecc...
    {
        void Input();

        bool IsClicked { get; set; }
    }
}
