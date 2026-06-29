using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001A9 RID: 425
	public class SimTouch
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0004EC19 File Offset: 0x0004CE19
		public SimTouch(int fingerID, KeyCode simKey)
		{
			this.FingerId = fingerID;
			SimTouch.SimulationKey = simKey;
			this.lastMouseStatus = SimTouch.MouseStatus.None;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004EC4B File Offset: 0x0004CE4B
		public void ScanInput()
		{
			this.UpdateTouchSim();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004EC54 File Offset: 0x0004CE54
		private SimTouch.MouseStatus GetMouseStatus()
		{
			bool flag = true;
			if (Input.GetKey(SimTouch.SimulationKey))
			{
				if (this.FingerId > 0)
				{
					if (this.lastMouseStatus != SimTouch.MouseStatus.None && this.lastMouseStatus != SimTouch.MouseStatus.StartDown)
					{
						this.DeltaPosition = Vector3.zero;
						this.Position = this.lastPosition;
						return this.lastMouseStatus;
					}
					flag = false;
					if (Input.GetMouseButtonDown(0))
					{
						return SimTouch.MouseStatus.StartDown;
					}
					if (Input.GetMouseButton(0))
					{
						return SimTouch.MouseStatus.Down;
					}
				}
			}
			else if (this.FingerId > 0)
			{
				return SimTouch.MouseStatus.None;
			}
			if (flag)
			{
				if (Input.GetMouseButtonDown(0))
				{
					return SimTouch.MouseStatus.StartDown;
				}
				if (Input.GetMouseButton(0))
				{
					return SimTouch.MouseStatus.Down;
				}
				if (Input.GetMouseButtonUp(0))
				{
					return SimTouch.MouseStatus.Up;
				}
			}
			return SimTouch.MouseStatus.None;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
		private void UpdateTouchSim()
		{
			this.DeltaTime = Time.deltaTime;
			this.Position = Input.mousePosition;
			this.DeltaPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - this.lastPosition;
			SimTouch.MouseStatus mouseStatus = this.GetMouseStatus();
			this.lastMouseStatus = mouseStatus;
			switch (mouseStatus)
			{
			case SimTouch.MouseStatus.StartDown:
				this.Status = TouchStatus.Start;
				this.StartPosition = Input.mousePosition;
				this.DeltaPosition = Vector2.zero;
				this.lastPosition = Input.mousePosition;
				this.tapTimeout = this.TapTimeWindow;
				break;
			case SimTouch.MouseStatus.Down:
				this.Status = TouchStatus.Stationary;
				if (this.DeltaPosition.sqrMagnitude > this.MoveThreshold * this.MoveThreshold)
				{
					this.Status = TouchStatus.Moving;
					this.lastPosition = this.Position;
				}
				break;
			case SimTouch.MouseStatus.Up:
				this.Status = TouchStatus.End;
				if (this.tapTimeout > 0f)
				{
					this.TapCount++;
				}
				break;
			case SimTouch.MouseStatus.None:
				if (this.Status != TouchStatus.Invalid && this.Status != TouchStatus.End)
				{
					this.Status = TouchStatus.End;
				}
				else if (this.Status == TouchStatus.End)
				{
					this.Status = TouchStatus.Invalid;
				}
				break;
			}
			this.tapTimeout -= Time.deltaTime;
			if (this.tapTimeout < 0f)
			{
				this.TapCount = 1;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004EE58 File Offset: 0x0004D058
		private bool GetTouchByID(int id, out Touch touch)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch2 = Input.GetTouch(i);
				if (touch2.fingerId == id)
				{
					touch = touch2;
					return true;
				}
			}
			touch = default(Touch);
			return false;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004EE98 File Offset: 0x0004D098
		private void UpdateTouchInput()
		{
			Touch touch;
			if (this.GetTouchByID(this.FingerId, out touch))
			{
				this.DeltaPosition = touch.position - this.lastPosition;
				this.DeltaTime = touch.deltaTime;
				this.Position = touch.position;
				this.TapCount = touch.tapCount;
				switch (touch.phase)
				{
				case TouchPhase.Began:
					this.StartPosition = touch.position;
					this.DeltaPosition = Vector2.zero;
					this.lastPosition = this.Position;
					this.Status = TouchStatus.Start;
					this.PressTime = 0f;
					return;
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					this.Status = TouchStatus.Stationary;
					if (this.DeltaPosition.sqrMagnitude > this.MoveThreshold * this.MoveThreshold)
					{
						this.Status = TouchStatus.Moving;
						this.lastPosition = this.Position;
					}
					this.PressTime += Time.deltaTime;
					return;
				case TouchPhase.Ended:
					this.Status = TouchStatus.End;
					return;
				default:
					this.Status = TouchStatus.Invalid;
					return;
				}
			}
			else
			{
				if (this.Status != TouchStatus.Invalid && this.Status != TouchStatus.End)
				{
					this.Status = TouchStatus.End;
					return;
				}
				if (this.Status == TouchStatus.End)
				{
					this.Status = TouchStatus.Invalid;
				}
				return;
			}
		}

		// Token: 0x04000BE3 RID: 3043
		public int FingerId;

		// Token: 0x04000BE4 RID: 3044
		public Vector2 Position;

		// Token: 0x04000BE5 RID: 3045
		public Vector2 StartPosition;

		// Token: 0x04000BE6 RID: 3046
		public TouchStatus Status;

		// Token: 0x04000BE7 RID: 3047
		public int TapCount;

		// Token: 0x04000BE8 RID: 3048
		public float DeltaTime;

		// Token: 0x04000BE9 RID: 3049
		public Vector2 DeltaPosition;

		// Token: 0x04000BEA RID: 3050
		public float TapTimeWindow = 0.3f;

		// Token: 0x04000BEB RID: 3051
		public float PressTime;

		// Token: 0x04000BEC RID: 3052
		public float MoveThreshold = 0.5f;

		// Token: 0x04000BED RID: 3053
		private Vector2 lastPosition;

		// Token: 0x04000BEE RID: 3054
		private static KeyCode SimulationKey;

		// Token: 0x04000BEF RID: 3055
		private float tapTimeout;

		// Token: 0x04000BF0 RID: 3056
		private SimTouch.MouseStatus lastMouseStatus;

		// Token: 0x020003E2 RID: 994
		private enum MouseStatus
		{
			// Token: 0x0400180E RID: 6158
			StartDown,
			// Token: 0x0400180F RID: 6159
			Down,
			// Token: 0x04001810 RID: 6160
			Up,
			// Token: 0x04001811 RID: 6161
			None
		}
	}
}
