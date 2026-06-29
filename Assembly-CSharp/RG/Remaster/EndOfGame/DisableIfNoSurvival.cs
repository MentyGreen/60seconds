using System;
using RG.Parsecs.EventEditor;
using RG.SecondsRemaster;
using UnityEngine;

namespace RG.Remaster.EndOfGame
{
	// Token: 0x0200023C RID: 572
	public class DisableIfNoSurvival : MonoBehaviour
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x00060904 File Offset: 0x0005EB04
		private void OnEnable()
		{
			if (this._challengeData != null && this._scavengeOnlyBoolVariable && (this._challengeData.RuntimeData.Challenge != null || this._scavengeOnlyBoolVariable.Value))
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x04000EAC RID: 3756
		[SerializeField]
		private CurrentChallengeData _challengeData;

		// Token: 0x04000EAD RID: 3757
		[SerializeField]
		private GlobalBoolVariable _scavengeOnlyBoolVariable;
	}
}
