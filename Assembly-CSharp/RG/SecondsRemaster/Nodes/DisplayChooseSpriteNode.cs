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
	// Token: 0x02000261 RID: 609
	[Node(false, "Player Input/Display Choose Sprite Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseSpriteNode : PlayerDecisionNode
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0006326F File Offset: 0x0006146F
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseSpriteNode ";
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00063278 File Offset: 0x00061478
		public override Node Create(Vector2 pos)
		{
			DisplayChooseSpriteNode displayChooseSpriteNode = ScriptableObject.CreateInstance<DisplayChooseSpriteNode>();
			displayChooseSpriteNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseSpriteNode.name = "Display Choose Sprite";
			displayChooseSpriteNode.CreateMutliInput("In", "Flow");
			displayChooseSpriteNode.CreateInput("Choice 1", "ActionCondition");
			displayChooseSpriteNode.CreateInput("Choice 2", "ActionCondition");
			displayChooseSpriteNode.CreateInput("Choice 3", "ActionCondition");
			displayChooseSpriteNode.CreateInput("Choice 4", "ActionCondition");
			displayChooseSpriteNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseSpriteNode;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00063320 File Offset: 0x00061520
		public override Node Duplicate(Vector2 pos)
		{
			DisplayChooseSpriteNode displayChooseSpriteNode = (DisplayChooseSpriteNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayChooseSpriteNode._choice1 = this._choice1;
			displayChooseSpriteNode._choice2 = this._choice2;
			displayChooseSpriteNode._choice3 = this._choice3;
			displayChooseSpriteNode._choice4 = this._choice4;
			return displayChooseSpriteNode;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00063387 File Offset: 0x00061587
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00063389 File Offset: 0x00061589
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0006338C File Offset: 0x0006158C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<BaseActionCondition>(this.Inputs[1], ref this._choice1, canvas);
			base.GetInputValue<BaseActionCondition>(this.Inputs[2], ref this._choice2, canvas);
			base.GetInputValue<BaseActionCondition>(this.Inputs[3], ref this._choice3, canvas);
			base.GetInputValue<BaseActionCondition>(this.Inputs[4], ref this._choice4, canvas);
			this._result.WasChosen = true;
			SpriteChoiceJournalContent content = new SpriteChoiceJournalContent(new List<BaseActionCondition>
			{
				this._choice1,
				this._choice2,
				this._choice3,
				this._choice4
			});
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00063450 File Offset: 0x00061650
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
					this._result.ChoosenNumber = -1;
					this._result.Result = null;
				}
				else
				{
					this._result.ChoosenNumber = playerChoice.GetCardId();
					if (playerChoice.GetCardId() == 0)
					{
						this._result.Result = this._choice4;
						this._result.ChoosenNumber = 3;
					}
					else if (playerChoice.GetCardId() == 1)
					{
						this._result.Result = this._choice3;
						this._result.ChoosenNumber = 2;
					}
					else if (playerChoice.GetCardId() == 2)
					{
						this._result.Result = this._choice2;
						this._result.ChoosenNumber = 1;
					}
					else if (playerChoice.GetCardId() == 3)
					{
						this._result.Result = this._choice1;
						this._result.ChoosenNumber = 0;
					}
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F94 RID: 3988
		private const string ID = "EE_DisplayChooseSpriteNode ";

		// Token: 0x04000F95 RID: 3989
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F96 RID: 3990
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F97 RID: 3991
		private const string NODE_NAME = "Display Choose Sprite";

		// Token: 0x04000F98 RID: 3992
		private const string INPUT_CHOICE_1_NAME = "Choice 1";

		// Token: 0x04000F99 RID: 3993
		private const string INPUT_CHOICE_2_NAME = "Choice 2";

		// Token: 0x04000F9A RID: 3994
		private const string INPUT_CHOICE_3_NAME = "Choice 3";

		// Token: 0x04000F9B RID: 3995
		private const string INPUT_CHOICE_4_NAME = "Choice 4";

		// Token: 0x04000F9C RID: 3996
		private const int INPUT_CHOICE_1_INDEX = 1;

		// Token: 0x04000F9D RID: 3997
		private const int INPUT_CHOICE_2_INDEX = 2;

		// Token: 0x04000F9E RID: 3998
		private const int INPUT_CHOICE_3_INDEX = 3;

		// Token: 0x04000F9F RID: 3999
		private const int INPUT_CHOICE_4_INDEX = 4;

		// Token: 0x04000FA0 RID: 4000
		private const int CHOICE_1_CARD_ID = 0;

		// Token: 0x04000FA1 RID: 4001
		private const int CHOICE_2_CARD_ID = 1;

		// Token: 0x04000FA2 RID: 4002
		private const int CHOICE_3_CARD_ID = 2;

		// Token: 0x04000FA3 RID: 4003
		private const int CHOICE_4_CARD_ID = 3;

		// Token: 0x04000FA4 RID: 4004
		private const int NO_CHOICE = -1;

		// Token: 0x04000FA5 RID: 4005
		[SerializeField]
		private BaseActionCondition _choice1;

		// Token: 0x04000FA6 RID: 4006
		[SerializeField]
		private BaseActionCondition _choice2;

		// Token: 0x04000FA7 RID: 4007
		[SerializeField]
		private BaseActionCondition _choice3;

		// Token: 0x04000FA8 RID: 4008
		[SerializeField]
		private BaseActionCondition _choice4;

		// Token: 0x04000FA9 RID: 4009
		[SerializeField]
		private PlayerSpriteDecision _result = new PlayerSpriteDecision();
	}
}
