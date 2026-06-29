using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class AspectUtility : MonoBehaviour
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x0003E060 File Offset: 0x0003C260
	private void Awake()
	{
		AspectUtility.cam = base.GetComponent<Camera>();
		if (!AspectUtility.cam)
		{
			AspectUtility.cam = Camera.main;
		}
		if (!AspectUtility.cam)
		{
			Debug.LogError("No camera available");
			return;
		}
		AspectUtility.wantedAspectRatio = this._wantedAspectRatio;
		AspectUtility.SetCamera();
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
	public static void SetCamera()
	{
		float num = (float)Screen.width / (float)Screen.height;
		if ((float)((int)(num * 100f)) / 100f == (float)((int)(AspectUtility.wantedAspectRatio * 100f)) / 100f)
		{
			AspectUtility.cam.rect = new Rect(0f, 0f, 1f, 1f);
			if (AspectUtility.backgroundCam)
			{
				Object.Destroy(AspectUtility.backgroundCam.gameObject);
			}
			return;
		}
		if (num > AspectUtility.wantedAspectRatio)
		{
			float num2 = 1f - AspectUtility.wantedAspectRatio / num;
			AspectUtility.cam.rect = new Rect(num2 / 2f, 0f, 1f - num2, 1f);
		}
		else
		{
			float num3 = 1f - num / AspectUtility.wantedAspectRatio;
			AspectUtility.cam.rect = new Rect(0f, num3 / 2f, 1f, 1f - num3);
		}
		if (!AspectUtility.backgroundCam)
		{
			AspectUtility.backgroundCam = new GameObject("BackgroundCam", new Type[]
			{
				typeof(Camera)
			}).GetComponent<Camera>();
			AspectUtility.backgroundCam.depth = -2.1474836E+09f;
			AspectUtility.backgroundCam.clearFlags = CameraClearFlags.Color;
			AspectUtility.backgroundCam.backgroundColor = Color.black;
			AspectUtility.backgroundCam.cullingMask = 0;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003E214 File Offset: 0x0003C414
	public static int screenHeight
	{
		get
		{
			return (int)((float)Screen.height * AspectUtility.cam.rect.height);
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0003E23C File Offset: 0x0003C43C
	public static int screenWidth
	{
		get
		{
			return (int)((float)Screen.width * AspectUtility.cam.rect.width);
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0003E264 File Offset: 0x0003C464
	public static int xOffset
	{
		get
		{
			return (int)((float)Screen.width * AspectUtility.cam.rect.x);
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0003E28C File Offset: 0x0003C48C
	public static int yOffset
	{
		get
		{
			return (int)((float)Screen.height * AspectUtility.cam.rect.y);
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
	public static Rect screenRect
	{
		get
		{
			return new Rect(AspectUtility.cam.rect.x * (float)Screen.width, AspectUtility.cam.rect.y * (float)Screen.height, AspectUtility.cam.rect.width * (float)Screen.width, AspectUtility.cam.rect.height * (float)Screen.height);
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0003E32C File Offset: 0x0003C52C
	public static Vector3 mousePosition
	{
		get
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.y -= (float)((int)(AspectUtility.cam.rect.y * (float)Screen.height));
			mousePosition.x -= (float)((int)(AspectUtility.cam.rect.x * (float)Screen.width));
			return mousePosition;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0003E38C File Offset: 0x0003C58C
	public static Vector2 guiMousePosition
	{
		get
		{
			Vector2 mousePosition = Event.current.mousePosition;
			mousePosition.y = Mathf.Clamp(mousePosition.y, AspectUtility.cam.rect.y * (float)Screen.height, AspectUtility.cam.rect.y * (float)Screen.height + AspectUtility.cam.rect.height * (float)Screen.height);
			mousePosition.x = Mathf.Clamp(mousePosition.x, AspectUtility.cam.rect.x * (float)Screen.width, AspectUtility.cam.rect.x * (float)Screen.width + AspectUtility.cam.rect.width * (float)Screen.width);
			return mousePosition;
		}
	}

	// Token: 0x04000909 RID: 2313
	public float _wantedAspectRatio = 1.3333333f;

	// Token: 0x0400090A RID: 2314
	private static float wantedAspectRatio;

	// Token: 0x0400090B RID: 2315
	private static Camera cam;

	// Token: 0x0400090C RID: 2316
	private static Camera backgroundCam;
}
