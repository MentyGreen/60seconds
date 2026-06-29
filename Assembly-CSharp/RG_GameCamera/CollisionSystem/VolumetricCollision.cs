using System;
using System.Collections.Generic;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001D2 RID: 466
	public class VolumetricCollision : ViewCollision
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x00055698 File Offset: 0x00053898
		public VolumetricCollision(Config config) : base(config)
		{
			this.rayHitComparer = new VolumetricCollision.RayHitComparer();
			this.hits = new List<RaycastHit>(40);
			this.rays = new Ray[10];
			for (int i = 0; i < this.rays.Length; i++)
			{
				this.rays[i] = new Ray(Vector3.zero, Vector3.zero);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00055700 File Offset: 0x00053900
		public override float Process(Vector3 cameraTarget, Vector3 dir, float distance)
		{
			float value = distance;
			float @float = this.config.GetFloat("RaycastTolerance");
			float float2 = this.config.GetFloat("MinDistance");
			Vector2 vector = this.config.GetVector2("ConeRadius");
			float float3 = this.config.GetFloat("ConeSegments");
			Vector3 a = cameraTarget - dir * distance;
			Vector3 a2 = Vector3.Cross(dir, Vector3.up);
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			while ((float)num < float3)
			{
				Quaternion rotation = Quaternion.AngleAxis((float)num / float3 * 360f, dir);
				Vector3 vector3 = cameraTarget + rotation * (a2 * vector.x);
				Vector3 a3 = a + rotation * (a2 * vector.y);
				vector2 = a3 - vector3;
				this.rays[num].origin = vector3;
				this.rays[num].direction = a3 - vector3;
				num++;
			}
			float magnitude = vector2.magnitude;
			this.hits.Clear();
			foreach (Ray ray in this.rays)
			{
				this.hits.AddRange(Physics.RaycastAll(ray, magnitude + @float));
			}
			this.hits.Sort(this.rayHitComparer);
			float num2 = float.PositiveInfinity;
			string @string = this.config.GetString("IgnoreCollisionTag");
			string string2 = this.config.GetString("TransparentCollisionTag");
			foreach (RaycastHit raycastHit in this.hits)
			{
				ViewCollision.CollisionClass collisionClass = ViewCollision.GetCollisionClass(raycastHit.collider, @string, string2);
				if (raycastHit.distance < num2 && collisionClass == ViewCollision.CollisionClass.Collision)
				{
					num2 = raycastHit.distance;
					value = raycastHit.distance - @float;
				}
				if (collisionClass == ViewCollision.CollisionClass.IgnoreTransparent)
				{
					base.UpdateTransparency(raycastHit.collider);
				}
			}
			return Mathf.Clamp(value, float2, distance);
		}

		// Token: 0x04000CAA RID: 3242
		private readonly List<RaycastHit> hits;

		// Token: 0x04000CAB RID: 3243
		private readonly Ray[] rays;

		// Token: 0x04000CAC RID: 3244
		private readonly VolumetricCollision.RayHitComparer rayHitComparer;

		// Token: 0x020003F7 RID: 1015
		public class RayHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x06001EC2 RID: 7874 RVA: 0x000810CC File Offset: 0x0007F2CC
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}
		}
	}
}
