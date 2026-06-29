using System;
using System.Collections;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Scavenge;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class Watch : MonoBehaviour
{
	// Token: 0x06000E2C RID: 3628 RVA: 0x0003AFBB File Offset: 0x000391BB
	public void Initialize()
	{
		this._gameFlow = base.GetComponent<GameFlow>();
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0003AFC9 File Offset: 0x000391C9
	public void StartTicking(int prestartTimeMargin)
	{
		base.StartCoroutine(this.WatchTick(prestartTimeMargin));
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x0003AFD9 File Offset: 0x000391D9
	private IEnumerator WatchTick(int prestartTimeMargin)
	{
		this._prepareTime = GameSessionData.Instance.GetPrepareTime();
		this._prepareTime += prestartTimeMargin;
		this._gameTime = GameSessionData.Instance.GetGameTime();
		this._comfortZoneTimeout = GameSessionData.Instance.GetComfortZoneTimeout();
		this._cautionZoneTimeout = GameSessionData.Instance.GetCautionZoneTimeout();
		int totalTime = this._gameTime + this._prepareTime;
		this._timeLeft = totalTime;
		this._clockController.Initialize((float)this._gameTime, (float)this._comfortZoneTimeout, (float)this._cautionZoneTimeout);
		yield return new WaitForSeconds(2f);
		float startTime = 0f;
		WaitForSeconds tickTimeout = new WaitForSeconds(1f);
		int num;
		for (int i = totalTime; i >= 0; i = num - 1)
		{
			this._clockController.UpdateHandPosition((float)i);
			if (i == this._gameTime && GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL)
			{
				this._gameFlow.StartGame();
				startTime = Time.time;
			}
			yield return tickTimeout;
			if (this._gameFlow.Terminated)
			{
				break;
			}
			this._timeLeft = i;
			num = i;
		}
		GameSessionData.Instance.ScavengeFinishedTime = Time.time - startTime;
		AudioManager.Instance.StopPlayingMusicFadeOut();
		if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL)
		{
			this._gameFlow.EndLevel();
		}
		yield break;
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0003AFEF File Offset: 0x000391EF
	// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0003AFF7 File Offset: 0x000391F7
	public int PrepareTime
	{
		get
		{
			return this._prepareTime;
		}
		set
		{
			this._prepareTime = value;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003B000 File Offset: 0x00039200
	// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0003B008 File Offset: 0x00039208
	public int GameTime
	{
		get
		{
			return this._gameTime;
		}
		set
		{
			this._gameTime = value;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0003B011 File Offset: 0x00039211
	// (set) Token: 0x06000E34 RID: 3636 RVA: 0x0003B019 File Offset: 0x00039219
	public int ComfortZoneTimeout
	{
		get
		{
			return this._comfortZoneTimeout;
		}
		set
		{
			this._comfortZoneTimeout = value;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0003B022 File Offset: 0x00039222
	// (set) Token: 0x06000E36 RID: 3638 RVA: 0x0003B02A File Offset: 0x0003922A
	public int CautionZoneTimeout
	{
		get
		{
			return this._cautionZoneTimeout;
		}
		set
		{
			this._cautionZoneTimeout = value;
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0003B033 File Offset: 0x00039233
	public int TimeLeft
	{
		get
		{
			return this._timeLeft;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0003B03B File Offset: 0x0003923B
	public ClockController ClockController
	{
		get
		{
			return this._clockController;
		}
	}

	// Token: 0x0400086D RID: 2157
	[SerializeField]
	private int _prepareTime = 15;

	// Token: 0x0400086E RID: 2158
	[SerializeField]
	private int _gameTime = 60;

	// Token: 0x0400086F RID: 2159
	[SerializeField]
	private int _comfortZoneTimeout = 20;

	// Token: 0x04000870 RID: 2160
	[SerializeField]
	private int _cautionZoneTimeout = 40;

	// Token: 0x04000871 RID: 2161
	[SerializeField]
	private float _tickRotation = 6f;

	// Token: 0x04000872 RID: 2162
	[SerializeField]
	private ClockController _clockController;

	// Token: 0x04000873 RID: 2163
	private GameFlow _gameFlow;

	// Token: 0x04000874 RID: 2164
	private int _timeLeft;
}
