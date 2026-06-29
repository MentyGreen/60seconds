using System;
using System.Collections.Generic;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C5 RID: 453
	public class LookAtConfig : Config
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x0005336C File Offset: 0x0005156C
		public override void LoadDefault()
		{
			Dictionary<string, Config.Param> value = new Dictionary<string, Config.Param>
			{
				{
					"FOV",
					new Config.RangeParam
					{
						value = 60f,
						min = 20f,
						max = 100f
					}
				},
				{
					"InterpolateTarget",
					new Config.BoolParam
					{
						value = true
					}
				},
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

		// Token: 0x0600130B RID: 4875 RVA: 0x0005347C File Offset: 0x0005167C
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
