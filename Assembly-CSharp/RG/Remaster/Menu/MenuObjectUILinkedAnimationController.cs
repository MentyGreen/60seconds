using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.Events;

namespace RG.Remaster.Menu
{
	// Token: 0x02000229 RID: 553
	[RequireComponent(typeof(Animator))]
	public class MenuObjectUILinkedAnimationController : MonoBehaviour
	{
		// Token: 0x06001567 RID: 5479 RVA: 0x0005E948 File Offset: 0x0005CB48
		private void Awake()
		{
			this._animator = base.GetComponent<Animator>();
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0005E958 File Offset: 0x0005CB58
		public void SetShouldBeActive(bool value)
		{
			if (this._animator != null)
			{
				if (this._usesRandomizedAnimations)
				{
					if (this._parameterNames != null && this._parameterNames.Count > 0)
					{
						if (value)
						{
							this._animator.SetBool(this._parameterNames[Random.Range(0, this._parameterNames.Count)], true);
							return;
						}
						for (int i = 0; i < this._parameterNames.Count; i++)
						{
							this._animator.SetBool(this._parameterNames[i], false);
						}
						return;
					}
				}
				else
				{
					this._animator.SetBool(this.SHOULDBEACTIVE_PARAMETER_NAME, value);
				}
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005EA01 File Offset: 0x0005CC01
		private void OnMouseEnter()
		{
			if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject() && this._onSelect != null)
			{
				this._onSelect.Invoke();
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0005EA22 File Offset: 0x0005CC22
		private void OnMouseExit()
		{
			if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject() && this._onSelect != null)
			{
				this._onDeselect.Invoke();
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0005EA43 File Offset: 0x0005CC43
		private void OnMouseDown()
		{
			if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject())
			{
				this._onClick.Invoke();
			}
		}

		// Token: 0x04000E4C RID: 3660
		private Animator _animator;

		// Token: 0x04000E4D RID: 3661
		private string SHOULDBEACTIVE_PARAMETER_NAME = "ShouldBeActive";

		// Token: 0x04000E4E RID: 3662
		[SerializeField]
		private UnityEvent _onSelect;

		// Token: 0x04000E4F RID: 3663
		[SerializeField]
		private UnityEvent _onDeselect;

		// Token: 0x04000E50 RID: 3664
		[SerializeField]
		private UnityEvent _onClick;

		// Token: 0x04000E51 RID: 3665
		[SerializeField]
		private bool _usesRandomizedAnimations;

		// Token: 0x04000E52 RID: 3666
		[Tooltip("If UsesRandomizedAnimations is set to true, the parameter to set will be chosen randomly from this list. In that case the list cannot be null nor empty and has to match with the AnimatorController.")]
		[SerializeField]
		private List<string> _parameterNames;
	}
}
