using System;
using System.Collections.Generic;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200025C RID: 604
	[Node(false, "Player Input/Display Choose Character List Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseCharacterListNodeV2 : PlayerDecisionNode
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0006207D File Offset: 0x0006027D
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseCharacterListNodeV2";
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00062084 File Offset: 0x00060284
		public override Node Create(Vector2 pos)
		{
			DisplayChooseCharacterListNodeV2 displayChooseCharacterListNodeV = ScriptableObject.CreateInstance<DisplayChooseCharacterListNodeV2>();
			displayChooseCharacterListNodeV.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseCharacterListNodeV.name = "Display Choose Character List";
			displayChooseCharacterListNodeV.CreateMutliInput("In", "Flow");
			displayChooseCharacterListNodeV.CreateInput("Character list", "CharacterList");
			displayChooseCharacterListNodeV.CreateInput("Call To Action", "LocalizedString");
			displayChooseCharacterListNodeV.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseCharacterListNodeV;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00062107 File Offset: 0x00060307
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0006212E File Offset: 0x0006032E
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00062130 File Offset: 0x00060330
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00062134 File Offset: 0x00060334
		public override void Execute(NodeCanvas canvas)
		{
			this._characters = base.GetInputValue<List<Character>>(this.Inputs[1], canvas);
			base.GetInputValue<LocalizedString>(this.Inputs[2], ref this._callToActionTerm, canvas);
			this._result.WasChosen = true;
			CharacterChoiceJournalContent content = null;
			if (this._characters.Count == 1)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					null,
					null,
					null
				}, this._callToActionTerm);
			}
			else if (this._characters.Count == 2)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					null,
					null
				}, this._callToActionTerm);
			}
			else if (this._characters.Count == 3)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					this._characters[2],
					null
				}, this._callToActionTerm);
			}
			else if (this._characters.Count == 4)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					this._characters[2],
					this._characters[3]
				}, this._callToActionTerm);
			}
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000622F4 File Offset: 0x000604F4
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (this._result.WasChosen)
			{
				ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
				if (playerChoice == null)
				{
					this._result.Result = null;
				}
				else if (playerChoice.GetCharacterValue() == this._characters[0])
				{
					this._result.Result = this._characters[0];
				}
				else if (playerChoice.GetCharacterValue() == this._characters[1])
				{
					this._result.Result = this._characters[1];
				}
				else if (playerChoice.GetCharacterValue() == this._characters[2])
				{
					this._result.Result = this._characters[2];
				}
				else if (playerChoice.GetCharacterValue() == this._characters[3])
				{
					this._result.Result = this._characters[3];
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F3A RID: 3898
		private const string ID = "EE_DisplayChooseCharacterListNodeV2";

		// Token: 0x04000F3B RID: 3899
		private const string INPUT_CHARACTER_LIST_NAME = "Character list";

		// Token: 0x04000F3C RID: 3900
		private const string INPUT_CALL_TO_ACTION_NAME = "Call To Action";

		// Token: 0x04000F3D RID: 3901
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F3E RID: 3902
		private const string NODE_NAME = "Display Choose Character List";

		// Token: 0x04000F3F RID: 3903
		private const int INPUT_CHARACTER_LIST_INDEX = 1;

		// Token: 0x04000F40 RID: 3904
		private const int INPUT_CALL_TO_ACTION_INDEX = 2;

		// Token: 0x04000F41 RID: 3905
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F42 RID: 3906
		private List<Character> _characters;

		// Token: 0x04000F43 RID: 3907
		[SerializeField]
		private PlayerCharacterDecision _result = new PlayerCharacterDecision();

		// Token: 0x04000F44 RID: 3908
		[SerializeField]
		private LocalizedString _callToActionTerm;
	}
}
