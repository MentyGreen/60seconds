using System;
using RG.Parsecs.Survival;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000345 RID: 837
	public class RemasterTooltipsUIManager : TooltipsUIManager
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x000773C4 File Offset: 0x000755C4
		public void HideTooltipsUI()
		{
			for (int i = 0; i < this._handlers.Count; i++)
			{
				this._handlers[i].gameObject.SetActive(false);
			}
		}
	}
}
