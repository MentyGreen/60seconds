using System;
using System.Collections.Generic;
using I2.Loc;
using Rewired;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.SecondsRemaster.EventEditor;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002AE RID: 686
	public class SettingsManager : MonoBehaviour
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x0006BB67 File Offset: 0x00069D67
		public void Start()
		{
			if (!this._loadOnStart)
			{
				return;
			}
			this.LoadSettings();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0006BB78 File Offset: 0x00069D78
		public void LoadSettings()
		{
			this.SetCurrentLanguage();
			this.SetCurrentResolution();
			this.SetCurrentQuality();
			this.SetCurrentAudio();
			this.SetCurrentGamma();
			this.SetSteamDeckSpecificSettings();
			this.LoadControlMaps();
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006BBA4 File Offset: 0x00069DA4
		private void SetSteamDeckSpecificSettings()
		{
			if (SteamManager.IsRunningOnSteamDeck())
			{
				Screen.SetResolution(this._steamDeckDefaultWidthVariable.Value, this._steamDeckDefaultHeightVariable.Value, true);
				QualitySettings.SetQualityLevel(this._steamDeckDefaultQualityVariable.Value);
				this._currentControlModeVariable.Value = this._steamDeckDefaultControlModeVariable.Value;
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0006BBFA File Offset: 0x00069DFA
		private void SetCurrentLanguage()
		{
			if (string.IsNullOrEmpty(this._languageVariable.Value))
			{
				return;
			}
			LocalizationManager.SetLanguageAndCode(LocalizationManager.GetLanguageFromCode(this._languageVariable.Value, true), this._languageVariable.Value, true, false);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0006BC34 File Offset: 0x00069E34
		private void SetCurrentResolution()
		{
			List<Resolution> list = new List<Resolution>(Screen.resolutions);
			if (!this.IsResolutionValid(this._resolutionWidthVariable.Value, this._resolutionHeightVariable.Value))
			{
				this._resolutionWidthVariable.Value = list[list.Count - 1].width;
				this._resolutionHeightVariable.Value = list[list.Count - 1].height;
				Screen.SetResolution(list[list.Count - 1].width, list[list.Count - 1].height, this._fullscreenVariable.Value);
				return;
			}
			Screen.SetResolution(this._resolutionWidthVariable.Value, this._resolutionHeightVariable.Value, this._fullscreenVariable.Value);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0006BD10 File Offset: 0x00069F10
		private bool IsResolutionValid(int width, int height)
		{
			Resolution[] resolutions = Screen.resolutions;
			bool result = false;
			for (int i = resolutions.Length - 1; i >= 0; i--)
			{
				if (resolutions[i].width == width && resolutions[i].height == height)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0006BD56 File Offset: 0x00069F56
		private void SetCurrentQuality()
		{
			if (this._qualityVariable.Value < 0)
			{
				return;
			}
			QualitySettings.SetQualityLevel(this._qualityVariable.Value, true);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0006BD78 File Offset: 0x00069F78
		private void SetCurrentAudio()
		{
			float num = (this._masterVolumeVariable.Value < 0f) ? 1f : this._masterVolumeVariable.Value;
			float num2 = (this._soundVolumeVariable.Value < 0f) ? 1f : this._soundVolumeVariable.Value;
			float num3 = (this._musicVolumeVariable.Value < 0f) ? 1f : this._musicVolumeVariable.Value;
			AudioManager.Instance.SetMusicVolume(num3 * num);
			AudioManager.Instance.SetSfxVolume(num2 * num);
			AudioManager.Instance.SetUiVolume(num2 * num);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0006BE1C File Offset: 0x0006A01C
		private void SetCurrentGamma()
		{
			for (int i = 0; i < this._postProcessingProfiles.Length; i++)
			{
				ColorGradingModel.Settings settings = this._postProcessingProfiles[i].colorGrading.settings;
				settings.basic.postExposure = this._gammaVariable.Value;
				this._postProcessingProfiles[i].colorGrading.settings = settings;
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0006BE7C File Offset: 0x0006A07C
		private void LoadControlMaps()
		{
			Player player = ReInput.players.GetPlayer(0);
			if (!string.IsNullOrEmpty(this._joystickMapsSaveDataVariable.Value))
			{
				MapsDataWrapper mapsDataWrapper = JsonUtility.FromJson<MapsDataWrapper>(this._joystickMapsSaveDataVariable.Value);
				player.controllers.maps.ClearMaps(ControllerType.Joystick, true);
				for (int i = 0; i < player.controllers.Joysticks.Count; i++)
				{
					for (int j = 0; j < mapsDataWrapper.MapSaveData.Count; j++)
					{
						player.controllers.maps.AddMapFromJson(ControllerType.Joystick, i, mapsDataWrapper.MapSaveData[j]);
					}
				}
				bool flag = false;
				using (IEnumerator<ControllerMap> enumerator = player.controllers.maps.GetAllMapsInCategory("Gamepad", ControllerType.Joystick).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.AllMaps.Count < 5)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
					List<string> list = new List<string>();
					for (int k = 0; k < player.controllers.Joysticks.Count; k++)
					{
						ControllerMapSaveData[] mapSaveData = player.controllers.maps.GetMapSaveData(ControllerType.Joystick, k, true);
						for (int l = 0; l < mapSaveData.Length; l++)
						{
							list.Add(mapSaveData[l].map.ToJsonString());
						}
					}
					MapsDataWrapper mapsDataWrapper2 = new MapsDataWrapper
					{
						MapSaveData = list
					};
					this._joystickMapsSaveDataVariable.Value = JsonUtility.ToJson(mapsDataWrapper2);
				}
			}
			else
			{
				Debug.Log("Loading default joystick maps!");
				player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
			}
			if (!string.IsNullOrEmpty(this._keyboardMapsSaveDataVariable.Value))
			{
				MapsDataWrapper mapsDataWrapper3 = JsonUtility.FromJson<MapsDataWrapper>(this._keyboardMapsSaveDataVariable.Value);
				player.controllers.maps.ClearMaps(ControllerType.Keyboard, true);
				for (int m = 0; m < mapsDataWrapper3.MapSaveData.Count; m++)
				{
					player.controllers.maps.AddMapFromJson(ControllerType.Keyboard, 0, mapsDataWrapper3.MapSaveData[m]);
				}
			}
			else if (!string.IsNullOrEmpty(this._keyboardMapsSaveDataVariable.GetInitialValue()))
			{
				MapsDataWrapper mapsDataWrapper4 = JsonUtility.FromJson<MapsDataWrapper>(this._keyboardMapsSaveDataVariable.GetInitialValue());
				player.controllers.maps.ClearMaps(ControllerType.Keyboard, true);
				for (int n = 0; n < mapsDataWrapper4.MapSaveData.Count; n++)
				{
					player.controllers.maps.AddMapFromJson(ControllerType.Keyboard, 0, mapsDataWrapper4.MapSaveData[n]);
				}
			}
			else
			{
				Debug.Log("Loading default keyboard maps!");
				player.controllers.maps.LoadDefaultMaps(ControllerType.Keyboard);
			}
			if (!string.IsNullOrEmpty(this._mouseMapsSaveDataVariable.Value))
			{
				MapsDataWrapper mapsDataWrapper5 = JsonUtility.FromJson<MapsDataWrapper>(this._mouseMapsSaveDataVariable.Value);
				player.controllers.maps.ClearMaps(ControllerType.Mouse, true);
				for (int num = 0; num < mapsDataWrapper5.MapSaveData.Count; num++)
				{
					player.controllers.maps.AddMapFromJson(ControllerType.Mouse, 0, mapsDataWrapper5.MapSaveData[num]);
				}
			}
			else if (!string.IsNullOrEmpty(this._mouseMapsSaveDataVariable.GetInitialValue()))
			{
				MapsDataWrapper mapsDataWrapper6 = JsonUtility.FromJson<MapsDataWrapper>(this._mouseMapsSaveDataVariable.GetInitialValue());
				player.controllers.maps.ClearMaps(ControllerType.Mouse, true);
				for (int num2 = 0; num2 < mapsDataWrapper6.MapSaveData.Count; num2++)
				{
					player.controllers.maps.AddMapFromJson(ControllerType.Mouse, 0, mapsDataWrapper6.MapSaveData[num2]);
				}
			}
			else
			{
				Debug.Log("Loading default mouse maps!");
				player.controllers.maps.LoadDefaultMaps(ControllerType.Mouse);
			}
			EPlayerInput eplayerInput = (EPlayerInput)this._currentControlModeVariable.Value;
			if (player.controllers.joystickCount == 0 && eplayerInput == EPlayerInput.GAMEPAD)
			{
				eplayerInput = EPlayerInput.KEYBOARD_MOUSE;
				this._currentControlModeVariable.Value = (int)eplayerInput;
			}
			int layoutId = (Application.systemLanguage == SystemLanguage.French) ? 1 : 0;
			int layoutId2 = (Application.systemLanguage != SystemLanguage.French) ? 1 : 0;
			string name = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId).name;
			string name2 = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId2).name;
			switch (eplayerInput)
			{
			case EPlayerInput.KEYBOARD:
				player.controllers.maps.SetMapsEnabled(false, 8);
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(7).name, name);
				player.controllers.maps.SetMapsEnabled(false, ReInput.mapping.GetMapCategory(7).name, name2);
				player.controllers.maps.SetMapsEnabled(false, 9);
				player.controllers.maps.SetMapsEnabled(false, 6);
				break;
			case EPlayerInput.KEYBOARD_MOUSE:
				player.controllers.maps.SetMapsEnabled(false, 8);
				player.controllers.maps.SetMapsEnabled(false, 7);
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(9).name, name);
				player.controllers.maps.SetMapsEnabled(false, ReInput.mapping.GetMapCategory(9).name, name2);
				player.controllers.maps.SetMapsEnabled(false, 6);
				break;
			case EPlayerInput.GAMEPAD:
				player.controllers.maps.SetMapsEnabled(true, 8);
				player.controllers.maps.SetMapsEnabled(false, 7);
				player.controllers.maps.SetMapsEnabled(false, 9);
				player.controllers.maps.SetMapsEnabled(false, 6);
				break;
			case EPlayerInput.MOUSE_ONLY:
				player.controllers.maps.SetMapsEnabled(false, 8);
				player.controllers.maps.SetMapsEnabled(false, 7);
				player.controllers.maps.SetMapsEnabled(false, 9);
				player.controllers.maps.SetMapsEnabled(true, 6);
				break;
			}
			this.DisableKeyboardSnappingCursorButtons();
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0006C4A0 File Offset: 0x0006A6A0
		private void DisableKeyboardSnappingCursorButtons()
		{
			Player player = ReInput.players.GetPlayer(0);
			int num = 25;
			int num2 = 28;
			foreach (ControllerMap controllerMap in player.controllers.maps.GetAllMapsInCategory("Default", ControllerType.Keyboard))
			{
				foreach (ActionElementMap actionElementMap in controllerMap.AllMaps)
				{
					if (actionElementMap.actionId == num || actionElementMap.actionId == num2)
					{
						actionElementMap.enabled = false;
					}
				}
			}
		}

		// Token: 0x0400125A RID: 4698
		[SerializeField]
		[Header("Main Settings")]
		private GlobalStringVariable _languageVariable;

		// Token: 0x0400125B RID: 4699
		[SerializeField]
		private GlobalIntVariable _resolutionWidthVariable;

		// Token: 0x0400125C RID: 4700
		[SerializeField]
		private GlobalIntVariable _resolutionHeightVariable;

		// Token: 0x0400125D RID: 4701
		[SerializeField]
		private GlobalIntVariable _qualityVariable;

		// Token: 0x0400125E RID: 4702
		[SerializeField]
		private GlobalBoolVariable _fullscreenVariable;

		// Token: 0x0400125F RID: 4703
		[SerializeField]
		private GlobalFloatVariable _masterVolumeVariable;

		// Token: 0x04001260 RID: 4704
		[SerializeField]
		private GlobalFloatVariable _soundVolumeVariable;

		// Token: 0x04001261 RID: 4705
		[SerializeField]
		private GlobalFloatVariable _musicVolumeVariable;

		// Token: 0x04001262 RID: 4706
		[SerializeField]
		private GlobalFloatVariable _gammaVariable;

		// Token: 0x04001263 RID: 4707
		[SerializeField]
		private PostProcessingProfile[] _postProcessingProfiles;

		// Token: 0x04001264 RID: 4708
		[SerializeField]
		[Header("Control Settings")]
		private GlobalStringVariable _joystickMapsSaveDataVariable;

		// Token: 0x04001265 RID: 4709
		[SerializeField]
		private GlobalStringVariable _keyboardMapsSaveDataVariable;

		// Token: 0x04001266 RID: 4710
		[SerializeField]
		private GlobalStringVariable _mouseMapsSaveDataVariable;

		// Token: 0x04001267 RID: 4711
		[SerializeField]
		private GlobalIntVariable _currentControlModeVariable;

		// Token: 0x04001268 RID: 4712
		[SerializeField]
		private bool _loadOnStart;

		// Token: 0x04001269 RID: 4713
		[SerializeField]
		[Header("Steam Deck Settings Defaults")]
		private GlobalIntVariable _steamDeckDefaultWidthVariable;

		// Token: 0x0400126A RID: 4714
		[SerializeField]
		private GlobalIntVariable _steamDeckDefaultHeightVariable;

		// Token: 0x0400126B RID: 4715
		[SerializeField]
		private GlobalIntVariable _steamDeckDefaultQualityVariable;

		// Token: 0x0400126C RID: 4716
		[SerializeField]
		private GlobalIntVariable _steamDeckDefaultControlModeVariable;
	}
}
