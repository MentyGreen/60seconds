using System;
using RG.Core.GameVersion;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class DebugGameVersion : MonoBehaviour
{
	// Token: 0x06000FFC RID: 4092 RVA: 0x00041EE4 File Offset: 0x000400E4
	private void Start()
	{
		Debug.Log(this._gameVersion.GetReadableVersion() + " " + Application.platform.ToString());
	}

	// Token: 0x040009EB RID: 2539
	[SerializeField]
	private GameVersion _gameVersion;
}
