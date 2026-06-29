using System;
using System.Collections.Generic;
using Rewired;
using RG.Core.SaveSystem;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.SecondsRemaster.EventEditor;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A5 RID: 677
	public class ControlsPanelController : MonoBehaviour
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0006A8F6 File Offset: 0x00068AF6
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0006A909 File Offset: 0x00068B09
		public void OnEnable()
		{
			if (this._controlModeVariable.Value < 0)
			{
				this._controlModeVariable.Value = (int)this._defaultPlayerMovement;
			}
			this._mainPanel.SetCloseActionBlocked(true);
			this.SetCurrentControlMode();
			this.DisableSteamDeckButtons();
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0006A944 File Offset: 0x00068B44
		private void DisableSteamDeckButtons()
		{
			if (SteamManager.IsRunningOnSteamDeck())
			{
				for (int i = 0; i <= this._buttonsToDisableOnSteamDeck.Count - 1; i++)
				{
					this._buttonsToDisableOnSteamDeck[i].interactable = false;
				}
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006A982 File Offset: 0x00068B82
		public void OnDisable()
		{
			this._mainPanel.SetCloseActionBlocked(false);
			this.SaveControlMaps();
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0006A998 File Offset: 0x00068B98
		private void SetCurrentControlMode()
		{
			for (int i = 0; i < this._playerMovements.Length; i++)
			{
				if (this._controlModeVariable.Value == (int)this._playerMovements[i])
				{
					this._currentIndex = i;
					this._controlModes[i].SetActive(true);
					this._player.controllers.maps.SetMapsEnabled(true, (int)this._mapCategories[i]);
				}
				else
				{
					this._controlModes[i].SetActive(false);
					this._player.controllers.maps.SetMapsEnabled(false, (int)this._mapCategories[i]);
				}
			}
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0006AA34 File Offset: 0x00068C34
		public void SetNextControlMode()
		{
			this._controlModes[this._currentIndex].SetActive(false);
			this._player.controllers.maps.SetMapsEnabled(false, (int)this._mapCategories[this._currentIndex]);
			if (this._currentIndex + 1 >= this._playerMovements.Length)
			{
				this._currentIndex = 0;
			}
			else
			{
				this._currentIndex++;
			}
			this._controlModeVariable.Value = (int)this._playerMovements[this._currentIndex];
			this._controlModes[this._currentIndex].SetActive(true);
			this._player.controllers.maps.SetMapsEnabled(true, (int)this._mapCategories[this._currentIndex]);
			Singleton<VirtualInputManager>.Instance.RefreshSelectablesModeSelection();
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006AAFC File Offset: 0x00068CFC
		public void SetPreviousControlMode()
		{
			this._controlModes[this._currentIndex].SetActive(false);
			this._player.controllers.maps.SetMapsEnabled(false, (int)this._mapCategories[this._currentIndex]);
			if (this._currentIndex - 1 < 0)
			{
				this._currentIndex = this._playerMovements.Length - 1;
			}
			else
			{
				this._currentIndex--;
			}
			this._controlModeVariable.Value = (int)this._playerMovements[this._currentIndex];
			this._controlModes[this._currentIndex].SetActive(true);
			this._player.controllers.maps.SetMapsEnabled(true, (int)this._mapCategories[this._currentIndex]);
			Singleton<VirtualInputManager>.Instance.RefreshSelectablesModeSelection();
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0006ABC4 File Offset: 0x00068DC4
		public void RefreshMap()
		{
			if (this._player == null)
			{
				return;
			}
			if (this._playerMovements[this._currentIndex] == EPlayerInput.KEYBOARD || this._playerMovements[this._currentIndex] == EPlayerInput.KEYBOARD_MOUSE)
			{
				int layoutId = (Application.systemLanguage == SystemLanguage.French) ? 1 : 0;
				int layoutId2 = (Application.systemLanguage != SystemLanguage.French) ? 1 : 0;
				string name = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId).name;
				string name2 = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId2).name;
				string name3 = ReInput.mapping.GetMapCategory((int)this._mapCategories[this._currentIndex]).name;
				this._player.controllers.maps.SetMapsEnabled(false, (int)this._mapCategories[this._currentIndex]);
				this._player.controllers.maps.SetMapsEnabled(true, name3, name);
				this._player.controllers.maps.SetMapsEnabled(false, name3, name2);
				return;
			}
			this._player.controllers.maps.SetMapsEnabled(false, (int)this._mapCategories[this._currentIndex]);
			this._player.controllers.maps.SetMapsEnabled(true, (int)this._mapCategories[this._currentIndex]);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0006ACFC File Offset: 0x00068EFC
		public void SaveControlMaps()
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			if (this._player.controllers.joystickCount > 0)
			{
				for (int i = 0; i < this._player.controllers.Joysticks.Count; i++)
				{
					ControllerMapSaveData[] mapSaveData = this._player.controllers.maps.GetMapSaveData(ControllerType.Joystick, i, true);
					for (int j = 0; j < mapSaveData.Length; j++)
					{
						list.Add(mapSaveData[j].map.ToJsonString());
					}
				}
				MapsDataWrapper mapsDataWrapper = new MapsDataWrapper
				{
					MapSaveData = list
				};
				this._joystickMapsSaveDataVariable.Value = JsonUtility.ToJson(mapsDataWrapper);
			}
			ControllerMapSaveData[] mapSaveData2 = this._player.controllers.maps.GetMapSaveData(ControllerType.Keyboard, 0, true);
			for (int k = 0; k < mapSaveData2.Length; k++)
			{
				list2.Add(mapSaveData2[k].map.ToJsonString());
			}
			MapsDataWrapper mapsDataWrapper2 = new MapsDataWrapper
			{
				MapSaveData = list2
			};
			this._keyboardMapsSaveDataVariable.Value = JsonUtility.ToJson(mapsDataWrapper2);
			ControllerMapSaveData[] mapSaveData3 = this._player.controllers.maps.GetMapSaveData(ControllerType.Mouse, 0, true);
			for (int l = 0; l < mapSaveData3.Length; l++)
			{
				list3.Add(mapSaveData3[l].map.ToJsonString());
			}
			MapsDataWrapper mapsDataWrapper3 = new MapsDataWrapper
			{
				MapSaveData = list3
			};
			this._mouseMapsSaveDataVariable.Value = JsonUtility.ToJson(mapsDataWrapper3);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0006AE9A File Offset: 0x0006909A
		public void SaveSettings()
		{
			this.RefreshMap();
			StorageDataManager.TheInstance.Save("settings", delegate()
			{
				Debug.Log("Saved Settings.");
			}, null, true, false);
		}

		// Token: 0x04001212 RID: 4626
		[SerializeField]
		private GameObject[] _controlModes;

		// Token: 0x04001213 RID: 4627
		[SerializeField]
		private EPlayerInput[] _playerMovements;

		// Token: 0x04001214 RID: 4628
		[SerializeField]
		private EMapCategory[] _mapCategories;

		// Token: 0x04001215 RID: 4629
		[SerializeField]
		private GlobalStringVariable _joystickMapsSaveDataVariable;

		// Token: 0x04001216 RID: 4630
		[SerializeField]
		private GlobalStringVariable _keyboardMapsSaveDataVariable;

		// Token: 0x04001217 RID: 4631
		[SerializeField]
		private GlobalStringVariable _mouseMapsSaveDataVariable;

		// Token: 0x04001218 RID: 4632
		[SerializeField]
		private GlobalIntVariable _controlModeVariable;

		// Token: 0x04001219 RID: 4633
		[SerializeField]
		private EPlayerInput _defaultPlayerMovement;

		// Token: 0x0400121A RID: 4634
		[SerializeField]
		private ClosePanelOnCancelPress _mainPanel;

		// Token: 0x0400121B RID: 4635
		[SerializeField]
		private List<Button> _buttonsToDisableOnSteamDeck = new List<Button>();

		// Token: 0x0400121C RID: 4636
		private int _currentIndex;

		// Token: 0x0400121D RID: 4637
		private Player _player;
	}
}
