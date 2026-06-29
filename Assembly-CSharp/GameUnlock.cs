using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000111 RID: 273
[Serializable]
public class GameUnlock : ScriptableObject
{
	// Token: 0x06000D4C RID: 3404 RVA: 0x000377BB File Offset: 0x000359BB
	public void Unlock(bool unlock)
	{
		this._unlocked = unlock;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x000377C4 File Offset: 0x000359C4
	public string GetParameter(string key)
	{
		string result = null;
		for (int i = 0; i < this._parameters.Count; i++)
		{
			if (this._parameters[i].Id == key)
			{
				return this._parameters[i].Val;
			}
		}
		return result;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00037818 File Offset: 0x00035A18
	public bool GetParameterBool(string key, ref bool val)
	{
		string parameter = this.GetParameter(key);
		if (!string.IsNullOrEmpty(parameter))
		{
			val = bool.Parse(parameter);
			return true;
		}
		return false;
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00037840 File Offset: 0x00035A40
	public bool GetParameterInt(string key, ref int val)
	{
		if (!string.IsNullOrEmpty(this.GetParameter(key)))
		{
			val = int.Parse(key);
			return true;
		}
		return false;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0003785C File Offset: 0x00035A5C
	public bool IsTarget(string target)
	{
		if (this._targets.Count > 0)
		{
			for (int i = 0; i < this._targets.Count; i++)
			{
				if (this._targets[i] == target)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x000378A4 File Offset: 0x00035AA4
	public string GetTarget()
	{
		if (this._targets.Count > 0)
		{
			return this._targets[0];
		}
		return null;
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000378C2 File Offset: 0x00035AC2
	public string[] GetTargets()
	{
		if (this._targets.Count > 0)
		{
			return this._targets.ToArray();
		}
		return null;
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000D53 RID: 3411 RVA: 0x000378DF File Offset: 0x00035ADF
	public string Id
	{
		get
		{
			return this._id;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000378E7 File Offset: 0x00035AE7
	public EGameUnlockType Type
	{
		get
		{
			return this._type;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000378EF File Offset: 0x00035AEF
	public string Icon
	{
		get
		{
			return this._icon;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000378F7 File Offset: 0x00035AF7
	public string Name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000D57 RID: 3415 RVA: 0x000378FF File Offset: 0x00035AFF
	public string Description
	{
		get
		{
			return this._descr;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00037907 File Offset: 0x00035B07
	public List<SKeyValuePair> Values
	{
		get
		{
			return this._parameters;
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003790F File Offset: 0x00035B0F
	public bool Unlocked
	{
		get
		{
			return this._unlocked;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00037917 File Offset: 0x00035B17
	public List<string> Targets
	{
		get
		{
			return this._targets;
		}
	}

	// Token: 0x04000743 RID: 1859
	[SerializeField]
	private string _id = string.Empty;

	// Token: 0x04000744 RID: 1860
	[SerializeField]
	private EGameUnlockType _type;

	// Token: 0x04000745 RID: 1861
	[SerializeField]
	private string _name = string.Empty;

	// Token: 0x04000746 RID: 1862
	[SerializeField]
	private string _descr = string.Empty;

	// Token: 0x04000747 RID: 1863
	[SerializeField]
	private List<string> _targets = new List<string>();

	// Token: 0x04000748 RID: 1864
	[SerializeField]
	private string _icon = string.Empty;

	// Token: 0x04000749 RID: 1865
	[SerializeField]
	private List<SKeyValuePair> _parameters = new List<SKeyValuePair>();

	// Token: 0x0400074A RID: 1866
	private bool _unlocked;
}
