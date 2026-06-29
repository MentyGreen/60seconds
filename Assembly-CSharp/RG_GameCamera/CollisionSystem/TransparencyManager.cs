using System;
using System.Collections.Generic;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.CollisionSystem
{
	// Token: 0x020001CF RID: 463
	public class TransparencyManager : MonoBehaviour
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0005531D File Offset: 0x0005351D
		public static TransparencyManager Instance
		{
			get
			{
				if (!TransparencyManager.instance)
				{
					TransparencyManager.instance = CameraInstance.CreateInstance<TransparencyManager>("TransparencyManager");
				}
				return TransparencyManager.instance;
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0005533F File Offset: 0x0005353F
		private void Awake()
		{
			TransparencyManager.instance = this;
			this.objects = new Dictionary<GameObject, TransparencyManager.TransObject>();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00055354 File Offset: 0x00053554
		private void Update()
		{
			foreach (KeyValuePair<GameObject, TransparencyManager.TransObject> keyValuePair in this.objects)
			{
				keyValuePair.Value.fadeoutTimer += Time.deltaTime;
				if (keyValuePair.Value.fadeoutTimer > 0.1f)
				{
					keyValuePair.Value.fadeIn = false;
				}
				float num = TransparencyManager.GetAlpha(keyValuePair.Key);
				bool flag = false;
				if (keyValuePair.Value.fadeIn)
				{
					num = Mathf.SmoothDamp(num, this.TransparencyMax, ref this.fadeVelocity, this.TransparencyFadeIn);
				}
				else
				{
					num = Mathf.SmoothDamp(num, keyValuePair.Value.originalAlpha, ref this.fadeVelocity, this.TransparencyFadeOut);
					if (Mathf.Abs(num - keyValuePair.Value.originalAlpha) < Mathf.Epsilon)
					{
						flag = true;
						num = keyValuePair.Value.originalAlpha;
					}
				}
				TransparencyManager.SetAlpha(keyValuePair.Key, num);
				if (flag)
				{
					this.objects.Remove(keyValuePair.Key);
					break;
				}
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00055484 File Offset: 0x00053684
		public void UpdateObject(GameObject obj)
		{
			TransparencyManager.TransObject transObject = null;
			if (this.objects.TryGetValue(obj, out transObject))
			{
				transObject.fadeIn = true;
				transObject.fadeoutTimer = 0f;
				return;
			}
			this.objects.Add(obj, new TransparencyManager.TransObject
			{
				originalAlpha = TransparencyManager.GetAlpha(obj),
				fadeIn = true,
				fadeoutTimer = 0f
			});
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000554E8 File Offset: 0x000536E8
		private static void SetAlpha(GameObject obj, float alpha)
		{
			MeshRenderer component = obj.GetComponent<MeshRenderer>();
			if (component)
			{
				Material sharedMaterial = component.sharedMaterial;
				if (sharedMaterial)
				{
					Color color = sharedMaterial.color;
					color.a = alpha;
					sharedMaterial.color = color;
				}
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0005552C File Offset: 0x0005372C
		private static float GetAlpha(GameObject obj)
		{
			MeshRenderer component = obj.GetComponent<MeshRenderer>();
			if (component)
			{
				Material sharedMaterial = component.sharedMaterial;
				if (sharedMaterial)
				{
					return sharedMaterial.color.a;
				}
			}
			return 1f;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00055568 File Offset: 0x00053768
		private void OnApplicationQuit()
		{
			foreach (KeyValuePair<GameObject, TransparencyManager.TransObject> keyValuePair in this.objects)
			{
				TransparencyManager.SetAlpha(keyValuePair.Key, keyValuePair.Value.originalAlpha);
			}
		}

		// Token: 0x04000CA2 RID: 3234
		private static TransparencyManager instance;

		// Token: 0x04000CA3 RID: 3235
		public float TransparencyMax = 0.5f;

		// Token: 0x04000CA4 RID: 3236
		public float TransparencyFadeOut = 0.2f;

		// Token: 0x04000CA5 RID: 3237
		public float TransparencyFadeIn = 0.1f;

		// Token: 0x04000CA6 RID: 3238
		private float fadeVelocity;

		// Token: 0x04000CA7 RID: 3239
		private const float fadeoutTimerMax = 0.1f;

		// Token: 0x04000CA8 RID: 3240
		private Dictionary<GameObject, TransparencyManager.TransObject> objects;

		// Token: 0x020003F5 RID: 1013
		private class TransObject
		{
			// Token: 0x04001830 RID: 6192
			public float originalAlpha;

			// Token: 0x04001831 RID: 6193
			public bool fadeIn;

			// Token: 0x04001832 RID: 6194
			public float fadeoutTimer;
		}
	}
}
