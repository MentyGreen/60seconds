using System;
using RG_GameCamera.Config;
using RG_GameCamera.Input;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x02000192 RID: 402
	[RequireComponent(typeof(RTSConfig))]
	public class RTSCameraMode : CameraMode
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004B886 File Offset: 0x00049A86
		public override Type Type
		{
			get
			{
				return Type.RTS;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004B889 File Offset: 0x00049A89
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<RTSConfig>();
			RG_GameCamera.Utils.DebugDraw.Enabled = true;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004B8BC File Offset: 0x00049ABC
		public override void OnActivate()
		{
			base.OnActivate();
			this.config.SetCameraMode("Default");
			this.targetDistance = this.config.GetFloat("Distance");
			this.groundPlane = new Plane(Vector3.up, this.config.GetFloat("GroundOffset"));
			if (this.Target)
			{
				this.cameraTarget = this.Target.position;
			}
			this.UpdateYAngle();
			this.UpdateXAngle(true);
			this.activateTimeout = 2f;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004B94C File Offset: 0x00049B4C
		public override void SetCameraTarget(Transform target)
		{
			base.SetCameraTarget(target);
			if (target)
			{
				this.cameraTarget = target.position;
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004B96C File Offset: 0x00049B6C
		private bool RotateCamera(Vector2 mousePosition)
		{
			if (this.config.GetBool("EnableRotation") && mousePosition.sqrMagnitude > Mathf.Epsilon)
			{
				this.rotX += this.config.GetFloat("RotationSpeed") * mousePosition.x * 0.01f;
				return true;
			}
			return false;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004B9C8 File Offset: 0x00049BC8
		private void DragCamera(Vector2 mousePosition)
		{
			if (this.panning)
			{
				this.UnityCamera.transform.position = this.panCameraPos;
				Ray ray = this.UnityCamera.ScreenPointToRay(mousePosition);
				float d = 0f;
				Vector3 a;
				if (this.groundPlane.Raycast(ray, out d))
				{
					a = ray.origin + ray.direction * d;
				}
				else
				{
					a = ray.origin + ray.direction * this.targetDistance;
				}
				Vector3 b = a - this.panMousePosition;
				b.y = 0f;
				Vector3 vector = this.panCameraTarget - b;
				this.ClampWithinMapBounds(this.cameraTarget, ref vector, true);
				this.dragVelocity = vector - this.cameraTarget;
				this.dragSlowdown = 1f;
				this.cameraTarget = vector;
				return;
			}
			this.panCameraTarget = this.cameraTarget;
			this.panCameraPos = this.UnityCamera.transform.position;
			Ray ray2 = this.UnityCamera.ScreenPointToRay(mousePosition);
			Vector3 vector2;
			if (GameInput.FindWaypointPosition(mousePosition, out vector2))
			{
				this.groundPlane.distance = vector2.y;
			}
			float d2 = 0f;
			if (this.groundPlane.Raycast(ray2, out d2))
			{
				this.panMousePosition = ray2.origin + ray2.direction * d2;
			}
			else
			{
				this.panMousePosition = ray2.origin + ray2.direction * this.targetDistance;
			}
			this.panning = true;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004BB6C File Offset: 0x00049D6C
		private void UpdateDragMomentum()
		{
			if (this.dragVelocity.sqrMagnitude > Mathf.Epsilon)
			{
				this.dragSlowdown -= Time.deltaTime;
				if (this.dragSlowdown < 0f)
				{
					this.dragSlowdown = 0f;
				}
				this.dragVelocity *= this.dragSlowdown;
				this.cameraTarget += this.dragVelocity * Time.deltaTime * this.config.GetFloat("DragMomentum") * 100f;
				this.ClampWithinMapBounds(this.cameraTarget, ref this.cameraTarget, true);
			}
			Vector2 vector = this.config.GetVector2("MapCenter");
			Vector2 vector2 = this.config.GetVector2("MapSize");
			float @float = this.config.GetFloat("SoftBorder");
			if (this.cameraTarget.x > vector.x + vector2.x / 2f)
			{
				float num = (this.cameraTarget.x - (vector.x + vector2.x / 2f)) / @float;
				this.cameraTarget.x = this.cameraTarget.x - Time.deltaTime * 40f * num;
			}
			else if (this.cameraTarget.x < vector.x - vector2.x / 2f)
			{
				float num = (-this.cameraTarget.x + vector.x - vector2.x / 2f) / @float;
				this.cameraTarget.x = this.cameraTarget.x + Time.deltaTime * 40f * num;
			}
			if (this.cameraTarget.z > vector.y + vector2.y / 2f)
			{
				float num = (this.cameraTarget.z - (vector.y + vector2.y / 2f)) / @float;
				this.cameraTarget.z = this.cameraTarget.z - Time.deltaTime * 40f * num;
				return;
			}
			if (this.cameraTarget.z < vector.y - vector2.y / 2f)
			{
				float num = (-this.cameraTarget.z + vector.y - vector2.y / 2f) / @float;
				this.cameraTarget.z = this.cameraTarget.z + Time.deltaTime * 40f * num;
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004BDD8 File Offset: 0x00049FD8
		private void MoveCamera(Vector2 move)
		{
			if (move.sqrMagnitude <= Mathf.Epsilon)
			{
				return;
			}
			move *= 0.1f * this.config.GetFloat("MoveSpeed") * base.GetZoomFactor();
			Vector3 forward = this.UnityCamera.transform.forward;
			forward.y = 0f;
			forward.Normalize();
			Vector3 right = this.UnityCamera.transform.right;
			right.y = 0f;
			right.Normalize();
			Vector3 b = forward * move.y + right * move.x;
			Vector3 cameraTarget = this.cameraTarget + b;
			this.ClampWithinMapBounds(this.cameraTarget, ref cameraTarget, false);
			this.cameraTarget = cameraTarget;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004BEA4 File Offset: 0x0004A0A4
		private void MoveCameraByScreenBorder(Vector2 mousePosition)
		{
			Vector2 vector = mousePosition;
			vector.y = (float)Screen.height - vector.y;
			float @float = this.config.GetFloat("ScreenBorderOffset");
			Vector2 zero = Vector2.zero;
			float num = 0f;
			if (vector.x <= @float)
			{
				zero.x = -1f;
				num = 1f - vector.x / @float;
			}
			else if (vector.x >= (float)Screen.width - @float)
			{
				zero.x = 1f;
				num = 1f - ((float)Screen.width - vector.x) / @float;
			}
			if (vector.y >= (float)Screen.height - @float)
			{
				zero.y = -1f;
				num = 1f - ((float)Screen.height - vector.y) / @float;
			}
			else if (vector.y <= @float)
			{
				zero.y = 1f;
				num = 1f - vector.y / @float;
			}
			if (zero.sqrMagnitude > Mathf.Epsilon)
			{
				zero.Normalize();
				num = Mathf.Clamp01(num);
				Vector2 vector2 = zero * Time.deltaTime * num * this.config.GetFloat("ScreenBorderSpeed") * base.GetZoomFactor();
				Vector3 forward = this.UnityCamera.transform.forward;
				forward.y = 0f;
				forward.Normalize();
				Vector3 right = this.UnityCamera.transform.right;
				right.y = 0f;
				right.Normalize();
				Vector3 b = forward * vector2.y + right * vector2.x;
				Vector3 cameraTarget = Vector3.Lerp(this.cameraTarget, this.cameraTarget + b, Time.deltaTime * 50f);
				this.ClampWithinMapBounds(this.cameraTarget, ref cameraTarget, false);
				this.cameraTarget = cameraTarget;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004C08F File Offset: 0x0004A28F
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this.config.GetFloat("FOV");
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004C0AC File Offset: 0x0004A2AC
		private void UpdateYAngle()
		{
			Math.ToSpherical(this.UnityCamera.transform.forward, out this.rotX, out this.rotY);
			float num = (this.targetDistance - this.config.GetFloat("DistanceMin")) / (this.config.GetFloat("DistanceMax") - this.config.GetFloat("DistanceMin"));
			float num2 = this.config.GetFloat("AngleZoomMin") * (1f - num) + this.config.GetFloat("AngleY") * num;
			this.rotY = Mathf.Lerp(this.rotY, num2 * -1f * 0.017453292f, Time.deltaTime * 50f);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004C16C File Offset: 0x0004A36C
		private void UpdateXAngle(bool force)
		{
			if (!this.config.GetBool("EnableRotation") || force || this.activateTimeout > 0f)
			{
				this.rotX = this.config.GetFloat("DefaultAngleX") * -0.017453292f;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		private void UpdateDir()
		{
			Vector3 forward;
			Math.ToCartesian(this.rotX, this.rotY, out forward);
			this.UnityCamera.transform.forward = forward;
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.targetDistance;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004C223 File Offset: 0x0004A423
		private void UpdateConfig()
		{
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004C225 File Offset: 0x0004A425
		private void UpdateDistance()
		{
			if (this.Target && this.config.GetBool("FollowTargetY"))
			{
				this.cameraTarget.y = this.Target.position.y;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004C264 File Offset: 0x0004A464
		private void UpdateZoom()
		{
			Math.ToSpherical(this.UnityCamera.transform.forward, out this.rotX, out this.rotY);
			if (Mathf.Abs(this.targetZoom) > Mathf.Epsilon)
			{
				float num = this.targetZoom * 20f * Time.deltaTime;
				if (Mathf.Abs(num) > Mathf.Abs(this.targetZoom))
				{
					num = this.targetZoom;
				}
				this.Zoom(num);
				this.targetZoom -= num;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004C2E8 File Offset: 0x0004A4E8
		private bool IsInMapBounds(Vector3 point)
		{
			Math.Swap<float>(ref point.y, ref point.z);
			Vector2 vector = this.config.GetVector2("MapCenter");
			Vector2 vector2 = this.config.GetVector2("MapSize");
			Rect rect = new Rect(vector.x - vector2.x / 2f, vector.y - vector2.y / 2f, vector2.x, vector2.y);
			return rect.Contains(point);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004C36C File Offset: 0x0004A56C
		private void ClampWithinMapBounds(Vector3 currTarget, ref Vector3 point, bool border)
		{
			Vector2 vector = this.config.GetVector2("MapCenter");
			Vector2 vector2 = this.config.GetVector2("MapSize");
			if (this.config.GetBool("DisableHorizontal"))
			{
				point.x = currTarget.x;
			}
			if (this.config.GetBool("DisableVertical"))
			{
				point.z = currTarget.z;
			}
			float num = this.config.GetFloat("SoftBorder");
			if (!border)
			{
				num = 0f;
			}
			if (point.x > vector.x + vector2.x / 2f + num)
			{
				point.x = vector.x + vector2.x / 2f + num;
			}
			else if (point.x < vector.x - vector2.x / 2f - num)
			{
				point.x = vector.x - vector2.x / 2f - num;
			}
			if (point.z > vector.y + vector2.y / 2f + num)
			{
				point.z = vector.y + vector2.y / 2f + num;
				return;
			}
			if (point.z < vector.y - vector2.y / 2f - num)
			{
				point.z = vector.y - vector2.y / 2f - num;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004C4D4 File Offset: 0x0004A6D4
		public void Zoom(float amount)
		{
			if (!this.config.GetBool("EnableZoom"))
			{
				return;
			}
			float num = amount * this.config.GetFloat("ZoomSpeed");
			if (Mathf.Abs(num) > Mathf.Epsilon)
			{
				if (this.UnityCamera.orthographic)
				{
					float zoomFactor = base.GetZoomFactor();
					num *= zoomFactor;
					this.UnityCamera.orthographicSize -= num;
					if (this.UnityCamera.orthographicSize < 0.01f)
					{
						this.UnityCamera.orthographicSize = 0.01f;
					}
				}
				else
				{
					float num2 = base.GetZoomFactor();
					if (num2 < 0.01f)
					{
						num2 = 0.01f;
					}
					num *= num2;
					Vector3 b = this.UnityCamera.transform.forward * num;
					Vector3 vector = this.UnityCamera.transform.position + b;
					Plane plane = new Plane(this.UnityCamera.transform.forward, this.cameraTarget);
					if (!plane.GetSide(vector))
					{
						this.UnityCamera.transform.position = vector;
					}
				}
				this.targetDistance = (this.UnityCamera.transform.position - this.cameraTarget).magnitude;
				this.targetDistance = Mathf.Clamp(this.targetDistance, this.config.GetFloat("DistanceMin"), this.config.GetFloat("DistanceMax"));
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004C64C File Offset: 0x0004A84C
		public override void PostUpdate()
		{
			if (this.disableInput)
			{
				return;
			}
			if (this.InputManager)
			{
				this.UpdateConfig();
				this.UpdateDistance();
				this.UpdateFOV();
				if (this.InputManager.GetInput(InputType.Zoom).Valid)
				{
					this.targetZoom = (float)this.InputManager.GetInput(InputType.Zoom).Value;
				}
				this.UpdateZoom();
				this.UpdateYAngle();
				this.UpdateXAngle(false);
				bool flag = false;
				if (this.InputManager.GetInput(InputType.Rotate).Valid)
				{
					this.RotateCamera((Vector2)this.InputManager.GetInput(InputType.Rotate).Value);
					flag = true;
				}
				if (!flag)
				{
					if (this.config.GetBool("DraggingMove"))
					{
						if (this.InputManager.GetInput(InputType.Pan).Valid)
						{
							this.DragCamera((Vector2)this.InputManager.GetInput(InputType.Pan).Value);
						}
						else
						{
							this.UpdateDragMomentum();
							this.panning = false;
						}
					}
					if (!this.panning)
					{
						if (this.config.GetBool("KeyMove") && this.InputManager.GetInput(InputType.Move).Valid)
						{
							this.MoveCamera((Vector2)this.InputManager.GetInput(InputType.Move).Value);
						}
						if (this.config.GetBool("ScreenBorderMove"))
						{
							this.MoveCameraByScreenBorder(UnityEngine.Input.mousePosition);
						}
					}
				}
				this.UpdateDir();
			}
			this.activateTimeout -= Time.deltaTime;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		private void UpdateCollision()
		{
			if (this.collision)
			{
				float num;
				float num2;
				this.collision.ProcessCollision(this.cameraTarget, this.cameraTarget, this.UnityCamera.transform.forward, this.targetDistance, out num, out num2);
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004C81F File Offset: 0x0004AA1F
		public override void FixedStepUpdate()
		{
			this.UpdateCollision();
		}

		// Token: 0x04000B56 RID: 2902
		private float rotX;

		// Token: 0x04000B57 RID: 2903
		private float rotY;

		// Token: 0x04000B58 RID: 2904
		private float targetZoom;

		// Token: 0x04000B59 RID: 2905
		private Plane groundPlane;

		// Token: 0x04000B5A RID: 2906
		private bool panning;

		// Token: 0x04000B5B RID: 2907
		private Vector3 panMousePosition;

		// Token: 0x04000B5C RID: 2908
		private Vector3 panCameraTarget;

		// Token: 0x04000B5D RID: 2909
		private Vector3 panCameraPos;

		// Token: 0x04000B5E RID: 2910
		private Vector3 newCameraTarget;

		// Token: 0x04000B5F RID: 2911
		private float activateTimeout;

		// Token: 0x04000B60 RID: 2912
		private Vector3 dragVelocity;

		// Token: 0x04000B61 RID: 2913
		private float dragSlowdown;
	}
}
