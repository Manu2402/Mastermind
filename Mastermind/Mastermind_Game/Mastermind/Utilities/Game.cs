using Aiv.Fast2D;

namespace Mastermind
{
    static class Game
    {
        // Variables
        public static Window Window;
        public static float OptimalScreenHeight;
        public static bool PlayState;

        // Properties
        public static float UnitSize { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static float DeltaTime { get => Window.DeltaTime; }

        // References
        public static WinsConfig WinsConfig;

        private static Background background;

        private static ConfirmButton confirmButton;
        private static RulesButton rulesButton;
        public static WinLoseText WinLoseButton;

        public static Indicator Indicator;
        private static Medal medal;

        public static RulesControlsConfig RulesControls;

        public static void Init()
        {
            //801x800 (even in Photoshop)
            Window = new Window(801, 800, "Mastermind", false); //1 pixel offset. RES: 801x800
            Window.SetVSync(true);
            Window.SetDefaultViewportOrthographicSize(10);

            OptimalScreenHeight = 800; //Best resolution
            UnitSize = Window.Height / Window.OrthoHeight; //80
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight; //80

            LoadAssets();

            //PlayScene
            WinsConfig = new WinsConfig();

            background = new Background("BG");
            RulesControls = new RulesControlsConfig("RlsCtrls");

            Indicator = new Indicator("Indicator");
            medal = new Medal("Medal");

            InitButtons();

            CoderMgr.Init();
            PinMgr.Init();

            PlayGame();
        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }

        public static void LoadAssets()
        {
            GfxMgr.AddTexture("BG", "Assets/Graphics/BG.png");
            GfxMgr.AddTexture("RlsCtrls", "Assets/Graphics/RlsCtrls.png");
            GfxMgr.AddTexture("Coder", "Assets/Graphics/Coder.png");

            GfxMgr.AddTexture("Pin_0", "Assets/Graphics/Pins/Pin_0.png");
            GfxMgr.AddTexture("Pin_1", "Assets/Graphics/Pins/Pin_1.png");
            GfxMgr.AddTexture("Pin_2", "Assets/Graphics/Pins/Pin_2.png");
            GfxMgr.AddTexture("Pin_3", "Assets/Graphics/Pins/Pin_3.png");
            GfxMgr.AddTexture("Pin_4", "Assets/Graphics/Pins/Pin_4.png");
            GfxMgr.AddTexture("Pin_5", "Assets/Graphics/Pins/Pin_5.png");

            GfxMgr.AddTexture("Confirm", "Assets/Graphics/Buttons/Confirm.png");
            GfxMgr.AddTexture("Rules", "Assets/Graphics/Buttons/Rules.png");

            GfxMgr.AddTexture("YouWin", "Assets/Graphics/Buttons/YOU_WIN.png");
            GfxMgr.AddTexture("YouLose", "Assets/Graphics/Buttons/YOU_LOSE.png");

            GfxMgr.AddTexture("Body", "Assets/Graphics/Slots/Body.png");
            GfxMgr.AddTexture("Question", "Assets/Graphics/Slots/Question_Point.png");
            GfxMgr.AddTexture("Selector", "Assets/Graphics/Slots/Selector.png");

            GfxMgr.AddTexture("Indicator", "Assets/Graphics/Indicator.png");
            GfxMgr.AddTexture("Medal", "Assets/Graphics/Medal.png");

            GfxMgr.AddTexture("Hover_Rules", "Assets/Graphics/Hovers/Hover_Rules.png");
            GfxMgr.AddTexture("Hover_Confirm", "Assets/Graphics/Hovers/Hover_Confirm.png");

            GfxMgr.AddTexture("Blue", "Assets/Graphics/Colors/Blue.png");
            GfxMgr.AddTexture("Brown", "Assets/Graphics/Colors/Brown.png");
            GfxMgr.AddTexture("Green", "Assets/Graphics/Colors/Green.png");
            GfxMgr.AddTexture("Orange", "Assets/Graphics/Colors/Orange.png");
            GfxMgr.AddTexture("Pink", "Assets/Graphics/Colors/Pink.png");
            GfxMgr.AddTexture("Yellow", "Assets/Graphics/Colors/Yellow.png");

            GfxMgr.AddTexture("X", "Assets/Graphics/X.png");
            GfxMgr.AddTexture("x", "Assets/Graphics/Colors/x.png");

            GfxMgr.AddTexture("Black", "Assets/Graphics/Colors/Black.png");
            GfxMgr.AddTexture("White", "Assets/Graphics/Colors/White.png");
        }

        public static void InitButtons()
        {
            confirmButton = new ConfirmButton("Confirm");
            rulesButton = new RulesButton("Rules");
            WinLoseButton = new WinLoseText("YouWin");
        }

        public static void ResumeGame()
        {
            SlotMgr.ClearAll();
            BlockMgr.ClearAll();

            Indicator.ResetPlayIndex();

            PlayGame();
        }

        private static void PlayGame()
        {
            SlotMgr.Init();
            SlotMgr.GenerateSecretCode();

            SlotMgr.SetWinLose(false);
            SlotMgr.ShowHiddenColors(true);

            PlayState = true;
        }

        public static void Play()
        {
            while (Window.IsOpened)
            {
                // Exit when ESC is pressed
                if (Window.GetKey(KeyCode.Esc)) break;

                //INPUT
                InputMgr.Input();
                if (PlayState) SlotMgr.SelectSlotOnIndicatorRow();

                //UPDATE
                UpdateMgr.Update();
                HoverMgr.Hover();

                //DRAW
                DrawMgr.Draw();

                Window.Update();
            }
        }

    }
}
