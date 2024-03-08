using System.Collections.Generic;

namespace Mastermind
{
    static class BlockMgr
    {
        private static List<Block> blocks;
        private static byte numBlocks = 36;

        static BlockMgr()
        {
            blocks = new List<Block>(numBlocks);
        }

        public static void Add(Block block)
        {
            blocks.Add(block);
        }

        public static void ClearAll()
        {
            for (byte i = 0; i < blocks.Count; i++)
            {
                blocks?[i].Destroy();
            }

            blocks.Clear();
            Block.ResetIndex();
        }

    }
}
