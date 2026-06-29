using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class IOChud : MonoBehaviour
{
	// Token: 0x06000B77 RID: 2935 RVA: 0x0003197B File Offset: 0x0002FB7B
	private void Awake()
	{
		this.Icon = (Texture2D)Resources.Load("Icon");
		this.hud = false;
		this.dirty = false;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x000319A0 File Offset: 0x0002FBA0
	private void Start()
	{
		this.ioc = Camera.main.transform.GetComponent<IOCcam>();
		this.iocActive = this.ioc.enabled;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000319C8 File Offset: 0x0002FBC8
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.I))
		{
			this.iocActive = !this.iocActive;
			this.ToggleIOC();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			this.ToggleHUD();
		}
		if (Input.GetMouseButtonUp(0) && this.dirty)
		{
			this.ToggleIOC();
			this.dirty = false;
		}
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00031A20 File Offset: 0x0002FC20
	private void OnGUI()
	{
		GUI.Label(new Rect(25f, 10f, 360f, 20f), "Press 'i' to toggle InstantOC - Press 'Esc' to toggle HUD");
		if (this.hud)
		{
			GUI.Label(new Rect(25f, 35f, 320f, 20f), "Samples");
			this.ioc.samples = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 55f, 150f, 20f), (float)this.ioc.samples, 10f, 1500f));
			GUI.Label(new Rect(180f, 50f, 50f, 20f), this.ioc.samples.ToString());
			GUI.Label(new Rect(25f, 65f, 320f, 20f), "Hide delay");
			this.ioc.hideDelay = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 85f, 150f, 20f), (float)this.ioc.hideDelay, 10f, 300f));
			GUI.Label(new Rect(180f, 80f, 50f, 20f), this.ioc.hideDelay.ToString());
			GUI.Label(new Rect(25f, 95f, 320f, 20f), "View Distance");
			this.ioc.viewDistance = (float)Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 115f, 150f, 20f), this.ioc.viewDistance, 100f, 3000f));
			GUI.Label(new Rect(180f, 110f, 50f, 20f), this.ioc.viewDistance.ToString());
			GUI.Label(new Rect(25f, 125f, 320f, 20f), "Lod 1");
			this.ioc.lod1Distance = Mathf.Round(GUI.HorizontalSlider(new Rect(25f, 145f, 150f, 20f), this.ioc.lod1Distance, 10f, 300f));
			GUI.Label(new Rect(180f, 140f, 50f, 20f), this.ioc.lod1Distance.ToString());
			GUI.Label(new Rect(25f, 155f, 320f, 20f), "Lod 2");
			this.ioc.lod2Distance = Mathf.Round(GUI.HorizontalSlider(new Rect(25f, 175f, 150f, 20f), this.ioc.lod2Distance, 10f, 600f));
			GUI.Label(new Rect(180f, 170f, 50f, 20f), this.ioc.lod2Distance.ToString());
			GUI.Label(new Rect(25f, 185f, 320f, 20f), "Lod margin");
			this.ioc.lodMargin = Mathf.Round(GUI.HorizontalSlider(new Rect(25f, 205f, 150f, 20f), this.ioc.lodMargin, 1f, 100f));
			GUI.Label(new Rect(180f, 200f, 50f, 20f), this.ioc.lodMargin.ToString());
		}
		if (this.iocActive)
		{
			GUI.Label(new Rect((float)Screen.width - 74f, 10f, 64f, 64f), this.Icon);
		}
		if (GUI.changed)
		{
			this.dirty = true;
		}
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00031E17 File Offset: 0x00030017
	private void ToggleHUD()
	{
		this.hud = !this.hud;
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00031E28 File Offset: 0x00030028
	private void ToggleIOC()
	{
		this.ioc.enabled = this.iocActive;
		foreach (GameObject gameObject in Object.FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			IOClod component = gameObject.GetComponent<IOClod>();
			IOClight component2 = gameObject.GetComponent<IOClight>();
			IOCterrain component3 = gameObject.GetComponent<IOCterrain>();
			if (component != null)
			{
				if (this.iocActive)
				{
					component.UpdateValues();
					component.Initialize();
					component.enabled = true;
				}
				else
				{
					component.enabled = false;
					component.UpdateValues();
					component.Initialize();
				}
			}
			if (component2 != null)
			{
				if (this.iocActive)
				{
					component2.Initialize();
					component2.enabled = true;
				}
				else
				{
					component2.enabled = false;
					component2.GetComponent<Light>().enabled = true;
				}
			}
			if (component3 != null)
			{
				if (this.iocActive)
				{
					component3.GetComponent<Terrain>().enabled = false;
					component3.enabled = true;
				}
				else
				{
					component3.enabled = false;
					component3.GetComponent<Terrain>().enabled = true;
				}
			}
		}
	}

	// Token: 0x040005D2 RID: 1490
	private Texture2D Icon;

	// Token: 0x040005D3 RID: 1491
	private bool iocActive;

	// Token: 0x040005D4 RID: 1492
	private IOCcam ioc;

	// Token: 0x040005D5 RID: 1493
	private bool realtimeShadows;

	// Token: 0x040005D6 RID: 1494
	private bool hud;

	// Token: 0x040005D7 RID: 1495
	private bool dirty;
}
