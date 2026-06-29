using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class IOClod : IOCcomp
{
	// Token: 0x06000B5C RID: 2908 RVA: 0x0003068B File Offset: 0x0002E88B
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00030694 File Offset: 0x0002E894
	public override void Init()
	{
		try
		{
			this.iocCam = Camera.main.GetComponent<IOCcam>();
			this.shadowDistance = QualitySettings.shadowDistance * 2f;
			this.currentLayer = base.gameObject.layer;
			this.prevDist = 0f;
			this.prevHitTime = Time.time;
			this.sleeping = true;
			this.h = default(RaycastHit);
			base.enabled = true;
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.Log(ex.Message);
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00030728 File Offset: 0x0002E928
	private void Start()
	{
		this.UpdateValues();
		if (base.transform.Find("Lod_0"))
		{
			this.lods = 1;
			this.rs0 = (from x in base.transform.Find("Lod_0").GetComponentsInChildren<Renderer>(false)
			where x.gameObject.GetComponent<Light>() == null
			select x).ToArray<Renderer>();
			this.sh0 = new Shader[this.rs0.Length][];
			for (int i = 0; i < this.rs0.Length; i++)
			{
				this.sh0[i] = new Shader[this.rs0[i].sharedMaterials.Length];
				for (int j = 0; j < this.rs0[i].sharedMaterials.Length; j++)
				{
					this.sh0[i][j] = this.rs0[i].sharedMaterials[j].shader;
				}
			}
			if (base.transform.Find("Lod_1"))
			{
				this.lods++;
				this.rs1 = (from x in base.transform.Find("Lod_1").GetComponentsInChildren<Renderer>(false)
				where x.gameObject.GetComponent<Light>() == null
				select x).ToArray<Renderer>();
				if (base.transform.Find("Lod_2"))
				{
					this.lods++;
					this.rs2 = (from x in base.transform.Find("Lod_2").GetComponentsInChildren<Renderer>(false)
					where x.gameObject.GetComponent<Light>() == null
					select x).ToArray<Renderer>();
				}
			}
		}
		else
		{
			this.lods = 0;
		}
		this.rs = (from x in base.GetComponentsInChildren<Renderer>(false)
		where x.gameObject.GetComponent<Light>() == null
		select x).ToArray<Renderer>();
		this.sh = new Shader[this.rs.Length][];
		for (int k = 0; k < this.rs.Length; k++)
		{
			this.sh[k] = new Shader[this.rs[k].sharedMaterials.Length];
			for (int l = 0; l < this.rs[k].sharedMaterials.Length; l++)
			{
				this.sh[k][l] = this.rs[k].sharedMaterials[l].shader;
			}
		}
		this.shInvisible = Shader.Find("Custom/Invisible");
		this.Initialize();
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x000309C1 File Offset: 0x0002EBC1
	public void Initialize()
	{
		if (this.iocCam.enabled)
		{
			this.HideAll();
		}
		else
		{
			base.enabled = false;
			this.ShowLod(1f);
		}
		base.gameObject.layer = this.currentLayer;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x000309FC File Offset: 0x0002EBFC
	private void Update()
	{
		this.frameInterval = Time.frameCount % 4;
		if (this.frameInterval == 0)
		{
			if (!this.LodOnly)
			{
				if (!this.hidden && Time.frameCount - this.counter > this.iocCam.hideDelay)
				{
					switch (this.currentLod)
					{
					case 0:
						this.visible = this.rs0[0].isVisible;
						break;
					case 1:
						this.visible = this.rs1[0].isVisible;
						break;
					case 2:
						this.visible = this.rs2[0].isVisible;
						break;
					default:
						this.visible = this.rs[0].isVisible;
						break;
					}
					if (!((this.iocCam.preCullCheck && this.visible) | this.Occludee) || !this.visible)
					{
						this.Hide();
						return;
					}
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
						if (this.Occludee & this.lods > 0)
						{
							this.ShowLod(this.h.distance);
							return;
						}
					}
				}
			}
			else if (!this.sleeping && Time.frameCount - this.counter > this.iocCam.hideDelay)
			{
				if (!this.Occludee)
				{
					this.ShowLod(3000f);
					this.sleeping = true;
					return;
				}
				base.gameObject.layer = this.currentLayer;
				Vector3 vector = base.transform.localToWorldMatrix.MultiplyPoint(this.hitPoint);
				this.r = new Ray(vector, this.iocCam.transform.position - vector);
				if (Physics.Raycast(this.r, out this.h, this.iocCam.viewDistance) && !this.h.collider.CompareTag(this.iocCam.tag))
				{
					this.ShowLod(3000f);
					this.sleeping = true;
					return;
				}
			}
		}
		else if (this.realtimeShadows && this.frameInterval == 2)
		{
			this.distanceFromCam = Vector3.Distance(base.transform.position, this.iocCam.transform.position);
			if (this.hidden)
			{
				if (this.lods == 0)
				{
					if (this.distanceFromCam > this.shadowDistance)
					{
						if (this.rs[0].enabled)
						{
							for (int i = 0; i < this.rs.Length; i++)
							{
								this.rs[i].enabled = false;
								for (int j = 0; j < this.rs[i].materials.Length; j++)
								{
									this.rs[i].materials[j].shader = this.sh[i][j];
								}
							}
							return;
						}
					}
					else if (!this.rs[0].enabled)
					{
						for (int k = 0; k < this.rs.Length; k++)
						{
							Material[] materials = this.rs[k].materials;
							for (int l = 0; l < materials.Length; l++)
							{
								materials[l].shader = this.shInvisible;
							}
							this.rs[k].enabled = true;
						}
						return;
					}
				}
				else if (this.distanceFromCam > this.shadowDistance)
				{
					if (this.rs0[0].enabled)
					{
						for (int m = 0; m < this.rs0.Length; m++)
						{
							this.rs0[m].enabled = false;
							for (int n = 0; n < this.rs0[m].materials.Length; n++)
							{
								this.rs0[m].materials[n].shader = this.sh0[m][n];
							}
						}
						return;
					}
				}
				else if (!this.rs0[0].enabled)
				{
					for (int num = 0; num < this.rs0.Length; num++)
					{
						Material[] materials = this.rs0[num].materials;
						for (int l = 0; l < materials.Length; l++)
						{
							materials[l].shader = this.shInvisible;
						}
						this.rs0[num].enabled = true;
					}
				}
			}
		}
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00030EC4 File Offset: 0x0002F0C4
	public void UpdateValues()
	{
		if (this.Lod1 != 0f)
		{
			this.lod_1 = this.Lod1;
		}
		else
		{
			this.lod_1 = this.iocCam.lod1Distance;
		}
		if (this.Lod2 != 0f)
		{
			this.lod_2 = this.Lod2;
		}
		else
		{
			this.lod_2 = this.iocCam.lod2Distance;
		}
		if (this.LodMargin != 0f)
		{
			this.lodMargin = this.LodMargin;
		}
		else
		{
			this.lodMargin = this.iocCam.lodMargin;
		}
		this.realtimeShadows = this.iocCam.realtimeShadows;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00030F68 File Offset: 0x0002F168
	public override void UnHide(RaycastHit h)
	{
		this.counter = Time.frameCount;
		this.hitPoint = base.transform.worldToLocalMatrix.MultiplyPoint(h.point);
		if (this.hidden)
		{
			this.hidden = false;
			this.ShowLod(h.distance);
			if (this.Occludee)
			{
				base.gameObject.layer = this.iocCam.occludeeLayer;
				return;
			}
		}
		else if (this.lods > 0)
		{
			this.distOffset = this.prevDist - h.distance;
			this.hitTimeOffset = Time.time - this.prevHitTime;
			this.prevHitTime = Time.time;
			if (Mathf.Abs(this.distOffset) > this.lodMargin | this.hitTimeOffset > 1f)
			{
				this.ShowLod(h.distance);
				this.prevDist = h.distance;
				this.sleeping = false;
				if (this.Occludee)
				{
					base.gameObject.layer = this.iocCam.occludeeLayer;
				}
			}
		}
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00031080 File Offset: 0x0002F280
	public void ShowLod(float d)
	{
		switch (this.lods)
		{
		case 0:
			this.currentLod = -1;
			break;
		case 2:
			if (d < this.lod_1)
			{
				this.currentLod = 0;
			}
			else
			{
				this.currentLod = 1;
			}
			break;
		case 3:
			if (d < this.lod_1)
			{
				this.currentLod = 0;
			}
			else if (d > this.lod_1 & d < this.lod_2)
			{
				this.currentLod = 1;
			}
			else
			{
				this.currentLod = 2;
			}
			break;
		}
		switch (this.currentLod)
		{
		case 0:
			if (!this.LodOnly && this.rs0[0].enabled)
			{
				for (int i = 0; i < this.rs0.Length; i++)
				{
					for (int j = 0; j < this.rs0[i].materials.Length; j++)
					{
						this.rs0[i].materials[j].shader = this.sh0[i][j];
					}
				}
			}
			else
			{
				for (int i = 0; i < this.rs0.Length; i++)
				{
					this.rs0[i].enabled = true;
				}
			}
			for (int i = 0; i < this.rs1.Length; i++)
			{
				this.rs1[i].enabled = false;
			}
			if (this.lods == 3)
			{
				for (int i = 0; i < this.rs2.Length; i++)
				{
					this.rs2[i].enabled = false;
				}
				return;
			}
			break;
		case 1:
			for (int i = 0; i < this.rs1.Length; i++)
			{
				this.rs1[i].enabled = true;
			}
			for (int i = 0; i < this.rs0.Length; i++)
			{
				this.rs0[i].enabled = false;
				if (!this.LodOnly && this.realtimeShadows)
				{
					for (int k = 0; k < this.rs0[i].materials.Length; k++)
					{
						this.rs0[i].materials[k].shader = this.sh0[i][k];
					}
				}
			}
			if (this.lods == 3)
			{
				for (int i = 0; i < this.rs2.Length; i++)
				{
					this.rs2[i].enabled = false;
				}
				return;
			}
			break;
		case 2:
			for (int i = 0; i < this.rs2.Length; i++)
			{
				this.rs2[i].enabled = true;
			}
			for (int i = 0; i < this.rs0.Length; i++)
			{
				this.rs0[i].enabled = false;
				if (!this.LodOnly && this.realtimeShadows)
				{
					for (int l = 0; l < this.rs0[i].materials.Length; l++)
					{
						this.rs0[i].materials[l].shader = this.sh0[i][l];
					}
				}
			}
			for (int i = 0; i < this.rs1.Length; i++)
			{
				this.rs1[i].enabled = false;
			}
			return;
		default:
			if (!this.LodOnly && this.rs[0].enabled)
			{
				for (int i = 0; i < this.rs.Length; i++)
				{
					for (int m = 0; m < this.rs[i].materials.Length; m++)
					{
						this.rs[i].materials[m].shader = this.sh[i][m];
					}
				}
				return;
			}
			for (int i = 0; i < this.rs.Length; i++)
			{
				this.rs[i].enabled = true;
			}
			break;
		}
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000313F0 File Offset: 0x0002F5F0
	public void Hide()
	{
		this.hidden = true;
		switch (this.currentLod)
		{
		case 0:
			if (this.realtimeShadows && this.distanceFromCam <= this.shadowDistance)
			{
				for (int i = 0; i < this.rs0.Length; i++)
				{
					Material[] materials = this.rs0[i].materials;
					for (int j = 0; j < materials.Length; j++)
					{
						materials[j].shader = this.shInvisible;
					}
				}
			}
			else
			{
				for (int i = 0; i < this.rs0.Length; i++)
				{
					this.rs0[i].enabled = false;
				}
			}
			break;
		case 1:
			for (int i = 0; i < this.rs1.Length; i++)
			{
				this.rs1[i].enabled = false;
			}
			break;
		case 2:
			for (int i = 0; i < this.rs2.Length; i++)
			{
				this.rs2[i].enabled = false;
			}
			break;
		default:
			if (this.realtimeShadows && this.distanceFromCam <= this.shadowDistance)
			{
				for (int i = 0; i < this.rs.Length; i++)
				{
					Material[] materials = this.rs[i].materials;
					for (int j = 0; j < materials.Length; j++)
					{
						materials[j].shader = this.shInvisible;
					}
				}
			}
			else
			{
				for (int i = 0; i < this.rs.Length; i++)
				{
					this.rs[i].enabled = false;
				}
			}
			break;
		}
		if (this.Occludee)
		{
			base.gameObject.layer = this.currentLayer;
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0003157C File Offset: 0x0002F77C
	public void HideAll()
	{
		if (!this.LodOnly)
		{
			this.hidden = true;
			if (this.lods == 0 && this.rs != null)
			{
				for (int i = 0; i < this.rs.Length; i++)
				{
					this.rs[i].enabled = false;
					if (this.realtimeShadows)
					{
						for (int j = 0; j < this.rs[i].materials.Length; j++)
						{
							this.rs[i].materials[j].shader = this.sh[i][j];
						}
					}
				}
				return;
			}
			for (int i = 0; i < this.rs0.Length; i++)
			{
				this.rs0[i].enabled = false;
				if (this.realtimeShadows)
				{
					for (int k = 0; k < this.rs0[i].materials.Length; k++)
					{
						this.rs0[i].materials[k].shader = this.sh0[i][k];
					}
				}
			}
			for (int i = 0; i < this.rs1.Length; i++)
			{
				this.rs1[i].enabled = false;
			}
			if (this.rs2 != null)
			{
				for (int i = 0; i < this.rs2.Length; i++)
				{
					this.rs2[i].enabled = false;
				}
				return;
			}
		}
		else
		{
			this.prevHitTime -= 3f;
			this.ShowLod(3000f);
		}
	}

	// Token: 0x0400059F RID: 1439
	public bool Occludee;

	// Token: 0x040005A0 RID: 1440
	public float Lod1;

	// Token: 0x040005A1 RID: 1441
	public float Lod2;

	// Token: 0x040005A2 RID: 1442
	public float LodMargin;

	// Token: 0x040005A3 RID: 1443
	public bool LodOnly;

	// Token: 0x040005A4 RID: 1444
	private int currentLayer;

	// Token: 0x040005A5 RID: 1445
	private Vector3 hitPoint;

	// Token: 0x040005A6 RID: 1446
	private float lod_1;

	// Token: 0x040005A7 RID: 1447
	private float lod_2;

	// Token: 0x040005A8 RID: 1448
	private float lodMargin;

	// Token: 0x040005A9 RID: 1449
	private bool realtimeShadows;

	// Token: 0x040005AA RID: 1450
	private IOCcam iocCam;

	// Token: 0x040005AB RID: 1451
	private int counter;

	// Token: 0x040005AC RID: 1452
	private Renderer[] rs0;

	// Token: 0x040005AD RID: 1453
	private Renderer[] rs1;

	// Token: 0x040005AE RID: 1454
	private Renderer[] rs2;

	// Token: 0x040005AF RID: 1455
	private Renderer[] rs;

	// Token: 0x040005B0 RID: 1456
	private bool hidden;

	// Token: 0x040005B1 RID: 1457
	private int currentLod;

	// Token: 0x040005B2 RID: 1458
	private float prevDist;

	// Token: 0x040005B3 RID: 1459
	private float distOffset;

	// Token: 0x040005B4 RID: 1460
	private int lods;

	// Token: 0x040005B5 RID: 1461
	private float dt;

	// Token: 0x040005B6 RID: 1462
	private float hitTimeOffset;

	// Token: 0x040005B7 RID: 1463
	private float prevHitTime;

	// Token: 0x040005B8 RID: 1464
	private bool sleeping;

	// Token: 0x040005B9 RID: 1465
	private Shader shInvisible;

	// Token: 0x040005BA RID: 1466
	private Shader[][] sh;

	// Token: 0x040005BB RID: 1467
	private Shader[][] sh0;

	// Token: 0x040005BC RID: 1468
	private float distanceFromCam;

	// Token: 0x040005BD RID: 1469
	private float shadowDistance;

	// Token: 0x040005BE RID: 1470
	private int frameInterval;

	// Token: 0x040005BF RID: 1471
	private RaycastHit h;

	// Token: 0x040005C0 RID: 1472
	private Ray r;

	// Token: 0x040005C1 RID: 1473
	private bool visible;

	// Token: 0x040005C2 RID: 1474
	private Vector3 p;
}
