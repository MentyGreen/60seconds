using System;
using RG_GameCamera.Modes;
using UnityEngine;

namespace RG_GameCamera.Examples
{
	// Token: 0x020001AD RID: 429
	public class SpawnDespawn : MonoBehaviour
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0004F520 File Offset: 0x0004D720
		private void Start()
		{
			this.spawned = true;
			this.cameraManager = CameraManager.Instance;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004F534 File Offset: 0x0004D734
		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 100f, 300f, 30f), this.spawned ? "Despawn" : "Spawn"))
			{
				this.spawned = !this.spawned;
				if (this.spawned)
				{
					this.Spawn();
					return;
				}
				this.Despawn();
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004F59C File Offset: 0x0004D79C
		private void Spawn()
		{
			this.CharacterControllerCurrent = Object.Instantiate<GameObject>(this.CharacterControllerPrefab, this.lastPos, Quaternion.identity);
			this.cameraManager.SetCameraTarget(this.CharacterControllerCurrent.transform);
			this.cameraManager.SetMode(Type.ThirdPerson);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		private void Despawn()
		{
			this.lastPos = this.CharacterControllerCurrent.transform.position;
			Object.Destroy(this.CharacterControllerCurrent.gameObject);
			this.cameraManager.SetMode(Type.None);
		}

		// Token: 0x04000BFF RID: 3071
		public GameObject CharacterControllerPrefab;

		// Token: 0x04000C00 RID: 3072
		public GameObject CharacterControllerCurrent;

		// Token: 0x04000C01 RID: 3073
		private CameraManager cameraManager;

		// Token: 0x04000C02 RID: 3074
		private Vector3 lastPos;

		// Token: 0x04000C03 RID: 3075
		private bool spawned;
	}
}
