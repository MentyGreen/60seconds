using System;
using Rewired.Dev;

namespace RG.SecondsRemaster.Inputs
{
	// Token: 0x02000283 RID: 643
	public static class RewiredConsts
	{
		// Token: 0x0200041F RID: 1055
		public static class Action
		{
			// Token: 0x040018BD RID: 6333
			[ActionIdFieldInfo(categoryName = "Default", friendlyName = "InitPlayer")]
			public const int INITPLAYER = 0;

			// Token: 0x040018BE RID: 6334
			[ActionIdFieldInfo(categoryName = "Default", friendlyName = "PauseMenu")]
			public const int PAUSEMENU = 12;

			// Token: 0x040018BF RID: 6335
			[ActionIdFieldInfo(categoryName = "Scavenge", friendlyName = "MoveHorizontal")]
			public const int MOVEHORIZONTAL = 2;

			// Token: 0x040018C0 RID: 6336
			[ActionIdFieldInfo(categoryName = "Scavenge", friendlyName = "MoveVertical")]
			public const int MOVEVERTICAL = 3;

			// Token: 0x040018C1 RID: 6337
			[ActionIdFieldInfo(categoryName = "Scavenge", friendlyName = "Interact")]
			public const int INTERACT = 4;

			// Token: 0x040018C2 RID: 6338
			[ActionIdFieldInfo(categoryName = "Scavenge", friendlyName = "Rotate")]
			public const int ROTATE = 14;

			// Token: 0x040018C3 RID: 6339
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "SelectVertical")]
			public const int SELECTVERTICAL = 25;

			// Token: 0x040018C4 RID: 6340
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "SelectHorizontal")]
			public const int SELECTHORIZONTAL = 28;

			// Token: 0x040018C5 RID: 6341
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Confirm")]
			public const int CONFIRM = 29;

			// Token: 0x040018C6 RID: 6342
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Cancel")]
			public const int CANCEL = 30;

			// Token: 0x040018C7 RID: 6343
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "MoveCursorVertical")]
			public const int MOVECURSORVERTICAL = 32;

			// Token: 0x040018C8 RID: 6344
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "MoveCursorHorizontal")]
			public const int MOVECURSORHORIZONTAL = 31;

			// Token: 0x040018C9 RID: 6345
			[ActionIdFieldInfo(categoryName = "UI", friendlyName = "ConfirmMouse")]
			public const int CONFIRMMOUSE = 36;

			// Token: 0x040018CA RID: 6346
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "ChangeHatOrSkinToNext")]
			public const int CHANGEHATORSKINTONEXT = 33;

			// Token: 0x040018CB RID: 6347
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "ChangeHatOrSkinToPrevious")]
			public const int CHANGEHATORSKINTOPREVIOUS = 37;

			// Token: 0x040018CC RID: 6348
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "NextItem")]
			public const int NEXTITEM = 34;

			// Token: 0x040018CD RID: 6349
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "PreviousItem")]
			public const int PREVIOUSITEM = 35;

			// Token: 0x040018CE RID: 6350
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "OpenOrHideJournal")]
			public const int OPENORHIDEJOURNAL = 38;

			// Token: 0x040018CF RID: 6351
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "NextPage")]
			public const int NEXTPAGE = 39;

			// Token: 0x040018D0 RID: 6352
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "PreviousPage")]
			public const int PREVIOUSPAGE = 40;

			// Token: 0x040018D1 RID: 6353
			[ActionIdFieldInfo(categoryName = "Survival", friendlyName = "ShowPrompts")]
			public const int SHOWPROMPTS = 41;
		}

		// Token: 0x02000420 RID: 1056
		public static class Category
		{
			// Token: 0x040018D2 RID: 6354
			public const int DEFAULT = 0;

			// Token: 0x040018D3 RID: 6355
			public const int MOUSE = 6;

			// Token: 0x040018D4 RID: 6356
			public const int KEYBOARD = 7;

			// Token: 0x040018D5 RID: 6357
			public const int GAMEPAD = 8;

			// Token: 0x040018D6 RID: 6358
			public const int MOUSEKEYBOARD = 9;
		}

		// Token: 0x02000421 RID: 1057
		public static class Layout
		{
			// Token: 0x0200044A RID: 1098
			public static class Joystick
			{
				// Token: 0x04001953 RID: 6483
				public const int DEFAULT = 0;
			}

			// Token: 0x0200044B RID: 1099
			public static class Keyboard
			{
				// Token: 0x04001954 RID: 6484
				public const int DEFAULT = 0;

				// Token: 0x04001955 RID: 6485
				public const int AZERTY = 1;
			}

			// Token: 0x0200044C RID: 1100
			public static class Mouse
			{
				// Token: 0x04001956 RID: 6486
				public const int DEFAULT = 0;
			}

			// Token: 0x0200044D RID: 1101
			public static class CustomController
			{
				// Token: 0x04001957 RID: 6487
				public const int DEFAULT = 0;
			}
		}

		// Token: 0x02000422 RID: 1058
		public static class Player
		{
			// Token: 0x040018D7 RID: 6359
			[PlayerIdFieldInfo(friendlyName = "System")]
			public const int SYSTEM = 9999999;

			// Token: 0x040018D8 RID: 6360
			[PlayerIdFieldInfo(friendlyName = "Main")]
			public const int MAIN = 0;
		}
	}
}
