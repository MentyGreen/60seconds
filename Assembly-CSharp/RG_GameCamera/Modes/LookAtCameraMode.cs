using System;
using RG_GameCamera.Config;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x0200018E RID: 398
	[RequireComponent(typeof(LookAtConfig))]
	public class LookAtCameraMode : CameraMode
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004A1ED File Offset: 0x000483ED
		public override Type Type
		{
			get
			{
				return Type.LookAt;
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004A1F0 File Offset: 0x000483F0
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<LookAtConfig>();
			this.targetTimeout = -1f;
			this.targetTimeoutMax = 1f;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004A230 File Offset: 0x00048430
		public override void OnActivate()
		{
			this.ApplyCurrentCamera();
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004A238 File Offset: 0x00048438
		public void RegisterFinishCallback(LookAtCameraMode.OnLookAtFinished callback)
		{
			this.finishedCallback = (LookAtCameraMode.OnLookAtFinished)Delegate.Combine(this.finishedCallback, callback);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004A251 File Offset: 0x00048451
		public void UnregisterFinishCallback(LookAtCameraMode.OnLookAtFinished callback)
		{
			this.finishedCallback = (LookAtCameraMode.OnLookAtFinished)Delegate.Remove(this.finishedCallback, callback);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0004A26C File Offset: 0x0004846C
		public void ApplyCurrentCamera()
		{
			Ray ray = new Ray(this.UnityCamera.transform.position, this.UnityCamera.transform.forward);
			Vector3 cameraTarget = ray.origin + ray.direction * 100f;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 3.4028235E+38f))
			{
				cameraTarget = raycastHit.point;
			}
			this.cameraTarget = cameraTarget;
			this.targetDistance = (this.UnityCamera.transform.position - this.cameraTarget).magnitude;
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.targetDistance;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004A33B File Offset: 0x0004853B
		public void LookAt(Vector3 point, float timeout)
		{
			this.LookAt(this.UnityCamera.transform.position, point, timeout);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004A358 File Offset: 0x00048558
		public void LookAt(Vector3 from, Vector3 point, float timeout)
		{
			this.oldPos = this.UnityCamera.transform.position;
			this.oldTarget = this.cameraTarget;
			this.oldRot = this.UnityCamera.transform.rotation;
			this.newPos = from;
			this.newTarget = point;
			if (timeout < 0f)
			{
				timeout = 0f;
			}
			this.newRot = Quaternion.LookRotation(point - from);
			this.targetTimeoutMax = timeout;
			this.targetTimeout = timeout;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004A3DA File Offset: 0x000485DA
		public void LookFrom(Vector3 from, float timeout)
		{
			this.LookAt(from, this.cameraTarget, timeout);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004A3EC File Offset: 0x000485EC
		private void UpdateLookAt()
		{
			if (this.targetTimeout >= 0f)
			{
				this.targetTimeout -= Time.deltaTime;
				float t;
				if (this.targetTimeoutMax < Mathf.Epsilon)
				{
					t = 1f;
				}
				else
				{
					t = 1f - this.targetTimeout / this.targetTimeoutMax;
				}
				Vector3 position = Interpolation.LerpS(this.oldPos, this.newPos, t);
				this.UnityCamera.transform.position = position;
				if (this.config.GetBool("InterpolateTarget"))
				{
					this.cameraTarget = Interpolation.LerpS(this.oldTarget, this.newTarget, t);
					this.UnityCamera.transform.LookAt(this.cameraTarget);
				}
				else
				{
					Quaternion rotation = Quaternion.Slerp(this.oldRot, this.newRot, Interpolation.LerpS(0f, 1f, t));
					this.UnityCamera.transform.rotation = rotation;
				}
				if (this.targetTimeout < 0f && this.finishedCallback != null)
				{
					this.finishedCallback();
				}
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004A4FD File Offset: 0x000486FD
		private void UpdateFOV()
		{
			this.UnityCamera.fieldOfView = this.config.GetFloat("FOV");
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004A51A File Offset: 0x0004871A
		public override void PostUpdate()
		{
			this.UpdateFOV();
			this.UpdateLookAt();
		}

		// Token: 0x04000B3F RID: 2879
		private Vector3 newTarget;

		// Token: 0x04000B40 RID: 2880
		private Vector3 newPos;

		// Token: 0x04000B41 RID: 2881
		private Vector3 oldPos;

		// Token: 0x04000B42 RID: 2882
		private Vector3 oldTarget;

		// Token: 0x04000B43 RID: 2883
		private Quaternion oldRot;

		// Token: 0x04000B44 RID: 2884
		private Quaternion newRot;

		// Token: 0x04000B45 RID: 2885
		private float targetTimeoutMax;

		// Token: 0x04000B46 RID: 2886
		private float targetTimeout;

		// Token: 0x04000B47 RID: 2887
		private LookAtCameraMode.OnLookAtFinished finishedCallback;

		// Token: 0x020003DE RID: 990
		// (Invoke) Token: 0x06001E76 RID: 7798
		public delegate void OnLookAtFinished();
	}
}
