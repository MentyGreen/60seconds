using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C4 RID: 452
	public class FPSConfig : Config
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x00052FE8 File Offset: 0x000511E8
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
					"RotationYMax",
					new Config.RangeParam
					{
						value = 80f,
						min = 0f,
						max = 80f
					}
				},
				{
					"RotationYMin",
					new Config.RangeParam
					{
						value = -80f,
						min = -80f,
						max = 0f
					}
				},
				{
					"TargetOffset",
					new Config.Vector3Param
					{
						value = Vector3.zero
					}
				},
				{
					"Orthographic",
					new Config.BoolParam
					{
						value = false
					}
				},
				{
					"HideTarget",
					new Config.BoolParam
					{
						value = true
					}
				}
			};
			Dictionary<string, Config.Param> value2 = new Dictionary<string, Config.Param>
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
					"RotationYMax",
					new Config.RangeParam
					{
						value = 80f,
						min = 0f,
						max = 80f
					}
				},
				{
					"RotationYMin",
					new Config.RangeParam
					{
						value = -80f,
						min = -80f,
						max = 0f
					}
				},
				{
					"TargetOffset",
					new Config.Vector3Param
					{
						value = Vector3.zero
					}
				},
				{
					"Orthographic",
					new Config.BoolParam
					{
						value = false
					}
				},
				{
					"HideTarget",
					new Config.BoolParam
					{
						value = true
					}
				}
			};
			this.Params = new Dictionary<string, Dictionary<string, Config.Param>>
			{
				{
					"Default",
					value
				},
				{
					"Crouch",
					value2
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

		// Token: 0x06001308 RID: 4872 RVA: 0x00053354 File Offset: 0x00051554
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
