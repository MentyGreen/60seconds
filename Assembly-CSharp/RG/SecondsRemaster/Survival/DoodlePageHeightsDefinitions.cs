using System;
using RG.Core.Base;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000316 RID: 790
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Doodle Page Heights Definition")]
	public class DoodlePageHeightsDefinitions : RGScriptableObject
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x00073BA8 File Offset: 0x00071DA8
		public float GetPageHeight(VisualId visualId)
		{
			for (int i = 0; i < this._visualIds.Length; i++)
			{
				if (this._visualIds[i] == visualId)
				{
					return this._percentagePageHeights[i];
				}
			}
			return 0.5f;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00073BE6 File Offset: 0x00071DE6
		private void OnValidate()
		{
			if (this._visualIds.Length != this._percentagePageHeights.Length)
			{
				Debug.LogError("VisualsIds and PercentagePageHeights arrays needs to be of an equal lengths.", this);
			}
		}

		// Token: 0x04001488 RID: 5256
		[SerializeField]
		private VisualId[] _visualIds;

		// Token: 0x04001489 RID: 5257
		[SerializeField]
		[Range(0f, 1f)]
		private float[] _percentagePageHeights;

		// Token: 0x0400148A RID: 5258
		private const float DEFAULT_PAGE_HEIGHT_ALLOWANCE = 0.5f;

		// Token: 0x0400148B RID: 5259
		private const string ARRAYS_NOT_EQUAL_ERROR_MESSAGE = "VisualsIds and PercentagePageHeights arrays needs to be of an equal lengths.";
	}
}
