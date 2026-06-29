using System;
using I2.Loc;
using RG.Parsecs.Loading;
using UnityEngine;

namespace RG.SecondsRemaster.Loading
{
	// Token: 0x020002C0 RID: 704
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New Loading Screen")]
	public class LoadingScreen : Poster
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0006D050 File Offset: 0x0006B250
		public LocalizedString MiddleText
		{
			get
			{
				return this._middleText;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0006D058 File Offset: 0x0006B258
		public Color BackgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0006D060 File Offset: 0x0006B260
		public Color SpikesColor
		{
			get
			{
				return this._spikesColor;
			}
		}

		// Token: 0x040012B5 RID: 4789
		[SerializeField]
		private LocalizedString _middleText;

		// Token: 0x040012B6 RID: 4790
		[SerializeField]
		private Color _backgroundColor;

		// Token: 0x040012B7 RID: 4791
		[SerializeField]
		private Color _spikesColor;
	}
}
