using System;
using System.Collections.Generic;
using RG_GameCamera.Effects;
using RG_GameCamera.Input;
using RG_GameCamera.Modes;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera
{
	// Token: 0x0200017E RID: 382
	public class CameraManager : MonoBehaviour
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00047B11 File Offset: 0x00045D11
		public static CameraManager Instance
		{
			get
			{
				if (!CameraManager.instance)
				{
					CameraManager.instance = CameraInstance.CreateInstance<CameraManager>("CameraManager");
				}
				return CameraManager.instance;
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00047B34 File Offset: 0x00045D34
		public void RegisterMode(CameraMode cameraModeMode)
		{
			if (this.cameraModes == null)
			{
				if (CameraManager.preRegistered == null)
				{
					CameraManager.preRegistered = new Queue<CameraMode>();
				}
				CameraManager.preRegistered.Enqueue(cameraModeMode);
				return;
			}
			this.cameraModes.Add(cameraModeMode.Type, cameraModeMode);
			cameraModeMode.gameObject.SetActive(false);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00047B84 File Offset: 0x00045D84
		public CameraMode SetMode(RG_GameCamera.Modes.Type cameraMode)
		{
			this.Initialize();
			if (this.currModeType != cameraMode)
			{
				this.cameraModes[this.currModeType].OnDeactivate();
				RG_GameCamera.Utils.Debug.SetActive(this.cameraModes[this.currModeType].gameObject, false);
				this.oldModeTransform = new CameraManager.CameraTransform(this.UnityCamera);
				this.transition = true;
				this.currModeType = cameraMode;
				RG_GameCamera.Utils.Debug.SetActive(this.cameraModes[this.currModeType].gameObject, true);
				this.cameraModes[this.currModeType].SetCameraTarget(this.CameraTarget);
				this.cameraModes[this.currModeType].OnActivate();
			}
			return this.cameraModes[this.currModeType];
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00047C53 File Offset: 0x00045E53
		public void SetCameraTarget(Transform target)
		{
			this.CameraTarget = target;
			this.cameraModes[this.currModeType].SetCameraTarget(target);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00047C73 File Offset: 0x00045E73
		public CameraMode GetCameraMode()
		{
			if (this.cameraModes != null && this.cameraModes.ContainsKey(this.currModeType))
			{
				return this.cameraModes[this.currModeType];
			}
			return null;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00047CA3 File Offset: 0x00045EA3
		public void RegisterTransitionCallback(CameraManager.OnTransitionFinished callback)
		{
			this.finishedCallbak = (CameraManager.OnTransitionFinished)Delegate.Combine(this.finishedCallbak, callback);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00047CBC File Offset: 0x00045EBC
		public void UnregisterTransitionCallback(CameraManager.OnTransitionFinished callback)
		{
			this.finishedCallbak = (CameraManager.OnTransitionFinished)Delegate.Remove(this.finishedCallbak, callback);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00047CD8 File Offset: 0x00045ED8
		private void Awake()
		{
			CameraManager.instance = this;
			this.cameraModes = new Dictionary<RG_GameCamera.Modes.Type, CameraMode>();
			this.currModeType = RG_GameCamera.Modes.Type.None;
			if (!this.UnityCamera)
			{
				this.UnityCamera = base.GetComponent<Camera>();
				if (!this.UnityCamera)
				{
					this.UnityCamera = Camera.main;
				}
			}
			if (CameraManager.preRegistered != null)
			{
				foreach (CameraMode cameraModeMode in CameraManager.preRegistered)
				{
					this.RegisterMode(cameraModeMode);
				}
				CameraManager.preRegistered.Clear();
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00047D84 File Offset: 0x00045F84
		private void Initialize()
		{
			if (!this.initialized)
			{
				foreach (KeyValuePair<RG_GameCamera.Modes.Type, CameraMode> keyValuePair in this.cameraModes)
				{
					keyValuePair.Value.Init();
				}
				this.initialized = true;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00047DEC File Offset: 0x00045FEC
		private void Start()
		{
			this.Initialize();
			this.SetMode(this.ActivateModeOnStart);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00047E01 File Offset: 0x00046001
		private void Update()
		{
			InputManager.Instance.GameUpdate();
			this.cameraModes[this.currModeType].GameUpdate();
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00047E24 File Offset: 0x00046024
		private void LateUpdate()
		{
			this.cameraModes[this.currModeType].PostUpdate();
			if (this.transition)
			{
				this.transition = this.oldModeTransform.Interpolate(this.UnityCamera, this.TransitionSpeed, this.TransitionTimeMax);
				if (!this.transition && this.finishedCallbak != null)
				{
					this.finishedCallbak();
				}
			}
			EffectManager.Instance.PostUpdate();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00047E97 File Offset: 0x00046097
		private void FixedUpdate()
		{
			this.cameraModes[this.currModeType].FixedStepUpdate();
		}

		// Token: 0x04000B08 RID: 2824
		public Camera UnityCamera;

		// Token: 0x04000B09 RID: 2825
		public float TransitionSpeed = 0.5f;

		// Token: 0x04000B0A RID: 2826
		public float TransitionTimeMax = 1f;

		// Token: 0x04000B0B RID: 2827
		public GUISkin GuiSkin;

		// Token: 0x04000B0C RID: 2828
		public RG_GameCamera.Modes.Type ActivateModeOnStart;

		// Token: 0x04000B0D RID: 2829
		public Transform CameraTarget;

		// Token: 0x04000B0E RID: 2830
		private static CameraManager instance;

		// Token: 0x04000B0F RID: 2831
		private static Queue<CameraMode> preRegistered;

		// Token: 0x04000B10 RID: 2832
		private Dictionary<RG_GameCamera.Modes.Type, CameraMode> cameraModes;

		// Token: 0x04000B11 RID: 2833
		private bool transition;

		// Token: 0x04000B12 RID: 2834
		private RG_GameCamera.Modes.Type currModeType;

		// Token: 0x04000B13 RID: 2835
		private bool initialized;

		// Token: 0x04000B14 RID: 2836
		private CameraManager.CameraTransform oldModeTransform;

		// Token: 0x04000B15 RID: 2837
		private CameraManager.OnTransitionFinished finishedCallbak;

		// Token: 0x020003D9 RID: 985
		// (Invoke) Token: 0x06001E68 RID: 7784
		public delegate void OnTransitionFinished();

		// Token: 0x020003DA RID: 986
		private struct CameraTransform
		{
			// Token: 0x06001E6B RID: 7787 RVA: 0x000806E8 File Offset: 0x0007E8E8
			public CameraTransform(Camera cam)
			{
				this.pos = cam.transform.position;
				this.rot = cam.transform.rotation;
				this.fov = cam.fieldOfView;
				this.posVel = Vector3.zero;
				this.rotVel = Vector3.zero;
				this.fovVel = 0f;
				this.timeout = 0f;
				this.speedRatio = 1f;
			}

			// Token: 0x06001E6C RID: 7788 RVA: 0x0008075C File Offset: 0x0007E95C
			public bool Interpolate(Camera cam, float speed, float timeMax)
			{
				float smoothTime = speed * this.speedRatio;
				this.pos = Vector3.SmoothDamp(this.pos, cam.transform.position, ref this.posVel, smoothTime);
				this.rot = Quaternion.Euler(Vector3.SmoothDamp(this.rot.eulerAngles, cam.transform.eulerAngles, ref this.rotVel, smoothTime));
				Math.CorrectRotationUp(ref this.rot);
				this.fov = Mathf.SmoothDamp(this.fov, cam.fieldOfView, ref this.fovVel, 0.05f);
				int num = ((cam.transform.position - this.pos).sqrMagnitude < 0.001f && Quaternion.Angle(cam.transform.rotation, this.rot) < 0.001f && Mathf.Abs(this.fov - cam.fieldOfView) < 0.001f) ? 1 : 0;
				this.timeout += Time.deltaTime;
				this.speedRatio = 1f - Mathf.Clamp01(this.timeout / timeMax);
				cam.transform.position = this.pos;
				cam.transform.rotation = this.rot;
				cam.fieldOfView = this.fov;
				return num == 0;
			}

			// Token: 0x040017F8 RID: 6136
			private Vector3 pos;

			// Token: 0x040017F9 RID: 6137
			private Quaternion rot;

			// Token: 0x040017FA RID: 6138
			private float fov;

			// Token: 0x040017FB RID: 6139
			private Vector3 posVel;

			// Token: 0x040017FC RID: 6140
			private Vector3 rotVel;

			// Token: 0x040017FD RID: 6141
			private float fovVel;

			// Token: 0x040017FE RID: 6142
			private float timeout;

			// Token: 0x040017FF RID: 6143
			private float speedRatio;
		}
	}
}
