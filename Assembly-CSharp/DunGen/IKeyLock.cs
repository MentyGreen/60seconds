using System;

namespace DunGen
{
	// Token: 0x020001E6 RID: 486
	public interface IKeyLock
	{
		// Token: 0x060013C9 RID: 5065
		void OnKeyAssigned(Key key, KeyManager manager);
	}
}
