using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class Effector : MonoBehaviour
{
	// Token: 0x06000E5B RID: 3675 RVA: 0x0003B76F File Offset: 0x0003996F
	private void Awake()
	{
		this.Initialize();
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0003B777 File Offset: 0x00039977
	protected virtual void Initialize()
	{
		if (this._spawnPoint == null)
		{
			this._spawnPoint = base.transform;
		}
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0003B793 File Offset: 0x00039993
	private void Start()
	{
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0003B795 File Offset: 0x00039995
	public virtual void Activate()
	{
		Object.Instantiate(this._effector, this._spawnPoint.position + this._spawnPointOffset, this._spawnPoint.rotation);
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003B7C4 File Offset: 0x000399C4
	// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0003B7CC File Offset: 0x000399CC
	public Vector3 SpawnPointOffset
	{
		get
		{
			return this._spawnPointOffset;
		}
		set
		{
			this._spawnPointOffset = value;
		}
	}

	// Token: 0x0400088B RID: 2187
	[SerializeField]
	[Tooltip("Particles prefab object to spawn")]
	protected Object _effector;

	// Token: 0x0400088C RID: 2188
	[SerializeField]
	protected Transform _spawnPoint;

	// Token: 0x0400088D RID: 2189
	[SerializeField]
	protected Vector3 _spawnPointOffset = Vector3.zero;
}
