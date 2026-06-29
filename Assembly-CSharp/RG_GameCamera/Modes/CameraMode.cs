using System;
using RG_GameCamera.CollisionSystem;
using RG_GameCamera.Config;
using RG_GameCamera.Input;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x0200018B RID: 395
	public abstract class CameraMode : MonoBehaviour
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600116C RID: 4460
		public abstract Type Type { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004961D File Offset: 0x0004781D
		public Config Configuration
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00049625 File Offset: 0x00047825
		protected virtual void Awake()
		{
			CameraManager.Instance.RegisterMode(this);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00049634 File Offset: 0x00047834
		public virtual void Init()
		{
			CameraManager instance = CameraManager.Instance;
			this.UnityCamera = instance.UnityCamera;
			this.InputManager = InputManager.Instance;
			if (!this.Target)
			{
				this.Target = instance.CameraTarget;
			}
			if (this.Target)
			{
				this.cameraTarget = this.Target.position;
				this.targetDistance = (this.UnityCamera.transform.position - this.Target.position).magnitude;
			}
			this.CreateTargetDummy();
			this.collision = CameraCollision.Instance;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000496D4 File Offset: 0x000478D4
		public virtual void OnActivate()
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000496D6 File Offset: 0x000478D6
		public virtual void OnDeactivate()
		{
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000496D8 File Offset: 0x000478D8
		public virtual void SetCameraTarget(Transform target)
		{
			this.Target = target;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000496E1 File Offset: 0x000478E1
		public virtual void SetCameraConfigMode(string modeName)
		{
			this.config.SetCameraMode(modeName);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000496F0 File Offset: 0x000478F0
		public void EnableOrthoCamera(bool status)
		{
			if (status == this.UnityCamera.orthographic)
			{
				return;
			}
			if (status)
			{
				this.UnityCamera.orthographic = true;
				this.UnityCamera.orthographicSize = (this.UnityCamera.transform.position - this.cameraTarget).magnitude / 2f;
				return;
			}
			this.UnityCamera.orthographic = false;
			this.UnityCamera.transform.position = this.cameraTarget - this.UnityCamera.transform.forward * this.UnityCamera.orthographicSize * 2f;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000497A1 File Offset: 0x000479A1
		public bool IsOrthoCamera()
		{
			return this.UnityCamera.orthographic;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000497B0 File Offset: 0x000479B0
		public void CreateTargetDummy()
		{
			this.targetDummy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.targetDummy.name = "TargetDummy";
			this.targetDummy.transform.parent = base.gameObject.transform;
			SphereCollider component = this.targetDummy.GetComponent<SphereCollider>();
			if (component)
			{
				Object.Destroy(component);
			}
			Material material = new Material(Shader.Find("Diffuse"));
			material.color = Color.magenta;
			this.targetDummy.GetComponent<MeshRenderer>().sharedMaterial = material;
			this.targetDummy.transform.position = this.cameraTarget;
			this.targetDummy.SetActive(this.ShowTargetDummy);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00049864 File Offset: 0x00047A64
		protected Vector3 GetTargetHeadPos()
		{
			float d = this.collision.GetHeadOffset();
			RG_GameCamera.Input.Input input = this.InputManager.GetInput(InputType.Crouch);
			if (input.Valid && (bool)input.Value)
			{
				d = 1.2f;
			}
			if (this.Target)
			{
				return this.Target.position + Vector3.up * d;
			}
			return this.cameraTarget + Vector3.up * d;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000498E4 File Offset: 0x00047AE4
		protected void UpdateTargetDummy()
		{
			RG_GameCamera.Utils.Debug.SetActive(this.targetDummy, this.ShowTargetDummy);
			if (this.targetDummy)
			{
				float num = (this.UnityCamera.transform.position - this.targetDummy.transform.position).magnitude;
				if (num > 70f)
				{
					num = 70f;
				}
				float num2 = num / 70f;
				this.targetDummy.transform.localScale = new Vector3(num2, num2, num2);
				this.targetDummy.transform.position = this.cameraTarget;
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00049984 File Offset: 0x00047B84
		public virtual void GameUpdate()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.O))
			{
				this.EnableOrthoCamera(!this.UnityCamera.orthographic);
			}
			this.UpdateTargetDummy();
			this.config.EnableLiveGUI(this.EnableLiveGUI);
			if (this.config.IsBool("Orthographic"))
			{
				this.EnableOrthoCamera(this.config.GetBool("Orthographic"));
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000499ED File Offset: 0x00047BED
		public virtual void FixedStepUpdate()
		{
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000499EF File Offset: 0x00047BEF
		public virtual void PostUpdate()
		{
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000499F4 File Offset: 0x00047BF4
		protected float GetZoomFactor()
		{
			float num;
			if (this.UnityCamera.orthographic)
			{
				num = this.UnityCamera.orthographicSize;
			}
			else
			{
				num = (this.UnityCamera.transform.position - this.cameraTarget).magnitude;
			}
			if (num > 1f)
			{
				return num / (1f + Mathf.Log(num));
			}
			return num;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00049A60 File Offset: 0x00047C60
		protected void DebugDraw()
		{
			UnityEngine.Debug.DrawLine(this.UnityCamera.transform.position, this.cameraTarget, Color.red, 1f);
			UnityEngine.Debug.DrawRay(this.cameraTarget, this.UnityCamera.transform.up, Color.green, 1f);
			UnityEngine.Debug.DrawRay(this.cameraTarget, this.UnityCamera.transform.right, Color.yellow, 1f);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00049ADC File Offset: 0x00047CDC
		private void OnGUI()
		{
			string[] results = Profiler.GetResults();
			int num = 10;
			int num2 = Screen.width - 300;
			foreach (string text in results)
			{
				GUI.Label(new Rect((float)num2, (float)num, 500f, 30f), text);
				num += 20;
			}
		}

		// Token: 0x04000B2D RID: 2861
		public Transform Target;

		// Token: 0x04000B2E RID: 2862
		public bool ShowTargetDummy;

		// Token: 0x04000B2F RID: 2863
		public bool EnableLiveGUI;

		// Token: 0x04000B30 RID: 2864
		protected CameraCollision collision;

		// Token: 0x04000B31 RID: 2865
		protected InputManager InputManager;

		// Token: 0x04000B32 RID: 2866
		protected Camera UnityCamera;

		// Token: 0x04000B33 RID: 2867
		protected Config config;

		// Token: 0x04000B34 RID: 2868
		protected Vector3 cameraTarget;

		// Token: 0x04000B35 RID: 2869
		protected float targetDistance;

		// Token: 0x04000B36 RID: 2870
		protected bool disableInput;

		// Token: 0x04000B37 RID: 2871
		private GameObject targetDummy;
	}
}
