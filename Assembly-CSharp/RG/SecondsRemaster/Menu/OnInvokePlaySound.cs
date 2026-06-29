using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002BD RID: 701
	public class OnInvokePlaySound : MonoBehaviour
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x0006CE26 File Offset: 0x0006B026
		private void Start()
		{
			if (this._isTubaDisabled != null)
			{
				this._isTubaDisabled.Value = true;
			}
			this.StopSounds();
			base.Invoke("PlaySound", (float)this._offSet);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0006CE5A File Offset: 0x0006B05A
		private void PlaySound()
		{
			AudioManager.PlaySound(this._soundSlot.SoundEventName, 1f, 1f, 0f);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0006CE7B File Offset: 0x0006B07B
		private void StopSounds()
		{
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			AudioManager.Instance.StopPlayingUiFadeOut();
		}

		// Token: 0x040012AC RID: 4780
		[SerializeField]
		private SoundSlot _soundSlot;

		// Token: 0x040012AD RID: 4781
		[SerializeField]
		private int _offSet;

		// Token: 0x040012AE RID: 4782
		[SerializeField]
		private GlobalBoolVariable _isTubaDisabled;
	}
}
