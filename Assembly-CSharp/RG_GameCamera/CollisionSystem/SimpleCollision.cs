using System;
using System.Collections;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001CC RID: 460
	public class SimpleCollision : ViewCollision
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00054EFF File Offset: 0x000530FF
		public SimpleCollision(Config config) : base(config)
		{
			this.rayHitComparer = new SimpleCollision.RayHitComparer();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00054F14 File Offset: 0x00053114
		public override float Process(Vector3 cameraTarget, Vector3 dir, float distance)
		{
			float value = distance;
			float @float = this.config.GetFloat("RaycastTolerance");
			float float2 = this.config.GetFloat("MinDistance");
			float num = float.PositiveInfinity;
			this.ray.origin = cameraTarget;
			this.ray.direction = -dir;
			this.hits = Physics.RaycastAll(this.ray, distance + @float);
			Array.Sort(this.hits, this.rayHitComparer);
			string @string = this.config.GetString("IgnoreCollisionTag");
			string string2 = this.config.GetString("TransparentCollisionTag");
			foreach (RaycastHit raycastHit in this.hits)
			{
				ViewCollision.CollisionClass collisionClass = ViewCollision.GetCollisionClass(raycastHit.collider, @string, string2);
				if (raycastHit.distance < num && collisionClass == ViewCollision.CollisionClass.Collision)
				{
					num = raycastHit.distance;
					value = raycastHit.distance - @float;
				}
				if (collisionClass == ViewCollision.CollisionClass.IgnoreTransparent)
				{
					base.UpdateTransparency(raycastHit.collider);
				}
			}
			return Mathf.Clamp(value, float2, distance);
		}

		// Token: 0x04000C98 RID: 3224
		private Ray ray;

		// Token: 0x04000C99 RID: 3225
		private RaycastHit[] hits;

		// Token: 0x04000C9A RID: 3226
		private readonly SimpleCollision.RayHitComparer rayHitComparer;

		// Token: 0x020003F2 RID: 1010
		public class RayHitComparer : IComparer
		{
			// Token: 0x06001EBB RID: 7867 RVA: 0x00081010 File Offset: 0x0007F210
			public int Compare(object x, object y)
			{
				return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			}
		}
	}
}
