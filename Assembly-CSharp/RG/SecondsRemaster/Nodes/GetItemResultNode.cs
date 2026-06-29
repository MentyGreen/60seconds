using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000266 RID: 614
	[Node(true, "Legacy/Get Item Result Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class GetItemResultNode : PlayerDecisionNode
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00063D0D File Offset: 0x00061F0D
		public override string GetID
		{
			get
			{
				return "EE_GetItemResultNode";
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00063D14 File Offset: 0x00061F14
		public override Node Create(Vector2 pos)
		{
			GetItemResultNode getItemResultNode = ScriptableObject.CreateInstance<GetItemResultNode>();
			getItemResultNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			getItemResultNode.name = "Get Item Result";
			getItemResultNode.CreateMutliInput("In", "Flow");
			getItemResultNode.CreateInput("Result", "PlayerDecision");
			getItemResultNode.CreateInput("Item 1", "Item");
			getItemResultNode.CreateInput("Item 2", "Item");
			getItemResultNode.CreateInput("Item 3", "Item");
			getItemResultNode.CreateOutput("Item 1", "Flow");
			getItemResultNode.CreateOutput("Item 2", "Flow");
			getItemResultNode.CreateOutput("Item 3", "Flow");
			getItemResultNode.CreateOutput("No Choice", "Flow");
			getItemResultNode.CreateOutput("Item", "Item");
			return getItemResultNode;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00063DFC File Offset: 0x00061FFC
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00063E24 File Offset: 0x00062024
		protected override void OnNodeValidate()
		{
			base.OnNodeValidate();
			if (!this.Inputs[1].isConnected)
			{
				base.LogMessage(string.Format("Result input is not connected: {0}", this.GetID), Node.EMessageType.ERROR);
			}
			if (this.Outputs[0].isConnected && !this.Inputs[2].isConnected && this._item1 == null)
			{
				base.LogMessage(string.Format("Item 1 Output is connected but Item 1 Input is not or it's value is null in node: {0}", this.GetID), Node.EMessageType.ERROR);
			}
			if (this.Outputs[1].isConnected && !this.Inputs[3].isConnected && this._item2 == null)
			{
				base.LogMessage(string.Format("Item 2 Output is connected but Item 2 Input is not or it's value is null in node: {0}", this.GetID), Node.EMessageType.ERROR);
			}
			if (this.Outputs[2].isConnected && !this.Inputs[4].isConnected && this._item3 == null)
			{
				base.LogMessage(string.Format("Item 3 Output is connected but Item 3 Input is not or it's value is null in node: {0}", this.GetID), Node.EMessageType.ERROR);
			}
			if (!this.Outputs[3].isConnected)
			{
				base.LogMessage(string.Format("No Choice output is not connected: {0}", this.GetID), Node.EMessageType.ERROR);
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00063F6C File Offset: 0x0006216C
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00063F6E File Offset: 0x0006216E
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00063F70 File Offset: 0x00062170
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<PlayerDecision>(this.Inputs[1], ref this._currentDecision, canvas);
			if (!(this._currentDecision is PlayerItemDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerItemDecision), this._currentDecision.GetType());
			}
			IItem result = ((PlayerItemDecision)this._currentDecision).Result;
			base.GetInputValue<IItem>(this.Inputs[2], ref this._item1, canvas);
			base.GetInputValue<IItem>(this.Inputs[3], ref this._item2, canvas);
			base.GetInputValue<IItem>(this.Inputs[4], ref this._item3, canvas);
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
			else
			{
				bool flag = result.IsDamaged();
				Item item = result as Item;
				if (item != null)
				{
					item.UseItem(ItemManager.ITEM_DURABAILITY_USAGE);
				}
				else
				{
					result.Use();
				}
				if (!flag)
				{
					bool isAvailable = result.BaseRuntimeData.IsAvailable;
					if (item != null)
					{
						item.UseItem(ItemManager.ITEM_DURABAILITY_USAGE);
					}
					else
					{
						result.Use();
					}
					if (result.IsDamaged() || (!result.BaseRuntimeData.IsAvailable && isAvailable))
					{
						TextIconJournalContent content = new TextIconJournalContent(result.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
						SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
					}
				}
				if (result == this._item1)
				{
					if (!this.Outputs[0].isConnected)
					{
						throw new NotConnectedOutputException(this.GetID, 0);
					}
					this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
					return;
				}
				else if (result == this._item2)
				{
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
					if (!this.Outputs[2].isConnected)
					{
						throw new NotConnectedOutputException(this.GetID, 2);
					}
					this.Outputs[2].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
					return;
				}
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000641EC File Offset: 0x000623EC
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

		// Token: 0x04000FC2 RID: 4034
		private const string ID = "EE_GetItemResultNode";

		// Token: 0x04000FC3 RID: 4035
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04000FC4 RID: 4036
		private const string INPUT_OUTPUT_ITEM_1_NAME = "Item 1";

		// Token: 0x04000FC5 RID: 4037
		private const string INPUT_OUTPUT_ITEM_2_NAME = "Item 2";

		// Token: 0x04000FC6 RID: 4038
		private const string INPUT_OUTPUT_ITEM_3_NAME = "Item 3";

		// Token: 0x04000FC7 RID: 4039
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x04000FC8 RID: 4040
		private const string OUTPUT_ITEM_NAME = "Item";

		// Token: 0x04000FC9 RID: 4041
		private const string NODE_NAME = "Get Item Result";

		// Token: 0x04000FCA RID: 4042
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x04000FCB RID: 4043
		private const int INPUT_ITEM_1_INDEX = 2;

		// Token: 0x04000FCC RID: 4044
		private const int INPUT_ITEM_2_INDEX = 3;

		// Token: 0x04000FCD RID: 4045
		private const int INPUT_ITEM_3_INDEX = 4;

		// Token: 0x04000FCE RID: 4046
		private const int OUTPUT_ITEM_1_INDEX = 0;

		// Token: 0x04000FCF RID: 4047
		private const int OUTPUT_ITEM_2_INDEX = 1;

		// Token: 0x04000FD0 RID: 4048
		private const int OUTPUT_ITEM_3_INDEX = 2;

		// Token: 0x04000FD1 RID: 4049
		private const int OUTPUT_NO_CHOICE_INDEX = 3;

		// Token: 0x04000FD2 RID: 4050
		private const int OUTPUT_ITEM_INDEX = 4;

		// Token: 0x04000FD3 RID: 4051
		[SerializeField]
		private PlayerDecision _currentDecision;

		// Token: 0x04000FD4 RID: 4052
		[SerializeField]
		private IItem _item1;

		// Token: 0x04000FD5 RID: 4053
		[SerializeField]
		private IItem _item2;

		// Token: 0x04000FD6 RID: 4054
		[SerializeField]
		private IItem _item3;
	}
}
