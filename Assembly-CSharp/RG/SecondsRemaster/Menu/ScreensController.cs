using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200029A RID: 666
	public class ScreensController : MonoBehaviour
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x00069FA1 File Offset: 0x000681A1
		private void OnEnable()
		{
			this.EnableScreen(this._firstScreen);
			this._previousScreens.Clear();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00069FBC File Offset: 0x000681BC
		private void EnableScreen(GameObject screen)
		{
			for (int i = 0; i < this._screens.Count; i++)
			{
				this._screens[i].SetActive(false);
			}
			screen.SetActive(true);
			this._currentScreen = screen;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0006A000 File Offset: 0x00068200
		public void ShowScreen(GameObject screen)
		{
			if (!this._screens.Contains(screen))
			{
				Debug.LogErrorFormat(this, "The 'ScreenController' doesn't contain screen '{0}'.", new object[]
				{
					screen
				});
			}
			this._tvButtonsController.SwitchRandomSelectable();
			this._previousScreens.Push(this._currentScreen);
			this.EnableScreen(screen);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0006A053 File Offset: 0x00068253
		public void GoBack()
		{
			if (this._previousScreens.Count == 0)
			{
				return;
			}
			this._tvButtonsController.SwitchRandomSelectable();
			this.EnableScreen(this._previousScreens.Pop());
		}

		// Token: 0x040011DE RID: 4574
		[SerializeField]
		private TvButtonsController _tvButtonsController;

		// Token: 0x040011DF RID: 4575
		[SerializeField]
		private GameObject _firstScreen;

		// Token: 0x040011E0 RID: 4576
		[SerializeField]
		private List<GameObject> _screens;

		// Token: 0x040011E1 RID: 4577
		private Stack<GameObject> _previousScreens = new Stack<GameObject>();

		// Token: 0x040011E2 RID: 4578
		private GameObject _currentScreen;
	}
}
