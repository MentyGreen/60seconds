using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C9 RID: 457
	public class ThirdPersonConfig : Config
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x00054268 File Offset: 0x00052468
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
					"FollowCoef",
					new Config.RangeParam
					{
						value = 1f,
						min = 0f,
						max = 10f
					}
				},
				{
					"Spring",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DefaultYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = -80f,
						max = 80f
					}
				},
				{
					"DefaultXRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = -180f,
						max = 180f
					}
				},
				{
					"AutoYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DeadZone",
					new Config.Vector2Param
					{
						value = Vector2.zero
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
					"FreeRotate",
					new Config.BoolParam
					{
						value = false
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
					"Distance",
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
					"FollowCoef",
					new Config.RangeParam
					{
						value = 1f,
						min = 0f,
						max = 10f
					}
				},
				{
					"Spring",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DefaultYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = -80f,
						max = 80f
					}
				},
				{
					"AutoYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DeadZone",
					new Config.Vector2Param
					{
						value = Vector2.zero
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
			Dictionary<string, Config.Param> value3 = new Dictionary<string, Config.Param>
			{
				{
					"FOV",
					new Config.RangeParam
					{
						value = 40f,
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
					"FollowCoef",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 10f
					}
				},
				{
					"Spring",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DefaultYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = -80f,
						max = 80f
					}
				},
				{
					"AutoYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DeadZone",
					new Config.Vector2Param
					{
						value = Vector2.zero
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
			Dictionary<string, Config.Param> value4 = new Dictionary<string, Config.Param>
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
					"FollowCoef",
					new Config.RangeParam
					{
						value = 1f,
						min = 0f,
						max = 10f
					}
				},
				{
					"Spring",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DefaultYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = -80f,
						max = 80f
					}
				},
				{
					"AutoYRotation",
					new Config.RangeParam
					{
						value = 0f,
						min = 0f,
						max = 1f
					}
				},
				{
					"DeadZone",
					new Config.Vector2Param
					{
						value = Vector2.zero
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
				},
				{
					"Crouch",
					value2
				},
				{
					"Aim",
					value3
				},
				{
					"Sprint",
					value4
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

		// Token: 0x06001317 RID: 4887 RVA: 0x00054CF0 File Offset: 0x00052EF0
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
