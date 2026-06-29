using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200023A RID: 570
	public class SkinManager : MonoBehaviour
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00060775 File Offset: 0x0005E975
		public static SkinManager Instance
		{
			get
			{
				if (SkinManager._instance == null)
				{
					SkinManager.FindInstanceInScene();
				}
				return SkinManager._instance;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0006078E File Offset: 0x0005E98E
		private static void FindInstanceInScene()
		{
			SkinManager._instance = Object.FindObjectOfType<SkinManager>();
			if (SkinManager._instance == null)
			{
				Debug.LogError("No SkinManager in scene!");
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000607B1 File Offset: 0x0005E9B1
		private void Awake()
		{
			if (SkinManager._instance == null)
			{
				SkinManager._instance = this;
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000607C6 File Offset: 0x0005E9C6
		public void RegisterSkinController(SkinController controller)
		{
			if (!this._skinControllers.Contains(controller))
			{
				this._skinControllers.Add(controller);
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000607E2 File Offset: 0x0005E9E2
		public void UnregisterSkinController(SkinController controller)
		{
			if (this._skinControllers.Contains(controller))
			{
				this._skinControllers.Remove(controller);
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00060800 File Offset: 0x0005EA00
		public void AllowChangingSkins(SkinDataList dataList)
		{
			for (int i = 0; i < this._skinControllers.Count; i++)
			{
				if (this._skinControllers[i].DataList != null && this._skinControllers[i].DataList.Equals(dataList))
				{
					this._skinControllers[i].AllowSkinChange();
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00060868 File Offset: 0x0005EA68
		public void ForceSkinUse(SkinDataList dataList, SkinId skinId)
		{
			for (int i = 0; i < this._skinControllers.Count; i++)
			{
				if (this._skinControllers[i].DataList != null && this._skinControllers[i].DataList.Equals(dataList))
				{
					this._skinControllers[i].ForceSkinUse(skinId);
				}
			}
		}

		// Token: 0x04000EA9 RID: 3753
		private static SkinManager _instance;

		// Token: 0x04000EAA RID: 3754
		[SerializeField]
		private List<SkinController> _skinControllers = new List<SkinController>();
	}
}
