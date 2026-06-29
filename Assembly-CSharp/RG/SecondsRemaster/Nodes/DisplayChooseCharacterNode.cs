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
	// Token: 0x0200025D RID: 605
	[Node(true, "Player Input/Display Choose Character Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseCharacterNode : PlayerDecisionNode
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00062429 File Offset: 0x00060629
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseCharacterNode";
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00062430 File Offset: 0x00060630
		public override Node Create(Vector2 pos)
		{
			DisplayChooseCharacterNode displayChooseCharacterNode = ScriptableObject.CreateInstance<DisplayChooseCharacterNode>();
			displayChooseCharacterNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseCharacterNode.name = "Display Choose Character";
			displayChooseCharacterNode.CreateMutliInput("In", "Flow");
			displayChooseCharacterNode.CreateInput("Character 1", "Character");
			displayChooseCharacterNode.CreateInput("Character 2", "Character");
			displayChooseCharacterNode.CreateInput("Character 3", "Character");
			displayChooseCharacterNode.CreateInput("Character 4", "Character");
			displayChooseCharacterNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseCharacterNode;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000624D5 File Offset: 0x000606D5
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000624FC File Offset: 0x000606FC
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000624FE File Offset: 0x000606FE
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00062500 File Offset: 0x00060700
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Character>(this.Inputs[1], ref this._character1, canvas);
			base.GetInputValue<Character>(this.Inputs[2], ref this._character2, canvas);
			base.GetInputValue<Character>(this.Inputs[3], ref this._character3, canvas);
			base.GetInputValue<Character>(this.Inputs[4], ref this._character4, canvas);
			this._result.WasChosen = true;
			CharacterChoiceJournalContent content = new CharacterChoiceJournalContent(new List<Character>
			{
				this._character1,
				this._character2,
				this._character3,
				this._character4
			}, null);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000625CC File Offset: 0x000607CC
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
				else if (playerChoice.GetCharacterValue() == this._character1)
				{
					this._result.Result = this._character1;
				}
				else if (playerChoice.GetCharacterValue() == this._character2)
				{
					this._result.Result = this._character2;
				}
				else if (playerChoice.GetCharacterValue() == this._character3)
				{
					this._result.Result = this._character3;
				}
				else if (playerChoice.GetCharacterValue() == this._character4)
				{
					this._result.Result = this._character4;
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F45 RID: 3909
		private const string ID = "EE_DisplayChooseCharacterNode";

		// Token: 0x04000F46 RID: 3910
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F47 RID: 3911
		private const string NODE_NAME = "Display Choose Character";

		// Token: 0x04000F48 RID: 3912
		private const string INPUT_CHARACTER_1_NAME = "Character 1";

		// Token: 0x04000F49 RID: 3913
		private const string INPUT_CHARACTER_2_NAME = "Character 2";

		// Token: 0x04000F4A RID: 3914
		private const string INPUT_CHARACTER_3_NAME = "Character 3";

		// Token: 0x04000F4B RID: 3915
		private const string INPUT_CHARACTER_4_NAME = "Character 4";

		// Token: 0x04000F4C RID: 3916
		private const int INPUT_CHARACTER_1_INDEX = 1;

		// Token: 0x04000F4D RID: 3917
		private const int INPUT_CHARACTER_2_INDEX = 2;

		// Token: 0x04000F4E RID: 3918
		private const int INPUT_CHARACTER_3_INDEX = 3;

		// Token: 0x04000F4F RID: 3919
		private const int INPUT_CHARACTER_4_INDEX = 4;

		// Token: 0x04000F50 RID: 3920
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F51 RID: 3921
		[SerializeField]
		private Character _character1;

		// Token: 0x04000F52 RID: 3922
		[SerializeField]
		private Character _character2;

		// Token: 0x04000F53 RID: 3923
		[SerializeField]
		private Character _character3;

		// Token: 0x04000F54 RID: 3924
		[SerializeField]
		private Character _character4;

		// Token: 0x04000F55 RID: 3925
		[SerializeField]
		private PlayerCharacterDecision _result = new PlayerCharacterDecision();
	}
}
