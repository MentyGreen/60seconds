using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class Spawner : MonoBehaviour
{
	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0003D388 File Offset: 0x0003B588
	// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0003D390 File Offset: 0x0003B590
	private GameObject SpawnedObject { get; set; }

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0003D399 File Offset: 0x0003B599
	private void Update()
	{
		if (this._autoRemove)
		{
			this._autoRemove = false;
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0003D3B8 File Offset: 0x0003B5B8
	public bool CanSpawn(GameObject template, bool log)
	{
		if (log)
		{
			Debug.LogFormat("!_used: {0} && (template != null: {1} || _template != null {2})", new object[]
			{
				!this._used,
				template != null,
				this._template != null
			});
		}
		return !this._used && (template != null || this._template != null);
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0003D42D File Offset: 0x0003B62D
	public bool Spawn(GameObject template = null)
	{
		return this.SpawnGameObject(template) != null;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0003D43C File Offset: 0x0003B63C
	public GameObject SpawnGameObject(GameObject template = null)
	{
		if (this.CanSpawn(template, false))
		{
			Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			Quaternion quaternion = Quaternion.Euler(new Vector3(eulerAngles.x + Random.Range(0f, this._randomRotation.x), eulerAngles.y + Random.Range(0f, this._randomRotation.y), eulerAngles.z + Random.Range(0f, this._randomRotation.z)));
			GameObject gameObject = Object.Instantiate<GameObject>((template == null) ? this._template : template, base.transform.position, quaternion);
			if (!gameObject)
			{
				Debug.LogFormat("Obj after instantiate is null! template: {0}, _template: {1}, transform.position: {2}, randRot: {3}", new object[]
				{
					template,
					this._template,
					base.transform.position,
					quaternion
				});
			}
			return gameObject;
		}
		this.CanSpawn(template, true);
		return null;
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0003D53A File Offset: 0x0003B73A
	// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0003D542 File Offset: 0x0003B742
	public GameObject Template
	{
		get
		{
			return this._template;
		}
		set
		{
			this._template = value;
		}
	}

	// Token: 0x040008E3 RID: 2275
	[SerializeField]
	private GameObject _template;

	// Token: 0x040008E4 RID: 2276
	[SerializeField]
	private bool _autoRemove;

	// Token: 0x040008E5 RID: 2277
	[SerializeField]
	private Vector3 _customRange = Vector3.zero;

	// Token: 0x040008E6 RID: 2278
	[SerializeField]
	private Vector3 _randomRotation = Vector3.zero;

	// Token: 0x040008E7 RID: 2279
	private bool _used;
}
