using System;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D1 RID: 721
	public class EndOfDaySoundController : MonoBehaviour
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x0006E104 File Offset: 0x0006C304
		private void OnEnable()
		{
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.RegisterCurrentSoundToPlay), "Visuals", 9, this, false);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0006E126 File Offset: 0x0006C326
		private void OnDisable()
		{
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.RegisterCurrentSoundToPlay), "Visuals");
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0006E144 File Offset: 0x0006C344
		private void RegisterCurrentSoundToPlay()
		{
			for (int i = 0; i < this._currentSoundToPlay.Length; i++)
			{
				if (this._currentSoundToPlay[i].EventName == string.Empty)
				{
					return;
				}
				if (this._currentSoundToPlay[i].OffsetCheck)
				{
					base.Invoke("PlaySound", (float)this._currentSoundToPlay[i].Offset);
				}
				else
				{
					this.PlaySound();
				}
			}
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0006E1B0 File Offset: 0x0006C3B0
		private void PlaySound()
		{
			AudioManager.PlaySound(this._currentSoundToPlay[this._soundIndex].EventName, this._currentSoundToPlay[this._soundIndex].Volume, 1f, 0f);
			this._currentSoundToPlay[this._soundIndex].ResetData();
		}

		// Token: 0x04001313 RID: 4883
		[SerializeField]
		private CurrentSoundToPlay[] _currentSoundToPlay;

		// Token: 0x04001314 RID: 4884
		[SerializeField]
		protected EndOfDayListenerList _endOfDayListenerList;

		// Token: 0x04001315 RID: 4885
		private int _soundIndex;
	}
}
