using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[AddComponentMenu("Daikon Forge/Examples/General/Load Level On Click")]
[Serializable]
public class dfLoadLevelByName : MonoBehaviour
{
	// Token: 0x06000A57 RID: 2647 RVA: 0x0002D49E File Offset: 0x0002B69E
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.LevelName))
		{
			Application.LoadLevel(this.LevelName);
		}
	}

	// Token: 0x040004FC RID: 1276
	public string LevelName;
}
