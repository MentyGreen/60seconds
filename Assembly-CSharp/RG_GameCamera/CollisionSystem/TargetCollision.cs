using System;
using System.Collections;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001CE RID: 462
	public class TargetCollision
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x000551DF File Offset: 0x000533DF
		public TargetCollision(Config config)
		{
			this.rayHitComparer = new TargetCollision.RayHitComparer();
			this.config = config;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000551FC File Offset: 0x000533FC
		public float CalculateTarget(Vector3 targetHead, Vector3 cameraTarget)
		{
			string @string = this.config.GetString("IgnoreCollisionTag");
			string string2 = this.config.GetString("TransparentCollisionTag");
			float @float = this.config.GetFloat("TargetSphereRadius");
			float num = 1f;
			Vector3 normalized = (cameraTarget - targetHead).normalized;
			Ray ray = new Ray(targetHead, normalized);
			this.hits = Physics.RaycastAll(ray, normalized.magnitude + @float);
			Array.Sort(this.hits, this.rayHitComparer);
			float num2 = float.PositiveInfinity;
			bool flag = false;
			foreach (RaycastHit raycastHit in this.hits)
			{
				ViewCollision.CollisionClass collisionClass = ViewCollision.GetCollisionClass(raycastHit.collider, @string, string2);
				if (raycastHit.distance < num2 && collisionClass == ViewCollision.CollisionClass.Collision)
				{
					num2 = raycastHit.distance;
					num = raycastHit.distance - @float;
					flag = true;
					Debug.DrawLine(targetHead, raycastHit.point, Color.yellow);
				}
			}
			if (flag)
			{
				return Mathf.Clamp01(num / (targetHead - cameraTarget).magnitude);
			}
			return 1f;
		}

		// Token: 0x04000C9E RID: 3230
		private Ray ray;

		// Token: 0x04000C9F RID: 3231
		private RaycastHit[] hits;

		// Token: 0x04000CA0 RID: 3232
		private readonly TargetCollision.RayHitComparer rayHitComparer;

		// Token: 0x04000CA1 RID: 3233
		private readonly Config config;

		// Token: 0x020003F4 RID: 1012
		public class RayHitComparer : IComparer
		{
			// Token: 0x06001EBF RID: 7871 RVA: 0x00081088 File Offset: 0x0007F288
			public int Compare(object x, object y)
			{
				return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			}
		}
	}
}
