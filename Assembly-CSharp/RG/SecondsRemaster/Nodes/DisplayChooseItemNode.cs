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
	// Token: 0x0200025F RID: 607
	[Node(false, "Player Input/Display Choose Item Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseItemNode : PlayerDecisionNode
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000629EE File Offset: 0x00060BEE
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseItemNodeLegacy";
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000629F8 File Offset: 0x00060BF8
		public override Node Create(Vector2 pos)
		{
			DisplayChooseItemNode displayChooseItemNode = ScriptableObject.CreateInstance<DisplayChooseItemNode>();
			displayChooseItemNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseItemNode.name = "Display Choose Item";
			displayChooseItemNode.CreateMutliInput("In", "Flow");
			displayChooseItemNode.CreateInput("Item 1", "Item");
			displayChooseItemNode.CreateInput("Item 2", "Item");
			displayChooseItemNode.CreateInput("Item 3", "Item");
			displayChooseItemNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseItemNode;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00062A8C File Offset: 0x00060C8C
		public override Node Duplicate(Vector2 pos)
		{
			DisplayChooseItemNode displayChooseItemNode = (DisplayChooseItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayChooseItemNode._item1 = this._item1;
			displayChooseItemNode._item2 = this._item2;
			displayChooseItemNode._item3 = this._item3;
			return displayChooseItemNode;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00062AE7 File Offset: 0x00060CE7
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00062AE9 File Offset: 0x00060CE9
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00062AEC File Offset: 0x00060CEC
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item1, canvas);
			base.GetInputValue<IItem>(this.Inputs[2], ref this._item2, canvas);
			base.GetInputValue<IItem>(this.Inputs[3], ref this._item3, canvas);
			this._result.WasChosen = true;
			ItemChoiceJournalContent content = new ItemChoiceJournalContent(new List<IItem>
			{
				this._item1,
				this._item2,
				this._item3,
				null
			});
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00062B94 File Offset: 0x00060D94
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (this._result.WasChosen)
			{
				ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
				if (playerChoice == null || playerChoice.GetChoiceType() == EPlayerChoice.NO_CHOICE)
				{
					this._result.Result = null;
				}
				else if (playerChoice.GetItemValue() == this._item1)
				{
					this._result.ChoosenNumber = 1;
					this._result.Result = this._item1;
				}
				else if (playerChoice.GetItemValue() == this._item2)
				{
					this._result.ChoosenNumber = 2;
					this._result.Result = this._item2;
				}
				else if (playerChoice.GetItemValue() == this._item3)
				{
					this._result.ChoosenNumber = 3;
					this._result.Result = this._item3;
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F6A RID: 3946
		private const string ID = "EE_DisplayChooseItemNodeLegacy";

		// Token: 0x04000F6B RID: 3947
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F6C RID: 3948
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F6D RID: 3949
		private const string NODE_NAME = "Display Choose Item";

		// Token: 0x04000F6E RID: 3950
		private const string INPUT_ITEM_1_NAME = "Item 1";

		// Token: 0x04000F6F RID: 3951
		private const string INPUT_ITEM_2_NAME = "Item 2";

		// Token: 0x04000F70 RID: 3952
		private const string INPUT_ITEM_3_NAME = "Item 3";

		// Token: 0x04000F71 RID: 3953
		private const int INPUT_ITEM_1_INDEX = 1;

		// Token: 0x04000F72 RID: 3954
		private const int INPUT_ITEM_2_INDEX = 2;

		// Token: 0x04000F73 RID: 3955
		private const int INPUT_ITEM_3_INDEX = 3;

		// Token: 0x04000F74 RID: 3956
		[SerializeField]
		private IItem _item1;

		// Token: 0x04000F75 RID: 3957
		[SerializeField]
		private IItem _item2;

		// Token: 0x04000F76 RID: 3958
		[SerializeField]
		private IItem _item3;

		// Token: 0x04000F77 RID: 3959
		[SerializeField]
		private PlayerItemDecision _result = new PlayerItemDecision();
	}
}
