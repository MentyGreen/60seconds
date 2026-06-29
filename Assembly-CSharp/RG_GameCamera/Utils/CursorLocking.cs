using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000180 RID: 384
	public class CursorLocking : MonoBehaviour
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x00047F65 File Offset: 0x00046165
		private void Awake()
		{
			CursorLocking.instance = this;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00047F70 File Offset: 0x00046170
		private void Update()
		{
			if (this.LockCursor)
			{
				CursorLocking.Lock();
			}
			else
			{
				CursorLocking.Unlock();
			}
			CursorLocking.IsLocked = Screen.lockCursor;
			if (Input.GetKeyDown(this.LockKey))
			{
				CursorLocking.Lock();
			}
			if (Input.GetKeyDown(this.UnlockKey))
			{
				CursorLocking.Unlock();
			}
			if (!Screen.lockCursor)
			{
				Cursor.visible = true;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00047FCC File Offset: 0x000461CC
		public static void Lock()
		{
			Screen.lockCursor = true;
			Cursor.visible = false;
			CursorLocking.instance.LockCursor = true;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00047FE5 File Offset: 0x000461E5
		public static void Unlock()
		{
			Screen.lockCursor = false;
			Cursor.visible = true;
			CursorLocking.instance.LockCursor = false;
		}

		// Token: 0x04000B18 RID: 2840
		public bool LockCursor;

		// Token: 0x04000B19 RID: 2841
		public KeyCode LockKey;

		// Token: 0x04000B1A RID: 2842
		public KeyCode UnlockKey;

		// Token: 0x04000B1B RID: 2843
		public static bool IsLocked;

		// Token: 0x04000B1C RID: 2844
		private static CursorLocking instance;
	}
}
