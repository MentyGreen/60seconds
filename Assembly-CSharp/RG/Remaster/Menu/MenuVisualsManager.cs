using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Remaster.Menu
{
	// Token: 0x02000227 RID: 551
	public class MenuVisualsManager : MonoBehaviour
	{
		// Token: 0x06001563 RID: 5475 RVA: 0x0005E7DC File Offset: 0x0005C9DC
		public void Start()
		{
			if (this._isContinueAvailable != null && this._isContinueAvailable.Value && this._postapoVisualsSetupFuntion != null)
			{
				if (this._objectsToActivateInPostapo != null)
				{
					for (int i = 0; i < this._objectsToActivateInPostapo.Count; i++)
					{
						this._objectsToActivateInPostapo[i].SetActive(true);
					}
				}
				this._postapoVisualsSetupFuntion.Execute(null);
			}
			else if (this._regularVisualsSetupFuntion != null)
			{
				this._regularVisualsSetupFuntion.Execute(null);
			}
			VisualsManager.Instance.RefreshVisualsStateCoroutine();
		}

		// Token: 0x04000E41 RID: 3649
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;

		// Token: 0x04000E42 RID: 3650
		[SerializeField]
		private NodeFunction _regularVisualsSetupFuntion;

		// Token: 0x04000E43 RID: 3651
		[SerializeField]
		private NodeFunction _postapoVisualsSetupFuntion;

		// Token: 0x04000E44 RID: 3652
		[SerializeField]
		private List<GameObject> _objectsToActivateInPostapo;
	}
}
