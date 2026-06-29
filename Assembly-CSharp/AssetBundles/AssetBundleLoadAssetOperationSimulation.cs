using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000208 RID: 520
	public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0005B14D File Offset: 0x0005934D
		public AssetBundleLoadAssetOperationSimulation(Object simulatedObject)
		{
			this.m_SimulatedObject = simulatedObject;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0005B15C File Offset: 0x0005935C
		public override T GetAsset<T>()
		{
			return this.m_SimulatedObject as T;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0005B16E File Offset: 0x0005936E
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0005B171 File Offset: 0x00059371
		public override bool IsDone()
		{
			return true;
		}

		// Token: 0x04000D7F RID: 3455
		private Object m_SimulatedObject;
	}
}
