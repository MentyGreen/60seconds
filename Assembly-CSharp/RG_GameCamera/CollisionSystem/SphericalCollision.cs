using System;
using System.Collections;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001CD RID: 461
	public class SphericalCollision : ViewCollision
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x00055023 File Offset: 0x00053223
		public SphericalCollision(Config config) : base(config)
		{
			this.rayHitComparer = new SphericalCollision.RayHitComparer();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00055038 File Offset: 0x00053238
		public override float Process(Vector3 cameraTarget, Vector3 dir, float distance)
		{
			float value = distance;
			float @float = this.config.GetFloat("MinDistance");
			float float2 = this.config.GetFloat("SphereCastTolerance");
			float float3 = this.config.GetFloat("SphereCastRadius");
			this.ray.origin = cameraTarget + dir * float3;
			this.ray.direction = -dir;
			Collider[] array = Physics.OverlapSphere(this.ray.origin, float3);
			bool flag = false;
			string @string = this.config.GetString("IgnoreCollisionTag");
			string string2 = this.config.GetString("TransparentCollisionTag");
			for (int i = 0; i < array.Length; i++)
			{
				if (ViewCollision.GetCollisionClass(array[i], @string, string2) == ViewCollision.CollisionClass.Collision)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.ray.origin = this.ray.origin + dir * float3;
				this.hits = Physics.RaycastAll(this.ray, distance - float3 + float2);
			}
			else
			{
				this.hits = Physics.SphereCastAll(this.ray, float3, distance + float3);
			}
			Array.Sort(this.hits, this.rayHitComparer);
			float num = float.PositiveInfinity;
			foreach (RaycastHit raycastHit in this.hits)
			{
				ViewCollision.CollisionClass collisionClass = ViewCollision.GetCollisionClass(raycastHit.collider, @string, string2);
				if (raycastHit.distance < num && collisionClass == ViewCollision.CollisionClass.Collision)
				{
					num = raycastHit.distance;
					value = raycastHit.distance - float2;
				}
				if (collisionClass == ViewCollision.CollisionClass.IgnoreTransparent)
				{
					base.UpdateTransparency(raycastHit.collider);
				}
			}
			return Mathf.Clamp(value, @float, distance);
		}

		// Token: 0x04000C9B RID: 3227
		private Ray ray;

		// Token: 0x04000C9C RID: 3228
		private RaycastHit[] hits;

		// Token: 0x04000C9D RID: 3229
		private readonly SphericalCollision.RayHitComparer rayHitComparer;

		// Token: 0x020003F3 RID: 1011
		public class RayHitComparer : IComparer
		{
			// Token: 0x06001EBD RID: 7869 RVA: 0x0008104C File Offset: 0x0007F24C
			public int Compare(object x, object y)
			{
				return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			}
		}
	}
}
