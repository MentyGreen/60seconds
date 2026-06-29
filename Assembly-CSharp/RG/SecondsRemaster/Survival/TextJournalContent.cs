using System;
using System.Collections.Generic;
using I2.Loc;
using RG.Core.SaveSystem;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002FF RID: 767
	[Serializable]
	public sealed class TextJournalContent : JournalContent, ILocalizationParamsManager
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00071444 File Offset: 0x0006F644
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0007144C File Offset: 0x0006F64C
		public List<LocalizedString> Terms
		{
			get
			{
				return this._terms;
			}
			set
			{
				this._terms = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00071455 File Offset: 0x0006F655
		public LocalizedString Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0007145D File Offset: 0x0006F65D
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00071465 File Offset: 0x0006F665
		public List<Character> Characters
		{
			get
			{
				return this._characters;
			}
			set
			{
				this._characters = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x0007146E File Offset: 0x0006F66E
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00071476 File Offset: 0x0006F676
		public Character ExpeditionCharacter
		{
			get
			{
				return this._expeditionCharacter;
			}
			set
			{
				this._expeditionCharacter = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0007147F File Offset: 0x0006F67F
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x00071487 File Offset: 0x0006F687
		public List<IItem> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00071490 File Offset: 0x0006F690
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00071498 File Offset: 0x0006F698
		public List<int> LocalVariablesInts
		{
			get
			{
				return this._localVariablesInts;
			}
			set
			{
				this._localVariablesInts = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x000714A1 File Offset: 0x0006F6A1
		public string PureText
		{
			get
			{
				return this._pureText;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000714A9 File Offset: 0x0006F6A9
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x000714B1 File Offset: 0x0006F6B1
		public EParsecsEventPhase EventPhase
		{
			get
			{
				return this._eventPhase;
			}
			set
			{
				this._eventPhase = value;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000714BA File Offset: 0x0006F6BA
		public TextJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000714CB File Offset: 0x0006F6CB
		public TextJournalContent(LocalizedString term, int displayPriority) : base(displayPriority)
		{
			this.type = EJournalContentType.TEXT;
			this._term = term;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000714E2 File Offset: 0x0006F6E2
		public TextJournalContent(string text, int displayPriority) : base(displayPriority)
		{
			this.type = EJournalContentType.TEXT;
			this._pureText = text;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000714FC File Offset: 0x0006F6FC
		public override string Serialize()
		{
			TextJournalContentWrapper textJournalContentWrapper = new TextJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				PureText = this._pureText,
				Term = this._term,
				Characters = new List<string>(),
				Items = new List<string>(),
				ExpeditionCharacter = ((this._expeditionCharacter != null) ? this._expeditionCharacter.Guid : string.Empty),
				LocalVariablesInts = this._localVariablesInts,
				Terms = this._terms
			};
			if (this._characters != null)
			{
				for (int i = 0; i < this._characters.Count; i++)
				{
					if (this._characters[i] != null)
					{
						textJournalContentWrapper.Characters.Add(this._characters[i].Guid);
					}
				}
			}
			if (this._items != null)
			{
				for (int j = 0; j < this._items.Count; j++)
				{
					if (this._items[j] != null)
					{
						textJournalContentWrapper.Items.Add(this._items[j].Guid);
					}
				}
			}
			return JsonUtility.ToJson(textJournalContentWrapper);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00071668 File Offset: 0x0006F868
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			TextJournalContentWrapper textJournalContentWrapper = JsonUtility.FromJson<TextJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(textJournalContentWrapper, saveEvent);
			this._pureText = textJournalContentWrapper.PureText;
			this._term = textJournalContentWrapper.Term;
			this._expeditionCharacter = ((!string.IsNullOrEmpty(textJournalContentWrapper.ExpeditionCharacter)) ? ((Character)saveEvent.GetReferenceObjectByID(textJournalContentWrapper.ExpeditionCharacter)) : null);
			this._terms = textJournalContentWrapper.Terms;
			this._localVariablesInts = textJournalContentWrapper.LocalVariablesInts;
			for (int i = 0; i < textJournalContentWrapper.Characters.Count; i++)
			{
				if (!string.IsNullOrEmpty(textJournalContentWrapper.Characters[i]))
				{
					this._characters.Add((Character)saveEvent.GetReferenceObjectByID(textJournalContentWrapper.Characters[i]));
				}
			}
			for (int j = 0; j < textJournalContentWrapper.Items.Count; j++)
			{
				if (!string.IsNullOrEmpty(textJournalContentWrapper.Items[j]))
				{
					this._items.Add((IItem)saveEvent.GetReferenceObjectByID(textJournalContentWrapper.Items[j]));
				}
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00071771 File Offset: 0x0006F971
		private void RegisterInI2Loc()
		{
			if (!LocalizationManager.ParamManagers.Contains(this))
			{
				LocalizationManager.ParamManagers.Add(this);
				LocalizationManager.LocalizeAll(true);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00071791 File Offset: 0x0006F991
		private void UnregisterFromI2Loc()
		{
			LocalizationManager.ParamManagers.Remove(this);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000717A0 File Offset: 0x0006F9A0
		public void RegisterManagers()
		{
			if (this._characters != null && this._characters.Count > 0 && this._characters[0] != null)
			{
				this._characters[0].RegisterInI2Loc();
			}
			if (this._items != null && this._items.Count > 0 && this._items[0] != null)
			{
				this._items[0].RegisterToI2Loc();
			}
			if (this._expeditionCharacter != null || this._terms != null || this._localVariablesInts != null)
			{
				this.RegisterInI2Loc();
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00071848 File Offset: 0x0006FA48
		public void UnregisterManagers()
		{
			if (this._characters != null && this._characters.Count > 0 && this._characters[0] != null)
			{
				this._characters[0].UnregisterFromI2Loc();
			}
			if (this._items != null && this._items.Count > 0 && this._items[0] != null)
			{
				this._items[0].UnregisterFromI2Loc();
			}
			if (this._expeditionCharacter != null || this._terms != null || this._localVariablesInts != null)
			{
				this.UnregisterFromI2Loc();
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000718F0 File Offset: 0x0006FAF0
		public string GetParameterValue(string param)
		{
			if (char.IsNumber(param[0]) && param.Contains("|"))
			{
				int num = Convert.ToInt32(param.Substring(0, param.IndexOf("|", StringComparison.Ordinal)));
				string[] array = param.Substring(param.IndexOf("|", StringComparison.Ordinal) + 1).Split(new char[]
				{
					'/'
				});
				if (this._localVariablesInts == null)
				{
					Debug.LogErrorFormat("This term {0} is trying to set int variable {1} but Variables connection is not connected in node.", new object[]
					{
						this._term.mTerm,
						num
					});
					return null;
				}
				if (this._localVariablesInts.Count <= num)
				{
					Debug.LogErrorFormat("This term {0} is trying to set int variable {1} which was not provided in node", new object[]
					{
						this._term.mTerm,
						num
					});
					return null;
				}
				if (array.Length <= this._localVariablesInts[num])
				{
					Debug.LogErrorFormat("This term {0} is trying to set option {1} which was not provided in node", new object[]
					{
						this._term.mTerm,
						this._localVariablesInts[num]
					});
					return null;
				}
				return array[this._localVariablesInts[num]];
			}
			else
			{
				if (!char.IsNumber(param[0]) || param.Contains("|"))
				{
					if (param.StartsWith("EXPEDITION"))
					{
						if (param == "EXPEDITION_CHARACTER_NAME")
						{
							return this._expeditionCharacter.StaticData.Name;
						}
						if (param == "EXPEDITION_CHARACTER_SURNAME")
						{
							return this._expeditionCharacter.StaticData.Surname;
						}
						if (param == "EXPEDITION_CHARACTER_FULL_NAME")
						{
							return this._expeditionCharacter.StaticData.Name + " " + this._expeditionCharacter.StaticData.Surname;
						}
						if (param.StartsWith("EXPEDITION_CHARACTER_SEX"))
						{
							return this.GetOptionBySex(param);
						}
					}
					else if (param.StartsWith("TERM_"))
					{
						if (this._terms.Count == 0)
						{
							return null;
						}
						int num2 = param.IndexOf("_", StringComparison.Ordinal) + 1;
						int num3 = Convert.ToInt32(param.Substring(num2, param.Length - num2));
						if (this._terms == null)
						{
							Debug.LogErrorFormat("This term {0} is trying to set term {1} but Terms connection is not connected in node", new object[]
							{
								this._term.mTerm,
								num3
							});
							return null;
						}
						if (this._terms.Count <= num3)
						{
							Debug.LogErrorFormat("This term {0} is trying to set term {1} which was not provided in node", new object[]
							{
								this._term.mTerm,
								num3
							});
							return null;
						}
						return this._terms[num3];
					}
					return null;
				}
				int num4 = Convert.ToInt32(param);
				if (this._localVariablesInts == null)
				{
					Debug.LogErrorFormat("This term {0} is trying to set int variable {1} but Variables connection is not connected in node.", new object[]
					{
						this._term.mTerm,
						num4
					});
					return null;
				}
				if (this._localVariablesInts.Count <= num4)
				{
					Debug.LogErrorFormat("This term {0} is trying to set int variable {1} which was not provided in node", new object[]
					{
						this._term.mTerm,
						num4
					});
					return null;
				}
				return this._localVariablesInts[num4].ToString();
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00071C34 File Offset: 0x0006FE34
		private string GetOptionBySex(string text)
		{
			string[] array = text.Substring(text.IndexOf("|", StringComparison.Ordinal) + 1).Split(new char[]
			{
				'/'
			});
			if (this._expeditionCharacter.StaticData.Female)
			{
				return array[0];
			}
			return array[1];
		}

		// Token: 0x040013FD RID: 5117
		[SerializeField]
		private string _pureText;

		// Token: 0x040013FE RID: 5118
		[SerializeField]
		private LocalizedString _term;

		// Token: 0x040013FF RID: 5119
		[SerializeField]
		private List<Character> _characters;

		// Token: 0x04001400 RID: 5120
		[SerializeField]
		private Character _expeditionCharacter;

		// Token: 0x04001401 RID: 5121
		[SerializeField]
		private List<IItem> _items;

		// Token: 0x04001402 RID: 5122
		[SerializeField]
		private List<int> _localVariablesInts;

		// Token: 0x04001403 RID: 5123
		[SerializeField]
		private List<LocalizedString> _terms;

		// Token: 0x04001404 RID: 5124
		[SerializeField]
		private EParsecsEventPhase _eventPhase;

		// Token: 0x04001405 RID: 5125
		private const string EXPEDITION_PREFIX = "EXPEDITION";

		// Token: 0x04001406 RID: 5126
		private const string EXPEDITION_CHARACTER_NAME_I2_LOC_PARAM = "EXPEDITION_CHARACTER_NAME";

		// Token: 0x04001407 RID: 5127
		private const string EXPEDITION_CHARACTER_SURNAME_I2_LOC_PARAM = "EXPEDITION_CHARACTER_SURNAME";

		// Token: 0x04001408 RID: 5128
		private const string EXPEDITION_CHARACTER_FULL_NAME_I2_LOC_PARAM = "EXPEDITION_CHARACTER_FULL_NAME";

		// Token: 0x04001409 RID: 5129
		private const string EXPEDITION_CHARACTER_SEX = "EXPEDITION_CHARACTER_SEX";

		// Token: 0x0400140A RID: 5130
		private const string TERM_PARAM = "TERM_";

		// Token: 0x0400140B RID: 5131
		private const string DIVIDE_CHAR = "|";
	}
}
