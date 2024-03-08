using OpenTK;

namespace Mastermind
{
    public struct MyTupla<T, U> //Tupla with one couple generic items
    {
        public T Item1;
        public U Item2;

        public MyTupla(T a, U b)
        {
            Item1 = a;
            Item2 = b;
        }
    }

    static class SlotMgr //Engine Class
    {
        private static PlaySlot[] slots;
        private static byte numSlots;

        private static HiddenSlot[] hiddenSlots;
        private static byte numHiddenSlots;

        private static byte rows;
        private static byte cols;

        private static Slot selectedSlot;

        public static sbyte PlayIndex { get => Game.Indicator.playIndex; set => Game.Indicator.playIndex = value; }
        public static bool PlayIndexPositive { get => PlayIndex >= 0; }

        public static void Init()
        {
            rows = 9;
            cols = 4;
            numSlots = 36;

            short[] posX = new short[] { 108, 178, 248, 318 };
            short[] posY = new short[] { 627, 556, 485, 414, 344, 274, 205, 134, 65 };

            slots = new PlaySlot[numSlots];

            //Up-Right
            for (byte i = 0; i < rows; i++)
            {
                for (byte j = 0; j < cols; j++)
                {
                    byte index = (byte)(i * cols + j);
                    slots[index] = new PlaySlot("Body");
                    slots[index].Position = new Vector2(Game.PixelsToUnits(posX[j]), Game.PixelsToUnits(posY[i]));
                    slots[index].SelectorSprite.position = slots[index].Position;
                }
            }

            InitHiddenSlots();
        }

        public static void SelectSlotOnIndicatorRow()
        {
            //Up-Right
            if (PlayIndexPositive)
            {
                for (byte i = 0; i < cols; i++)
                {
                    byte index = (byte)(PlayIndex * cols + i);
                    slots[index].Input(); //Hover and selection
                }
            }
        }

        public static void SetColor(Colors color)
        {
            //Change color on selected slot, if exists

            if (GetSelectedSlot())
            {
                selectedSlot.Color?.Destroy();
                GenerateColor(color);
            }
        }

        private static void GenerateColor(Colors color)
        {
            selectedSlot.Color = new Color(color.ToString(), color);
            selectedSlot.Color.Pivot = new Vector2(-0.04f, selectedSlot.HalfHeight - 0.046f);
            selectedSlot.Color.Position = selectedSlot.Position;
        }

        //Override --> Return a new Color for HiddenSlots.
        private static void GenerateColor(this HiddenSlot selectedSlot, int index)
        {
            selectedSlot.Color = new Color(((Colors)index).ToString(), (Colors)index);
            selectedSlot.Color.Pivot = new Vector2(-0.04f, selectedSlot.HalfHeight - 0.046f);
            selectedSlot.Color.Position = selectedSlot.Position;

            selectedSlot.Color.IsActive = true; //Trigger for "secret code" colors
        }

        private static bool GetSelectedSlot()
        {
            //Exploits PlayIndex var to cycle just slots of the active row
            for (byte i = 0; i < cols; i++)
            {
                byte index = (byte)(PlayIndex * cols + i);
                if (slots[index].IsSelected)
                {
                    selectedSlot = slots[index];
                    return true;
                }
            }

            return false;
        }

        public static void DisableAllSlots()
        {
            //Up-Right
            for (byte i = 0; i < cols; i++)
            {
                byte index = (byte)(PlayIndex * cols + i);
                slots[index].IsSelected = false;
            }
        }

        private static void DisableAllSlotsForFollowingRow()
        {
            //Up-Right
            if (PlayIndexPositive)
            {
                for (byte i = 0; i < cols; i++)
                {
                    byte index = (byte)(PlayIndex * cols + i);

                    //Add "X" on unused slots
                    if (slots[index].Color == null)
                    {
                        selectedSlot = slots[index];
                        GenerateColor(Colors.X);
                    }

                    slots[index].IsSelected = false;
                }
            }
        }

        public static void FollowingRow()
        {
            Game.Indicator.SetPosition(++PlayIndex);
        }

        private static void InitHiddenSlots()
        {
            numHiddenSlots = 4;

            short[] posX = new short[] { 108, 178, 248, 318 };
            short posY = 719;

            hiddenSlots = new HiddenSlot[numHiddenSlots];

            for (byte i = 0; i < numHiddenSlots; i++)
            {
                hiddenSlots[i] = new HiddenSlot("Body");
                hiddenSlots[i].Position = new Vector2(Game.PixelsToUnits(posX[i]), Game.PixelsToUnits(posY));
                hiddenSlots[i].SetQuestionPos();
            }
        }

        public static void GenerateSecretCode()
        {
            for (byte i = 0; i < numHiddenSlots; i++)
            {
                hiddenSlots[i].GenerateColor(RandomGenerator.GetRandomByte(0, (byte)Colors.X)); //Extension Method
            }
        }

        public static void CalculateRow()
        {
            if (!PlayIndexPositive) return;

            Block[] blocks = new Block[cols];
            MyTupla<bool, bool> coderResults = new MyTupla<bool, bool>(false, false);
            byte counterOverwriteWhite = 0;
            bool[] colorsCheck = new bool[(byte)Colors.X];

            //Counters for colors in codes
            byte[] counterColorPlaySlot = new byte[(byte)(Colors.X + 1)];
            byte[] counterColorPlaySlotTotal = new byte[(byte)(Colors.X + 1)];
            byte[] counterColorHiddenSlot = new byte[(byte)Colors.X];

            byte[] results = new byte[cols]; //Array results [0, 1, 2] --> [Black, White, X]

            DisableAllSlotsForFollowingRow();

            counterColorHiddenSlot.CounterColorsInSecretCode(hiddenSlots);
            counterColorPlaySlotTotal.CounterColorsInCode();

            //Cycle for check single slots
            for (byte i = 0; i < cols; i++)
            {
                coderResults.Item1 = false;
                coderResults.Item2 = false;

                byte index = (byte)(PlayIndex * cols + i);

                byte playSlotIndexColor = slots[index].Color.IndexColor;
                counterColorPlaySlot[playSlotIndexColor]++;

                for (byte j = 0; j < hiddenSlots.Length; j++)
                {
                    byte hiddenSlotIndexColor = hiddenSlots[j].Color.IndexColor;

                    //Check for white block
                    if (playSlotIndexColor == hiddenSlotIndexColor)
                    {
                        //If i haven't already seen the color or my colors are less or equal than secret code is white
                        coderResults.Item1 = !colorsCheck[slots[index].Color.IndexColor] || (counterColorPlaySlot[playSlotIndexColor] <= counterColorHiddenSlot[hiddenSlotIndexColor]);

                        //Black block
                        if (i == j)
                        {
                            //If i have already seen the color and my colors are more than secret code i must change a white block in "x" block
                            if (colorsCheck[slots[index].Color.IndexColor] && (counterColorPlaySlot[playSlotIndexColor] > counterColorHiddenSlot[hiddenSlotIndexColor])) counterOverwriteWhite++;

                            coderResults.Item1 = true;
                            coderResults.Item2 = true;
                            j = (byte)hiddenSlots.Length; //Skip iterations
                        }
                    }
                }

                if (playSlotIndexColor < (byte)Colors.X) colorsCheck[playSlotIndexColor] = true; //I "see" the color, except "X"

                results[i] = GetIndexColor(coderResults);
            }

            for (byte i = 0; i < counterOverwriteWhite; i++)
            {
                //Remove the first white block               
                results.RemoveWhite();
            }

            results.SortBlocks();
            blocks.InitBlocks(results);
        }

        private static void CounterColorsInCode(this byte[] counterColorPlaySlotTotal)
        {
            if (PlayIndexPositive)
            {
                for (byte k = 0; k < cols; k++)
                {
                    byte indexTotal = (byte)(PlayIndex * cols + k);

                    byte playSlotIndexColorTotal = slots[indexTotal].Color.IndexColor;
                    counterColorPlaySlotTotal[playSlotIndexColorTotal]++;
                }
            }
        }

        //Counter for every color in secret code for some calculus
        private static void CounterColorsInSecretCode(this byte[] code, HiddenSlot[] slots)
        {
            for (byte i = 0; i < slots.Length; i++)
            {
                code[slots[i].Color.IndexColor]++;
            }
        }

        //Search the first white block to change it in "x" block
        private static void RemoveWhite(this byte[] blocks)
        {
            for (byte i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] == 1) //If is white...
                {
                    blocks[i] = 2; //Become "X"
                    return;
                }
            }
        }

        private static void SortBlocks(this byte[] blocks) //Selection Sort
        {
            for (byte i = 0; i < blocks.Length - 1; i++)
            {
                for (byte j = (byte)(i + 1); j < blocks.Length; j++)
                {
                    if (blocks[i] > blocks[j])
                    {
                        (blocks[i], blocks[j]) = (blocks[j], blocks[i]); //Swap values shortcut
                    }
                }
            }
        }

        private static void InitBlocks(this Block[] blocks, byte[] results)
        {
            for (int i = 0; i < results.Length; i++)
            {
                blocks[i] = GetColor(results[i]);
            }

            if (blocks[results.Length - 1].BlockColor == BlockColors.Black)
            {
                SetWinLose(true);

                Game.WinsConfig.AddWin();

                PlayIndex = 8; //End Game
            }
        }

        private static byte GetIndexColor(MyTupla<bool, bool> result)
        {
            if (result.Item1 && !result.Item2) return 1; //White
            else if (result.Item1 && result.Item2) return 0; //Black
            else return 2; //x
        }

        //Getting color based on a Tupla values (bool, bool)
        private static Block GetColor(byte index)
        {
            return new Block(((BlockColors)index).ToString());
        }

        public static void ClearAll()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Destroy();
            }

            for (int i = 0; i < hiddenSlots.Length; i++)
            {
                hiddenSlots[i].Destroy();
            }
        }

        public static void SetWinLose(bool win)
        {
            Game.WinLoseButton.LoseSpriteIsActive = !win;
        }

        public static void ShowHiddenColors(bool show)
        {
            for (int i = 0; i < hiddenSlots.Length; i++)
            {
                hiddenSlots[i].ShowColors(show);
            }
        }

    }
}
