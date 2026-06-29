using System;
using FMODUnity;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class RandomDropController : MonoBehaviour
{
	// Token: 0x06000E8D RID: 3725 RVA: 0x0003C6CA File Offset: 0x0003A8CA
	private void OnTriggerEnter(Collider collider)
	{
		if (this._shelter != null && this.IsCollisionAllowed(collider.gameObject))
		{
			this._shelter.DropIntoShelter(this._dropSoundName);
		}
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0003C6F9 File Offset: 0x0003A8F9
	private bool IsCollisionAllowed(GameObject collider)
	{
		return !collider.CompareTag("Player");
	}

	// Token: 0x040008BE RID: 2238
	[SerializeField]
	private Shelter _shelter;

	// Token: 0x040008BF RID: 2239
	[EventRef]
	[SerializeField]
	private string _dropSoundName;
}
