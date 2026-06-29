using System;
using Rewired;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022B RID: 555
	public class HatClickDetector : MonoBehaviour
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x0005EBA3 File Offset: 0x0005CDA3
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0005EBB6 File Offset: 0x0005CDB6
		private void Start()
		{
			this.SetUpControllerReference();
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0005EBC0 File Offset: 0x0005CDC0
		private void SetUpControllerReference()
		{
			Transform transform = base.transform;
			while (this._controller == null && transform != null)
			{
				this._controller = transform.GetComponent<HatController>();
				if (this._controller == null)
				{
					transform = transform.parent;
				}
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0005EC0E File Offset: 0x0005CE0E
		private void OnMouseUpAsButton()
		{
			if (this._useLeftClick)
			{
				this.CheckClick(true);
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005EC20 File Offset: 0x0005CE20
		private void Update()
		{
			if (this.isMouseOver)
			{
				if (this._useRightClick && this._player.GetButtonUp(33))
				{
					this.CheckClick(true);
					return;
				}
				if (this._useRightClick && this._player.GetButtonUp(37))
				{
					this.CheckClick(false);
				}
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005EC72 File Offset: 0x0005CE72
		private void OnMouseEnter()
		{
			this.isMouseOver = true;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005EC7B File Offset: 0x0005CE7B
		private void OnMouseExit()
		{
			this.isMouseOver = false;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0005EC84 File Offset: 0x0005CE84
		private void CheckClick(bool moveForward)
		{
			if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject() && this._controller != null)
			{
				this._controller.HatClicked(moveForward);
			}
		}

		// Token: 0x04000E5A RID: 3674
		private HatController _controller;

		// Token: 0x04000E5B RID: 3675
		[SerializeField]
		private bool _useRightClick = true;

		// Token: 0x04000E5C RID: 3676
		[SerializeField]
		private bool _useLeftClick;

		// Token: 0x04000E5D RID: 3677
		private Player _player;

		// Token: 0x04000E5E RID: 3678
		private bool isMouseOver;
	}
}
