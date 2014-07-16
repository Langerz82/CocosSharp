using CocosSharp;

namespace tests.FontTest
{
	public class SystemFontTestScene : TestScene
	{
		private static int fontIdx;

		private static readonly string[] fontList =
		{
#if IOS || IPHONE || MACOS
			"Chalkboard SE",
			"Chalkduster",
			"Noteworthy",
			"Marker Felt",
			"Papyrus",
			"American Typewriter",
			"Arial",
			"fonts/A Damn Mess.ttf",
			"fonts/Abberancy.ttf",
			"fonts/Abduction.ttf",
			"fonts/American Typewriter.ttf",
			"fonts/Courier New.ttf",
			"fonts/Marker Felt.ttf",
			"fonts/Paint Boy.ttf",
			"fonts/Schwarzwald Regular.ttf",
			"fonts/Scissor Cuts.ttf",
			"fonts/tahoma.ttf",
			"fonts/Thonburi.ttf",
			"fonts/ThonburiBold.ttf"

#endif
#if WINDOWS || WINDOWSGL
			"Comic Sans MS",
			"Felt",
			"MoolBoran",
			"Courier New",
			"Georgia",
			"Symbol",
            "Wingdings",
			"Arial",

            "fonts/A Damn Mess.ttf",
            "fonts/Abberancy.ttf",
            "fonts/Abduction.ttf",
            "fonts/American Typewriter.ttf",
            "fonts/arial.ttf",
            "fonts/Courier New.ttf",
            "fonts/Marker Felt.ttf",
            "fonts/Paint Boy.ttf",
            "fonts/Schwarzwald Regular.ttf",
            "fonts/Scissor Cuts.ttf",
            "fonts/tahoma.ttf",
            "fonts/Thonburi.ttf",
            "fonts/ThonburiBold.ttf"
#endif
#if ANDROID
			"Arial",
			"Courier New",
			"Georgia",

            "fonts/A Damn Mess.ttf",
            "fonts/Abberancy.ttf",
            "fonts/Abduction.ttf",
            "fonts/American Typewriter.ttf",
            "fonts/arial.ttf",
            "fonts/Courier New.ttf",
            "fonts/Marker Felt.ttf",
            "fonts/Paint Boy.ttf",
            "fonts/Schwarzwald Regular.ttf",
            "fonts/Scissor Cuts.ttf",
            "fonts/tahoma.ttf",
            "fonts/Thonburi.ttf",
            "fonts/ThonburiBold.ttf"
#endif
		};

		public static int vAlignIdx = 0;

		public static CCVerticalTextAlignment[] verticalAlignment =
		{
			CCVerticalTextAlignment.Top,
			CCVerticalTextAlignment.Center,
			CCVerticalTextAlignment.Bottom
		};

		public override void runThisTest()
		{
			CCLayer pLayer = new SystemFontTest();
			AddChild(pLayer);

			Scene.Director.ReplaceScene(this);
		}

		protected override void NextTestCase()
		{
			nextAction();
		}
		protected override void PreviousTestCase()
		{
			backAction();
		}
		protected override void RestTestCase()
		{
			restartAction();
		}
		public static string nextAction()
		{
			fontIdx++;
			if (fontIdx >= fontList.Length)
			{
				fontIdx = 0;
				vAlignIdx = (vAlignIdx + 1) % verticalAlignment.Length;
			}
			return fontList[fontIdx];
		}

		public static string backAction()
		{
			fontIdx--;
			if (fontIdx < 0)
			{
				fontIdx = fontList.Length - 1;
				vAlignIdx--;
				if (vAlignIdx < 0)
					vAlignIdx = verticalAlignment.Length - 1;
			}

			return fontList[fontIdx];
		}

		public static string restartAction()
		{
			return fontList[fontIdx];
		}
	}


	public class SystemFontTest : CCLayer
	{
		private const int kTagLabel1 = 1;
		private const int kTagLabel2 = 2;
		private const int kTagLabel3 = 3;
		private const int kTagLabel4 = 4;

		private CCSize blockSize;
		private CCSize size;
		private float fontSize = 26;


		public SystemFontTest()
		{
			size = Layer.VisibleBoundsWorldspace.Size;

			CCMenuItemImage item1 = new CCMenuItemImage(TestResource.s_pPathB1, TestResource.s_pPathB2, backCallback);
			CCMenuItemImage item2 = new CCMenuItemImage(TestResource.s_pPathR1, TestResource.s_pPathR2, restartCallback);
			CCMenuItemImage item3 = new CCMenuItemImage(TestResource.s_pPathF1, TestResource.s_pPathF2, nextCallback);

			CCMenu menu = new CCMenu(item1, item2, item3);
			menu.Position = CCPoint.Zero;
			item1.Position = new CCPoint(size.Width / 2 - item2.ContentSize.Width * 2, item2.ContentSize.Height / 2);
			item2.Position = new CCPoint(size.Width / 2, item2.ContentSize.Height / 2);
			item3.Position = new CCPoint(size.Width / 2 + item2.ContentSize.Width * 2, item2.ContentSize.Height / 2);
			AddChild(menu, 1);

			blockSize = new CCSize(size.Width / 3, 200);

			var leftColor = new CCLayerColor(new CCColor4B(100, 100, 100, 255));
			var centerColor = new CCLayerColor(new CCColor4B(200, 100, 100, 255));
			var rightColor = new CCLayerColor(new CCColor4B(100, 100, 200, 255));

			leftColor.IgnoreAnchorPointForPosition = false;
			centerColor.IgnoreAnchorPointForPosition = false;
			rightColor.IgnoreAnchorPointForPosition = false;

			leftColor.AnchorPoint = CCPoint.AnchorMiddleLeft;
			centerColor.AnchorPoint = CCPoint.AnchorMiddleLeft;
			rightColor.AnchorPoint = CCPoint.AnchorMiddleLeft;

			leftColor.Position = new CCPoint(0, size.Height / 2);
			centerColor.Position = new CCPoint(blockSize.Width, size.Height / 2);
			rightColor.Position = new CCPoint(blockSize.Width * 2, size.Height / 2);

			AddChild(leftColor, -1);
			AddChild(rightColor, -1);
			AddChild(centerColor, -1);

			showFont(SystemFontTestScene.restartAction());
		}

		public void showFont(string pFont)
		{

			RemoveChildByTag(kTagLabel1, true);
			RemoveChildByTag(kTagLabel2, true);
			RemoveChildByTag(kTagLabel3, true);
			RemoveChildByTag(kTagLabel4, true);

			var top = new CCLabel(pFont,"Helvetica", 32);

			var left = new CCLabel("alignment left", pFont, fontSize,
				blockSize, CCTextAlignment.Left,
				SystemFontTestScene.verticalAlignment[SystemFontTestScene.vAlignIdx]);

			var center = new CCLabel("alignment center", pFont, fontSize,
				blockSize, CCTextAlignment.Center,
				SystemFontTestScene.verticalAlignment[SystemFontTestScene.vAlignIdx]);

			var right = new CCLabel("alignment right", pFont, fontSize,
				blockSize, CCTextAlignment.Right,
				SystemFontTestScene.verticalAlignment[SystemFontTestScene.vAlignIdx]);

			top.AnchorPoint = new CCPoint(0.5f, 1);
			left.AnchorPoint = CCPoint.AnchorMiddleLeft;
			center.AnchorPoint = CCPoint.AnchorMiddleLeft;
			right.AnchorPoint = CCPoint.AnchorMiddleLeft;

			top.Position = new CCPoint(size.Width / 2, size.Height - 20);
			left.Position = new CCPoint(0, size.Height / 2);
			center.Position = new CCPoint(blockSize.Width, size.Height / 2);

			right.Position = new CCPoint(blockSize.Width * 2, size.Height / 2);

			AddChild(left, 0, kTagLabel1);
			AddChild(right, 0, kTagLabel2);
			AddChild(center, 0, kTagLabel3);
			AddChild(top, 0, kTagLabel4);
		}

		public void restartCallback(object pSender)
		{
			showFont(SystemFontTestScene.restartAction());
		}

		public void nextCallback(object pSender)
		{
			showFont(SystemFontTestScene.nextAction());
		}

		public void backCallback(object pSender)
		{
			showFont(SystemFontTestScene.backAction());
		}

		public virtual string title()
		{
			return "System Font test";
		}
	}
}