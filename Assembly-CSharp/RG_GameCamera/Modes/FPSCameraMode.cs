using System;
using RG_GameCamera.Config;
using RG_GameCamera.Input;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x0200018D RID: 397
	[RequireComponent(typeof(FPSConfig))]
	public class FPSCameraMode : CameraMode
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00049DED File Offset: 0x00047FED
		public override Type Type
		{
			get
			{
				return Type.FPS;
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00049DF0 File Offset: 0x00047FF0
		public override void OnActivate()
		{
			base.OnActivate();
			if (this.Target)
			{
				this.cameraTarget = this.Target.position;
				this.UnityCamera.transform.position = this.GetEyePos();
				this.UnityCamera.transform.LookAt(this.GetEyePos() + this.Target.forward);
				this.RotateCamera(Vector2.zero);
				this.targetHide = false;
				this.activateTimeout = 1f;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00049E7A File Offset: 0x0004807A
		public override void OnDeactivate()
		{
			this.ShowTarget(true);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00049E84 File Offset: 0x00048084
		private Vector3 GetEyePos()
		{
			if (this.config.IsVector3("TargetOffset"))
			{
				return this.Target.transform.position + this.config.GetVector3("TargetOffset");
			}
			return this.Target.transform.position;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00049ED9 File Offset: 0x000480D9
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this.config.GetFloat("FOV");
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00049EF8 File Offset: 0x000480F8
		public override void SetCameraTarget(Transform target)
		{
			base.SetCameraTarget(target);
			if (target)
			{
				this.cameraTarget = this.Target.position;
				this.UnityCamera.transform.position = this.GetEyePos();
				this.UnityCamera.transform.LookAt(this.GetEyePos() + this.Target.forward);
				this.RotateCamera(Vector2.zero);
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00049F6C File Offset: 0x0004816C
		public override void Init()
		{
			base.Init();
			this.config = base.GetComponent<FPSConfig>();
			this.cameraTarget = this.Target.position;
			this.UnityCamera.transform.position = this.GetEyePos();
			this.RotateCamera(Vector2.zero);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00049FC0 File Offset: 0x000481C0
		public void RotateCamera(Vector2 mousePosition)
		{
			Math.ToSpherical(this.UnityCamera.transform.forward, out this.rotX, out this.rotY);
			this.rotY += this.config.GetFloat("RotationSpeedY") * mousePosition.y * 0.01f;
			this.rotX += this.config.GetFloat("RotationSpeedX") * mousePosition.x * 0.01f;
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

		// Token: 0x06001192 RID: 4498 RVA: 0x0004A094 File Offset: 0x00048294
		private void UpdateDir()
		{
			Vector3 forward;
			Math.ToCartesian(this.rotX, this.rotY, out forward);
			this.UnityCamera.transform.forward = forward;
			this.UnityCamera.transform.position = this.GetEyePos();
			this.activateTimeout -= Time.deltaTime;
			if (this.activateTimeout > 0f)
			{
				this.UnityCamera.transform.LookAt(this.GetEyePos() + this.Target.forward);
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004A120 File Offset: 0x00048320
		private void UpdateTargetVisibility()
		{
			bool @bool = this.config.GetBool("HideTarget");
			if (@bool != this.targetHide)
			{
				this.targetHide = @bool;
				this.ShowTarget(!this.targetHide);
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004A15D File Offset: 0x0004835D
		private void ShowTarget(bool show)
		{
			RG_GameCamera.Utils.Debug.SetVisible(this.Target.gameObject, show, true);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004A174 File Offset: 0x00048374
		public override void PostUpdate()
		{
			if (this.disableInput)
			{
				return;
			}
			if (this.InputManager)
			{
				if (this.activateTimeout < 0f)
				{
					this.UpdateTargetVisibility();
				}
				this.UpdateFOV();
				if (this.InputManager.GetInput(InputType.Rotate).Valid)
				{
					this.RotateCamera((Vector2)this.InputManager.GetInput(InputType.Rotate).Value);
				}
				this.UpdateDir();
			}
		}

		// Token: 0x04000B3B RID: 2875
		private float rotX;

		// Token: 0x04000B3C RID: 2876
		private float rotY;

		// Token: 0x04000B3D RID: 2877
		private bool targetHide;

		// Token: 0x04000B3E RID: 2878
		private float activateTimeout;
	}
}
