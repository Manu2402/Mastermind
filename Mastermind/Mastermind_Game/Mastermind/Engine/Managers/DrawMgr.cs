using System.Collections.Generic;

namespace Mastermind
{
    enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, LAST }

    static class DrawMgr
    {
        private static List<IDrawable>[] items; //All drawable items

        static DrawMgr()
        {
            items = new List<IDrawable>[(byte)DrawLayer.LAST];

            for (byte i = 0; i < items.Length; i++)
            {
                items[i] = new List<IDrawable>();
            }
        }

        public static void AddItem(IDrawable item)
        {
            items[(byte)item.Layer].Add(item);
        }

        public static void RemoveItem(IDrawable item)
        {
            items[(byte)item.Layer].Remove(item);
        }

        public static void ClearAll()
        {
            for (byte i = 0; i < items.Length; i++)
            {
                items[i].Clear();
            }
        }

        public static void Draw()
        {
            for (byte i = 0; i < items.Length; i++)
            {
                for (byte j = 0; j < items[i].Count; j++)
                {
                    items[i][j].Draw();
                }
            }
        }
    }
}
