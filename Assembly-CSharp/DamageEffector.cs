using System;
using System.Collections;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class DamageEffector : MonoBehaviour
{
	// Token: 0x14000065 RID: 101
	// (add) Token: 0x06000E4E RID: 3662 RVA: 0x0003B4C8 File Offset: 0x000396C8
	// (remove) Token: 0x06000E4F RID: 3663 RVA: 0x0003B500 File Offset: 0x00039700
	public event DamageHandler OnDamage;

	// Token: 0x06000E50 RID: 3664 RVA: 0x0003B538 File Offset: 0x00039738
	private void Start()
	{
		for (int i = 0; i < this._effector.Length; i++)
		{
			this._effector[i].gameObject.SetActive(false);
			this._effector[i].Stop();
		}
		if (this._useRaycastsForDamageDetection)
		{
			base.StartCoroutine(this.DetectDamage());
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0003B58D File Offset: 0x0003978D
	private IEnumerator DetectDamage()
	{
		yield return new WaitForSeconds(1f);
		do
		{
			yield return new WaitForSeconds(0.5f);
		}
		while (Physics.Raycast(this._target.transform.position + new Vector3(0f, 0.5f, 0f), -this._target.transform.up, 100f, this._layerMask.value));
		this.Activate();
		yield break;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0003B59C File Offset: 0x0003979C
	private void OnTriggerEnter(Collider other)
	{
		if (this._useCollisionsForDamageDetection && other.transform.CompareTag("Player"))
		{
			this.Activate();
			this._useCollisionsForDamageDetection = false;
		}
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0003B5C8 File Offset: 0x000397C8
	private void Activate()
	{
		if (this._achievementToUnlock != null)
		{
			AchievementsSystem.UnlockAchievement(this._achievementToUnlock);
		}
		for (int i = 0; i < this._effector.Length; i++)
		{
			this._effector[i].gameObject.SetActive(true);
			this._effector[i].Play();
		}
		if (!string.IsNullOrEmpty(this._soundName))
		{
			if (this._transformTarget != null)
			{
				AudioManager.PlaySoundAtPoint(this._soundName, this._transformTarget, 1f, 1f, 0f);
			}
			else
			{
				AudioManager.PlaySoundAtPoint(this._soundName, base.transform, 1f, 1f, 0f);
			}
		}
		if (this.OnDamage != null)
		{
			this.OnDamage();
		}
	}

	// Token: 0x0400087F RID: 2175
	[SerializeField]
	private GameObject _target;

	// Token: 0x04000880 RID: 2176
	[SerializeField]
	private ParticleSystem[] _effector;

	// Token: 0x04000881 RID: 2177
	[SerializeField]
	private LayerMask _layerMask;

	// Token: 0x04000882 RID: 2178
	[EventRef]
	[SerializeField]
	private string _soundName = string.Empty;

	// Token: 0x04000883 RID: 2179
	[SerializeField]
	private Achievement _achievementToUnlock;

	// Token: 0x04000884 RID: 2180
	[SerializeField]
	private Transform _transformTarget;

	// Token: 0x04000885 RID: 2181
	[SerializeField]
	private bool _useRaycastsForDamageDetection = true;

	// Token: 0x04000886 RID: 2182
	[SerializeField]
	private bool _useCollisionsForDamageDetection;

	// Token: 0x04000887 RID: 2183
	private const string PLAYER_TAG = "Player";
}
