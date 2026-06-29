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
	// Token: 0x0200027E RID: 638
	[Node(false, "Remaster/Player Input/Display Choose Item Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SRDisplayChooseItemNode : PlayerDecisionNode
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0006706C File Offset: 0x0006526C
		public override string GetID
		{
			get
			{
				return "EE_SRDisplayChooseItemNode";
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00067074 File Offset: 0x00065274
		public override Node Create(Vector2 pos)
		{
			SRDisplayChooseItemNode srdisplayChooseItemNode = ScriptableObject.CreateInstance<SRDisplayChooseItemNode>();
			srdisplayChooseItemNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			srdisplayChooseItemNode.name = "Display Choose Item";
			srdisplayChooseItemNode.CreateMutliInput("In", "Flow");
			srdisplayChooseItemNode.CreateInput("Item 1", "Item");
			srdisplayChooseItemNode.CreateInput("Item 2", "Item");
			srdisplayChooseItemNode.CreateInput("Item 3", "Item");
			srdisplayChooseItemNode.CreateInput("Item 4", "Item");
			srdisplayChooseItemNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return srdisplayChooseItemNode;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0006711C File Offset: 0x0006531C
		public override Node Duplicate(Vector2 pos)
		{
			SRDisplayChooseItemNode srdisplayChooseItemNode = (SRDisplayChooseItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			srdisplayChooseItemNode._item1 = this._item1;
			srdisplayChooseItemNode._item2 = this._item2;
			srdisplayChooseItemNode._item3 = this._item3;
			srdisplayChooseItemNode._item3 = this._item4;
			return srdisplayChooseItemNode;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00067183 File Offset: 0x00065383
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00067185 File Offset: 0x00065385
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00067188 File Offset: 0x00065388
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item1, canvas);
			base.GetInputValue<IItem>(this.Inputs[2], ref this._item2, canvas);
			base.GetInputValue<IItem>(this.Inputs[3], ref this._item3, canvas);
			base.GetInputValue<IItem>(this.Inputs[4], ref this._item4, canvas);
			this._result.WasChosen = true;
			ItemChoiceJournalContent content = new ItemChoiceJournalContent(new List<IItem>
			{
				this._item1,
				this._item2,
				this._item3,
				this._item4
			});
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0006724C File Offset: 0x0006544C
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
					this._result.ChoosenNumber = 0;
					this._result.Result = this._item1;
				}
				else if (playerChoice.GetItemValue() == this._item2)
				{
					this._result.ChoosenNumber = 1;
					this._result.Result = this._item2;
				}
				else if (playerChoice.GetItemValue() == this._item3)
				{
					this._result.ChoosenNumber = 2;
					this._result.Result = this._item3;
				}
				else if (playerChoice.GetItemValue() == this._item4)
				{
					this._result.ChoosenNumber = 3;
					this._result.Result = this._item4;
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x040010F2 RID: 4338
		private const string ID = "EE_SRDisplayChooseItemNode";

		// Token: 0x040010F3 RID: 4339
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x040010F4 RID: 4340
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x040010F5 RID: 4341
		private const string NODE_NAME = "Display Choose Item";

		// Token: 0x040010F6 RID: 4342
		private const string INPUT_ITEM_1_NAME = "Item 1";

		// Token: 0x040010F7 RID: 4343
		private const string INPUT_ITEM_2_NAME = "Item 2";

		// Token: 0x040010F8 RID: 4344
		private const string INPUT_ITEM_3_NAME = "Item 3";

		// Token: 0x040010F9 RID: 4345
		private const string INPUT_ITEM_4_NAME = "Item 4";

		// Token: 0x040010FA RID: 4346
		private const int INPUT_ITEM_1_INDEX = 1;

		// Token: 0x040010FB RID: 4347
		private const int INPUT_ITEM_2_INDEX = 2;

		// Token: 0x040010FC RID: 4348
		private const int INPUT_ITEM_3_INDEX = 3;

		// Token: 0x040010FD RID: 4349
		private const int INPUT_ITEM_4_INDEX = 4;

		// Token: 0x040010FE RID: 4350
		[SerializeField]
		private IItem _item1;

		// Token: 0x040010FF RID: 4351
		[SerializeField]
		private IItem _item2;

		// Token: 0x04001100 RID: 4352
		[SerializeField]
		private IItem _item3;

		// Token: 0x04001101 RID: 4353
		[SerializeField]
		private IItem _item4;

		// Token: 0x04001102 RID: 4354
		[SerializeField]
		private PlayerItemDecision _result = new PlayerItemDecision();
	}
}
