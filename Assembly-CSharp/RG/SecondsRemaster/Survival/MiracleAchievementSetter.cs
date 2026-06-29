using System;
using FMOD.Studio;
using FMODUnity;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000341 RID: 833
	public class MiracleAchievementSetter : OnClickSoundPlayer
	{
		// Token: 0x06001BB7 RID: 7095 RVA: 0x00077008 File Offset: 0x00075208
		private void Start()
		{
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.ResetCounter), "Reset", 40, this, false);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0007702A File Offset: 0x0007522A
		private void OnDestroy()
		{
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.ResetCounter), "Reset");
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00077048 File Offset: 0x00075248
		private void ResetCounter()
		{
			this._clickCounter = 0;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00077054 File Offset: 0x00075254
		private void OnMouseUpAsButton()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			if (this._soundSlot == null)
			{
				return;
			}
			if (!this.IsLastPlayFinished())
			{
				return;
			}
			if (this._clickCounter == this._clickToPlaySpecialSound - 1)
			{
				if (this._survivalData.CurrentDay % 7 == 3 && !this._wednesdayClicked.Value && !this._radio.IsDamaged())
				{
					this._wednesdayClicked.Value = true;
					this._playEvent = AudioManager.PlaySoundAndReturnInstance(this._firstSFX, 1f, 1f, 0f);
				}
				else if (this._survivalData.CurrentDay % 7 == 0 && !this._radio.IsDamaged())
				{
					this._sundayClicked.Value = true;
					this._playEvent = AudioManager.PlaySoundAndReturnInstance(this._secondSFX, 1f, 1f, 0f);
				}
				else
				{
					this.PlayStandardRadio();
				}
				if (!AchievementsSystem.IsAchievementUnlocked(this._miracle) && this._wednesdayClicked.Value && this._sundayClicked.Value)
				{
					AchievementsSystem.UnlockAchievement(this._miracle);
				}
			}
			else
			{
				this.PlayStandardRadio();
			}
			this._clickCounter++;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0007718C File Offset: 0x0007538C
		private bool IsLastPlayFinished()
		{
			PLAYBACK_STATE playback_STATE;
			this._playEvent.getPlaybackState(out playback_STATE);
			return playback_STATE == PLAYBACK_STATE.STOPPED;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000771AC File Offset: 0x000753AC
		private void PlayStandardRadio()
		{
			if (this._soundSlot != null && !string.IsNullOrEmpty(this._soundSlot.SoundEventName))
			{
				this._playEvent = AudioManager.PlaySoundAndReturnInstance(this._soundSlot.SoundEventName, 1f, 1f, 0f);
			}
		}

		// Token: 0x04001582 RID: 5506
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001583 RID: 5507
		[SerializeField]
		private GlobalBoolVariable _wednesdayClicked;

		// Token: 0x04001584 RID: 5508
		[SerializeField]
		private GlobalBoolVariable _sundayClicked;

		// Token: 0x04001585 RID: 5509
		[SerializeField]
		private Achievement _miracle;

		// Token: 0x04001586 RID: 5510
		[EventRef]
		[SerializeField]
		private string _firstSFX;

		// Token: 0x04001587 RID: 5511
		[EventRef]
		[SerializeField]
		private string _secondSFX;

		// Token: 0x04001588 RID: 5512
		[SerializeField]
		private int _clickToPlaySpecialSound = 3;

		// Token: 0x04001589 RID: 5513
		[SerializeField]
		private EndOfDayListenerList _endOfDayListenerList;

		// Token: 0x0400158A RID: 5514
		[SerializeField]
		private IItem _radio;

		// Token: 0x0400158B RID: 5515
		private int _clickCounter;

		// Token: 0x0400158C RID: 5516
		private EventInstance _playEvent;

		// Token: 0x0400158D RID: 5517
		private const int WEDNESDAY = 3;

		// Token: 0x0400158E RID: 5518
		private const int SUNDAY = 0;

		// Token: 0x0400158F RID: 5519
		private const int WEEK = 7;
	}
}
