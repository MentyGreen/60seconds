using System;
using System.Collections.Generic;
using Rewired;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RG.SecondsRemaster.ChallengeConclusion
{
	// Token: 0x020002C3 RID: 707
	public class ConclusionScreenController : MonoBehaviour
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x0006D0A8 File Offset: 0x0006B2A8
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006D0BB File Offset: 0x0006B2BB
		public void Start()
		{
			Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = true;
			this._startTime = Time.realtimeSinceStartup;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006D0D8 File Offset: 0x0006B2D8
		private void Update()
		{
			if (Time.realtimeSinceStartup > this._startTime + this._clickAwayDelayTime && this._player.GetButtonDown(29) && this._clickAwayAllowed)
			{
				this.OnReturnToMenuButtonClick();
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0006D10C File Offset: 0x0006B30C
		public void OnReturnToMenuButtonClick()
		{
			List<Scene> list = new List<Scene>();
			SceneManager.GetSceneByName(this._survivalSceneName);
			list.Add(SceneManager.GetSceneByName(this._survivalSceneName));
			list.Add(SceneManager.GetSceneByName(this._challengeConclusionSceneName));
			Singleton<GameManager>.Instance.LoadMenu(list);
		}

		// Token: 0x040012B9 RID: 4793
		[SerializeField]
		private string _survivalSceneName = "Survival";

		// Token: 0x040012BA RID: 4794
		[SerializeField]
		private string _challengeConclusionSceneName = "ChallengeConclusion";

		// Token: 0x040012BB RID: 4795
		[SerializeField]
		private float _clickAwayDelayTime = 2f;

		// Token: 0x040012BC RID: 4796
		[SerializeField]
		private bool _clickAwayAllowed;

		// Token: 0x040012BD RID: 4797
		private Player _player;

		// Token: 0x040012BE RID: 4798
		private float _startTime;
	}
}
