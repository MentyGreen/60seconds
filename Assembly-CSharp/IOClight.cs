using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class IOClight : IOCcomp
{
	// Token: 0x06000B53 RID: 2899 RVA: 0x00030199 File Offset: 0x0002E399
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x000301A4 File Offset: 0x0002E3A4
	public override void Init()
	{
		try
		{
			this.iocCam = Camera.main.GetComponent<IOCcam>();
			this.hit = default(RaycastHit);
			this.currentLayer = base.gameObject.layer;
			this.h = default(RaycastHit);
			base.enabled = true;
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.Log(ex.Message);
		}
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00030218 File Offset: 0x0002E418
	private void Start()
	{
		this.UpdateValues();
		this.Initialize();
		if (base.GetComponent<Renderer>() == null)
		{
			MeshRenderer meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			meshRenderer.castShadows = false;
			meshRenderer.receiveShadows = false;
		}
		this.prefab = (Resources.Load("probe") as GameObject);
		this.prefab.GetComponent<SphereCollider>().radius = this.probeRadius;
		this.center = base.transform.position;
		this.range = base.GetComponent<Light>().range;
		this.angle = base.GetComponent<Light>().spotAngle;
		this.parent = base.transform;
		LightType type = base.GetComponent<Light>().type;
		if (type != LightType.Spot)
		{
			if (type == LightType.Point)
			{
				for (int i = 0; i < this.probes; i++)
				{
					this.ray = new Ray(this.center, Random.onUnitSphere);
					if (Physics.Raycast(this.ray, out this.hit, this.range))
					{
						this.go = Object.Instantiate<GameObject>(this.prefab, this.hit.point, Quaternion.identity);
						this.go.transform.parent = this.parent;
						this.go.layer = this.currentLayer;
					}
				}
				return;
			}
		}
		else
		{
			for (int j = 0; j < this.probes; j++)
			{
				this.rndPoint = Random.insideUnitCircle * (Mathf.Tan(0.017453292f * this.angle * 0.5f) * this.range);
				this.rayDir = (this.center + this.parent.forward * this.range + this.parent.rotation * new Vector3(this.rndPoint.x, this.rndPoint.y) - this.center).normalized;
				this.ray = new Ray(this.center, this.rayDir);
				if (Physics.Raycast(this.ray, out this.hit, this.range))
				{
					this.go = Object.Instantiate<GameObject>(this.prefab, this.hit.point, Quaternion.identity);
					this.go.transform.parent = this.parent;
					this.go.layer = this.currentLayer;
				}
			}
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x0003048D File Offset: 0x0002E68D
	public void Initialize()
	{
		base.GetComponent<Light>().enabled = false;
		base.GetComponent<Light>().renderMode = LightRenderMode.ForcePixel;
		this.hidden = true;
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x000304B0 File Offset: 0x0002E6B0
	public void UpdateValues()
	{
		if (this.Probes != 0)
		{
			this.probes = this.Probes;
		}
		else
		{
			this.probes = this.iocCam.lightProbes;
		}
		if (this.ProbeRadius != 0f)
		{
			this.probeRadius = this.ProbeRadius;
			return;
		}
		this.probeRadius = this.iocCam.probeRadius;
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00030510 File Offset: 0x0002E710
	public override void UnHide(RaycastHit hit)
	{
		this.counter = Time.frameCount;
		this.hitPoint = base.transform.worldToLocalMatrix.MultiplyPoint(hit.point);
		if (this.hidden)
		{
			this.hidden = false;
			base.GetComponent<Light>().enabled = true;
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00030563 File Offset: 0x0002E763
	public void Hide()
	{
		this.hidden = true;
		base.GetComponent<Light>().enabled = false;
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00030578 File Offset: 0x0002E778
	private void Update()
	{
		this.frameInterval = Time.frameCount % 6;
		if (!this.hidden && this.frameInterval == 0 && Time.frameCount - this.counter > this.iocCam.hideDelay)
		{
			if (this.iocCam.preCullCheck && base.GetComponent<Renderer>().isVisible)
			{
				this.p = base.transform.localToWorldMatrix.MultiplyPoint(this.hitPoint);
				this.r = new Ray(this.p, this.iocCam.transform.position - this.p);
				if (Physics.Raycast(this.r, out this.h, this.iocCam.viewDistance))
				{
					if (!this.h.collider.CompareTag(this.iocCam.tag))
					{
						this.Hide();
						return;
					}
					this.counter = Time.frameCount;
					return;
				}
			}
			else
			{
				this.Hide();
			}
		}
	}

	// Token: 0x04000587 RID: 1415
	public int Probes;

	// Token: 0x04000588 RID: 1416
	public float ProbeRadius;

	// Token: 0x04000589 RID: 1417
	private int probes;

	// Token: 0x0400058A RID: 1418
	private float probeRadius;

	// Token: 0x0400058B RID: 1419
	private GameObject go;

	// Token: 0x0400058C RID: 1420
	private RaycastHit hit;

	// Token: 0x0400058D RID: 1421
	private Vector3 hitPoint;

	// Token: 0x0400058E RID: 1422
	private SphereCollider probe;

	// Token: 0x0400058F RID: 1423
	private Vector3 center;

	// Token: 0x04000590 RID: 1424
	private float range;

	// Token: 0x04000591 RID: 1425
	private float angle;

	// Token: 0x04000592 RID: 1426
	private Ray ray;

	// Token: 0x04000593 RID: 1427
	private Vector3 rayDir;

	// Token: 0x04000594 RID: 1428
	private int counter;

	// Token: 0x04000595 RID: 1429
	private IOCcam iocCam;

	// Token: 0x04000596 RID: 1430
	private int frameInterval;

	// Token: 0x04000597 RID: 1431
	private bool hidden;

	// Token: 0x04000598 RID: 1432
	private Transform parent;

	// Token: 0x04000599 RID: 1433
	private int currentLayer;

	// Token: 0x0400059A RID: 1434
	private Vector2 rndPoint;

	// Token: 0x0400059B RID: 1435
	private GameObject prefab;

	// Token: 0x0400059C RID: 1436
	private RaycastHit h;

	// Token: 0x0400059D RID: 1437
	private Ray r;

	// Token: 0x0400059E RID: 1438
	private Vector3 p;
}
