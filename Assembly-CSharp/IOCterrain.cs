using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class IOCterrain : IOCcomp
{
	// Token: 0x06000B67 RID: 2919 RVA: 0x000316E0 File Offset: 0x0002F8E0
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000316E8 File Offset: 0x0002F8E8
	public override void Init()
	{
		try
		{
			this.iocCam = Camera.main.GetComponent<IOCcam>();
			this.terrain = base.GetComponent<Terrain>();
			base.enabled = true;
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.Log(ex.Message);
		}
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00031740 File Offset: 0x0002F940
	private void Start()
	{
		this.terrain.enabled = false;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0003174E File Offset: 0x0002F94E
	private void Update()
	{
		this.frameInterval = Time.frameCount % 4;
		if (this.frameInterval == 0 && !this.hidden && Time.frameCount - this.counter > this.iocCam.hideDelay)
		{
			this.Hide();
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0003178C File Offset: 0x0002F98C
	public void Hide()
	{
		this.terrain.enabled = false;
		this.hidden = true;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000317A1 File Offset: 0x0002F9A1
	public override void UnHide(RaycastHit hit)
	{
		this.counter = Time.frameCount;
		this.terrain.enabled = true;
		this.hidden = false;
	}

	// Token: 0x040005C3 RID: 1475
	private IOCcam iocCam;

	// Token: 0x040005C4 RID: 1476
	private bool hidden;

	// Token: 0x040005C5 RID: 1477
	private int counter;

	// Token: 0x040005C6 RID: 1478
	private int frameInterval;

	// Token: 0x040005C7 RID: 1479
	private Terrain terrain;
}
