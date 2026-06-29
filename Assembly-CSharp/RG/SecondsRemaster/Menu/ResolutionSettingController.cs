using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002AC RID: 684
	public class ResolutionSettingController : MonoBehaviour
	{
		// Token: 0x0600188D RID: 6285 RVA: 0x0006B61C File Offset: 0x0006981C
		private void Awake()
		{
			this._resolutions = new List<Resolution>(Screen.resolutions);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0006B630 File Offset: 0x00069830
		private void OnEnable()
		{
			this._currentIndex = this.GetCurrentResolutionIndex(this._widthVariable.Value, this._heightVariable.Value);
			if (this._currentIndex == -1)
			{
				this._currentIndex = Screen.resolutions.Length - 1;
			}
			this.SetCurrentResolutionValues();
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0006B680 File Offset: 0x00069880
		private int GetCurrentResolutionIndex(int width, int height)
		{
			Resolution[] resolutions = Screen.resolutions;
			int result = -1;
			for (int i = resolutions.Length - 1; i >= 0; i--)
			{
				if (resolutions[i].width == width && resolutions[i].height == height)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0006B6C8 File Offset: 0x000698C8
		public void SetNext()
		{
			bool flag = false;
			if (this._currentIndex + 1 < this._resolutions.Count)
			{
				this._currentIndex++;
				flag = true;
			}
			if (flag)
			{
				Resolution resolution = this._resolutions[this._currentIndex];
				if (resolution.width == this._widthVariable.Value && resolution.height == this._heightVariable.Value)
				{
					this.SetNext();
					return;
				}
				this.SetCurrentResolutionValues();
				if (this._applyInstantly)
				{
					Screen.SetResolution(resolution.width, resolution.height, this._isFullScreen.Value);
				}
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0006B76C File Offset: 0x0006996C
		public void SetPrevious()
		{
			bool flag = false;
			if (this._currentIndex - 1 >= 0)
			{
				this._currentIndex--;
				flag = true;
			}
			if (flag)
			{
				Resolution resolution = this._resolutions[this._currentIndex];
				if (resolution.width == this._widthVariable.Value && resolution.height == this._heightVariable.Value)
				{
					this.SetPrevious();
					return;
				}
				this.SetCurrentResolutionValues();
				if (this._applyInstantly)
				{
					Screen.SetResolution(resolution.width, resolution.height, this._isFullScreen.Value, resolution.refreshRate);
				}
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0006B810 File Offset: 0x00069A10
		private void SetCurrentResolutionValues()
		{
			this._valueField.text = string.Format("{0} x {1}", this._resolutions[this._currentIndex].width, this._resolutions[this._currentIndex].height);
			this._widthVariable.Value = this._resolutions[this._currentIndex].width;
			this._heightVariable.Value = this._resolutions[this._currentIndex].height;
		}

		// Token: 0x04001243 RID: 4675
		[SerializeField]
		private TextMeshProUGUI _valueField;

		// Token: 0x04001244 RID: 4676
		[SerializeField]
		private GlobalIntVariable _widthVariable;

		// Token: 0x04001245 RID: 4677
		[SerializeField]
		private GlobalIntVariable _heightVariable;

		// Token: 0x04001246 RID: 4678
		[SerializeField]
		private GlobalBoolVariable _isFullScreen;

		// Token: 0x04001247 RID: 4679
		[SerializeField]
		private bool _applyInstantly = true;

		// Token: 0x04001248 RID: 4680
		private List<Resolution> _resolutions;

		// Token: 0x04001249 RID: 4681
		private int _currentIndex;

		// Token: 0x0400124A RID: 4682
		private const int RESOLUTION_NOT_FOUND = -1;
	}
}
