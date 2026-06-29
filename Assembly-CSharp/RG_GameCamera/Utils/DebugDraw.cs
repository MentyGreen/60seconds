using System;
using System.Diagnostics;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000182 RID: 386
	internal class DebugDraw : MonoBehaviour
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000481D2 File Offset: 0x000463D2
		private static DebugDraw Instance
		{
			get
			{
				if (!DebugDraw.instance)
				{
					DebugDraw.instance = CameraInstance.CreateInstance<DebugDraw>("DebugDraw");
				}
				return DebugDraw.instance;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000481F4 File Offset: 0x000463F4
		private void Awake()
		{
			DebugDraw.instance = this;
			this.debugRoot = DebugDraw.instance.gameObject;
			this.dbgObjects = new DebugDraw.DbgObject[20];
			for (int i = 0; i < this.dbgObjects.Length; i++)
			{
				this.dbgObjects[i] = new DebugDraw.DbgObject();
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00048244 File Offset: 0x00046444
		private void Update()
		{
			this.debugRoot.SetActive(DebugDraw.Enabled);
			foreach (DebugDraw.DbgObject dbgObject in this.dbgObjects)
			{
				if (dbgObject.obj)
				{
					dbgObject.timer--;
					if ((float)dbgObject.timer < 0f)
					{
						dbgObject.obj.gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000482B4 File Offset: 0x000464B4
		[Conditional("UNITY_EDITOR")]
		public static void Sphere(Vector3 pos, float scale, Color color, int time)
		{
			DebugDraw debugDraw = DebugDraw.Instance;
			bool flag = false;
			DebugDraw.DbgObject dbgObject = null;
			foreach (DebugDraw.DbgObject dbgObject2 in debugDraw.dbgObjects)
			{
				if (dbgObject2.obj && !Debug.IsActive(dbgObject2.obj))
				{
					dbgObject2.obj.SetActive(true);
					dbgObject2.obj.transform.position = pos;
					dbgObject2.obj.transform.localScale = new Vector3(scale, scale, scale);
					dbgObject2.timer = time;
					dbgObject2.obj.GetComponent<MeshRenderer>().material.color = color;
					flag = true;
					break;
				}
				if (!dbgObject2.obj)
				{
					dbgObject = dbgObject2;
				}
			}
			if (!flag && dbgObject != null)
			{
				dbgObject.obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				Object.Destroy(dbgObject.obj.GetComponent<SphereCollider>());
				dbgObject.obj.transform.position = pos;
				dbgObject.obj.transform.parent = debugDraw.debugRoot.transform;
				dbgObject.timer = time;
				Material material = new Material(Shader.Find("VertexLit"));
				dbgObject.obj.GetComponent<MeshRenderer>().material = material;
				material.color = color;
				flag = true;
			}
			if (!flag)
			{
				Array.Sort<DebugDraw.DbgObject>(debugDraw.dbgObjects, (DebugDraw.DbgObject obj0, DebugDraw.DbgObject obj1) => obj0.timer.CompareTo(obj1.timer));
				DebugDraw.DbgObject dbgObject3 = debugDraw.dbgObjects[0];
				dbgObject3.obj.SetActive(true);
				dbgObject3.obj.transform.position = pos;
				dbgObject3.obj.transform.localScale = new Vector3(scale, scale, scale);
				dbgObject3.timer = time;
				dbgObject3.obj.GetComponent<MeshRenderer>().material.color = color;
			}
		}

		// Token: 0x04000B1D RID: 2845
		public static bool Enabled = true;

		// Token: 0x04000B1E RID: 2846
		private static DebugDraw instance;

		// Token: 0x04000B1F RID: 2847
		private GameObject debugRoot;

		// Token: 0x04000B20 RID: 2848
		private DebugDraw.DbgObject[] dbgObjects;

		// Token: 0x020003DB RID: 987
		private class DbgObject
		{
			// Token: 0x04001800 RID: 6144
			public GameObject obj;

			// Token: 0x04001801 RID: 6145
			public int timer;
		}
	}
}
