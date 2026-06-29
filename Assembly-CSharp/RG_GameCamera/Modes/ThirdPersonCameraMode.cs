using System;
using RG_GameCamera.Config;
using RG_GameCamera.Input;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x02000193 RID: 403
	[RequireComponent(typeof(ThirdPersonConfig))]
	public class ThirdPersonCameraMode : CameraMode
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004C82F File Offset: 0x0004AA2F
		public override Type Type
		{
			get
			{
				return Type.ThirdPerson;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004C834 File Offset: 0x0004AA34
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<ThirdPersonConfig>();
			this.lastTargetPos = this.Target.position;
			this.targetVelocity = 0f;
			this.debugRing = RingPrimitive.Create(3f, 3f, 0.1f, 50, Color.red);
			this.debugRing.GetComponent<MeshRenderer>().castShadows = false;
			RG_GameCamera.Utils.Debug.SetActive(this.debugRing, this.dbgRing);
			this.targetFilter = new PositionFilter(10, 1f);
			this.targetFilter.Reset(this.Target.position);
			this._fov = Object.FindObjectOfType<ResolutionHandler>().SelectedAspectRatio.CamFov;
			RG_GameCamera.Utils.DebugDraw.Enabled = true;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004C910 File Offset: 0x0004AB10
		public override void OnActivate()
		{
			base.OnActivate();
			this.config.SetCameraMode("Default");
			this.targetFilter.Reset(this.Target.position);
			this.lastTargetPos = this.Target.position;
			this.targetVelocity = 0f;
			this.activateTimeout = 1f;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004C974 File Offset: 0x0004AB74
		private void RotateCamera(Vector2 mousePosition)
		{
			this.rotationInput = (mousePosition.sqrMagnitude > Mathf.Epsilon);
			if (this.rotationInput)
			{
				this.rotationInputTimeout = 0f;
			}
			else
			{
				this.rotationInputTimeout += Time.deltaTime;
			}
			bool @bool = this.config.GetBool("FreeRotate");
			this.rotY += this.config.GetFloat("RotationSpeedY") * mousePosition.y * (@bool ? 0.01f : 1f);
			this.rotX += this.config.GetFloat("RotationSpeedX") * mousePosition.x * (@bool ? 0.01f : 1f);
			float num = -this.rotY * 57.29578f;
			float @float = this.config.GetFloat("RotationYMax");
			float float2 = this.config.GetFloat("RotationYMin");
			if (num > @float)
			{
				this.rotY = -@float * 0.017453292f;
			}
			if (num < float2)
			{
				this.rotY = -float2 * 0.017453292f;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004CA88 File Offset: 0x0004AC88
		private void UpdateFollow()
		{
			Vector3 vector = this.targetPos - this.lastTargetPos;
			vector.y = 0f;
			float num = Mathf.Clamp(vector.magnitude, 0f, 5f);
			if (Time.deltaTime > Mathf.Epsilon)
			{
				this.targetVelocity = num / Time.deltaTime;
			}
			else
			{
				this.targetVelocity = 0f;
			}
			if (this.InputManager.GetInput(InputType.Move).Valid)
			{
				Vector2 vector2 = (Vector2)this.InputManager.GetInput(InputType.Move).Value;
				vector2.Normalize();
				float @float = this.config.GetFloat("FollowCoef");
				float num2 = Mathf.Sin(Mathf.Atan2(vector2.x, vector2.y));
				float num3 = Mathf.Clamp01(this.rotationInputTimeout);
				float num4 = num2 * Time.deltaTime * @float * this.targetVelocity * 0.2f * num3;
				this.rotX += num4;
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004CB80 File Offset: 0x0004AD80
		private void UpdateDistance()
		{
			Vector3 a = this.targetPos + this.GetOffsetPos();
			this.cameraTarget = Vector3.Lerp(a, base.GetTargetHeadPos(), 1f - this.currCollisionTargetDist);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004CBBD File Offset: 0x0004ADBD
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this._fov;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004CBD0 File Offset: 0x0004ADD0
		private void UpdateDir()
		{
			this.activateTimeout -= Time.deltaTime;
			if (this.activateTimeout > 0f)
			{
				float @float = this.config.GetFloat("DefaultYRotation");
				this.rotY = -@float * 0.017453292f;
				this.rotX = Mathf.Atan2(this.Target.forward.x, this.Target.forward.z);
			}
			Vector3 forward;
			Math.ToCartesian(this.rotX, this.rotY, out forward);
			this.UnityCamera.transform.forward = forward;
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.targetDistance;
			this.lastTargetPos = this.targetPos;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004CCAC File Offset: 0x0004AEAC
		private Vector3 GetOffsetPos()
		{
			Vector3 vector = Vector3.zero;
			if (this.config.IsVector3("TargetOffset"))
			{
				vector = this.config.GetVector3("TargetOffset");
			}
			Vector3 a = Math.VectorXZ(this.UnityCamera.transform.forward);
			Vector3 a2 = Math.VectorXZ(this.UnityCamera.transform.right);
			Vector3 up = Vector3.up;
			return a * vector.z + a2 * vector.x + up * vector.y;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004CD40 File Offset: 0x0004AF40
		private void UpdateYRotation()
		{
			if (!this.rotationInput && this.targetVelocity > 0.1f)
			{
				float num = -this.rotY * 57.29578f;
				float @float = this.config.GetFloat("DefaultYRotation");
				float num2 = Mathf.Clamp01(this.rotationInputTimeout);
				float t = Mathf.Clamp01(this.targetVelocity * this.config.GetFloat("AutoYRotation") * Time.deltaTime) * num2;
				num = Mathf.Lerp(num, @float, t);
				this.rotY = -num * 0.017453292f;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004CDCC File Offset: 0x0004AFCC
		public override void PostUpdate()
		{
			if (this.disableInput)
			{
				return;
			}
			if (this.InputManager)
			{
				this.UpdateFOV();
				if (this.InputManager.GetInput(InputType.Rotate).Valid)
				{
					this.RotateCamera((Vector2)this.InputManager.GetInput(InputType.Rotate).Value);
				}
				this.UpdateFollow();
				this.UpdateDistance();
				this.UpdateYRotation();
				this.UpdateDir();
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004CE3C File Offset: 0x0004B03C
		private void UpdateCollision()
		{
			Vector3 cameraTarget = this.targetPos + this.GetOffsetPos();
			float @float = this.config.GetFloat("Distance");
			this.collision.ProcessCollision(cameraTarget, base.GetTargetHeadPos(), this.UnityCamera.transform.forward, @float, out this.collisionTargetDist, out this.collisionDistance);
			float num = this.collisionDistance / @float;
			if (this.collisionTargetDist > num)
			{
				this.collisionTargetDist = num;
			}
			this.targetDistance = Interpolation.Lerp(this.targetDistance, this.collisionDistance, (this.targetDistance > this.collisionDistance) ? this.collision.GetClipSpeed() : this.collision.GetReturnSpeed());
			this.currCollisionTargetDist = Mathf.SmoothDamp(this.currCollisionTargetDist, this.collisionTargetDist, ref this.collisionTargetVelocity, (this.currCollisionTargetDist > this.collisionTargetDist) ? this.collision.GetTargetClipSpeed() : this.collision.GetReturnTargetSpeed());
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004CF34 File Offset: 0x0004B134
		public override void GameUpdate()
		{
			base.GameUpdate();
			float @float = this.config.GetFloat("Spring");
			Vector2 vector = this.config.GetVector2("DeadZone");
			if (@float <= 0f && vector.sqrMagnitude <= Mathf.Epsilon)
			{
				this.targetPos = this.targetFilter.GetValue();
			}
			base.UpdateTargetDummy();
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004CF94 File Offset: 0x0004B194
		public override void FixedStepUpdate()
		{
			this.targetFilter.AddSample(this.Target.position);
			this.UpdateCollision();
			Vector2 vector = this.config.GetVector2("DeadZone");
			if (vector.sqrMagnitude > Mathf.Epsilon)
			{
				RingPrimitive.Generate(this.debugRing, vector.x, vector.y, 0.1f, 50);
				this.debugRing.transform.position = this.targetPos + Vector3.up * 2f;
				Vector3 forward = Math.VectorXZ(this.UnityCamera.transform.forward);
				if (forward.sqrMagnitude < Mathf.Epsilon)
				{
					forward = Vector3.forward;
				}
				this.debugRing.transform.forward = forward;
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
						return;
					}
				}
			}
			else
			{
				this.targetPos = Vector3.SmoothDamp(this.targetPos, this.targetFilter.GetValue(), ref this.springVelocity, this.config.GetFloat("Spring"));
			}
		}

		// Token: 0x04000B62 RID: 2914
		public bool dbgRing;

		// Token: 0x04000B63 RID: 2915
		private bool rotationInput;

		// Token: 0x04000B64 RID: 2916
		private float rotationInputTimeout;

		// Token: 0x04000B65 RID: 2917
		private float rotX;

		// Token: 0x04000B66 RID: 2918
		private float rotY;

		// Token: 0x04000B67 RID: 2919
		private float targetVelocity;

		// Token: 0x04000B68 RID: 2920
		private float collisionDistance;

		// Token: 0x04000B69 RID: 2921
		private float collisionZoomVelocity;

		// Token: 0x04000B6A RID: 2922
		private float currCollisionTargetDist;

		// Token: 0x04000B6B RID: 2923
		private float collisionTargetDist;

		// Token: 0x04000B6C RID: 2924
		private float collisionTargetVelocity;

		// Token: 0x04000B6D RID: 2925
		private Vector3 targetPos;

		// Token: 0x04000B6E RID: 2926
		private Vector3 lastTargetPos;

		// Token: 0x04000B6F RID: 2927
		private Vector3 springVelocity;

		// Token: 0x04000B70 RID: 2928
		private GameObject debugRing;

		// Token: 0x04000B71 RID: 2929
		private float activateTimeout;

		// Token: 0x04000B72 RID: 2930
		private float _fov = 60f;

		// Token: 0x04000B73 RID: 2931
		private PositionFilter targetFilter;

		// Token: 0x04000B74 RID: 2932
		private float _freeRotateTimeout = 0.75f;

		// Token: 0x04000B75 RID: 2933
		private float _freeRotateTimer;

		// Token: 0x04000B76 RID: 2934
		private bool _doFreeRotateTimeout = true;

		// Token: 0x04000B77 RID: 2935
		private float _angleJumpLimit = 5f;
	}
}
