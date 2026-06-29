using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A6 RID: 678
	public class FullscreenSettingController : MonoBehaviour
	{
		// Token: 0x06001875 RID: 6261 RVA: 0x0006AEE8 File Offset: 0x000690E8
		private void OnEnable()
		{
			this._knobAnimator.Play(this._isFullScreen.Value ? "Value_Knob_Right" : "Value_Knob_Left");
			this._knobAnimator.SetBool(FullscreenSettingController.Right, this._isFullScreen.Value);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0006AF34 File Offset: 0x00069134
		public void SwitchFullscreen()
		{
			this.SetFullscreen(!this._isFullScreen.Value);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0006AF4C File Offset: 0x0006914C
		public void SetFullscreen(bool fullscreen)
		{
			this._isFullScreen.Value = fullscreen;
			this._knobAnimator.SetBool(FullscreenSettingController.Right, fullscreen);
			if (this._applyInstantly)
			{
				Screen.SetResolution(this._widthVariable.Value, this._heightVariable.Value, this._isFullScreen.Value);
			}
		}

		// Token: 0x0400121E RID: 4638
		[SerializeField]
		private Animator _knobAnimator;

		// Token: 0x0400121F RID: 4639
		[SerializeField]
		private GlobalBoolVariable _isFullScreen;

		// Token: 0x04001220 RID: 4640
		[SerializeField]
		private GlobalIntVariable _widthVariable;

		// Token: 0x04001221 RID: 4641
		[SerializeField]
		private GlobalIntVariable _heightVariable;

		// Token: 0x04001222 RID: 4642
		[SerializeField]
		private bool _applyInstantly;

		// Token: 0x04001223 RID: 4643
		private static readonly int Right = Animator.StringToHash("Right");

		// Token: 0x04001224 RID: 4644
		private const string NO_KNOB_STATE = "Value_Knob_Left";

		// Token: 0x04001225 RID: 4645
		private const string YES_KNOB_STATE = "Value_Knob_Right";

		// Token: 0x04001226 RID: 4646
		private const string RIGHT_PARAM_NAME = "Right";
	}
}
