using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000289 RID: 649
	public class CreditsEyeGroup : MonoBehaviour
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x00068C44 File Offset: 0x00066E44
		private Vector2 GetRandomPointInRect(Vector3[] corners)
		{
			return new Vector2(Random.Range(corners[0].x, corners[2].x), Random.Range(corners[0].y, corners[2].y));
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00068C90 File Offset: 0x00066E90
		public void Initialize()
		{
			if (this._corners == null)
			{
				this._corners = new Vector3[4];
				this._rectTransform.GetLocalCorners(this._corners);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00068CB8 File Offset: 0x00066EB8
		public CreditsEyesController StartAnimationAtRandomPointAndReturnInstance()
		{
			EEyesType groupEyesType = this.GroupEyesType;
			if (groupEyesType == EEyesType.TED)
			{
				this._tedEyesController.RectTransform.anchoredPosition = this.GetRandomPointInRect(this._corners);
				this._tedEyesController.ShowAnimation();
				return this._tedEyesController;
			}
			if (groupEyesType != EEyesType.DOLORES)
			{
				return null;
			}
			this._doloresEyesController.RectTransform.anchoredPosition = this.GetRandomPointInRect(this._corners);
			this._doloresEyesController.ShowAnimation();
			return this._doloresEyesController;
		}

		// Token: 0x04001180 RID: 4480
		[HideInInspector]
		public EEyesType GroupEyesType;

		// Token: 0x04001181 RID: 4481
		[SerializeField]
		private CreditsEyesController _tedEyesController;

		// Token: 0x04001182 RID: 4482
		[SerializeField]
		private CreditsEyesController _doloresEyesController;

		// Token: 0x04001183 RID: 4483
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04001184 RID: 4484
		private Vector3[] _corners;
	}
}
