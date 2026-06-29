using System;
using System.Collections;
using I2.Loc;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Startup
{
	// Token: 0x02000253 RID: 595
	public class RemasterStartupManager : MonoBehaviour
	{
		// Token: 0x0600163A RID: 5690 RVA: 0x00061219 File Offset: 0x0005F419
		public void StartLoadingGame()
		{
			base.StartCoroutine(this.LoadGame());
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00061228 File Offset: 0x0005F428
		private IEnumerator LoadGame()
		{
			Singleton<GameManager>.Instance.FirstMenuEnter = true;
			LocalizationManager.InitializeIfNeeded();
			yield return Singleton<GameManager>.Instance.ContentManager.LoadCommonAsset();
			Singleton<GameManager>.Instance.LoadMenuWithOpening();
			yield break;
		}
	}
}
