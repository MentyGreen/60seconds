using System;
using System.Collections.Generic;
using RG.Core.Base;
using RG.Core.SaveSystem;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002DD RID: 733
	[CreateAssetMenu(fileName = "New Time Rationing", menuName = "60 Seconds Remaster!/Characters/New Time Rationing")]
	public class TimeRationing : RGScriptableObject, ISaveable
	{
		// Token: 0x0600199B RID: 6555 RVA: 0x0006F5E9 File Offset: 0x0006D7E9
		private void OnEnable()
		{
			this._characterList.RegisterCharacterListChangedListener(new CharacterList.OnCharacterListChanged(this.OnCharacterListChange));
			this.Register();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006F608 File Offset: 0x0006D808
		public int GetLastRationingTime(ConsumableRemedium consumableRemedium, Character character)
		{
			int charIndex = this.GetCharIndex(character);
			if (charIndex == -1)
			{
				return 0;
			}
			for (int i = 0; i < this._timeRationing.Count; i++)
			{
				if (this._timeRationing[i].ConsumableRemedium == consumableRemedium)
				{
					return this._timeRationing[i].Characters[charIndex];
				}
			}
			return 0;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0006F66C File Offset: 0x0006D86C
		private int GetCharIndex(Character character)
		{
			for (int i = 0; i < this._characterList.CharactersInGame.Count; i++)
			{
				if (this._characterList.CharactersInGame[i] == character)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
		public void ResetLastRationingTime(ConsumableRemedium consumableRemedium, int characterIndex)
		{
			this.SetLastRationingTime(consumableRemedium, characterIndex, 0);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0006F6BB File Offset: 0x0006D8BB
		public void SetLastRationingTime(ConsumableRemedium consumableRemedium, Character character, int value)
		{
			this.SetLastRationingTime(consumableRemedium, this.GetCharIndex(character), value);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0006F6CC File Offset: 0x0006D8CC
		public void SetLastRationingTime(ConsumableRemedium consumableRemedium, int characterIndex, int value)
		{
			if (characterIndex == -1)
			{
				return;
			}
			for (int i = 0; i < this._timeRationing.Count; i++)
			{
				if (this._timeRationing[i].ConsumableRemedium == consumableRemedium)
				{
					this._timeRationing[i].Characters[characterIndex] = value;
					return;
				}
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0006F728 File Offset: 0x0006D928
		public void IncrementTime()
		{
			for (int i = 0; i < this._timeRationing.Count; i++)
			{
				for (int j = 0; j < this._characterList.GetCharacterCount(); j++)
				{
					List<int> characters = this._timeRationing[i].Characters;
					int index = j;
					int num = characters[index];
					characters[index] = num + 1;
				}
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0006F788 File Offset: 0x0006D988
		private void OnCharacterListChange()
		{
			int num = this._characterList.GetCharacterCount() - 1;
			if (num < 4)
			{
				for (int i = 0; i < this._timeRationing.Count; i++)
				{
					this._timeRationing[i].Characters[num] = 0;
				}
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006F7D8 File Offset: 0x0006D9D8
		public string Serialize()
		{
			List<TimeRationingWrapper> list = new List<TimeRationingWrapper>();
			for (int i = 0; i < this._timeRationing.Count; i++)
			{
				list.Add(new TimeRationingWrapper
				{
					ConsumableRemediumID = this._timeRationing[i].ConsumableRemedium.ID,
					Characters = new List<int>(this._timeRationing[i].Characters)
				});
			}
			return JsonUtility.ToJson(list);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006F850 File Offset: 0x0006DA50
		public void Deserialize(string jsonData)
		{
			List<TimeRationingWrapper> list = JsonUtility.FromJson<List<TimeRationingWrapper>>(jsonData);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < this._timeRationing.Count; j++)
				{
					if (this._timeRationing[j].ConsumableRemedium.ID == list[i].ConsumableRemediumID)
					{
						this._timeRationing[j].Characters.Clear();
						this._timeRationing[j].Characters.AddRange(list[i].Characters);
					}
				}
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006F8EF File Offset: 0x0006DAEF
		public void Register()
		{
			this._saveEvent.RegisterListener(this);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006F8FD File Offset: 0x0006DAFD
		public void Unregister()
		{
			this._saveEvent.UnregisterListener(this);
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0006F90B File Offset: 0x0006DB0B
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0006F914 File Offset: 0x0006DB14
		public void ResetData()
		{
			for (int i = 0; i < this._timeRationing.Count; i++)
			{
				for (int j = 0; j < this._timeRationing[i].Characters.Count; j++)
				{
					this._timeRationing[i].Characters[j] = 0;
				}
			}
		}

		// Token: 0x04001395 RID: 5013
		private const int RESET_TIME_VALUE = 0;

		// Token: 0x04001396 RID: 5014
		[SerializeField]
		private SaveEvent _saveEvent;

		// Token: 0x04001397 RID: 5015
		[SerializeField]
		private List<TimeRationingStruct> _timeRationing;

		// Token: 0x04001398 RID: 5016
		[SerializeField]
		private CharacterList _characterList;

		// Token: 0x04001399 RID: 5017
		private const int CHARACTER_NOT_FOUND = -1;
	}
}
