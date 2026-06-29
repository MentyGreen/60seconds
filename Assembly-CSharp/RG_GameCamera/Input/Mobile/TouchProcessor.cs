using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001AB RID: 427
	public class TouchProcessor
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x0004F1B8 File Offset: 0x0004D3B8
		public TouchProcessor(int numberOfTouches)
		{
			this.touches = new SimTouch[numberOfTouches];
			for (int i = 0; i < this.touches.Length; i++)
			{
				this.touches[i] = new SimTouch(i, KeyCode.LeftAlt);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004F1FD File Offset: 0x0004D3FD
		public SimTouch[] GetTouches()
		{
			return this.touches;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004F205 File Offset: 0x0004D405
		public int GetTouchCount()
		{
			return this.touches.Length;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004F210 File Offset: 0x0004D410
		public int GetActiveTouchCount()
		{
			int num = 0;
			SimTouch[] array = this.touches;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Status != TouchStatus.Invalid)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004F243 File Offset: 0x0004D443
		public SimTouch GetTouch(int index)
		{
			return this.touches[index];
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004F250 File Offset: 0x0004D450
		public void ScanInput()
		{
			for (int i = 0; i < this.touches.Length; i++)
			{
				this.touches[i].ScanInput();
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004F27D File Offset: 0x0004D47D
		public void ShowDebug(bool status)
		{
		}

		// Token: 0x04000BF8 RID: 3064
		private readonly SimTouch[] touches;
	}
}
