using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C0 RID: 448
	public class CollisionConfig : Config
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x0005194C File Offset: 0x0004FB4C
		public override void LoadDefault()
		{
			Dictionary<string, Config.Param> value = new Dictionary<string, Config.Param>
			{
				{
					"MinDistance",
					new Config.RangeParam
					{
						value = 0.5f,
						min = 0f,
						max = 10f
					}
				},
				{
					"ReturnSpeed",
					new Config.RangeParam
					{
						value = 0.4f,
						min = 0f,
						max = 1f
					}
				},
				{
					"ClipSpeed",
					new Config.RangeParam
					{
						value = 0.05f,
						min = 0f,
						max = 1f
					}
				},
				{
					"IgnoreCollisionTag",
					new Config.StringParam
					{
						value = "Player"
					}
				},
				{
					"TransparentCollisionTag",
					new Config.StringParam
					{
						value = "CameraTransparent"
					}
				},
				{
					"TargetSphereRadius",
					new Config.RangeParam
					{
						value = 0.5f,
						min = 0f,
						max = 1f
					}
				},
				{
					"RaycastTolerance",
					new Config.RangeParam
					{
						value = 0.5f,
						min = 0f,
						max = 1f
					}
				},
				{
					"TargetClipSpeed",
					new Config.RangeParam
					{
						value = 0.1f,
						min = 0f,
						max = 1f
					}
				},
				{
					"ReturnTargetSpeed",
					new Config.RangeParam
					{
						value = 0.1f,
						min = 0f,
						max = 1f
					}
				},
				{
					"SphereCastRadius",
					new Config.RangeParam
					{
						value = 0.1f,
						min = 0f,
						max = 1f
					}
				},
				{
					"SphereCastTolerance",
					new Config.RangeParam
					{
						value = 0.5f,
						min = 0f,
						max = 1f
					}
				},
				{
					"CollisionAlgorithm",
					new Config.SelectionParam
					{
						index = 0,
						value = new string[]
						{
							"Simple",
							"Spherical",
							"Volumetric"
						}
					}
				},
				{
					"ConeRadius",
					new Config.Vector2Param
					{
						value = new Vector2(0.5f, 1f)
					}
				},
				{
					"ConeSegments",
					new Config.RangeParam
					{
						value = 3f,
						min = 3f,
						max = 10f
					}
				},
				{
					"HeadOffset",
					new Config.RangeParam
					{
						value = 1.6f,
						min = 0f,
						max = 10f
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

		// Token: 0x060012D2 RID: 4818 RVA: 0x00051CC8 File Offset: 0x0004FEC8
		protected override void Awake()
		{
			base.Awake();
			this.LoadDefault();
		}
	}
}
