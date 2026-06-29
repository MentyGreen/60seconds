using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class IOCcam : MonoBehaviour
{
	// Token: 0x06000B4E RID: 2894 RVA: 0x0002FDEC File Offset: 0x0002DFEC
	private void Awake()
	{
		this.cam = base.GetComponent<Camera>();
		this.hit = default(RaycastHit);
		if (this.viewDistance == 0f)
		{
			this.viewDistance = 100f;
		}
		this.cam.farClipPlane = this.viewDistance;
		this.haltonIndex = 0;
		if (base.GetComponent<SphereCollider>() == null)
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.radius = 1f;
			sphereCollider.isTrigger = true;
		}
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0002FE6C File Offset: 0x0002E06C
	private void Start()
	{
		this.pixels = Mathf.FloorToInt((float)(Screen.width * Screen.height) / 4f);
		this.hx = new float[this.pixels];
		this.hy = new float[this.pixels];
		for (int i = 0; i < this.pixels; i++)
		{
			this.hx[i] = this.HaltonSequence(i, 2);
			this.hy[i] = this.HaltonSequence(i, 3);
		}
		foreach (GameObject gameObject in Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (this.tags.Contains(gameObject.tag))
			{
				if (gameObject.GetComponent<Light>() != null)
				{
					if (gameObject.GetComponent<IOClight>() == null)
					{
						gameObject.AddComponent<IOClight>();
					}
				}
				else if (gameObject.GetComponent<Terrain>() != null)
				{
					gameObject.AddComponent<IOCterrain>();
				}
				else if (gameObject.GetComponent<IOClod>() == null)
				{
					gameObject.AddComponent<IOClod>();
				}
			}
		}
		GameObject gameObject2 = new GameObject("RayCaster");
		gameObject2.transform.Translate(base.transform.position);
		gameObject2.transform.rotation = base.transform.rotation;
		this.rayCaster = gameObject2.AddComponent<Camera>();
		this.rayCaster.enabled = false;
		this.rayCaster.clearFlags = CameraClearFlags.Nothing;
		this.rayCaster.cullingMask = 0;
		this.rayCaster.aspect = this.cam.aspect;
		this.rayCaster.nearClipPlane = this.cam.nearClipPlane;
		this.rayCaster.farClipPlane = this.cam.farClipPlane;
		this.rayCaster.fieldOfView = this.raysFov;
		gameObject2.transform.parent = base.transform;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00030048 File Offset: 0x0002E248
	private void Update()
	{
		for (int i = 0; i <= this.samples; i++)
		{
			this.r = this.rayCaster.ViewportPointToRay(new Vector3(this.hx[this.haltonIndex], this.hy[this.haltonIndex], 0f));
			this.haltonIndex++;
			if (this.haltonIndex >= this.pixels)
			{
				this.haltonIndex = 0;
			}
			if (Physics.Raycast(this.r, out this.hit, this.viewDistance, this.layerMsk.value))
			{
				if (this.iocComp = this.hit.transform.GetComponent<IOCcomp>())
				{
					this.iocComp.UnHide(this.hit);
				}
				else if (this.iocComp = this.hit.transform.parent.GetComponent<IOCcomp>())
				{
					this.iocComp.UnHide(this.hit);
				}
			}
		}
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00030154 File Offset: 0x0002E354
	private float HaltonSequence(int index, int b)
	{
		float num = 0f;
		float num2 = 1f / (float)b;
		int i = index;
		while (i > 0)
		{
			num += num2 * (float)(i % b);
			i = Mathf.FloorToInt((float)(i / b));
			num2 /= (float)b;
		}
		return num;
	}

	// Token: 0x0400056F RID: 1391
	public string tags;

	// Token: 0x04000570 RID: 1392
	public LayerMask layerMsk;

	// Token: 0x04000571 RID: 1393
	public int occludeeLayer;

	// Token: 0x04000572 RID: 1394
	public int samples;

	// Token: 0x04000573 RID: 1395
	public float raysFov;

	// Token: 0x04000574 RID: 1396
	public bool preCullCheck;

	// Token: 0x04000575 RID: 1397
	public float viewDistance;

	// Token: 0x04000576 RID: 1398
	public int hideDelay;

	// Token: 0x04000577 RID: 1399
	public bool realtimeShadows;

	// Token: 0x04000578 RID: 1400
	public float lod1Distance;

	// Token: 0x04000579 RID: 1401
	public float lod2Distance;

	// Token: 0x0400057A RID: 1402
	public float lodMargin;

	// Token: 0x0400057B RID: 1403
	public int lightProbes;

	// Token: 0x0400057C RID: 1404
	public float probeRadius;

	// Token: 0x0400057D RID: 1405
	private RaycastHit hit;

	// Token: 0x0400057E RID: 1406
	private Ray r;

	// Token: 0x0400057F RID: 1407
	private int layerMask;

	// Token: 0x04000580 RID: 1408
	private IOCcomp iocComp;

	// Token: 0x04000581 RID: 1409
	private int haltonIndex;

	// Token: 0x04000582 RID: 1410
	private float[] hx;

	// Token: 0x04000583 RID: 1411
	private float[] hy;

	// Token: 0x04000584 RID: 1412
	private int pixels;

	// Token: 0x04000585 RID: 1413
	private Camera cam;

	// Token: 0x04000586 RID: 1414
	private Camera rayCaster;
}
