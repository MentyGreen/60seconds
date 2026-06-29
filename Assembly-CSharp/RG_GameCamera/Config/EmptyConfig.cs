using System;
using System.Collections.Generic;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C3 RID: 451
	public class EmptyConfig : Config
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00052F10 File Offset: 0x00051110
		public override void LoadDefault()
		{
			Dictionary<string, Config.Param> value = new Dictionary<string, Config.Param>
			{
				{
					"Orthographic",
					new Config.BoolParam
					{
						value = false
					}
				}
			};
			this.Params = new Dictionary<string, Dictionary<string, Config.Param>>
			{
				{
					"Default",
					value
				}
			};
			this.Transitions = new Dictionary<string, float>();
			foreach (KeyValuePair<string, Dictionary<string, Config.Param>> keyValuePair in this.Params)
			{
				this.Transitions.Add(keyValuePair.Key, 0.25f);
			}
			base.Deserialize(base.DefaultConfigPath);
			base.LoadDefault();
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00052FD0 File Offset: 0x000511D0
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
