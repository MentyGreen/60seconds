using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.Remaster.Menu
{
	// Token: 0x02000226 RID: 550
	public class MenuSoundManager : MonoBehaviour
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x0005E6F3 File Offset: 0x0005C8F3
		private void Start()
		{
			this.PlayMusic();
			this.PlayAmbient();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0005E704 File Offset: 0x0005C904
		private void PlayMusic()
		{
			if (this._isContinueAvailable.Value)
			{
				AudioManager.PlaySound(this._alternativeMenuMusic.SoundEventName, 1f, 1f, 0f);
				return;
			}
			AudioManager.PlaySound(this._defaultMenuMusic.SoundEventName, 1f, 1f, 0f);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0005E760 File Offset: 0x0005C960
		private void PlayAmbient()
		{
			int num = Random.Range(0, this._ambientParameterIndex);
			if (this._isContinueAvailable.Value)
			{
				AudioManager.PlaySoundWithParameter(this._alternativeMenuAmbient.SoundEventName, "Ambient", (float)num, 1f, 1f, 0f);
				return;
			}
			AudioManager.PlaySoundWithParameter(this._defaultMenuAmbient.SoundEventName, "Ambient", (float)num, 1f, 1f, 0f);
		}

		// Token: 0x04000E3A RID: 3642
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;

		// Token: 0x04000E3B RID: 3643
		[SerializeField]
		private int _ambientParameterIndex;

		// Token: 0x04000E3C RID: 3644
		[Header("Default Menu")]
		[SerializeField]
		private SoundSlot _defaultMenuMusic;

		// Token: 0x04000E3D RID: 3645
		[SerializeField]
		private SoundSlot _defaultMenuAmbient;

		// Token: 0x04000E3E RID: 3646
		[Header("Alternative Menu")]
		[SerializeField]
		private SoundSlot _alternativeMenuMusic;

		// Token: 0x04000E3F RID: 3647
		[SerializeField]
		private SoundSlot _alternativeMenuAmbient;

		// Token: 0x04000E40 RID: 3648
		private const string AMBIENT_PARAMETER = "Ambient";
	}
}
