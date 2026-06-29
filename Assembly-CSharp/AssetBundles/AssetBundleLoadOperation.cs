using System;
using System.Collections;

namespace AssetBundles
{
	// Token: 0x02000205 RID: 517
	public abstract class AssetBundleLoadOperation : IEnumerator
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005B081 File Offset: 0x00059281
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005B084 File Offset: 0x00059284
		public bool MoveNext()
		{
			return !this.IsDone();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005B08F File Offset: 0x0005928F
		public void Reset()
		{
		}

		// Token: 0x0600146A RID: 5226
		public abstract bool Update();

		// Token: 0x0600146B RID: 5227
		public abstract bool IsDone();
	}
}
