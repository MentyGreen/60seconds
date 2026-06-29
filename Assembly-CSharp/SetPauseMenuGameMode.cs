using System;
using RG.Parsecs.Menu;
using RG.SecondsRemaster.Menu;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class SetPauseMenuGameMode : MonoBehaviour
{
	// Token: 0x06000FA8 RID: 4008 RVA: 0x00041171 File Offset: 0x0003F371
	private void Start()
	{
		this.SetGameType();
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0004117C File Offset: 0x0003F37C
	private void SetGameType()
	{
		PauseMenuControl pauseMenuControl = BasePauseMenu.Instance as PauseMenuControl;
		if (pauseMenuControl != null)
		{
			pauseMenuControl.GameType = this._gameType;
		}
	}

	// Token: 0x040009A8 RID: 2472
	[SerializeField]
	private PauseMenuControl.EGameType _gameType;
}
