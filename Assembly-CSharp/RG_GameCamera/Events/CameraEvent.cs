using System;
using System.Collections.Generic;
using RG_GameCamera.Config;
using RG_GameCamera.Effects;
using RG_GameCamera.Modes;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Events
{
	// Token: 0x020001AE RID: 430
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(BoxCollider))]
	public class CameraEvent : MonoBehaviour
	{
		// Token: 0x0600127E RID: 4734 RVA: 0x0004F625 File Offset: 0x0004D825
		private void Awake()
		{
			this.tweens = new List<CameraEvent.ITween>();
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004F634 File Offset: 0x0004D834
		private void SmoothParam(string mode, string key, float t0, float t1, float time)
		{
			CameraEvent.FloatTween item = new CameraEvent.FloatTween
			{
				key = key,
				mode = mode,
				t0 = t0,
				t1 = t1,
				time = time,
				timeout = time
			};
			this.tweens.Add(item);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004F680 File Offset: 0x0004D880
		private void SmoothParam(string mode, string key, Vector2 t0, Vector2 t1, float time)
		{
			CameraEvent.Vector2Tween item = new CameraEvent.Vector2Tween
			{
				key = key,
				mode = mode,
				t0 = t0,
				t1 = t1,
				time = time,
				timeout = time
			};
			this.tweens.Add(item);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004F6CC File Offset: 0x0004D8CC
		private void SmoothParam(string mode, string key, Vector3 t0, Vector3 t1, float time)
		{
			CameraEvent.Vector3Tween item = new CameraEvent.Vector3Tween
			{
				key = key,
				mode = mode,
				t0 = t0,
				t1 = t1,
				time = time,
				timeout = time
			};
			this.tweens.Add(item);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004F718 File Offset: 0x0004D918
		private void Update()
		{
			foreach (CameraEvent.ITween tween in this.tweens)
			{
				tween.timeout -= Time.deltaTime;
				float t = 1f - Mathf.Clamp01(tween.timeout / tween.time);
				tween.Interpolate(t);
				if (tween.timeout < 0f)
				{
					this.tweens.Remove(tween);
					break;
				}
			}
			if (this.cameraTrigger != null && this.RestoreOnTimeout)
			{
				this.restorationTimeout -= Time.deltaTime;
				if (this.restorationTimeout < 0f)
				{
					this.Exit(true, this.cameraTrigger);
				}
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004F7F4 File Offset: 0x0004D9F4
		private void OnTriggerEnter(Collider other)
		{
			if (other && other.gameObject)
			{
				if (other.gameObject.GetComponent<CameraTrigger>())
				{
					if (this.cameraTrigger)
					{
						return;
					}
					this.cameraTrigger = other;
					switch (this.Type)
					{
					case EventType.Effect:
						EffectManager.Instance.Create(this.CameraEffect).Play();
						break;
					case EventType.ConfigParam:
					{
						Config configuration = CameraManager.Instance.GetCameraMode().Configuration;
						string currentMode = configuration.GetCurrentMode();
						this.oldParam2 = currentMode;
						if (configuration && !string.IsNullOrEmpty(this.StringParam0))
						{
							this.oldParam0 = this.StringParam0;
							switch (this.ConfigParamValueType)
							{
							case Config.ConfigValue.Bool:
								this.oldParam1 = configuration.GetBool(currentMode, this.StringParam0);
								configuration.SetBool(currentMode, this.StringParam0, this.ConfigParamBool);
								break;
							case Config.ConfigValue.Range:
								this.oldParam1 = configuration.GetFloat(currentMode, this.StringParam0);
								if (this.SmoothFloatParams)
								{
									this.SmoothParam(currentMode, this.StringParam0, (float)this.oldParam1, this.ConfigParamFloat, this.SmoothTimeout);
								}
								else
								{
									configuration.SetFloat(currentMode, this.StringParam0, this.ConfigParamFloat);
								}
								break;
							case Config.ConfigValue.Vector3:
								this.oldParam1 = configuration.GetVector3(currentMode, this.StringParam0);
								if (this.SmoothFloatParams)
								{
									this.SmoothParam(currentMode, this.StringParam0, (Vector3)this.oldParam1, this.ConfigParamVector3, this.SmoothTimeout);
								}
								else
								{
									configuration.SetVector2(currentMode, this.StringParam0, this.ConfigParamVector2);
								}
								break;
							case Config.ConfigValue.Vector2:
								this.oldParam1 = configuration.GetVector2(currentMode, this.StringParam0);
								if (this.SmoothFloatParams)
								{
									this.SmoothParam(currentMode, this.StringParam0, (Vector2)this.oldParam1, this.ConfigParamVector2, this.SmoothTimeout);
								}
								else
								{
									configuration.SetVector2(currentMode, this.StringParam0, this.ConfigParamVector2);
								}
								break;
							case Config.ConfigValue.String:
								this.oldParam1 = configuration.GetString(currentMode, this.StringParam0);
								configuration.SetString(currentMode, this.StringParam0, this.StringParam1);
								break;
							case Config.ConfigValue.Selection:
								this.oldParam1 = configuration.GetSelection(currentMode, this.StringParam0);
								configuration.SetSelection(currentMode, this.StringParam0, this.StringParam1);
								break;
							}
						}
						break;
					}
					case EventType.ConfigMode:
					{
						Config configuration2 = CameraManager.Instance.GetCameraMode().Configuration;
						if (configuration2 && !string.IsNullOrEmpty(this.StringParam0))
						{
							this.oldParam0 = configuration2.GetCurrentMode();
							if ((string)this.oldParam0 != this.StringParam0)
							{
								this.paramChanged = configuration2.SetCameraMode(this.StringParam0);
							}
						}
						break;
					}
					case EventType.LookAt:
						if ((!this.LookAtFrom || this.LookAtFromObject) && (!this.LookAtTo || this.LookAtToObject) && (this.LookAtTo || this.LookAtFrom))
						{
							this.oldParam0 = CameraManager.Instance.GetCameraMode().Type;
							LookAtCameraMode lookAtCameraMode = CameraManager.Instance.SetMode(RG_GameCamera.Modes.Type.LookAt) as LookAtCameraMode;
							if (this.LookAtFrom)
							{
								if (this.LookAtTo)
								{
									lookAtCameraMode.LookAt(this.LookAtFromObject.position, this.LookAtToObject.position, this.SmoothTimeout);
								}
								else
								{
									lookAtCameraMode.LookFrom(this.LookAtFromObject.position, this.SmoothTimeout);
								}
							}
							else
							{
								lookAtCameraMode.LookAt(this.LookAtToObject.position, this.SmoothTimeout);
							}
						}
						break;
					case EventType.CustomMessage:
						if (this.CustomObject && !string.IsNullOrEmpty(this.StringParam0))
						{
							this.CustomObject.SendMessage(this.StringParam0);
						}
						break;
					}
				}
				if (this.RestoreOnTimeout)
				{
					this.restorationTimeout = this.RestoreTimeout;
				}
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		private void Exit(bool onTimeout, Collider other)
		{
			bool flag;
			if (onTimeout)
			{
				flag = this.RestoreOnTimeout;
			}
			else
			{
				flag = (this.RestoreOnExit && this.cameraTrigger == other);
			}
			if (!this.RestoreOnExit && !this.RestoreOnTimeout)
			{
				this.cameraTrigger = null;
			}
			if (flag)
			{
				this.cameraTrigger = null;
				switch (this.Type)
				{
				case EventType.ConfigParam:
				{
					Config configuration = CameraManager.Instance.GetCameraMode().Configuration;
					if (configuration && !string.IsNullOrEmpty((string)this.oldParam0) && this.oldParam1 != null && !string.IsNullOrEmpty((string)this.oldParam2))
					{
						switch (this.ConfigParamValueType)
						{
						case Config.ConfigValue.Bool:
							configuration.SetBool((string)this.oldParam2, (string)this.oldParam0, (bool)this.oldParam1);
							return;
						case Config.ConfigValue.Range:
						{
							float @float = configuration.GetFloat((string)this.oldParam2, (string)this.oldParam0);
							if (this.SmoothFloatParams)
							{
								this.SmoothParam((string)this.oldParam2, (string)this.oldParam0, @float, (float)this.oldParam1, this.SmoothTimeout);
								return;
							}
							configuration.SetFloat((string)this.oldParam2, (string)this.oldParam0, (float)this.oldParam1);
							return;
						}
						case Config.ConfigValue.Vector3:
						{
							Vector3 vector = configuration.GetVector3((string)this.oldParam2, (string)this.oldParam0);
							if (this.SmoothFloatParams)
							{
								this.SmoothParam((string)this.oldParam2, (string)this.oldParam0, vector, (Vector3)this.oldParam1, this.SmoothTimeout);
								return;
							}
							configuration.SetVector3((string)this.oldParam2, (string)this.oldParam0, (Vector3)this.oldParam1);
							return;
						}
						case Config.ConfigValue.Vector2:
						{
							Vector2 vector2 = configuration.GetVector2((string)this.oldParam2, (string)this.oldParam0);
							if (this.SmoothFloatParams)
							{
								this.SmoothParam((string)this.oldParam2, (string)this.oldParam0, vector2, (Vector2)this.oldParam1, this.SmoothTimeout);
								return;
							}
							configuration.SetVector2((string)this.oldParam2, (string)this.oldParam0, (Vector2)this.oldParam1);
							return;
						}
						case Config.ConfigValue.String:
							configuration.SetString((string)this.oldParam2, (string)this.oldParam0, (string)this.oldParam1);
							return;
						case Config.ConfigValue.Selection:
							configuration.SetSelection((string)this.oldParam2, (string)this.oldParam0, (string)this.oldParam1);
							return;
						default:
							return;
						}
					}
					break;
				}
				case EventType.ConfigMode:
					if (this.paramChanged)
					{
						Config configuration2 = CameraManager.Instance.GetCameraMode().Configuration;
						if (configuration2 && !string.IsNullOrEmpty((string)this.oldParam0) && (string)this.oldParam0 != configuration2.GetCurrentMode())
						{
							configuration2.SetCameraMode((string)this.oldParam0);
							return;
						}
					}
					break;
				case EventType.LookAt:
					if (this.oldParam0 is RG_GameCamera.Modes.Type)
					{
						CameraManager.Instance.SetMode((RG_GameCamera.Modes.Type)this.oldParam0);
					}
					break;
				case EventType.CustomMessage:
					if (this.CustomObject && !string.IsNullOrEmpty(this.StringParam1))
					{
						this.CustomObject.SendMessage(this.StringParam1);
						return;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004FFC5 File Offset: 0x0004E1C5
		private void OnTriggerExit(Collider other)
		{
			this.Exit(false, other);
		}

		// Token: 0x04000C04 RID: 3076
		private List<CameraEvent.ITween> tweens;

		// Token: 0x04000C05 RID: 3077
		public EventType Type;

		// Token: 0x04000C06 RID: 3078
		public RG_GameCamera.Modes.Type CameraMode;

		// Token: 0x04000C07 RID: 3079
		public string StringParam0;

		// Token: 0x04000C08 RID: 3080
		public string StringParam1;

		// Token: 0x04000C09 RID: 3081
		public Config.ConfigValue ConfigParamValueType;

		// Token: 0x04000C0A RID: 3082
		public bool ConfigParamBool;

		// Token: 0x04000C0B RID: 3083
		public string ConfigParamString;

		// Token: 0x04000C0C RID: 3084
		public float ConfigParamFloat;

		// Token: 0x04000C0D RID: 3085
		public Vector2 ConfigParamVector2;

		// Token: 0x04000C0E RID: 3086
		public Vector3 ConfigParamVector3;

		// Token: 0x04000C0F RID: 3087
		public RG_GameCamera.Effects.Type CameraEffect;

		// Token: 0x04000C10 RID: 3088
		public GameObject CustomObject;

		// Token: 0x04000C11 RID: 3089
		public bool RestoreOnExit;

		// Token: 0x04000C12 RID: 3090
		public bool SmoothFloatParams;

		// Token: 0x04000C13 RID: 3091
		public float SmoothTimeout;

		// Token: 0x04000C14 RID: 3092
		public bool LookAtFrom;

		// Token: 0x04000C15 RID: 3093
		public bool LookAtTo;

		// Token: 0x04000C16 RID: 3094
		public Transform LookAtFromObject;

		// Token: 0x04000C17 RID: 3095
		public Transform LookAtToObject;

		// Token: 0x04000C18 RID: 3096
		public bool RestoreOnTimeout;

		// Token: 0x04000C19 RID: 3097
		public float RestoreTimeout;

		// Token: 0x04000C1A RID: 3098
		private Collider cameraTrigger;

		// Token: 0x04000C1B RID: 3099
		private object oldParam0;

		// Token: 0x04000C1C RID: 3100
		private object oldParam1;

		// Token: 0x04000C1D RID: 3101
		private object oldParam2;

		// Token: 0x04000C1E RID: 3102
		private float restorationTimeout;

		// Token: 0x04000C1F RID: 3103
		private bool paramChanged;

		// Token: 0x020003E3 RID: 995
		private abstract class ITween
		{
			// Token: 0x06001E7F RID: 7807
			public abstract void Interpolate(float t);

			// Token: 0x04001812 RID: 6162
			public string mode;

			// Token: 0x04001813 RID: 6163
			public string key;

			// Token: 0x04001814 RID: 6164
			public float time;

			// Token: 0x04001815 RID: 6165
			public float timeout;
		}

		// Token: 0x020003E4 RID: 996
		private class FloatTween : CameraEvent.ITween
		{
			// Token: 0x06001E81 RID: 7809 RVA: 0x00080940 File Offset: 0x0007EB40
			public override void Interpolate(float t)
			{
				float inputValue = Interpolation.LerpS(this.t0, this.t1, t);
				CameraManager.Instance.GetCameraMode().Configuration.SetFloat(this.mode, this.key, inputValue);
			}

			// Token: 0x04001816 RID: 6166
			public float t0;

			// Token: 0x04001817 RID: 6167
			public float t1;
		}

		// Token: 0x020003E5 RID: 997
		private class Vector2Tween : CameraEvent.ITween
		{
			// Token: 0x06001E83 RID: 7811 RVA: 0x0008098C File Offset: 0x0007EB8C
			public override void Interpolate(float t)
			{
				Vector2 inputValue = Interpolation.LerpS(this.t0, this.t1, t);
				CameraManager.Instance.GetCameraMode().Configuration.SetVector2(this.mode, this.key, inputValue);
			}

			// Token: 0x04001818 RID: 6168
			public Vector2 t0;

			// Token: 0x04001819 RID: 6169
			public Vector2 t1;
		}

		// Token: 0x020003E6 RID: 998
		private class Vector3Tween : CameraEvent.ITween
		{
			// Token: 0x06001E85 RID: 7813 RVA: 0x000809D8 File Offset: 0x0007EBD8
			public override void Interpolate(float t)
			{
				Vector3 inputValue = Interpolation.LerpS(this.t0, this.t1, t);
				CameraManager.Instance.GetCameraMode().Configuration.SetVector3(this.mode, this.key, inputValue);
			}

			// Token: 0x0400181A RID: 6170
			public Vector3 t0;

			// Token: 0x0400181B RID: 6171
			public Vector3 t1;
		}
	}
}
