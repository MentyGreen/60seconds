using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200029D RID: 669
	public class TvButtonsController : MonoBehaviour
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x0006A1BC File Offset: 0x000683BC
		public void SwitchRandomSelectable()
		{
			int num = Random.Range(0, this._tvButtons.Length);
			ExecuteEvents.Execute<ISubmitHandler>(this._tvButtons[num].gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
		}

		// Token: 0x040011F1 RID: 4593
		[SerializeField]
		private GameObject[] _tvButtons;
	}
}
