using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000267 RID: 615
	[Node(false, "Player Input/Get Item Result Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class GetItemResultOptionNode : PlayerDecisionNode
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00064252 File Offset: 0x00062452
		public override string GetID
		{
			get
			{
				return "EE_GetItemResultOptionNode";
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006425C File Offset: 0x0006245C
		public override Node Create(Vector2 pos)
		{
			GetItemResultOptionNode getItemResultOptionNode = ScriptableObject.CreateInstance<GetItemResultOptionNode>();
			getItemResultOptionNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			getItemResultOptionNode.name = "Get Item Result";
			getItemResultOptionNode.CreateMutliInput("In", "Flow");
			getItemResultOptionNode.CreateInput("Result", "PlayerDecision");
			getItemResultOptionNode.CreateInput("Item 1", "Item");
			getItemResultOptionNode.CreateInput("Use item", "Bool");
			getItemResultOptionNode.CreateInput("Item 2", "Item");
			getItemResultOptionNode.CreateInput("Use item", "Bool");
			getItemResultOptionNode.CreateInput("Item 3", "Item");
			getItemResultOptionNode.CreateInput("Use item", "Bool");
			getItemResultOptionNode.CreateOutput("Item 1", "Flow");
			getItemResultOptionNode.CreateOutput("Item 2", "Flow");
			getItemResultOptionNode.CreateOutput("Item 3", "Flow");
			getItemResultOptionNode.CreateOutput("No Choice", "Flow");
			getItemResultOptionNode.CreateOutput("Item", "Item");
			return getItemResultOptionNode;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00064377 File Offset: 0x00062577
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0006439E File Offset: 0x0006259E
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000643A0 File Offset: 0x000625A0
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000643A2 File Offset: 0x000625A2
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000643A4 File Offset: 0x000625A4
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
			base.GetInputValue<bool>(this.Inputs[3], ref this._useItem1, canvas);
			base.GetInputValue<bool>(this.Inputs[5], ref this._useItem2, canvas);
			base.GetInputValue<bool>(this.Inputs[7], ref this._useItem3, canvas);
			if (base.ParentEvent is SurvivalEvent)
			{
				((SurvivalEvent)base.ParentEvent).WasEventSuccessful = (result != null);
			}
			if (result == null)
			{
				if (!this.Outputs[3].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 3);
				}
				this.Outputs[3].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
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
			else
			{
				if (!(result == this._item3))
				{
					throw new WrongDecisionConnectionExcpetion(this.GetID);
				}
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
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00064608 File Offset: 0x00062808
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

		// Token: 0x060016D7 RID: 5847 RVA: 0x00064688 File Offset: 0x00062888
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 4)
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

		// Token: 0x04000FD7 RID: 4055
		private const string ID = "EE_GetItemResultOptionNode";

		// Token: 0x04000FD8 RID: 4056
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04000FD9 RID: 4057
		private const string INPUT_OUTPUT_ITEM_1_NAME = "Item 1";

		// Token: 0x04000FDA RID: 4058
		private const string INPUT_OUTPUT_ITEM_2_NAME = "Item 2";

		// Token: 0x04000FDB RID: 4059
		private const string INPUT_OUTPUT_ITEM_3_NAME = "Item 3";

		// Token: 0x04000FDC RID: 4060
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x04000FDD RID: 4061
		private const string OUTPUT_ITEM_NAME = "Item";

		// Token: 0x04000FDE RID: 4062
		private const string NODE_NAME = "Get Item Result";

		// Token: 0x04000FDF RID: 4063
		private const string USE_ITEM_NAME = "Use item";

		// Token: 0x04000FE0 RID: 4064
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x04000FE1 RID: 4065
		private const int INPUT_ITEM_1_INDEX = 2;

		// Token: 0x04000FE2 RID: 4066
		private const int INPUT_USE_ITEM_1_INDEX = 3;

		// Token: 0x04000FE3 RID: 4067
		private const int INPUT_ITEM_2_INDEX = 4;

		// Token: 0x04000FE4 RID: 4068
		private const int INPUT_USE_ITEM_2_INDEX = 5;

		// Token: 0x04000FE5 RID: 4069
		private const int INPUT_ITEM_3_INDEX = 6;

		// Token: 0x04000FE6 RID: 4070
		private const int INPUT_USE_ITEM_3_INDEX = 7;

		// Token: 0x04000FE7 RID: 4071
		private const int OUTPUT_ITEM_1_INDEX = 0;

		// Token: 0x04000FE8 RID: 4072
		private const int OUTPUT_ITEM_2_INDEX = 1;

		// Token: 0x04000FE9 RID: 4073
		private const int OUTPUT_ITEM_3_INDEX = 2;

		// Token: 0x04000FEA RID: 4074
		private const int OUTPUT_NO_CHOICE_INDEX = 3;

		// Token: 0x04000FEB RID: 4075
		private const int OUTPUT_ITEM_INDEX = 4;

		// Token: 0x04000FEC RID: 4076
		[SerializeField]
		private PlayerDecision _currentDecision;

		// Token: 0x04000FED RID: 4077
		[SerializeField]
		private IItem _item1;

		// Token: 0x04000FEE RID: 4078
		[SerializeField]
		private bool _useItem1 = true;

		// Token: 0x04000FEF RID: 4079
		[SerializeField]
		private IItem _item2;

		// Token: 0x04000FF0 RID: 4080
		[SerializeField]
		private bool _useItem2 = true;

		// Token: 0x04000FF1 RID: 4081
		[SerializeField]
		private IItem _item3;

		// Token: 0x04000FF2 RID: 4082
		[SerializeField]
		private bool _useItem3 = true;
	}
}
