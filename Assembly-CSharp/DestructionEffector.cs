using System;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class DestructionEffector : Effector
{
	// Token: 0x06000E55 RID: 3669 RVA: 0x0003B6AB File Offset: 0x000398AB
	private void Awake()
	{
		base.Initialize();
		this._currentHitpoints = this._startHitpoints;
		base.SpawnPointOffset = new Vector3(0f, 1f, 0f);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0003B6D9 File Offset: 0x000398D9
	private void Start()
	{
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0003B6DB File Offset: 0x000398DB
	private void Update()
	{
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0003B6DD File Offset: 0x000398DD
	public void Hit(int points = 10)
	{
		this._currentHitpoints -= points;
		this._currentHitpoints = Mathf.Clamp(this._currentHitpoints, 0, this._startHitpoints);
		if (this._currentHitpoints <= 0)
		{
			this.Activate();
		}
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x0003B714 File Offset: 0x00039914
	public override void Activate()
	{
		base.Activate();
		if (!string.IsNullOrEmpty(this._soundName))
		{
			AudioManager.PlaySoundAtPoint(this._soundName, base.transform, 1f, 1f, 0f);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000888 RID: 2184
	[EventRef]
	[SerializeField]
	private string _soundName = string.Empty;

	// Token: 0x04000889 RID: 2185
	[SerializeField]
	private int _startHitpoints = 100;

	// Token: 0x0400088A RID: 2186
	private int _currentHitpoints;
}
