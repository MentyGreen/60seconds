using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.Remaster.Scavenge
{
	// Token: 0x0200023D RID: 573
	public class ScavengeItemManager : MonoBehaviour
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00060965 File Offset: 0x0005EB65
		public static ScavengeItemManager Instance
		{
			get
			{
				if (ScavengeItemManager._instance == null)
				{
					ScavengeItemManager.FindInstanceInScene();
				}
				return ScavengeItemManager._instance;
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0006097E File Offset: 0x0005EB7E
		private static void FindInstanceInScene()
		{
			ScavengeItemManager._instance = Object.FindObjectOfType<ScavengeItemManager>();
			if (ScavengeItemManager._instance == null)
			{
				Debug.LogError("No ScavengeItemManager in scene!");
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000609A1 File Offset: 0x0005EBA1
		private void Awake()
		{
			if (ScavengeItemManager._instance == null)
			{
				ScavengeItemManager._instance = this;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000609B6 File Offset: 0x0005EBB6
		public List<ScavengeItemController> ScavengeItems
		{
			get
			{
				return this._scavengeItems;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000609BE File Offset: 0x0005EBBE
		public void RegisterController(ScavengeItemController controller)
		{
			if (controller != null && !this._scavengeItems.Contains(controller))
			{
				this._scavengeItems.Add(controller);
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000609E3 File Offset: 0x0005EBE3
		public void UnregisterController(ScavengeItemController controller)
		{
			if (controller != null && this._scavengeItems.Contains(controller))
			{
				this._scavengeItems.Remove(controller);
			}
		}

		// Token: 0x04000EAE RID: 3758
		private static ScavengeItemManager _instance;

		// Token: 0x04000EAF RID: 3759
		private List<ScavengeItemController> _scavengeItems = new List<ScavengeItemController>();
	}
}
