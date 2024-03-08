using System.Collections.Generic;

namespace Mastermind
{
    static class UpdateMgr
    {
        private static List<IUpdatable> items; //All updatable items

        static UpdateMgr()
        {
            items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IUpdatable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (byte i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }
        }
    }
}
