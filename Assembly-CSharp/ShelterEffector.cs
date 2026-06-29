using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class ShelterEffector : Effector
{
	// Token: 0x06000EB1 RID: 3761 RVA: 0x0003D2BD File Offset: 0x0003B4BD
	private void Awake()
	{
		this.Initialize();
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0003D2C8 File Offset: 0x0003B4C8
	private void Start()
	{
		this._dropParticlesGameObject = (Object.Instantiate(this._effector, this._spawnPoint.position + this._spawnPointOffset, this._spawnPoint.rotation) as GameObject);
		this._dropParticlesSystem = this._dropParticlesGameObject.GetComponent<ParticleSystem>();
		this._dropParticlesGameObject.SetActive(false);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0003D32C File Offset: 0x0003B52C
	public override void Activate()
	{
		if (this._dropParticlesSystem.isPlaying)
		{
			this._dropParticlesSystem.time = 0f;
			this._dropParticlesSystem.Simulate(0f, true, true);
			this._dropParticlesSystem.Play();
			return;
		}
		this._dropParticlesGameObject.SetActive(true);
	}

	// Token: 0x040008E1 RID: 2273
	private GameObject _dropParticlesGameObject;

	// Token: 0x040008E2 RID: 2274
	private ParticleSystem _dropParticlesSystem;
}
