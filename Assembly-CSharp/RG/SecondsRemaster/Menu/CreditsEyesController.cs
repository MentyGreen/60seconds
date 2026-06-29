using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200028B RID: 651
	public class CreditsEyesController : MonoBehaviour
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x00068D3C File Offset: 0x00066F3C
		public void ShowAnimation()
		{
			if (this._isContinueAvailable != null)
			{
				if (this._isContinueAvailable.Value)
				{
					if (Random.Range(0f, 1f) < this._chanceForLongStare)
					{
						this._animator.SetTrigger("LongLookPostApo");
						return;
					}
					this._animator.SetTrigger("ShortLookPostApo");
					return;
				}
				else
				{
					if (Random.Range(0f, 1f) < this._chanceForLongStare)
					{
						this._animator.SetTrigger("LongLook");
						return;
					}
					this._animator.SetTrigger("ShortLook");
				}
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00068DD5 File Offset: 0x00066FD5
		public RectTransform RectTransform
		{
			get
			{
				return this._rectTransform;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00068DDD File Offset: 0x00066FDD
		public CreditsEyeGroup ParentGroup
		{
			get
			{
				return this._parentGroup;
			}
		}

		// Token: 0x04001189 RID: 4489
		[SerializeField]
		private Animator _animator;

		// Token: 0x0400118A RID: 4490
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x0400118B RID: 4491
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;

		// Token: 0x0400118C RID: 4492
		[SerializeField]
		private CreditsEyeGroup _parentGroup;

		// Token: 0x0400118D RID: 4493
		[SerializeField]
		private EEyesType _eyesType;

		// Token: 0x0400118E RID: 4494
		[HideInInspector]
		public bool IsFree = true;

		// Token: 0x0400118F RID: 4495
		[Range(0f, 1f)]
		[SerializeField]
		private float _chanceForLongStare = 0.5f;

		// Token: 0x04001190 RID: 4496
		private const string LONG_STARE_TRIGGER_NAME = "LongLook";

		// Token: 0x04001191 RID: 4497
		private const string SHORT_STARE_TRIGGER_NAME = "ShortLook";

		// Token: 0x04001192 RID: 4498
		private const string LONG_STARE_TRIGGER_POSTAPO_NAME = "LongLookPostApo";

		// Token: 0x04001193 RID: 4499
		private const string SHORT_STARE_TRIGGER_POSTAPO_NAME = "ShortLookPostApo";
	}
}
