using System;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000342 RID: 834
	public class OnLightClick : MonoBehaviour
	{
		// Token: 0x06001BBE RID: 7102 RVA: 0x0007720D File Offset: 0x0007540D
		private void OnMouseDown()
		{
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0007720F File Offset: 0x0007540F
		private void OnMouseUp()
		{
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00077211 File Offset: 0x00075411
		private void OnMouseOver()
		{
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00077213 File Offset: 0x00075413
		private void OnMouseEnter()
		{
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00077215 File Offset: 0x00075415
		private void OnMouseExit()
		{
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00077217 File Offset: 0x00075417
		private void OnMouseUpAsButton()
		{
			if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject())
			{
				this._flickerLights.StartFlicking();
			}
		}

		// Token: 0x04001590 RID: 5520
		[SerializeField]
		private FlickerLights _flickerLights;
	}
}
