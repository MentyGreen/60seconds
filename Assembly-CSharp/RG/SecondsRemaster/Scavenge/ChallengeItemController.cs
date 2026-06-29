using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002C5 RID: 709
	public class ChallengeItemController : MonoBehaviour
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0006D3A6 File Offset: 0x0006B5A6
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x0006D3AE File Offset: 0x0006B5AE
		public bool Enabled { get; private set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0006D3B7 File Offset: 0x0006B5B7
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0006D3BF File Offset: 0x0006B5BF
		public ScavengeItem Item { get; private set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0006D3C8 File Offset: 0x0006B5C8
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0006D3D0 File Offset: 0x0006B5D0
		public RectTransform ParentRectTransform { get; private set; }

		// Token: 0x06001904 RID: 6404 RVA: 0x0006D3D9 File Offset: 0x0006B5D9
		public void SetRect(RectTransform parentCanvas)
		{
			this.ParentRectTransform = parentCanvas;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006D3E2 File Offset: 0x0006B5E2
		public void SetIcon(ScavengeItem item)
		{
			this.Enabled = true;
			this.Item = item;
			this._image.sprite = item.Icon;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0006D403 File Offset: 0x0006B603
		public void DisableIcon()
		{
			this.Enabled = false;
			this._animator.SetTrigger("Hide");
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0006D41C File Offset: 0x0006B61C
		public void ChangeIconRow()
		{
			this._animator.SetTrigger("HideAndShow");
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0006D42E File Offset: 0x0006B62E
		public void DisableObject()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x040012C4 RID: 4804
		[SerializeField]
		private Image _image;

		// Token: 0x040012C5 RID: 4805
		[SerializeField]
		private Animator _animator;

		// Token: 0x040012C9 RID: 4809
		private const string HIDE_PARAM_NAME = "Hide";

		// Token: 0x040012CA RID: 4810
		private const string CHANGE_ROWS = "HideAndShow";
	}
}
