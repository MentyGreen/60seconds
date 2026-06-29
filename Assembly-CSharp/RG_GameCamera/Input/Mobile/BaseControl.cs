using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001A4 RID: 420
	public abstract class BaseControl : MonoBehaviour
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600122A RID: 4650
		public abstract ControlType Type { get; }

		// Token: 0x0600122B RID: 4651 RVA: 0x0004E270 File Offset: 0x0004C470
		public virtual void Init(TouchProcessor processor)
		{
			base.hideFlags = HideFlags.HideInInspector;
			this.touchProcessor = processor;
			this.TouchIndex = -1;
		}

		// Token: 0x0600122C RID: 4652
		public abstract void GameUpdate();

		// Token: 0x0600122D RID: 4653
		public abstract void Draw();

		// Token: 0x0600122E RID: 4654 RVA: 0x0004E287 File Offset: 0x0004C487
		public virtual Vector2 GetInputAxis()
		{
			return Vector2.zero;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004E28E File Offset: 0x0004C48E
		public virtual bool AbortUpdateOtherControls()
		{
			return false;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004E294 File Offset: 0x0004C494
		protected virtual void DetectTouches()
		{
			int activeTouchCount = this.touchProcessor.GetActiveTouchCount();
			if (activeTouchCount == 0)
			{
				this.TouchIndex = -1;
				return;
			}
			if (this.TouchIndex == -1)
			{
				for (int i = 0; i < activeTouchCount; i++)
				{
					SimTouch touch = this.touchProcessor.GetTouch(i);
					if (touch.Status != TouchStatus.Invalid && this.IsSide(touch.StartPosition) && this.TouchIndex == -1)
					{
						this.TouchIndex = i;
					}
				}
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004E300 File Offset: 0x0004C500
		protected bool IsSide(Vector2 pos)
		{
			if (this.Side == ControlSide.Arbitrary)
			{
				return true;
			}
			if (pos.x < (float)Screen.width * 0.5f)
			{
				return this.Side == ControlSide.Left;
			}
			return this.Side == ControlSide.Right;
		}

		// Token: 0x04000BC0 RID: 3008
		public Vector2 Position;

		// Token: 0x04000BC1 RID: 3009
		public Vector2 Size;

		// Token: 0x04000BC2 RID: 3010
		public bool PreserveTextureRatio = true;

		// Token: 0x04000BC3 RID: 3011
		public ControlSide Side;

		// Token: 0x04000BC4 RID: 3012
		public int TouchIndex;

		// Token: 0x04000BC5 RID: 3013
		public int TouchIndexAux;

		// Token: 0x04000BC6 RID: 3014
		public string InputKey0;

		// Token: 0x04000BC7 RID: 3015
		public string InputKey1;

		// Token: 0x04000BC8 RID: 3016
		public bool HideGUI;

		// Token: 0x04000BC9 RID: 3017
		public int Priority = 1;

		// Token: 0x04000BCA RID: 3018
		protected TouchProcessor touchProcessor;
	}
}
