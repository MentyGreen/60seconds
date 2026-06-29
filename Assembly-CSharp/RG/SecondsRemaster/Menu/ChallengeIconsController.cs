using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000292 RID: 658
	public class ChallengeIconsController : MonoBehaviour
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x00069BF8 File Offset: 0x00067DF8
		public void DisableAllIcons()
		{
			for (int i = 0; i < this._iconLists.Length; i++)
			{
				IconList iconList = this._iconLists[i];
				for (int j = 0; j < iconList.Icons.Length; j++)
				{
					iconList.Icons[j].gameObject.SetActive(false);
				}
				this._rows[i].SetActive(false);
			}
			this._currentRow = 0;
			this._currentElement = 0;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00069C64 File Offset: 0x00067E64
		public void SetNextIcon(Sprite sprite)
		{
			if (this._currentRow >= this._rows.Length || this._currentElement >= this._iconLists[this._currentRow].Icons.Length)
			{
				Debug.LogErrorFormat("There was not enough space for all Collectables for this Challenge ('{1}')!", Array.Empty<object>());
				return;
			}
			if (!this._rows[this._currentRow].activeSelf)
			{
				this._rows[this._currentRow].SetActive(true);
			}
			IconList iconList = this._iconLists[this._currentRow];
			Image image = iconList.Icons[this._currentElement];
			image.sprite = sprite;
			image.gameObject.SetActive(true);
			this._currentElement++;
			if (this._currentElement >= iconList.Icons.Length)
			{
				this._currentRow++;
				this._currentElement = 0;
			}
		}

		// Token: 0x040011C6 RID: 4550
		[SerializeField]
		private GameObject[] _rows;

		// Token: 0x040011C7 RID: 4551
		[SerializeField]
		private IconList[] _iconLists;

		// Token: 0x040011C8 RID: 4552
		private int _currentRow;

		// Token: 0x040011C9 RID: 4553
		private int _currentElement;
	}
}
