using System;
using System.Collections.Generic;
using RG_GameCamera.ThirdParty;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Config
{
	// Token: 0x020001C1 RID: 449
	public abstract class Config : MonoBehaviour
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x00051CE0 File Offset: 0x0004FEE0
		public virtual void LoadDefault()
		{
			this.currentMode = "Default";
			this.CopyParams(this.Params[this.currentMode], ref this.currParams);
			this.CopyParams(this.Params[this.currentMode], ref this.oldParams);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00051D32 File Offset: 0x0004FF32
		public string DefaultConfigPath
		{
			get
			{
				return this.ResourceDir + base.GetType().Name + ".json";
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00051D4F File Offset: 0x0004FF4F
		public string ResourceDir
		{
			get
			{
				return Application.dataPath + "/External/GameCamera/Resources/Config/";
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00051D60 File Offset: 0x0004FF60
		public string ResourceDirRel
		{
			get
			{
				return "Config/";
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00051D67 File Offset: 0x0004FF67
		protected virtual void Awake()
		{
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00051D6C File Offset: 0x0004FF6C
		public bool SetCameraMode(string mode)
		{
			if (this.Params.ContainsKey(mode) && mode != this.currentMode)
			{
				if (this.TransitionStartCallback != null)
				{
					this.TransitionStartCallback(this.currentMode, mode);
				}
				this.currentMode = mode;
				this.transitionTime = 0f;
				this.CopyParams(this.currParams, ref this.oldParams);
				return true;
			}
			return false;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00051DD6 File Offset: 0x0004FFD6
		public string GetCurrentMode()
		{
			return this.currentMode;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00051DE0 File Offset: 0x0004FFE0
		private float GetTransitionTime(string key)
		{
			float num = this.transitionTime / this.Transitions[key];
			if (num > 1f)
			{
				num = 1f;
			}
			return num;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00051E10 File Offset: 0x00050010
		private void Update()
		{
			this.transitionTime += Time.deltaTime;
			Dictionary<string, Config.Param> dictionary = this.Params[this.currentMode];
			float num = this.GetTransitionTime(this.currentMode);
			if (num > 0f && num < 1f && this.TransitCallback != null)
			{
				this.TransitCallback(this.currentMode, num);
			}
			foreach (Dictionary<string, Config.Param> dictionary2 in this.Params.Values)
			{
				using (Dictionary<string, Config.Param>.KeyCollection.Enumerator enumerator2 = dictionary2.Keys.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string key = enumerator2.Current;
						this.currParams[key].Interpolate(this.oldParams[key], dictionary[key], num);
					}
					break;
				}
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00051F20 File Offset: 0x00050120
		private void CopyParams(Dictionary<string, Config.Param> src, ref Dictionary<string, Config.Param> dst)
		{
			foreach (KeyValuePair<string, Config.Param> keyValuePair in src)
			{
				Config.Param param;
				if (dst.TryGetValue(keyValuePair.Key, out param))
				{
					param.Set(keyValuePair.Value);
				}
				else
				{
					dst.Add(keyValuePair.Key, keyValuePair.Value.Clone());
				}
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00051FA4 File Offset: 0x000501A4
		public bool GetBool(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			return this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param) && ((Config.BoolParam)param).value;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00051FD9 File Offset: 0x000501D9
		public bool GetBool(string key)
		{
			return this.GetBool(this.currentMode, key);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00051FE8 File Offset: 0x000501E8
		public bool IsBool(string key)
		{
			return this.Params.ContainsKey(this.currentMode) && this.Params[this.currentMode].ContainsKey(key);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00052018 File Offset: 0x00050218
		public void SetBool(string mode, string key, bool inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.BoolParam boolParam = (Config.BoolParam)param;
				boolParam.value = inputValue;
				dictionary[key] = boolParam;
				return;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00052060 File Offset: 0x00050260
		public float GetFloat(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.RangeParam)param).value;
			}
			return -1f;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00052099 File Offset: 0x00050299
		public float GetFloat(string key)
		{
			return ((Config.RangeParam)this.currParams[key]).value;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000520B4 File Offset: 0x000502B4
		public float GetFloatMin(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.RangeParam)param).min;
			}
			return -1f;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000520ED File Offset: 0x000502ED
		public float GetFloatMin(string key)
		{
			return this.GetFloatMin(this.currentMode, key);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000520FC File Offset: 0x000502FC
		public float GetFloatMax(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.RangeParam)param).max;
			}
			return -1f;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00052135 File Offset: 0x00050335
		public float GetFloatMax(string key)
		{
			return this.GetFloatMax(this.currentMode, key);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00052144 File Offset: 0x00050344
		public void SetFloat(string mode, string key, float inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.RangeParam rangeParam = (Config.RangeParam)param;
				rangeParam.value = inputValue;
				dictionary[key] = rangeParam;
				return;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00052184 File Offset: 0x00050384
		public Vector3 GetVector3(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.Vector3Param)param).value;
			}
			return Vector3.zero;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000521BD File Offset: 0x000503BD
		public Vector3 GetVector3(string key)
		{
			return ((Config.Vector3Param)this.currParams[key]).value;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000521D5 File Offset: 0x000503D5
		public bool IsVector3(string key)
		{
			return this.currParams != null && this.currParams.ContainsKey(key);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000521F0 File Offset: 0x000503F0
		public Vector3 GetVector3Direct(string key)
		{
			return ((Config.Vector3Param)this.Params[this.currentMode][key]).value;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00052214 File Offset: 0x00050414
		public void SetVector3(string mode, string key, Vector3 inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.Vector3Param vector3Param = (Config.Vector3Param)param;
				vector3Param.value = inputValue;
				dictionary[key] = vector3Param;
				return;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0005225C File Offset: 0x0005045C
		public Vector2 GetVector2(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.Vector2Param)param).value;
			}
			return Vector2.zero;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00052295 File Offset: 0x00050495
		public Vector2 GetVector2(string key)
		{
			return ((Config.Vector2Param)this.currParams[key]).value;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000522B0 File Offset: 0x000504B0
		public void SetVector2(string mode, string key, Vector2 inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.Vector2Param vector2Param = (Config.Vector2Param)param;
				vector2Param.value = inputValue;
				dictionary[key] = vector2Param;
				return;
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000522F8 File Offset: 0x000504F8
		public string GetString(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				return ((Config.StringParam)param).value;
			}
			return null;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0005232D File Offset: 0x0005052D
		public string GetString(string key)
		{
			return this.GetString(this.currentMode, key);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0005233C File Offset: 0x0005053C
		public void SetString(string mode, string key, string inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.StringParam stringParam = (Config.StringParam)param;
				stringParam.value = inputValue;
				dictionary[key] = stringParam;
				return;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00052384 File Offset: 0x00050584
		public string GetSelection(string mode, string key)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.SelectionParam selectionParam = (Config.SelectionParam)param;
				return selectionParam.value[selectionParam.index];
			}
			return null;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000523C2 File Offset: 0x000505C2
		public string GetSelection(string key)
		{
			return this.GetSelection(this.currentMode, key);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000523D4 File Offset: 0x000505D4
		public void SetSelection(string mode, string key, int inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.SelectionParam selectionParam = (Config.SelectionParam)param;
				selectionParam.index = inputValue;
				dictionary[key] = selectionParam;
				return;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0005241C File Offset: 0x0005061C
		public void SetSelection(string mode, string key, string inputValue)
		{
			Dictionary<string, Config.Param> dictionary;
			Config.Param param;
			if (this.Params.TryGetValue(mode, out dictionary) && dictionary.TryGetValue(key, out param))
			{
				Config.SelectionParam selectionParam = (Config.SelectionParam)param;
				int num = selectionParam.Find(inputValue);
				if (num != -1)
				{
					selectionParam.index = num;
					dictionary[key] = selectionParam;
				}
				return;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00052470 File Offset: 0x00050670
		public void AddMode(string cfgName)
		{
			Dictionary<string, Config.Param> dictionary = this.Params["Default"];
			Dictionary<string, Config.Param> value = new Dictionary<string, Config.Param>(dictionary.Count);
			this.CopyParams(dictionary, ref value);
			this.Params.Add(cfgName, value);
			this.Transitions.Add(cfgName, 0.25f);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000524C1 File Offset: 0x000506C1
		public void DeleteMode(string cfgName)
		{
			this.Params.Remove(cfgName);
			this.Transitions.Remove(cfgName);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000524E0 File Offset: 0x000506E0
		public void Serialize(string file)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this.Params.Count);
			foreach (KeyValuePair<string, Dictionary<string, Config.Param>> keyValuePair in this.Params)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>(keyValuePair.Value.Count);
				foreach (KeyValuePair<string, Config.Param> keyValuePair2 in keyValuePair.Value)
				{
					object[] value = keyValuePair2.Value.Serialize();
					dictionary2.Add(keyValuePair2.Key, value);
				}
				dictionary.Add(keyValuePair.Key, dictionary2);
			}
			if (this.Params.Count > 1)
			{
				dictionary.Add("Transitions", this.Transitions);
			}
			string text = Json.Serialize(dictionary);
			if (!string.IsNullOrEmpty(text))
			{
				IO.WriteTextFile(file, text);
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000525F4 File Offset: 0x000507F4
		public void Deserialize(string file)
		{
			string text = IO.ReadTextFile(file);
			if (string.IsNullOrEmpty(text))
			{
				if (!this.ResourceAsset)
				{
					this.RefreshResourceAsset();
				}
				if (this.ResourceAsset)
				{
					text = this.ResourceAsset.text;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				Dictionary<string, object> dictionary = Json.Deserialize(text) as Dictionary<string, object>;
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						Dictionary<string, object> dictionary2 = keyValuePair.Value as Dictionary<string, object>;
						if (dictionary2 != null)
						{
							foreach (KeyValuePair<string, object> keyValuePair2 in dictionary2)
							{
								if (keyValuePair.Key == "Transitions")
								{
									this.Transitions[keyValuePair2.Key] = Convert.ToSingle(keyValuePair2.Value);
								}
								else
								{
									List<object> list = keyValuePair2.Value as List<object>;
									object[] data = list.ToArray();
									Config.ConfigValue configValue = (Config.ConfigValue)Enum.Parse(typeof(Config.ConfigValue), list[0] as string);
									Config.Param param = null;
									switch (configValue)
									{
									case Config.ConfigValue.Bool:
										param = default(Config.BoolParam);
										break;
									case Config.ConfigValue.Range:
										param = new Config.RangeParam();
										break;
									case Config.ConfigValue.Vector3:
										param = default(Config.Vector3Param);
										break;
									case Config.ConfigValue.Vector2:
										param = default(Config.Vector2Param);
										break;
									case Config.ConfigValue.String:
										param = default(Config.StringParam);
										break;
									case Config.ConfigValue.Selection:
										param = default(Config.SelectionParam);
										break;
									}
									param.Deserialize(data);
									this.Params[keyValuePair.Key][keyValuePair2.Key] = param;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00052828 File Offset: 0x00050A28
		public void RefreshResourceAsset()
		{
			this.ResourceAsset = Resources.Load<TextAsset>(this.ResourceDirRel + base.GetType().Name);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0005284B File Offset: 0x00050A4B
		public void EnableLiveGUI(bool status)
		{
			this.enableLiveGUI = status;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00052854 File Offset: 0x00050A54
		private void OnGUI()
		{
			if (!this.enableLiveGUI)
			{
				return;
			}
			float num = this.WindowSize.y;
			float num2 = this.WindowSize.x;
			if (num > (float)Screen.height)
			{
				num = (float)Screen.height;
			}
			if (num2 > (float)Screen.width)
			{
				num2 = (float)Screen.width;
			}
			GUISkin guiSkin = CameraManager.Instance.GuiSkin;
			if (guiSkin)
			{
				GUI.skin = guiSkin;
			}
			GUILayout.Window(0, new Rect((float)Screen.width - num2 - this.WindowPos.x, this.WindowPos.y, num2, num), new GUI.WindowFunction(this.GUIWindow), "Live GUI", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00052900 File Offset: 0x00050B00
		private void GUIWindow(int id)
		{
			if (this.Params != null)
			{
				this.scrolling = GUILayout.BeginScrollView(this.scrolling, Array.Empty<GUILayoutOption>());
				string[] array = new string[this.Params.Keys.Count + 1];
				array[0] = "All";
				this.Params.Keys.CopyTo(array, 1);
				GUIUtils.Selection("Show modes", array, ref this.modeIndex);
				foreach (KeyValuePair<string, Dictionary<string, Config.Param>> keyValuePair in this.Params)
				{
					bool flag = false;
					if (!(array[this.modeIndex] != "All") || !(array[this.modeIndex] != keyValuePair.Key))
					{
						GUIUtils.Separator(keyValuePair.Key, 23f);
						foreach (KeyValuePair<string, Config.Param> keyValuePair2 in keyValuePair.Value)
						{
							string key = keyValuePair2.Key;
							Config.Param value = keyValuePair2.Value;
							switch (value.Type)
							{
							case Config.ConfigValue.Bool:
							{
								Config.BoolParam boolParam = (Config.BoolParam)value;
								if (GUIUtils.Toggle(key, ref boolParam.value))
								{
									keyValuePair.Value[key] = boolParam;
									flag = true;
								}
								break;
							}
							case Config.ConfigValue.Range:
							{
								Config.RangeParam rangeParam = (Config.RangeParam)value;
								if (GUIUtils.SliderEdit(keyValuePair2.Key, rangeParam.min, rangeParam.max, ref rangeParam.value))
								{
									keyValuePair.Value[key] = rangeParam;
									flag = true;
								}
								break;
							}
							case Config.ConfigValue.Vector3:
							{
								Config.Vector3Param vector3Param = (Config.Vector3Param)value;
								if (GUIUtils.Vector3(keyValuePair2.Key, ref vector3Param.value))
								{
									keyValuePair.Value[key] = vector3Param;
									flag = true;
								}
								break;
							}
							case Config.ConfigValue.Vector2:
							{
								Config.Vector2Param vector2Param = (Config.Vector2Param)value;
								if (GUIUtils.Vector2(keyValuePair2.Key, ref vector2Param.value))
								{
									keyValuePair.Value[key] = vector2Param;
									flag = true;
								}
								break;
							}
							case Config.ConfigValue.String:
							{
								Config.StringParam stringParam = (Config.StringParam)value;
								if (GUIUtils.String(keyValuePair2.Key, ref stringParam.value))
								{
									keyValuePair.Value[key] = stringParam;
									flag = true;
								}
								break;
							}
							case Config.ConfigValue.Selection:
							{
								Config.SelectionParam selectionParam = (Config.SelectionParam)value;
								if (GUIUtils.Selection(keyValuePair2.Key, selectionParam.value, ref selectionParam.index))
								{
									keyValuePair.Value[key] = selectionParam;
									flag = true;
								}
								break;
							}
							}
							if (flag)
							{
								break;
							}
						}
					}
				}
				if (this.Params.Count > 1)
				{
					if (!this.showTransitions)
					{
						GUIUtils.Separator("", 1f);
					}
					GUIUtils.Toggle("Show transitions", ref this.showTransitions);
					if (this.showTransitions)
					{
						GUIUtils.Separator("Transitions", 23f);
						foreach (KeyValuePair<string, float> keyValuePair3 in this.Transitions)
						{
							float value2 = keyValuePair3.Value;
							if (GUIUtils.SliderEdit(keyValuePair3.Key, 0f, 2f, ref value2))
							{
								this.Transitions[keyValuePair3.Key] = value2;
								break;
							}
						}
					}
				}
				GUILayout.EndScrollView();
			}
		}

		// Token: 0x04000C81 RID: 3201
		public Dictionary<string, Dictionary<string, Config.Param>> Params;

		// Token: 0x04000C82 RID: 3202
		public Dictionary<string, float> Transitions;

		// Token: 0x04000C83 RID: 3203
		public TextAsset ResourceAsset;

		// Token: 0x04000C84 RID: 3204
		public Config.OnTransitMode TransitCallback;

		// Token: 0x04000C85 RID: 3205
		public Config.OnTransitionStart TransitionStartCallback;

		// Token: 0x04000C86 RID: 3206
		public int ModeIndex;

		// Token: 0x04000C87 RID: 3207
		protected string currentMode;

		// Token: 0x04000C88 RID: 3208
		private float transitionTime;

		// Token: 0x04000C89 RID: 3209
		private Dictionary<string, Config.Param> currParams = new Dictionary<string, Config.Param>();

		// Token: 0x04000C8A RID: 3210
		private Dictionary<string, Config.Param> oldParams = new Dictionary<string, Config.Param>();

		// Token: 0x04000C8B RID: 3211
		private bool enableLiveGUI;

		// Token: 0x04000C8C RID: 3212
		private Vector2 scrolling;

		// Token: 0x04000C8D RID: 3213
		private Vector2 WindowPos = new Vector2(10f, 10f);

		// Token: 0x04000C8E RID: 3214
		private Vector2 WindowSize = new Vector2(400f, 800f);

		// Token: 0x04000C8F RID: 3215
		private int modeIndex;

		// Token: 0x04000C90 RID: 3216
		private bool showTransitions;

		// Token: 0x020003E8 RID: 1000
		// (Invoke) Token: 0x06001E88 RID: 7816
		public delegate void OnTransitMode(string newMode, float t);

		// Token: 0x020003E9 RID: 1001
		// (Invoke) Token: 0x06001E8C RID: 7820
		public delegate void OnTransitionStart(string oldMode, string newMode);

		// Token: 0x020003EA RID: 1002
		public enum ConfigValue
		{
			// Token: 0x04001821 RID: 6177
			Bool,
			// Token: 0x04001822 RID: 6178
			Range,
			// Token: 0x04001823 RID: 6179
			Vector3,
			// Token: 0x04001824 RID: 6180
			Vector2,
			// Token: 0x04001825 RID: 6181
			String,
			// Token: 0x04001826 RID: 6182
			Selection
		}

		// Token: 0x020003EB RID: 1003
		public interface Param
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06001E8F RID: 7823
			Config.ConfigValue Type { get; }

			// Token: 0x06001E90 RID: 7824
			object[] Serialize();

			// Token: 0x06001E91 RID: 7825
			void Deserialize(object[] data);

			// Token: 0x06001E92 RID: 7826
			void Interpolate(Config.Param p0, Config.Param p1, float t);

			// Token: 0x06001E93 RID: 7827
			void Set(Config.Param p);

			// Token: 0x06001E94 RID: 7828
			Config.Param Clone();
		}

		// Token: 0x020003EC RID: 1004
		public class RangeParam : Config.Param
		{
			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00080A21 File Offset: 0x0007EC21
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.Range;
				}
			}

			// Token: 0x06001E96 RID: 7830 RVA: 0x00080A24 File Offset: 0x0007EC24
			public object[] Serialize()
			{
				return new object[]
				{
					Config.ConfigValue.Range.ToString(),
					this.value,
					this.min,
					this.max
				};
			}

			// Token: 0x06001E97 RID: 7831 RVA: 0x00080A73 File Offset: 0x0007EC73
			public void Deserialize(object[] data)
			{
				this.value = Convert.ToSingle(data[1]);
				this.min = Convert.ToSingle(data[2]);
				this.max = Convert.ToSingle(data[3]);
			}

			// Token: 0x06001E98 RID: 7832 RVA: 0x00080AA0 File Offset: 0x0007ECA0
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.RangeParam rangeParam = (Config.RangeParam)p0;
				Config.RangeParam rangeParam2 = (Config.RangeParam)p1;
				this.value = Interpolation.LerpS2(rangeParam.value, rangeParam2.value, t);
				this.min = Interpolation.LerpS2(rangeParam.min, rangeParam2.min, t);
				this.max = Interpolation.LerpS2(rangeParam.max, rangeParam2.max, t);
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x00080B04 File Offset: 0x0007ED04
			public void Set(Config.Param p)
			{
				Config.RangeParam rangeParam = (Config.RangeParam)p;
				this.value = rangeParam.value;
				this.min = rangeParam.min;
				this.max = rangeParam.max;
			}

			// Token: 0x06001E9A RID: 7834 RVA: 0x00080B3C File Offset: 0x0007ED3C
			public Config.Param Clone()
			{
				Config.RangeParam rangeParam = new Config.RangeParam();
				rangeParam.Set(this);
				return rangeParam;
			}

			// Token: 0x04001827 RID: 6183
			public float value;

			// Token: 0x04001828 RID: 6184
			public float min;

			// Token: 0x04001829 RID: 6185
			public float max;
		}

		// Token: 0x020003ED RID: 1005
		public struct Vector3Param : Config.Param
		{
			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00080B52 File Offset: 0x0007ED52
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.Vector3;
				}
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x00080B58 File Offset: 0x0007ED58
			public object[] Serialize()
			{
				return new object[]
				{
					Config.ConfigValue.Vector3.ToString(),
					this.value.x,
					this.value.y,
					this.value.z
				};
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x00080BB6 File Offset: 0x0007EDB6
			public void Deserialize(object[] data)
			{
				this.value.x = Convert.ToSingle(data[1]);
				this.value.y = Convert.ToSingle(data[2]);
				this.value.z = Convert.ToSingle(data[3]);
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x00080BF4 File Offset: 0x0007EDF4
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.Vector3Param vector3Param = (Config.Vector3Param)p0;
				Config.Vector3Param vector3Param2 = (Config.Vector3Param)p1;
				this.value = Interpolation.LerpS2(vector3Param.value, vector3Param2.value, t);
			}

			// Token: 0x06001EA0 RID: 7840 RVA: 0x00080C28 File Offset: 0x0007EE28
			public void Set(Config.Param p)
			{
				Config.Vector3Param vector3Param = (Config.Vector3Param)p;
				this.value = vector3Param.value;
			}

			// Token: 0x06001EA1 RID: 7841 RVA: 0x00080C48 File Offset: 0x0007EE48
			public Config.Param Clone()
			{
				Config.Vector3Param vector3Param = default(Config.Vector3Param);
				vector3Param.Set(this);
				return vector3Param;
			}

			// Token: 0x0400182A RID: 6186
			public Vector3 value;
		}

		// Token: 0x020003EE RID: 1006
		public struct Vector2Param : Config.Param
		{
			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x00080C75 File Offset: 0x0007EE75
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.Vector2;
				}
			}

			// Token: 0x06001EA3 RID: 7843 RVA: 0x00080C78 File Offset: 0x0007EE78
			public object[] Serialize()
			{
				return new object[]
				{
					Config.ConfigValue.Vector2.ToString(),
					this.value.x,
					this.value.y
				};
			}

			// Token: 0x06001EA4 RID: 7844 RVA: 0x00080CC3 File Offset: 0x0007EEC3
			public void Deserialize(object[] data)
			{
				this.value.x = Convert.ToSingle(data[1]);
				this.value.y = Convert.ToSingle(data[2]);
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x00080CEC File Offset: 0x0007EEEC
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.Vector2Param vector2Param = (Config.Vector2Param)p0;
				Config.Vector2Param vector2Param2 = (Config.Vector2Param)p1;
				this.value = Interpolation.LerpS2(vector2Param.value, vector2Param2.value, t);
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x00080D20 File Offset: 0x0007EF20
			public void Set(Config.Param p)
			{
				Config.Vector2Param vector2Param = (Config.Vector2Param)p;
				this.value = vector2Param.value;
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x00080D40 File Offset: 0x0007EF40
			public Config.Param Clone()
			{
				Config.Vector2Param vector2Param = default(Config.Vector2Param);
				vector2Param.Set(this);
				return vector2Param;
			}

			// Token: 0x0400182B RID: 6187
			public Vector2 value;
		}

		// Token: 0x020003EF RID: 1007
		public struct StringParam : Config.Param
		{
			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00080D6D File Offset: 0x0007EF6D
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.String;
				}
			}

			// Token: 0x06001EA9 RID: 7849 RVA: 0x00080D70 File Offset: 0x0007EF70
			public object[] Serialize()
			{
				return new object[]
				{
					Config.ConfigValue.String.ToString(),
					this.value
				};
			}

			// Token: 0x06001EAA RID: 7850 RVA: 0x00080D9E File Offset: 0x0007EF9E
			public void Deserialize(object[] data)
			{
				this.value = Convert.ToString(data[1]);
			}

			// Token: 0x06001EAB RID: 7851 RVA: 0x00080DB0 File Offset: 0x0007EFB0
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.StringParam stringParam = (Config.StringParam)p1;
				this.value = stringParam.value;
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x00080DD0 File Offset: 0x0007EFD0
			public void Set(Config.Param p)
			{
				Config.StringParam stringParam = (Config.StringParam)p;
				this.value = stringParam.value;
			}

			// Token: 0x06001EAD RID: 7853 RVA: 0x00080DF0 File Offset: 0x0007EFF0
			public Config.Param Clone()
			{
				Config.StringParam stringParam = default(Config.StringParam);
				stringParam.Set(this);
				return stringParam;
			}

			// Token: 0x0400182C RID: 6188
			public string value;
		}

		// Token: 0x020003F0 RID: 1008
		public struct BoolParam : Config.Param
		{
			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001EAE RID: 7854 RVA: 0x00080E1D File Offset: 0x0007F01D
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.Bool;
				}
			}

			// Token: 0x06001EAF RID: 7855 RVA: 0x00080E20 File Offset: 0x0007F020
			public object[] Serialize()
			{
				return new object[]
				{
					Config.ConfigValue.Bool.ToString(),
					this.value
				};
			}

			// Token: 0x06001EB0 RID: 7856 RVA: 0x00080E53 File Offset: 0x0007F053
			public void Deserialize(object[] data)
			{
				this.value = Convert.ToBoolean(data[1]);
			}

			// Token: 0x06001EB1 RID: 7857 RVA: 0x00080E64 File Offset: 0x0007F064
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.BoolParam boolParam = (Config.BoolParam)p1;
				this.value = boolParam.value;
			}

			// Token: 0x06001EB2 RID: 7858 RVA: 0x00080E84 File Offset: 0x0007F084
			public void Set(Config.Param p)
			{
				Config.BoolParam boolParam = (Config.BoolParam)p;
				this.value = boolParam.value;
			}

			// Token: 0x06001EB3 RID: 7859 RVA: 0x00080EA4 File Offset: 0x0007F0A4
			public Config.Param Clone()
			{
				Config.BoolParam boolParam = default(Config.BoolParam);
				boolParam.Set(this);
				return boolParam;
			}

			// Token: 0x0400182D RID: 6189
			public bool value;
		}

		// Token: 0x020003F1 RID: 1009
		public struct SelectionParam : Config.Param
		{
			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00080ED1 File Offset: 0x0007F0D1
			public Config.ConfigValue Type
			{
				get
				{
					return Config.ConfigValue.Selection;
				}
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x00080ED4 File Offset: 0x0007F0D4
			public object[] Serialize()
			{
				object[] array = new object[this.value.Length + 2];
				array[0] = Config.ConfigValue.Selection.ToString();
				array[1] = this.index;
				this.value.CopyTo(array, 2);
				return array;
			}

			// Token: 0x06001EB6 RID: 7862 RVA: 0x00080F20 File Offset: 0x0007F120
			public int Find(string val)
			{
				for (int i = 0; i < this.value.Length; i++)
				{
					if (this.value[i] == val)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06001EB7 RID: 7863 RVA: 0x00080F54 File Offset: 0x0007F154
			public void Deserialize(object[] data)
			{
				this.index = Convert.ToInt32(data[1]);
				this.value = new string[data.Length - 2];
				for (int i = 2; i < data.Length; i++)
				{
					this.value[i - 2] = Convert.ToString(data[i]);
				}
			}

			// Token: 0x06001EB8 RID: 7864 RVA: 0x00080FA0 File Offset: 0x0007F1A0
			public void Interpolate(Config.Param p0, Config.Param p1, float t)
			{
				Config.SelectionParam selectionParam = (Config.SelectionParam)p1;
				this.index = selectionParam.index;
			}

			// Token: 0x06001EB9 RID: 7865 RVA: 0x00080FC0 File Offset: 0x0007F1C0
			public void Set(Config.Param p)
			{
				Config.SelectionParam selectionParam = (Config.SelectionParam)p;
				this.index = selectionParam.index;
			}

			// Token: 0x06001EBA RID: 7866 RVA: 0x00080FE0 File Offset: 0x0007F1E0
			public Config.Param Clone()
			{
				Config.SelectionParam selectionParam = default(Config.SelectionParam);
				selectionParam.Set(this);
				return selectionParam;
			}

			// Token: 0x0400182E RID: 6190
			public int index;

			// Token: 0x0400182F RID: 6191
			public string[] value;
		}
	}
}
