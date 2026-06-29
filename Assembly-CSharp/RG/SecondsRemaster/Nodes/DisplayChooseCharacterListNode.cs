using System;
using System.Collections.Generic;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200025B RID: 603
	[Node(true, "Player Input/Display Choose Character List Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseCharacterListNode : PlayerDecisionNode
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00061CFB File Offset: 0x0005FEFB
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseCharacterListNode";
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00061D04 File Offset: 0x0005FF04
		public override Node Create(Vector2 pos)
		{
			DisplayChooseCharacterListNode displayChooseCharacterListNode = ScriptableObject.CreateInstance<DisplayChooseCharacterListNode>();
			displayChooseCharacterListNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseCharacterListNode.name = "Display Choose Character List";
			displayChooseCharacterListNode.CreateMutliInput("In", "Flow");
			displayChooseCharacterListNode.CreateInput("Character list", "CharacterList");
			displayChooseCharacterListNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseCharacterListNode;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00061D76 File Offset: 0x0005FF76
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00061D9D File Offset: 0x0005FF9D
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		public override void Execute(NodeCanvas canvas)
		{
			this._characters = base.GetInputValue<List<Character>>(this.Inputs[1], canvas);
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
				}, null);
			}
			else if (this._characters.Count == 2)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					null,
					null
				}, null);
			}
			else if (this._characters.Count == 3)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					this._characters[2],
					null
				}, null);
			}
			else if (this._characters.Count == 4)
			{
				content = new CharacterChoiceJournalContent(new List<Character>
				{
					this._characters[0],
					this._characters[1],
					this._characters[2],
					this._characters[3]
				}, null);
			}
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00061F48 File Offset: 0x00060148
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

		// Token: 0x04000F32 RID: 3890
		private const string ID = "EE_DisplayChooseCharacterListNode";

		// Token: 0x04000F33 RID: 3891
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F34 RID: 3892
		private const string NODE_NAME = "Display Choose Character List";

		// Token: 0x04000F35 RID: 3893
		private const int INPUT_CHARACTER_LIST_INDEX = 1;

		// Token: 0x04000F36 RID: 3894
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F37 RID: 3895
		private const string INPUT_CHARACTER_LIST_NAME = "Character list";

		// Token: 0x04000F38 RID: 3896
		private List<Character> _characters;

		// Token: 0x04000F39 RID: 3897
		[SerializeField]
		private PlayerCharacterDecision _result = new PlayerCharacterDecision();
	}
}
