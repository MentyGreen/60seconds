using System;
using RG.SecondsRemaster;
using UnityEngine;

// Token: 0x02000116 RID: 278
[Serializable]
public class ControlMode
{
	// Token: 0x06000D92 RID: 3474 RVA: 0x000387E9 File Offset: 0x000369E9
	public bool IsGamepad()
	{
		return this._scavengeControl == EPlayerInput.GAMEPAD;
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000387F4 File Offset: 0x000369F4
	public string Name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000387FC File Offset: 0x000369FC
	public string Key
	{
		get
		{
			return this._key;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00038804 File Offset: 0x00036A04
	public bool Enabled
	{
		get
		{
			return this._enabled;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003880C File Offset: 0x00036A0C
	public string Icon
	{
		get
		{
			return this._icon;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00038814 File Offset: 0x00036A14
	public EPlayerInput ScavengeControl
	{
		get
		{
			return this._scavengeControl;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003881C File Offset: 0x00036A1C
	// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00038824 File Offset: 0x00036A24
	public float RotationSensitivity
	{
		get
		{
			return this._rotationSensitivity;
		}
		set
		{
			this._rotationSensitivity = value;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0003882D File Offset: 0x00036A2D
	public bool Mobile
	{
		get
		{
			return this._mobile;
		}
	}

	// Token: 0x04000759 RID: 1881
	[SerializeField]
	private bool _enabled = true;

	// Token: 0x0400075A RID: 1882
	[SerializeField]
	private string _name = string.Empty;

	// Token: 0x0400075B RID: 1883
	[SerializeField]
	private string _key = string.Empty;

	// Token: 0x0400075C RID: 1884
	[SerializeField]
	private bool _mobile;

	// Token: 0x0400075D RID: 1885
	[SerializeField]
	private bool _desktop = true;

	// Token: 0x0400075E RID: 1886
	[SerializeField]
	private string _icon = string.Empty;

	// Token: 0x0400075F RID: 1887
	[SerializeField]
	private EPlayerInput _scavengeControl;

	// Token: 0x04000760 RID: 1888
	[SerializeField]
	private float _rotationSensitivity = 1f;
}
