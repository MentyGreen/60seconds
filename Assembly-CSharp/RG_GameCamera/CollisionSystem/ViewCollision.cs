using System;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001D1 RID: 465
	public abstract class ViewCollision
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x000555FD File Offset: 0x000537FD
		protected ViewCollision(Config config)
		{
			this.config = config;
		}

		// Token: 0x06001337 RID: 4919
		public abstract float Process(Vector3 cameraTarget, Vector3 cameraDir, float distance);

		// Token: 0x06001338 RID: 4920 RVA: 0x0005560C File Offset: 0x0005380C
		public static ViewCollision.CollisionClass GetCollisionClass(Collider collider, string ignoreTag, string transparentTag)
		{
			ViewCollision.CollisionClass result = ViewCollision.CollisionClass.Collision;
			if (collider.isTrigger)
			{
				result = ViewCollision.CollisionClass.Trigger;
			}
			else if (collider.gameObject != null)
			{
				if (collider.gameObject.tag == ignoreTag || collider.gameObject.GetComponent<IgnoreCollision>())
				{
					result = ViewCollision.CollisionClass.Ignore;
				}
				else if (collider.gameObject.tag == transparentTag || collider.gameObject.GetComponent<TransparentCollision>())
				{
					result = ViewCollision.CollisionClass.IgnoreTransparent;
				}
			}
			return result;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00055686 File Offset: 0x00053886
		protected void UpdateTransparency(Collider collider)
		{
			TransparencyManager.Instance.UpdateObject(collider.gameObject);
		}

		// Token: 0x04000CA9 RID: 3241
		protected readonly Config config;

		// Token: 0x020003F6 RID: 1014
		public enum CollisionClass
		{
			// Token: 0x04001834 RID: 6196
			Collision,
			// Token: 0x04001835 RID: 6197
			Trigger,
			// Token: 0x04001836 RID: 6198
			Ignore,
			// Token: 0x04001837 RID: 6199
			IgnoreTransparent
		}
	}
}
