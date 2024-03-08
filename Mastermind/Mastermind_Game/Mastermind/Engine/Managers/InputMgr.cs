using System.Collections.Generic;

namespace Mastermind
{
    static class InputMgr
    {
        private static List<IInputable> items; //All inputable items

        static InputMgr()
        {
            items = new List<IInputable>();
        }

        public static void AddItem(IInputable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IInputable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Input()
        {
            for (byte i = 0; i < items.Count; i++)
            {
                items[i].Input();
            }
        }

    }
}
