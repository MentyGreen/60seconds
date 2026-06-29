using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000280 RID: 640
	[Node(false, "Remaster/Player Input/Get Item Result Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SRGetItemResultOptionNode : PlayerDecisionNode
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0006771E File Offset: 0x0006591E
		public override string GetID
		{
			get
			{
				return "EE_SRGetItemResultOptionNode";
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00067728 File Offset: 0x00065928
		public override Node Create(Vector2 pos)
		{
			SRGetItemResultOptionNode srgetItemResultOptionNode = ScriptableObject.CreateInstance<SRGetItemResultOptionNode>();
			srgetItemResultOptionNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			srgetItemResultOptionNode.name = "Get Item Result";
			srgetItemResultOptionNode.CreateMutliInput("In", "Flow");
			srgetItemResultOptionNode.CreateInput("Result", "PlayerDecision");
			srgetItemResultOptionNode.CreateInput("Item 1", "Item");
			srgetItemResultOptionNode.CreateInput("Use item", "Bool");
			srgetItemResultOptionNode.CreateInput("Item 2", "Item");
			srgetItemResultOptionNode.CreateInput("Use item", "Bool");
			srgetItemResultOptionNode.CreateInput("Item 3", "Item");
			srgetItemResultOptionNode.CreateInput("Use item", "Bool");
			srgetItemResultOptionNode.CreateInput("Item 4", "Item");
			srgetItemResultOptionNode.CreateInput("Use item", "Bool");
			srgetItemResultOptionNode.CreateOutput("Item 1", "Flow");
			srgetItemResultOptionNode.CreateOutput("Item 2", "Flow");
			srgetItemResultOptionNode.CreateOutput("Item 3", "Flow");
			srgetItemResultOptionNode.CreateOutput("Item 4", "Flow");
			srgetItemResultOptionNode.CreateOutput("No Choice", "Flow");
			srgetItemResultOptionNode.CreateOutput("Item", "Item");
			return srgetItemResultOptionNode;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00067876 File Offset: 0x00065A76
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0006789D File Offset: 0x00065A9D
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0006789F File Offset: 0x00065A9F
		protected override void NodeEnable()
		{
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000678A1 File Offset: 0x00065AA1
		protected override void NodeGUI()
		{
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000678A4 File Offset: 0x00065AA4
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<PlayerDecision>(this.Inputs[1], ref this._currentDecision, canvas);
			if (!(this._currentDecision is PlayerItemDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerItemDecision), this._currentDecision.GetType());
			}
			IItem result = ((PlayerItemDecision)this._currentDecision).Result;
			base.GetInputValue<IItem>(this.Inputs[2], ref this._item1, canvas);
			base.GetInputValue<IItem>(this.Inputs[4], ref this._item2, canvas);
			base.GetInputValue<IItem>(this.Inputs[6], ref this._item3, canvas);
			base.GetInputValue<IItem>(this.Inputs[8], ref this._item4, canvas);
			base.GetInputValue<bool>(this.Inputs[3], ref this._useItem1, canvas);
			base.GetInputValue<bool>(this.Inputs[5], ref this._useItem2, canvas);
			base.GetInputValue<bool>(this.Inputs[7], ref this._useItem3, canvas);
			base.GetInputValue<bool>(this.Inputs[9], ref this._useItem4, canvas);
			if (base.ParentEvent is SurvivalEvent)
			{
				((SurvivalEvent)base.ParentEvent).WasEventSuccessful = (result != null);
			}
			if (result == null)
			{
				if (!this.Outputs[4].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 4);
				}
				this.Outputs[4].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._item1)
			{
				if (this._useItem1)
				{
					this.UseItem(result);
				}
				if (!this.Outputs[0].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 0);
				}
				this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._item2)
			{
				if (this._useItem2)
				{
					this.UseItem(result);
				}
				if (!this.Outputs[1].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 1);
				}
				this.Outputs[1].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._item3)
			{
				if (this._useItem3)
				{
					this.UseItem(result);
				}
				if (!this.Outputs[2].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 2);
				}
				this.Outputs[2].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else
			{
				if (!(result == this._item4))
				{
					throw new WrongDecisionConnectionExcpetion(this.GetID);
				}
				if (this._useItem4)
				{
					this.UseItem(result);
				}
				if (!this.Outputs[3].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 3);
				}
				this.Outputs[3].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00067B90 File Offset: 0x00065D90
		private void UseItem(IItem chosenItem)
		{
			if (!chosenItem.IsDamaged())
			{
				Item item = chosenItem as Item;
				bool isAvailable = chosenItem.BaseRuntimeData.IsAvailable;
				if (item != null)
				{
					item.UseItem(ItemManager.ITEM_DURABAILITY_USAGE);
				}
				else
				{
					chosenItem.Use();
				}
				if (chosenItem.IsDamaged() || (!chosenItem.BaseRuntimeData.IsAvailable && isAvailable))
				{
					TextIconJournalContent content = new TextIconJournalContent(chosenItem.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
					SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
				}
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00067C10 File Offset: 0x00065E10
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 5)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (!(this._currentDecision is PlayerItemDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerItemDecision), this._currentDecision.GetType());
			}
			IItem result = ((PlayerItemDecision)this._currentDecision).Result;
			return base.CastValue<T>(result);
		}

		// Token: 0x04001116 RID: 4374
		private const string ID = "EE_SRGetItemResultOptionNode";

		// Token: 0x04001117 RID: 4375
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04001118 RID: 4376
		private const string INPUT_OUTPUT_ITEM_1_NAME = "Item 1";

		// Token: 0x04001119 RID: 4377
		private const string INPUT_OUTPUT_ITEM_2_NAME = "Item 2";

		// Token: 0x0400111A RID: 4378
		private const string INPUT_OUTPUT_ITEM_3_NAME = "Item 3";

		// Token: 0x0400111B RID: 4379
		private const string INPUT_OUTPUT_ITEM_4_NAME = "Item 4";

		// Token: 0x0400111C RID: 4380
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x0400111D RID: 4381
		private const string OUTPUT_ITEM_NAME = "Item";

		// Token: 0x0400111E RID: 4382
		private const string NODE_NAME = "Get Item Result";

		// Token: 0x0400111F RID: 4383
		private const string USE_ITEM_NAME = "Use item";

		// Token: 0x04001120 RID: 4384
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x04001121 RID: 4385
		private const int INPUT_ITEM_1_INDEX = 2;

		// Token: 0x04001122 RID: 4386
		private const int INPUT_USE_ITEM_1_INDEX = 3;

		// Token: 0x04001123 RID: 4387
		private const int INPUT_ITEM_2_INDEX = 4;

		// Token: 0x04001124 RID: 4388
		private const int INPUT_USE_ITEM_2_INDEX = 5;

		// Token: 0x04001125 RID: 4389
		private const int INPUT_ITEM_3_INDEX = 6;

		// Token: 0x04001126 RID: 4390
		private const int INPUT_USE_ITEM_3_INDEX = 7;

		// Token: 0x04001127 RID: 4391
		private const int INPUT_ITEM_4_INDEX = 8;

		// Token: 0x04001128 RID: 4392
		private const int INPUT_USE_ITEM_4_INDEX = 9;

		// Token: 0x04001129 RID: 4393
		private const int OUTPUT_ITEM_1_INDEX = 0;

		// Token: 0x0400112A RID: 4394
		private const int OUTPUT_ITEM_2_INDEX = 1;

		// Token: 0x0400112B RID: 4395
		private const int OUTPUT_ITEM_3_INDEX = 2;

		// Token: 0x0400112C RID: 4396
		private const int OUTPUT_ITEM_4_INDEX = 3;

		// Token: 0x0400112D RID: 4397
		private const int OUTPUT_NO_CHOICE_INDEX = 4;

		// Token: 0x0400112E RID: 4398
		private const int OUTPUT_ITEM_INDEX = 5;

		// Token: 0x0400112F RID: 4399
		[SerializeField]
		private PlayerDecision _currentDecision;

		// Token: 0x04001130 RID: 4400
		[SerializeField]
		private IItem _item1;

		// Token: 0x04001131 RID: 4401
		[SerializeField]
		private bool _useItem1 = true;

		// Token: 0x04001132 RID: 4402
		[SerializeField]
		private IItem _item2;

		// Token: 0x04001133 RID: 4403
		[SerializeField]
		private bool _useItem2 = true;

		// Token: 0x04001134 RID: 4404
		[SerializeField]
		private IItem _item3;

		// Token: 0x04001135 RID: 4405
		[SerializeField]
		private bool _useItem3 = true;

		// Token: 0x04001136 RID: 4406
		[SerializeField]
		private IItem _item4;

		// Token: 0x04001137 RID: 4407
		[SerializeField]
		private bool _useItem4 = true;
	}
}
