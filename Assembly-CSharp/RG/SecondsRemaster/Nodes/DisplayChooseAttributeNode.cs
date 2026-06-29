using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200025A RID: 602
	[Node(false, "Player Input/Display Choose Attribute Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseAttributeNode : PlayerDecisionNode
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x000619E3 File Offset: 0x0005FBE3
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseAttributeNode";
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000619EC File Offset: 0x0005FBEC
		public override Node Create(Vector2 pos)
		{
			DisplayChooseAttributeNode displayChooseAttributeNode = ScriptableObject.CreateInstance<DisplayChooseAttributeNode>();
			displayChooseAttributeNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseAttributeNode.name = "Display Choose Attribute";
			displayChooseAttributeNode.CreateMutliInput("In", "Flow");
			displayChooseAttributeNode.CreateInput("Attribute 1", "Attributes");
			displayChooseAttributeNode.CreateInput("Attribute 2", "Attributes");
			displayChooseAttributeNode.CreateInput("Attribute 3", "Attributes");
			displayChooseAttributeNode.CreateInput("Attribute 4", "Attributes");
			displayChooseAttributeNode.CreateInput("Character", "Character");
			displayChooseAttributeNode.CreateInput("Is Team", "Bool");
			displayChooseAttributeNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseAttributeNode;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00061AB3 File Offset: 0x0005FCB3
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00061ADA File Offset: 0x0005FCDA
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00061ADC File Offset: 0x0005FCDC
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00061ADE File Offset: 0x0005FCDE
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00061AE0 File Offset: 0x0005FCE0
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<CharacterAttribute>(this.Inputs[1], ref this._attribute1, canvas);
			base.GetInputValue<CharacterAttribute>(this.Inputs[2], ref this._attribute2, canvas);
			base.GetInputValue<CharacterAttribute>(this.Inputs[3], ref this._attribute3, canvas);
			base.GetInputValue<CharacterAttribute>(this.Inputs[4], ref this._attribute4, canvas);
			base.GetInputValue<Character>(this.Inputs[5], ref this._character, canvas);
			base.GetInputValue<bool>(this.Inputs[6], ref this._isTeam, canvas);
			this._result.WasChosen = true;
			this._result.IsTeam = this._isTeam;
			this._result.Character = this._character;
			ParsecsEventManager.DisplayChoiceContent(this._isTeam ? null : this._character, this._attribute1, this._attribute2, this._attribute3, this._attribute4);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00061BE0 File Offset: 0x0005FDE0
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (this._result.WasChosen)
			{
				ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
				if (playerChoice.GetAttributeValue() == this._attribute1)
				{
					this._result.ChoosenNumber = 0;
					this._result.Result = this._attribute1;
				}
				else if (playerChoice.GetAttributeValue() == this._attribute2)
				{
					this._result.ChoosenNumber = 1;
					this._result.Result = this._attribute2;
				}
				else if (playerChoice.GetAttributeValue() == this._attribute3)
				{
					this._result.ChoosenNumber = 2;
					this._result.Result = this._attribute3;
				}
				else if (playerChoice.GetAttributeValue() == this._attribute4)
				{
					this._result.ChoosenNumber = 3;
					this._result.Result = this._attribute4;
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F21 RID: 3873
		private const string ID = "EE_DisplayChooseAttributeNode";

		// Token: 0x04000F22 RID: 3874
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F23 RID: 3875
		private const string NODE_NAME = "Display Choose Attribute";

		// Token: 0x04000F24 RID: 3876
		private const string INPUT_CHARACTER_NAME = "Character";

		// Token: 0x04000F25 RID: 3877
		private const string INPUT_IS_TEAM = "Is Team";

		// Token: 0x04000F26 RID: 3878
		private const string ATTRIBUTE_ONE_NAME = "Attribute 1";

		// Token: 0x04000F27 RID: 3879
		private const string ATTRIBUTE_TWO_NAME = "Attribute 2";

		// Token: 0x04000F28 RID: 3880
		private const string ATTRIBUTE_THREE_NAME = "Attribute 3";

		// Token: 0x04000F29 RID: 3881
		private const string ATTRIBUTE_FOUR_NAME = "Attribute 4";

		// Token: 0x04000F2A RID: 3882
		private const int CHARACTER_INPUT_INDEX = 5;

		// Token: 0x04000F2B RID: 3883
		[SerializeField]
		private CharacterAttribute _attribute1;

		// Token: 0x04000F2C RID: 3884
		[SerializeField]
		private CharacterAttribute _attribute2;

		// Token: 0x04000F2D RID: 3885
		[SerializeField]
		private CharacterAttribute _attribute3;

		// Token: 0x04000F2E RID: 3886
		[SerializeField]
		private CharacterAttribute _attribute4;

		// Token: 0x04000F2F RID: 3887
		[SerializeField]
		private Character _character;

		// Token: 0x04000F30 RID: 3888
		[SerializeField]
		private bool _isTeam;

		// Token: 0x04000F31 RID: 3889
		[SerializeField]
		private PlayerAttributeDecision _result = new PlayerAttributeDecision();
	}
}
