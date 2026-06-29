using System;
using RG_GameCamera.CollisionSystem;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x02000197 RID: 407
	public abstract class GameInput : MonoBehaviour
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060011F1 RID: 4593
		public abstract InputPreset PresetType { get; }

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004D363 File Offset: 0x0004B563
		protected GameInput()
		{
			this.mouseFilter = new InputFilter(10, 0.5f);
			this.padFilter = new InputFilter(10, 0.6f);
		}

		// Token: 0x060011F3 RID: 4595
		public abstract void UpdateInput(Input[] inputs);

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004D38F File Offset: 0x0004B58F
		protected void SetInput(Input[] inputs, InputType type, object value)
		{
			inputs[(int)type].Value = value;
			inputs[(int)type].Valid = true;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004D3A4 File Offset: 0x0004B5A4
		public static bool FindWaypointPosition(Vector2 mousePos, out Vector3 pos)
		{
			RaycastHit[] array = Physics.RaycastAll(CameraManager.Instance.UnityCamera.ScreenPointToRay(mousePos), float.MaxValue);
			if (array.Length == 0)
			{
				pos = Vector3.zero;
				return false;
			}
			Array.Sort<RaycastHit>(array, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
			float num = float.MaxValue;
			Vector3 vector = Vector3.zero;
			foreach (RaycastHit raycastHit in array)
			{
				Collider collider = raycastHit.collider;
				ViewCollision.CollisionClass collisionClass = CameraCollision.Instance.GetCollisionClass(collider);
				if (raycastHit.distance < num && collisionClass == ViewCollision.CollisionClass.Collision)
				{
					num = raycastHit.distance;
					vector = raycastHit.point;
				}
			}
			pos = vector;
			return true;
		}

		// Token: 0x04000B85 RID: 2949
		protected InputFilter mouseFilter;

		// Token: 0x04000B86 RID: 2950
		protected InputFilter padFilter;

		// Token: 0x04000B87 RID: 2951
		protected float doubleClickTimeout;
	}
}
