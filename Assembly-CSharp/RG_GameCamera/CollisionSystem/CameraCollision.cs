using System;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001CA RID: 458
	[RequireComponent(typeof(CollisionConfig))]
	public class CameraCollision : MonoBehaviour
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00054D06 File Offset: 0x00052F06
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x00054D0D File Offset: 0x00052F0D
		public static CameraCollision Instance { get; private set; }

		// Token: 0x0600131B RID: 4891 RVA: 0x00054D18 File Offset: 0x00052F18
		private void Awake()
		{
			CameraCollision.Instance = this;
			this.unityCamera = CameraManager.Instance.UnityCamera;
			this.config = base.GetComponent<CollisionConfig>();
			this.unityCamera.nearClipPlane = 0.01f;
			this.targetCollision = new TargetCollision(this.config);
			this.simpleCollision = new SimpleCollision(this.config);
			this.sphericalCollision = new SphericalCollision(this.config);
			this.volumetricCollision = new VolumetricCollision(this.config);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00054D9C File Offset: 0x00052F9C
		private ViewCollision GetCollisionAlgorithm(string algorithm)
		{
			if (algorithm != null)
			{
				if (algorithm == "Simple")
				{
					return this.simpleCollision;
				}
				if (algorithm == "Spherical")
				{
					return this.sphericalCollision;
				}
				if (algorithm == "Volumetric")
				{
					return this.volumetricCollision;
				}
			}
			return null;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00054DEC File Offset: 0x00052FEC
		public void ProcessCollision(Vector3 cameraTarget, Vector3 targetHead, Vector3 dir, float distance, out float collisionTarget, out float collisionDistance)
		{
			collisionTarget = this.targetCollision.CalculateTarget(targetHead, cameraTarget);
			ViewCollision collisionAlgorithm = this.GetCollisionAlgorithm(this.config.GetSelection("CollisionAlgorithm"));
			Vector3 cameraTarget2 = cameraTarget * collisionTarget + targetHead * (1f - collisionTarget);
			collisionDistance = collisionAlgorithm.Process(cameraTarget2, dir, distance);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00054E4B File Offset: 0x0005304B
		public float GetRaycastTolerance()
		{
			return this.config.GetFloat("RaycastTolerance");
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00054E5D File Offset: 0x0005305D
		public float GetClipSpeed()
		{
			return this.config.GetFloat("ClipSpeed");
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00054E6F File Offset: 0x0005306F
		public float GetTargetClipSpeed()
		{
			return this.config.GetFloat("TargetClipSpeed");
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00054E81 File Offset: 0x00053081
		public float GetReturnSpeed()
		{
			return this.config.GetFloat("ReturnSpeed");
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00054E93 File Offset: 0x00053093
		public float GetReturnTargetSpeed()
		{
			return this.config.GetFloat("ReturnTargetSpeed");
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00054EA5 File Offset: 0x000530A5
		public float GetHeadOffset()
		{
			return this.config.GetFloat("HeadOffset");
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00054EB8 File Offset: 0x000530B8
		public ViewCollision.CollisionClass GetCollisionClass(Collider coll)
		{
			string @string = this.config.GetString("IgnoreCollisionTag");
			string string2 = this.config.GetString("TransparentCollisionTag");
			return ViewCollision.GetCollisionClass(coll, @string, string2);
		}

		// Token: 0x04000C92 RID: 3218
		private Camera unityCamera;

		// Token: 0x04000C93 RID: 3219
		private Config config;

		// Token: 0x04000C94 RID: 3220
		private TargetCollision targetCollision;

		// Token: 0x04000C95 RID: 3221
		private SimpleCollision simpleCollision;

		// Token: 0x04000C96 RID: 3222
		private VolumetricCollision volumetricCollision;

		// Token: 0x04000C97 RID: 3223
		private SphericalCollision sphericalCollision;
	}
}
