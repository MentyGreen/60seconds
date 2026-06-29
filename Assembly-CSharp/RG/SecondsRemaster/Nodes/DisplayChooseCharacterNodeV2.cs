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
	// Token: 0x0200025E RID: 606
	[Node(false, "Player Input/Display Choose Character Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseCharacterNodeV2 : PlayerDecisionNode
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000626CE File Offset: 0x000608CE
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseCharacterNode";
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000626D8 File Offset: 0x000608D8
		public override Node Create(Vector2 pos)
		{
			DisplayChooseCharacterNodeV2 displayChooseCharacterNodeV = ScriptableObject.CreateInstance<DisplayChooseCharacterNodeV2>();
			displayChooseCharacterNodeV.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseCharacterNodeV.name = "Display Choose Character";
			displayChooseCharacterNodeV.CreateMutliInput("In", "Flow");
			displayChooseCharacterNodeV.CreateInput("Character 1", "Character");
			displayChooseCharacterNodeV.CreateInput("Character 2", "Character");
			displayChooseCharacterNodeV.CreateInput("Character 3", "Character");
			displayChooseCharacterNodeV.CreateInput("Character 4", "Character");
			displayChooseCharacterNodeV.CreateInput("Call To Action", "LocalizedString");
			displayChooseCharacterNodeV.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseCharacterNodeV;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00062790 File Offset: 0x00060990
		public override Node Duplicate(Vector2 pos)
		{
			DisplayChooseCharacterNodeV2 displayChooseCharacterNodeV = (DisplayChooseCharacterNodeV2)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayChooseCharacterNodeV._character1 = this._character1;
			displayChooseCharacterNodeV._character2 = this._character2;
			displayChooseCharacterNodeV._character3 = this._character3;
			displayChooseCharacterNodeV._character4 = this._character4;
			displayChooseCharacterNodeV._callToActionTerm = this._callToActionTerm;
			return displayChooseCharacterNodeV;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00062803 File Offset: 0x00060A03
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00062805 File Offset: 0x00060A05
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00062808 File Offset: 0x00060A08
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Character>(this.Inputs[1], ref this._character1, canvas);
			base.GetInputValue<Character>(this.Inputs[2], ref this._character2, canvas);
			base.GetInputValue<Character>(this.Inputs[3], ref this._character3, canvas);
			base.GetInputValue<Character>(this.Inputs[4], ref this._character4, canvas);
			base.GetInputValue<LocalizedString>(this.Inputs[5], ref this._callToActionTerm, canvas);
			this._result.WasChosen = true;
			CharacterChoiceJournalContent content = new CharacterChoiceJournalContent(new List<Character>
			{
				this._character1,
				this._character2,
				this._character3,
				this._character4
			}, this._callToActionTerm);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000628EC File Offset: 0x00060AEC
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

		// Token: 0x04000F56 RID: 3926
		private const string ID = "EE_DisplayChooseCharacterNode";

		// Token: 0x04000F57 RID: 3927
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F58 RID: 3928
		private const string NODE_NAME = "Display Choose Character";

		// Token: 0x04000F59 RID: 3929
		private const string INPUT_CHARACTER_1_NAME = "Character 1";

		// Token: 0x04000F5A RID: 3930
		private const string INPUT_CHARACTER_2_NAME = "Character 2";

		// Token: 0x04000F5B RID: 3931
		private const string INPUT_CHARACTER_3_NAME = "Character 3";

		// Token: 0x04000F5C RID: 3932
		private const string INPUT_CHARACTER_4_NAME = "Character 4";

		// Token: 0x04000F5D RID: 3933
		private const string INPUT_CALL_TO_ACTION_NAME = "Call To Action";

		// Token: 0x04000F5E RID: 3934
		private const int INPUT_CHARACTER_1_INDEX = 1;

		// Token: 0x04000F5F RID: 3935
		private const int INPUT_CHARACTER_2_INDEX = 2;

		// Token: 0x04000F60 RID: 3936
		private const int INPUT_CHARACTER_3_INDEX = 3;

		// Token: 0x04000F61 RID: 3937
		private const int INPUT_CHARACTER_4_INDEX = 4;

		// Token: 0x04000F62 RID: 3938
		private const int INPUT_CALL_TO_ACTION_INDEX = 5;

		// Token: 0x04000F63 RID: 3939
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F64 RID: 3940
		[SerializeField]
		private Character _character1;

		// Token: 0x04000F65 RID: 3941
		[SerializeField]
		private Character _character2;

		// Token: 0x04000F66 RID: 3942
		[SerializeField]
		private Character _character3;

		// Token: 0x04000F67 RID: 3943
		[SerializeField]
		private Character _character4;

		// Token: 0x04000F68 RID: 3944
		[SerializeField]
		private PlayerCharacterDecision _result = new PlayerCharacterDecision();

		// Token: 0x04000F69 RID: 3945
		[SerializeField]
		private LocalizedString _callToActionTerm;
	}
}
