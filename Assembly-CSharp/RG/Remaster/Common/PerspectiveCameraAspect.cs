using System;
using Cinemachine;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace RG.Remaster.Common
{
	// Token: 0x0200021F RID: 543
	[ExecuteInEditMode]
	public class PerspectiveCameraAspect : MonoBehaviour
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0005E048 File Offset: 0x0005C248
		private void Update()
		{
			float f = Mathf.Tan(0.5f * this._horizontalFoV * 0.017453292f) * (float)this._screenHeight.Value / (float)this._screenWidth.Value;
			float num = 2f * Mathf.Atan(f) * 57.29578f;
			if (CinemachineBrain.SoloCamera != this._forcedCamera && this._forcedCamera != null)
			{
				CinemachineBrain.SoloCamera = this._forcedCamera;
			}
			CinemachineVirtualCamera cinemachineVirtualCamera = this._cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
			if (cinemachineVirtualCamera != null && cinemachineVirtualCamera.m_Lens.FieldOfView != num)
			{
				cinemachineVirtualCamera.m_Lens.FieldOfView = num;
			}
		}

		// Token: 0x04000E09 RID: 3593
		[SerializeField]
		[FormerlySerializedAs("horizontalFoV")]
		private float _horizontalFoV = 90f;

		// Token: 0x04000E0A RID: 3594
		[SerializeField]
		private GlobalIntVariable _screenHeight;

		// Token: 0x04000E0B RID: 3595
		[SerializeField]
		private GlobalIntVariable _screenWidth;

		// Token: 0x04000E0C RID: 3596
		[SerializeField]
		private CinemachineBrain _cinemachineBrain;

		// Token: 0x04000E0D RID: 3597
		[SerializeField]
		private CinemachineVirtualCamera _forcedCamera;
	}
}
