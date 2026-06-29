using System;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CB RID: 715
	[RequireComponent(typeof(Animator))]
	public class ScavengeTextController : MonoBehaviour
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x0006DB69 File Offset: 0x0006BD69
		private void Awake()
		{
			this._animator = base.GetComponent<Animator>();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0006DB77 File Offset: 0x0006BD77
		public void ShowText()
		{
			this._animator.SetTrigger("Show");
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0006DB89 File Offset: 0x0006BD89
		internal void ShowTextDelayed(float delay)
		{
			base.Invoke("ShowText", delay);
		}

		// Token: 0x040012F9 RID: 4857
		private Animator _animator;

		// Token: 0x040012FA RID: 4858
		private const string SHOW_TEXT_PROPERTY_NAME = "Show";
	}
}
