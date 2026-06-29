using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C2 RID: 450
	public class DeadConfig : Config
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x00052D2C File Offset: 0x00050F2C
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
					"Distance",
					new Config.RangeParam
					{
						value = 2f,
						min = 0f,
						max = 10f
					}
				},
				{
					"RotationSpeed",
					new Config.RangeParam
					{
						value = 0.5f,
						min = -10f,
						max = 10f
					}
				},
				{
					"Angle",
					new Config.RangeParam
					{
						value = 50f,
						min = 0f,
						max = 80f
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
					"Collision",
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

		// Token: 0x06001302 RID: 4866 RVA: 0x00052EF8 File Offset: 0x000510F8
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
