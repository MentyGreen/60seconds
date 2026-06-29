using System;
using RG_GameCamera.Config;
using RG_GameCamera.Input;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x02000191 RID: 401
	[RequireComponent(typeof(RPGConfig))]
	public class RPGCameraMode : CameraMode
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004AF13 File Offset: 0x00049113
		public override Type Type
		{
			get
			{
				return Type.RPG;
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004AF18 File Offset: 0x00049118
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<RPGConfig>();
			RG_GameCamera.Utils.DebugDraw.Enabled = true;
			this.targetFilter = new PositionFilter(10, 1f);
			this.targetFilter.Reset(this.Target.position);
			this.debugRing = RingPrimitive.Create(3f, 3f, 0.1f, 50, Color.red);
			this.debugRing.GetComponent<MeshRenderer>().castShadows = false;
			RG_GameCamera.Utils.Debug.SetActive(this.debugRing, this.dbgRing);
			this.config.TransitCallback = new Config.OnTransitMode(this.OnTransitMode);
			this.config.TransitionStartCallback = new Config.OnTransitionStart(this.OnTransitStartMode);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004AFEC File Offset: 0x000491EC
		public override void OnActivate()
		{
			base.OnActivate();
			this.config.SetCameraMode("Default");
			this.targetDistance = this.config.GetFloat("Distance");
			this.cameraTarget = this.Target.position;
			this.targetFilter.Reset(this.Target.position);
			this.targetPos = this.Target.position;
			this.UpdateYAngle();
			this.UpdateXAngle(true);
			this.UpdateDir();
			this.activateTimeout = 2f;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004B07C File Offset: 0x0004927C
		private void OnTransitMode(string newMode, float t)
		{
			float @float = this.config.GetFloat("Distance");
			this.targetDistance = Mathf.Lerp(this.transitDistance, @float, t);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004B0AD File Offset: 0x000492AD
		private void OnTransitStartMode(string oldMode, string newMode)
		{
			this.transitDistance = this.targetDistance;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004B0BB File Offset: 0x000492BB
		public override void SetCameraTarget(Transform target)
		{
			base.SetCameraTarget(target);
			if (target)
			{
				this.cameraTarget = target.position;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004B0D8 File Offset: 0x000492D8
		private void RotateCamera(Vector2 mousePosition)
		{
			if (this.config.GetBool("EnableRotation") && mousePosition.sqrMagnitude > Mathf.Epsilon)
			{
				this.rotX += this.config.GetFloat("RotationSpeed") * mousePosition.x * 0.01f;
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004B12F File Offset: 0x0004932F
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this.config.GetFloat("FOV");
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004B14C File Offset: 0x0004934C
		private void UpdateYAngle()
		{
			Math.ToSpherical(this.UnityCamera.transform.forward, out this.rotX, out this.rotY);
			float num = (this.targetDistance - this.config.GetFloat("DistanceMin")) / (this.config.GetFloat("DistanceMax") - this.config.GetFloat("DistanceMin"));
			float num2 = this.config.GetFloat("AngleZoomMin") * (1f - num) + this.config.GetFloat("AngleY") * num;
			this.rotY = Mathf.Lerp(this.rotY, num2 * -1f * 0.017453292f, Time.deltaTime * 50f);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004B20C File Offset: 0x0004940C
		private void UpdateXAngle(bool force)
		{
			if (!this.config.GetBool("EnableRotation") || force || this.activateTimeout > 0f)
			{
				this.rotX = this.config.GetFloat("DefaultAngleX") * -0.017453292f;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004B25C File Offset: 0x0004945C
		private void UpdateDir()
		{
			Vector3 forward;
			Math.ToCartesian(this.rotX, this.rotY, out forward);
			this.UnityCamera.transform.forward = forward;
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.targetDistance;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004B2C3 File Offset: 0x000494C3
		private void UpdateConfig()
		{
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004B2C8 File Offset: 0x000494C8
		private void UpdateCollision()
		{
			if (this.collision)
			{
				float num;
				float num2;
				this.collision.ProcessCollision(this.cameraTarget, this.cameraTarget, this.UnityCamera.transform.forward, this.targetDistance, out num, out num2);
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004B314 File Offset: 0x00049514
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

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004B398 File Offset: 0x00049598
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

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004B510 File Offset: 0x00049710
		private Vector3 GetOffsetPos()
		{
			Vector3 vector = this.config.GetVector3("TargetOffset");
			Vector3 a = Math.VectorXZ(this.UnityCamera.transform.forward);
			Vector3 a2 = Math.VectorXZ(this.UnityCamera.transform.right);
			Vector3 up = Vector3.up;
			return a * vector.z + a2 * vector.x + up * vector.y;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004B58C File Offset: 0x0004978C
		private void UpdateDistance()
		{
			this.cameraTarget = this.targetPos + this.GetOffsetPos();
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004B5A8 File Offset: 0x000497A8
		public override void PostUpdate()
		{
			if (this.disableInput)
			{
				return;
			}
			if (this.InputManager)
			{
				this.UpdateConfig();
				this.UpdateFOV();
				if (this.InputManager.GetInput(InputType.Zoom).Valid)
				{
					this.targetZoom = (float)this.InputManager.GetInput(InputType.Zoom).Value;
				}
				this.UpdateZoom();
				this.UpdateYAngle();
				this.UpdateXAngle(false);
				if (this.InputManager.GetInput(InputType.Rotate).Valid)
				{
					this.RotateCamera((Vector2)this.InputManager.GetInput(InputType.Rotate).Value);
				}
				this.UpdateDistance();
				this.UpdateDir();
			}
			this.activateTimeout -= Time.deltaTime;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004B66C File Offset: 0x0004986C
		public override void FixedStepUpdate()
		{
			this.targetFilter.AddSample(this.Target.position);
			Vector2 vector = this.config.GetVector2("DeadZone");
			if (vector.sqrMagnitude > Mathf.Epsilon)
			{
				RingPrimitive.Generate(this.debugRing, vector.x, vector.y, 0.1f, 50);
				this.debugRing.transform.position = this.targetPos + Vector3.up * 2f;
				this.debugRing.transform.forward = Math.VectorXZ(this.UnityCamera.transform.forward);
				RG_GameCamera.Utils.Debug.SetActive(this.debugRing, this.dbgRing);
				Vector3 vector2 = this.targetFilter.GetValue() - this.targetPos;
				float magnitude = vector2.magnitude;
				vector2 /= magnitude;
				if (magnitude > vector.x || magnitude > vector.y)
				{
					Vector3 vector3 = this.UnityCamera.transform.InverseTransformDirection(vector2);
					float f = Mathf.Atan2(vector3.x, vector3.z);
					Vector3 vector4 = new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
					Vector3 vector5 = new Vector3(vector4.x * vector.x, 0f, vector4.z * vector.y);
					float magnitude2 = vector5.magnitude;
					if (magnitude > magnitude2)
					{
						Vector3 target = this.targetPos + vector2 * (magnitude - magnitude2);
						this.targetPos = Vector3.SmoothDamp(this.targetPos, target, ref this.springVelocity, this.config.GetFloat("Spring"));
					}
				}
			}
			else
			{
				this.targetPos = Vector3.SmoothDamp(this.targetPos, this.targetFilter.GetValue(), ref this.springVelocity, this.config.GetFloat("Spring"));
				this.targetPos.y = this.targetFilter.GetValue().y;
			}
			this.UpdateCollision();
			base.UpdateTargetDummy();
		}

		// Token: 0x04000B4C RID: 2892
		public bool dbgRing;

		// Token: 0x04000B4D RID: 2893
		private float rotX;

		// Token: 0x04000B4E RID: 2894
		private float rotY;

		// Token: 0x04000B4F RID: 2895
		private float targetZoom;

		// Token: 0x04000B50 RID: 2896
		private Vector3 targetPos;

		// Token: 0x04000B51 RID: 2897
		private PositionFilter targetFilter;

		// Token: 0x04000B52 RID: 2898
		private Vector3 springVelocity;

		// Token: 0x04000B53 RID: 2899
		private GameObject debugRing;

		// Token: 0x04000B54 RID: 2900
		private float transitDistance;

		// Token: 0x04000B55 RID: 2901
		private float activateTimeout;
	}
}
