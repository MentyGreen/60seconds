using System;
using RG_GameCamera.Config;
using UnityEngine;

namespace RG_GameCamera.Modes
{
	// Token: 0x0200018F RID: 399
	[RequireComponent(typeof(EmptyConfig))]
	public class EmptyCameraMode : CameraMode
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004A530 File Offset: 0x00048730
		public override Type Type
		{
			get
			{
				return Type.None;
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004A533 File Offset: 0x00048733
		public override void Init()
		{
			base.Init();
			this.UnityCamera.transform.LookAt(this.cameraTarget);
			this.config = base.GetComponent<EmptyConfig>();
		}
	}
}
