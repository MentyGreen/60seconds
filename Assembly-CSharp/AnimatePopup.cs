using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AB RID: 171
[AddComponentMenu("Daikon Forge/Examples/General/Animate Popup")]
public class AnimatePopup : MonoBehaviour
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x0002C464 File Offset: 0x0002A664
	private void OnDropdownOpen(dfDropdown dropdown, dfListbox popup)
	{
		if (this.target != null)
		{
			base.StopCoroutine("animateOpen");
			base.StopCoroutine("animateClose");
			Object.Destroy(this.target.gameObject);
		}
		this.target = popup;
		base.StartCoroutine(this.animateOpen(popup));
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0002C4BA File Offset: 0x0002A6BA
	private void OnDropdownClose(dfDropdown dropdown, dfListbox popup)
	{
		base.StartCoroutine(this.animateClose(popup));
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0002C4CA File Offset: 0x0002A6CA
	private IEnumerator animateOpen(dfListbox popup)
	{
		float runningTime = 0f;
		float startAlpha = 0f;
		float endAlpha = 1f;
		float startHeight = 20f;
		float endHeight = popup.Height;
		while (this.target == popup && runningTime < 0.15f)
		{
			runningTime = Mathf.Min(runningTime + Time.deltaTime, 0.15f);
			popup.Opacity = Mathf.Lerp(startAlpha, endAlpha, runningTime / 0.15f);
			float height = Mathf.Lerp(startHeight, endHeight, runningTime / 0.15f);
			popup.Height = height;
			yield return null;
		}
		popup.Opacity = 1f;
		popup.Height = endHeight;
		yield return null;
		popup.Invalidate();
		yield break;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
	private IEnumerator animateClose(dfListbox popup)
	{
		float runningTime = 0f;
		float startAlpha = 1f;
		float endAlpha = 0f;
		float startHeight = popup.Height;
		float endHeight = 20f;
		while (this.target == popup && runningTime < 0.15f)
		{
			runningTime = Mathf.Min(runningTime + Time.deltaTime, 0.15f);
			popup.Opacity = Mathf.Lerp(startAlpha, endAlpha, runningTime / 0.15f);
			float height = Mathf.Lerp(startHeight, endHeight, runningTime / 0.15f);
			popup.Height = height;
			yield return null;
		}
		this.target = null;
		Object.Destroy(popup.gameObject);
		yield break;
	}

	// Token: 0x040004C8 RID: 1224
	private const float ANIMATION_LENGTH = 0.15f;

	// Token: 0x040004C9 RID: 1225
	private dfListbox target;
}
