using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000297 RID: 663
	public class RobotScreenController : MonoBehaviour
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x00069DEC File Offset: 0x00067FEC
		private void OnEnable()
		{
			base.Invoke("ShowGameModeScreen", this._screenVisibleTime);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00069DFF File Offset: 0x00067FFF
		public void ShowGameModeScreen()
		{
			this._screensController.ShowScreen(this._chooseGameModeScreen);
		}

		// Token: 0x040011D2 RID: 4562
		[SerializeField]
		private float _screenVisibleTime;

		// Token: 0x040011D3 RID: 4563
		[SerializeField]
		private GameObject _chooseGameModeScreen;

		// Token: 0x040011D4 RID: 4564
		[SerializeField]
		private ScreensController _screensController;

		// Token: 0x040011D5 RID: 4565
		private const string SHOW_GAME_MODE_SCREEN = "ShowGameModeScreen";
	}
}
