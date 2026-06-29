using System;
using System.Collections.Generic;
using System.Text;
using I2.Loc;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using TMPro;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000232 RID: 562
	public class RemasterCharacterTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x0005F810 File Offset: 0x0005DA10
		private void Awake()
		{
			if (this._characterStatusesVisibleInUI == null)
			{
				this._characterStatusesVisibleInUI = new Dictionary<Character, List<CharacterStatus>>();
			}
			this._eodListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "Reset", 999, this, true);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005F848 File Offset: 0x0005DA48
		private void OnDestroy()
		{
			this._eodListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "Reset");
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0005F866 File Offset: 0x0005DA66
		public override void HandleContent(TooltipContent content)
		{
			if (content as RemasterCharacterTooltipContent)
			{
				this.HandleCharacterTooltipContent((RemasterCharacterTooltipContent)content);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005F884 File Offset: 0x0005DA84
		private void HandleCharacterTooltipContent(RemasterCharacterTooltipContent content)
		{
			if (this._characterStatusesVisibleInUI == null)
			{
				this._characterStatusesVisibleInUI = new Dictionary<Character, List<CharacterStatus>>();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!this._characterStatusesVisibleInUI.ContainsKey(content.Character))
			{
				this._characterStatusesVisibleInUI.Add(content.Character, new List<CharacterStatus>());
				if (this._text != null && content.Character != null)
				{
					if (this._useFullCharacterName)
					{
						this._text.text = content.Character.StaticData.FullName;
					}
					else
					{
						this._text.text = content.Character.StaticData.Name;
					}
					if (content.Character.RuntimeData.CurrentStatuses != null)
					{
						if (this._specialHandlingOfDeadCharacterStatus && this._deadCharacterStatus != null && content.Character.RuntimeData.CurrentStatuses.Contains(this._deadCharacterStatus))
						{
							stringBuilder.AppendLine(string.Empty);
							stringBuilder.AppendLine(this._deadCharacterStatus.Name);
						}
						else
						{
							for (int i = 0; i < content.Character.RuntimeData.CurrentStatuses.Count; i++)
							{
								if (content.Character.RuntimeData.CurrentStatuses[i].IsVisibleInUI)
								{
									stringBuilder.AppendLine(string.Format("<color={1}>{0}</color>", content.Character.RuntimeData.CurrentStatuses[i].Name, this._statusInfoColor));
									this._characterStatusesVisibleInUI[content.Character].Add(content.Character.RuntimeData.CurrentStatuses[i]);
								}
							}
						}
					}
					this._additionalText.text = stringBuilder.ToString();
					this._additionalText.gameObject.GetComponent<Localize>().OnLocalize(false);
					this._text.gameObject.GetComponent<Localize>().OnLocalize(false);
				}
			}
			else if (this._text != null && this._additionalText != null)
			{
				if (this._useFullCharacterName)
				{
					this._text.text = content.Character.StaticData.FullName;
				}
				else
				{
					this._text.text = content.Character.StaticData.Name;
				}
				if (this._specialHandlingOfDeadCharacterStatus && this._deadCharacterStatus != null && this._characterStatusesVisibleInUI[content.Character].Contains(this._deadCharacterStatus))
				{
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.AppendLine(this._deadCharacterStatus.Name);
				}
				for (int j = 0; j < this._characterStatusesVisibleInUI[content.Character].Count; j++)
				{
					stringBuilder.AppendLine(string.Format("<color={1}>{0}</color>", this._characterStatusesVisibleInUI[content.Character][j].Name, this._statusInfoColor));
				}
				this._additionalText.text = stringBuilder.ToString();
				this._additionalText.gameObject.GetComponent<Localize>().OnLocalize(false);
				this._text.gameObject.GetComponent<Localize>().OnLocalize(false);
			}
			this._additionalText.gameObject.SetActive(!string.IsNullOrEmpty(this._additionalText.text));
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005FC18 File Offset: 0x0005DE18
		private void OnEndOfDay()
		{
			foreach (Character character in this._characterStatusesVisibleInUI.Keys)
			{
				this._characterStatusesVisibleInUI[character].Clear();
				for (int i = 0; i < character.RuntimeData.CurrentStatuses.Count; i++)
				{
					if (character.RuntimeData.CurrentStatuses[i].IsVisibleInUI)
					{
						this._characterStatusesVisibleInUI[character].Add(character.RuntimeData.CurrentStatuses[i]);
					}
				}
			}
		}

		// Token: 0x04000E85 RID: 3717
		[Tooltip("Text handler of tooltip.")]
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04000E86 RID: 3718
		[SerializeField]
		private TextMeshProUGUI _additionalText;

		// Token: 0x04000E87 RID: 3719
		[SerializeField]
		private bool _specialHandlingOfDeadCharacterStatus = true;

		// Token: 0x04000E88 RID: 3720
		[SerializeField]
		private bool _useFullCharacterName = true;

		// Token: 0x04000E89 RID: 3721
		[SerializeField]
		private LocalizedString _statusInfoColor;

		// Token: 0x04000E8A RID: 3722
		[SerializeField]
		private EndOfDayListenerList _eodListenerList;

		// Token: 0x04000E8B RID: 3723
		private const string ADDITIONAL_TOOLTIP_FORMAT = "<color={1}>{0}</color>";

		// Token: 0x04000E8C RID: 3724
		[Tooltip("The 'dead' character status. This needs to be set so that if the character is dead that will be the only status displayed")]
		[SerializeField]
		private CharacterStatus _deadCharacterStatus;

		// Token: 0x04000E8D RID: 3725
		private Dictionary<Character, List<CharacterStatus>> _characterStatusesVisibleInUI;
	}
}
