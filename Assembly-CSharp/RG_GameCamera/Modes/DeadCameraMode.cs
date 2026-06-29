using System;
using RG_GameCamera.Config;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x0200018C RID: 396
	[RequireComponent(typeof(DeadConfig))]
	public class DeadCameraMode : CameraMode
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00049B38 File Offset: 0x00047D38
		public override Type Type
		{
			get
			{
				return Type.Dead;
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00049B3B File Offset: 0x00047D3B
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<DeadConfig>();
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00049B68 File Offset: 0x00047D68
		public override void OnActivate()
		{
			base.OnActivate();
			this.targetDistance = (this.cameraTarget - this.UnityCamera.transform.position).magnitude;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00049BA4 File Offset: 0x00047DA4
		private void RotateCamera()
		{
			Math.ToSpherical(this.UnityCamera.transform.forward, out this.rotX, out this.rotY);
			this.angle = this.config.GetFloat("RotationSpeed") * Time.deltaTime;
			this.rotY = -this.config.GetFloat("Angle") * 0.017453292f;
			this.rotX += this.angle;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00049C1E File Offset: 0x00047E1E
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this.config.GetFloat("FOV");
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00049C3C File Offset: 0x00047E3C
		private Vector3 GetOffsetPos()
		{
			Vector3 vector = this.config.GetVector3("TargetOffset");
			Vector3 a = Math.VectorXZ(this.UnityCamera.transform.forward);
			Vector3 a2 = Math.VectorXZ(this.UnityCamera.transform.right);
			Vector3 up = Vector3.up;
			return a * vector.z + a2 * vector.x + up * vector.y;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00049CB8 File Offset: 0x00047EB8
		public override void PostUpdate()
		{
			this.UpdateFOV();
			this.RotateCamera();
			if (this.config.GetBool("Collision"))
			{
				this.UpdateCollision();
			}
			this.UpdateDir();
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00049CE4 File Offset: 0x00047EE4
		private void UpdateDir()
		{
			Vector3 forward;
			Math.ToCartesian(this.rotX, this.rotY, out forward);
			this.UnityCamera.transform.forward = forward;
			this.cameraTarget = this.Target.position + this.GetOffsetPos();
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.targetDistance;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00049D68 File Offset: 0x00047F68
		private void UpdateCollision()
		{
			float @float = this.config.GetFloat("Distance");
			float num;
			float num2;
			this.collision.ProcessCollision(this.cameraTarget, base.GetTargetHeadPos(), this.UnityCamera.transform.forward, @float, out num, out num2);
			this.targetDistance = Interpolation.Lerp(this.targetDistance, num2, (this.targetDistance > num2) ? this.collision.GetClipSpeed() : this.collision.GetReturnSpeed());
		}

		// Token: 0x04000B38 RID: 2872
		private float rotX;

		// Token: 0x04000B39 RID: 2873
		private float rotY;

		// Token: 0x04000B3A RID: 2874
		private float angle;
	}
}
