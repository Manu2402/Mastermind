using System.Collections.Generic;

namespace Mastermind
{
    static class HoverMgr
    {
        private static List<IHoverable> items; //All hoverable items

        static HoverMgr()
        {
            items = new List<IHoverable>();
        }

        public static void AddItem(IHoverable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IHoverable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Hover()
        {
            for (byte i = 0; i < items.Count; i++)
            {
                items[i].Hover();
            }
        }
    }
}
