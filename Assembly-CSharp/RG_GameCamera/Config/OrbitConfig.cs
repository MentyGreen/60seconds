using System;
using System.Collections.Generic;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C6 RID: 454
	public class OrbitConfig : Config
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x00053494 File Offset: 0x00051694
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
					"ZoomSpeed",
					new Config.RangeParam
					{
						value = 2f,
						min = 0f,
						max = 10f
					}
				},
				{
					"RotationSpeedX",
					new Config.RangeParam
					{
						value = 8f,
						min = 0f,
						max = 10f
					}
				},
				{
					"RotationSpeedY",
					new Config.RangeParam
					{
						value = 5f,
						min = 0f,
						max = 10f
					}
				},
				{
					"PanSpeed",
					new Config.RangeParam
					{
						value = 1f,
						min = 0f,
						max = 10f
					}
				},
				{
					"RotationYMax",
					new Config.RangeParam
					{
						value = 90f,
						min = 0f,
						max = 90f
					}
				},
				{
					"RotationYMin",
					new Config.RangeParam
					{
						value = -90f,
						min = -90f,
						max = 0f
					}
				},
				{
					"DisablePan",
					new Config.BoolParam
					{
						value = false
					}
				},
				{
					"DisableZoom",
					new Config.BoolParam
					{
						value = false
					}
				},
				{
					"DisableRotation",
					new Config.BoolParam
					{
						value = false
					}
				},
				{
					"TargetInterpolation",
					new Config.RangeParam
					{
						value = 0.5f,
						min = 0f,
						max = 1f
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

		// Token: 0x0600130E RID: 4878 RVA: 0x00053740 File Offset: 0x00051940
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
