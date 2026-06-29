using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class TVAnnouncer : Announcer
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x0003D569 File Offset: 0x0003B769
	private void Awake()
	{
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0003D56B File Offset: 0x0003B76B
	public void TurnOn()
	{
		if (this._displayMaterials.Length != 0)
		{
			this._turnedOn = true;
			if (this._displayMaterials.Length > 1)
			{
				base.StartCoroutine(this.Display());
				return;
			}
			this._tvRenderer.material = this._displayMaterials[0];
		}
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0003D5A9 File Offset: 0x0003B7A9
	private IEnumerator Display()
	{
		while (this._turnedOn)
		{
			this._tvRenderer.material = this._displayMaterials[this._curMaterialIndex];
			this._curMaterialIndex++;
			yield return new WaitForSeconds(this._displayTimeout);
		}
		yield break;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0003D5B8 File Offset: 0x0003B7B8
	public void TurnOff()
	{
		this._tvRenderer.material = this._offMaterial;
		this._turnedOn = false;
		this._curMaterialIndex = 0;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0003D5D9 File Offset: 0x0003B7D9
	protected override void OnActivation(string customItem)
	{
		base.OnActivation(customItem);
		this.TurnOn();
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
	protected override void OnDeactivation()
	{
		base.OnDeactivation();
		this.TurnOff();
	}

	// Token: 0x040008E9 RID: 2281
	[SerializeField]
	private MeshRenderer _tvRenderer;

	// Token: 0x040008EA RID: 2282
	[SerializeField]
	private Material[] _displayMaterials;

	// Token: 0x040008EB RID: 2283
	[SerializeField]
	private Material _offMaterial;

	// Token: 0x040008EC RID: 2284
	[SerializeField]
	private float _displayTimeout;

	// Token: 0x040008ED RID: 2285
	private int _curMaterialIndex;

	// Token: 0x040008EE RID: 2286
	private bool _turnedOn;
}
