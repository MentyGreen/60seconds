using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x0200021D RID: 541
	public class OnSpriteClickAnimationPlayer : MonoBehaviour
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x0005DE6B File Offset: 0x0005C06B
		private void Start()
		{
			this.ValidateSetup();
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0005DE74 File Offset: 0x0005C074
		private void ValidateSetup()
		{
			if (this._animator == null)
			{
				Debug.LogError("Animator not set up properly in " + base.gameObject.name);
			}
			if (this._animationTriggerNames == null || this._animationTriggerNames.Count < 1 || this._animationTriggerNames.Contains(null))
			{
				Debug.LogError("Animation Trigger Names not set up properly in " + base.gameObject.name);
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005DEE7 File Offset: 0x0005C0E7
		private void OnMouseUpAsButton()
		{
			if (this._useLeftClick && !Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject())
			{
				this.PlayRandomAnimation();
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005DF03 File Offset: 0x0005C103
		private void OnMouseOver()
		{
			if (this._useRightClick && Input.GetMouseButtonUp(1) && !Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject())
			{
				this.PlayRandomAnimation();
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0005DF28 File Offset: 0x0005C128
		private void PlayRandomAnimation()
		{
			int index = (this._animationTriggerNames.Count > 1) ? Random.Range(0, this._animationTriggerNames.Count) : 0;
			this._animator.SetTrigger(this._animationTriggerNames[index]);
		}

		// Token: 0x04000E03 RID: 3587
		private const int RIGHT_MOUSE_BUTTON = 1;

		// Token: 0x04000E04 RID: 3588
		[SerializeField]
		private bool _useRightClick;

		// Token: 0x04000E05 RID: 3589
		[SerializeField]
		private bool _useLeftClick = true;

		// Token: 0x04000E06 RID: 3590
		[SerializeField]
		private Animator _animator;

		// Token: 0x04000E07 RID: 3591
		[SerializeField]
		private List<string> _animationTriggerNames;
	}
}
