using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class Announcer : MonoBehaviour
{
	// Token: 0x06000E3A RID: 3642 RVA: 0x0003B076 File Offset: 0x00039276
	private void Awake()
	{
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0003B078 File Offset: 0x00039278
	private void Start()
	{
		DamageEffector component = base.GetComponent<DamageEffector>();
		if (component != null)
		{
			component.OnDamage += this.Terminate;
		}
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0003B0A7 File Offset: 0x000392A7
	private void Update()
	{
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0003B0AC File Offset: 0x000392AC
	private void SelectItemToAnnounce(string customItem)
	{
		GameFlow component = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameFlow>();
		if (customItem != null)
		{
			for (int i = 0; i < component.SpecialLevelItems.Length; i++)
			{
				ScavengeItemController component2 = component.SpecialLevelItems[i].GetComponent<ScavengeItemController>();
				if (component2.SurvivalName == customItem)
				{
					this._announcedScavengeItemController = component2;
					component.ReportSpecialItem(component2);
					return;
				}
			}
			return;
		}
		while (this._announcedScavengeItemController == null)
		{
			ScavengeItemController scavengeItemController = null;
			while (scavengeItemController == null || (scavengeItemController != null && !scavengeItemController.gameObject.activeSelf))
			{
				scavengeItemController = component.SpecialLevelItems[Random.Range(0, component.SpecialLevelItems.Length)].GetComponent<ScavengeItemController>();
			}
			if (component.ReportSpecialItem(scavengeItemController))
			{
				this._announcedScavengeItemController = scavengeItemController;
			}
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0003B16B File Offset: 0x0003936B
	private IEnumerator Run()
	{
		this._running = true;
		yield return new WaitForSeconds(this._timeout);
		if (!this._terminated)
		{
			base.StartCoroutine(this.Cleanup());
		}
		yield break;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0003B17A File Offset: 0x0003937A
	private IEnumerator Cleanup()
	{
		this._terminated = true;
		if (this._running)
		{
			this._running = false;
			if (this._hideTween != null)
			{
				this._hideTween.Play();
			}
			yield return new WaitForSeconds(0.25f);
			this.Activate(false, null);
		}
		yield break;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0003B189 File Offset: 0x00039389
	public void Activate(bool activate, string customItem)
	{
		if (activate)
		{
			this.OnActivation(customItem);
			return;
		}
		this.OnDeactivation();
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0003B19C File Offset: 0x0003939C
	protected virtual void OnActivation(string customITem)
	{
		this.SelectItemToAnnounce(customITem);
		if (this._annoucerFollowerGUI == null)
		{
			GameObject gameObject = GameObject.Find("InGame");
			dfControl dfControl = (gameObject == null) ? null : gameObject.GetComponent<dfControl>();
			if (dfControl != null)
			{
				dfControl dfControl2 = dfControl.GetComponent<dfControl>().AddPrefab(this._guiTemplate);
				this._annoucerFollowerGUI = dfControl2.GetComponent<dfFollowObject>();
				dfTweenPlayableBase[] components = dfControl2.GetComponents<dfTweenPlayableBase>();
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].TweenName == "PopOut")
					{
						this._hideTween = components[i];
						break;
					}
				}
				dfDataObjectProxy component = dfControl2.GetComponent<dfDataObjectProxy>();
				if (component != null)
				{
					component.Data = this;
				}
				dfPivotPoint pivot = dfPivotPoint.MiddleCenter;
				dfPivotPoint anchor = dfPivotPoint.MiddleCenter;
				ResolutionHandler resolutionHandler = Object.FindObjectOfType<ResolutionHandler>();
				if (resolutionHandler != null)
				{
					if (ResolutionHandler.Is169(resolutionHandler.SelectedAspectRatio.AspectRatio))
					{
						pivot = dfPivotPoint.TopLeft;
						anchor = dfPivotPoint.TopLeft;
						this._annoucerFollowerGUI.offset = new Vector3(0.5f, 0.5f, 0.5f);
					}
					else if (ResolutionHandler.Is1610(resolutionHandler.SelectedAspectRatio.AspectRatio))
					{
						pivot = dfPivotPoint.TopLeft;
						anchor = dfPivotPoint.TopLeft;
						this._annoucerFollowerGUI.offset = new Vector3(0.5f, 1f, 1f);
					}
					else if (ResolutionHandler.Is43(resolutionHandler.SelectedAspectRatio.AspectRatio))
					{
						pivot = dfPivotPoint.TopRight;
						anchor = dfPivotPoint.TopRight;
						this._annoucerFollowerGUI.offset = new Vector3(0f, 1f, 0f);
					}
					else if (ResolutionHandler.Is54(resolutionHandler.SelectedAspectRatio.AspectRatio))
					{
						pivot = dfPivotPoint.TopRight;
						anchor = dfPivotPoint.TopRight;
						this._annoucerFollowerGUI.offset = new Vector3(0f, 1f, 0f);
					}
				}
				dfControl.Pivot = pivot;
				this._annoucerFollowerGUI.anchor = anchor;
				this._annoucerFollowerGUI.gameObject.SetActive(true);
				this._annoucerFollowerGUI.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
				this._annoucerFollowerGUI.attach = base.gameObject;
				this._annoucerFollowerGUI.enabled = true;
			}
		}
		if (!string.IsNullOrEmpty(this._soundName))
		{
			this._audio = AudioManager.PlaySoundAndReturnInstance(this._soundName, 1f, 1f, 0f);
		}
		base.StartCoroutine(this.Run());
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0003B408 File Offset: 0x00039608
	protected virtual void OnDeactivation()
	{
		if (this._annoucerFollowerGUI != null && this._annoucerFollowerGUI.gameObject != null)
		{
			this._annoucerFollowerGUI.gameObject.SetActive(false);
			this._annoucerFollowerGUI.GetComponent<dfControl>().IsVisible = false;
			this._annoucerFollowerGUI.enabled = false;
		}
		if (this._audio.isValid())
		{
			AudioManager.StopSound(this._audio, FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0003B47D File Offset: 0x0003967D
	public void Terminate()
	{
		base.StartCoroutine(this.Cleanup());
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003B48C File Offset: 0x0003968C
	public ScavengeItemController AnnouncedScavengeItemController
	{
		get
		{
			return this._announcedScavengeItemController;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003B494 File Offset: 0x00039694
	public bool Terminated
	{
		get
		{
			return this._terminated;
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003B49C File Offset: 0x0003969C
	public bool Running
	{
		get
		{
			return this._running;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0003B4A4 File Offset: 0x000396A4
	// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0003B4AC File Offset: 0x000396AC
	public float Timeout
	{
		get
		{
			return this._timeout;
		}
		set
		{
			this._timeout = value;
		}
	}

	// Token: 0x04000875 RID: 2165
	[SerializeField]
	private GameObject _guiTemplate;

	// Token: 0x04000876 RID: 2166
	[SerializeField]
	private float _timeout;

	// Token: 0x04000877 RID: 2167
	[EventRef]
	[SerializeField]
	private string _soundName = string.Empty;

	// Token: 0x04000878 RID: 2168
	private EventInstance _audio;

	// Token: 0x04000879 RID: 2169
	private ScavengeItemController _announcedScavengeItemController;

	// Token: 0x0400087A RID: 2170
	private dfFollowObject _annoucerFollowerGUI;

	// Token: 0x0400087B RID: 2171
	private dfTweenPlayableBase _hideTween;

	// Token: 0x0400087C RID: 2172
	private bool _terminated;

	// Token: 0x0400087D RID: 2173
	private bool _running;
}
