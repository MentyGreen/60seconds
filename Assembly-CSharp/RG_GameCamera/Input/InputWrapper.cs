using System;
using RG_GameCamera.Input.Mobile;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x0200019C RID: 412
	public class InputWrapper
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x0004D76C File Offset: 0x0004B96C
		public static bool GetButton(string key)
		{
			if (InputWrapper.Mobile)
			{
				return MobileControls.Instance.GetButton(key);
			}
			return Input.GetButton(key);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004D787 File Offset: 0x0004B987
		public static float GetZoom(string key)
		{
			if (InputWrapper.Mobile)
			{
				return MobileControls.Instance.GetZoom(key);
			}
			return 0f;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004D7A1 File Offset: 0x0004B9A1
		public static float GetAxis(string key)
		{
			if (InputWrapper.Mobile)
			{
				return MobileControls.Instance.GetAxis(key);
			}
			return InputHandler.Instance.GetAxis(key);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004D7C1 File Offset: 0x0004B9C1
		public static bool GetButtonDown(string buttonName)
		{
			if (InputWrapper.Mobile)
			{
				return MobileControls.Instance.GetButtonDown(buttonName);
			}
			return Input.GetButtonDown(buttonName);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004D7DC File Offset: 0x0004B9DC
		public static bool GetButtonUp(string buttonName)
		{
			if (InputWrapper.Mobile)
			{
				return MobileControls.Instance.GetButtonUp(buttonName);
			}
			return Input.GetButton(buttonName);
		}

		// Token: 0x04000BA7 RID: 2983
		public static bool Mobile;
	}
}
