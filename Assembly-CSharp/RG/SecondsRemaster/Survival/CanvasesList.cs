using System;
using System.Collections.Generic;
using NodeEditorFramework;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031C RID: 796
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New Canvases List", fileName = "New Canvases List")]
	public class CanvasesList : ScriptableObject
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000744FE File Offset: 0x000726FE
		public List<NodeCanvas> Canvases
		{
			get
			{
				return this._canvases;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00074506 File Offset: 0x00072706
		public bool Contains(NodeCanvas canvas)
		{
			return this._canvases.Contains(canvas);
		}

		// Token: 0x040014AB RID: 5291
		[SerializeField]
		private List<NodeCanvas> _canvases;
	}
}
