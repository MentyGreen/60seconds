using System;

namespace RG.SecondsRemaster.EventEditor
{
	// Token: 0x0200034A RID: 842
	[Serializable]
	internal struct CurrentSoundToPlayWrapper
	{
		// Token: 0x040015A3 RID: 5539
		public string EventName;

		// Token: 0x040015A4 RID: 5540
		public int EventPriority;

		// Token: 0x040015A5 RID: 5541
		public float Volume;

		// Token: 0x040015A6 RID: 5542
		public float Pan;

		// Token: 0x040015A7 RID: 5543
		public float Pitch;

		// Token: 0x040015A8 RID: 5544
		public int Offset;

		// Token: 0x040015A9 RID: 5545
		public bool OffsetCheck;
	}
}
