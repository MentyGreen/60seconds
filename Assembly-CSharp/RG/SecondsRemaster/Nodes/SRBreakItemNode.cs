using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200027D RID: 637
	[Node(false, "Remaster/Supplies Nodes/Items/Break Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas),
		typeof(Goal),
		typeof(ConditionEvent)
	})]
	public class SRBreakItemNode : ResourceNode
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00066D45 File Offset: 0x00064F45
		public override string GetID
		{
			get
			{
				return "EE_SRBreakItemNode";
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00066D4C File Offset: 0x00064F4C
		public override Node Create(Vector2 pos)
		{
			SRBreakItemNode srbreakItemNode = ScriptableObject.CreateInstance<SRBreakItemNode>();
			srbreakItemNode.rect = new Rect(pos.x, pos.y, 180f, 155f);
			srbreakItemNode.name = "Break Item Remaster";
			srbreakItemNode.CreateInput("Item", "Item");
			srbreakItemNode.CreateOutput("Name", "LocalizedString");
			srbreakItemNode.CreateOutput("Is Available", "Bool");
			srbreakItemNode.CreateOutput("Is On Expedition", "Bool");
			srbreakItemNode.CreateOutput("Actual Level", "Int");
			srbreakItemNode.CreateOutput("Is max level", "Bool");
			srbreakItemNode.CreateOutput("Durability", "Int");
			srbreakItemNode.CreateOutput("Is damage", "Bool");
			srbreakItemNode.CreateOutput("Amount", "Int");
			srbreakItemNode.CreateOutput("Icon Term", "LocalizedString");
			return srbreakItemNode;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00066E34 File Offset: 0x00065034
		public override Node Duplicate(Vector2 pos)
		{
			SRBreakItemNode srbreakItemNode = (SRBreakItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			srbreakItemNode._item = this._item;
			return srbreakItemNode;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00066E6C File Offset: 0x0006506C
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00066E6E File Offset: 0x0006506E
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00066E70 File Offset: 0x00065070
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00066E74 File Offset: 0x00065074
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[0], ref this._item, canvas);
			switch (output)
			{
			case 0:
				return base.CastValue<T>(this._item.BaseStaticData.Name);
			case 1:
				return base.CastValue<T>(this._item.BaseRuntimeData.IsAvailable);
			case 2:
				return base.CastValue<T>(this._item.BaseRuntimeData.IsOnExpedition);
			case 3:
				return base.CastValue<T>(this._item.BaseRuntimeData.Level);
			case 4:
				return base.CastValue<T>(this._item.IsMaxLevel());
			case 5:
				if (this._item is Item)
				{
					return base.CastValue<T>(((Item)this._item).RuntimeData.Durability);
				}
				return base.CastValue<T>(1);
			case 6:
				if (this._item is Item)
				{
					return base.CastValue<T>(((Item)this._item).RuntimeData.IsDamaged);
				}
				if (this._item is SecondsRemedium)
				{
					return base.CastValue<T>(((SecondsRemedium)this._item).SecondsRemediumRuntimeData.IsDamaged);
				}
				return base.CastValue<T>(false);
			case 7:
				if (this._item is ConsumableRemedium)
				{
					ConsumableRemedium consumableRemedium = (ConsumableRemedium)this._item;
					return base.CastValue<T>(consumableRemedium.RuntimeData.Amount - consumableRemedium.RuntimeData.PlannedConsumption);
				}
				return base.CastValue<T>(1);
			case 8:
				return base.CastValue<T>(this._item.BaseStaticData.IconTerm);
			default:
				throw new NotExistingOutputException("EE_SRBreakItemNode", output);
			}
		}

		// Token: 0x040010E6 RID: 4326
		public const string ID = "EE_SRBreakItemNode";

		// Token: 0x040010E7 RID: 4327
		private const int INDEX_ITEM_INPUT = 0;

		// Token: 0x040010E8 RID: 4328
		private const int INDEX_OUTPUT_NAME = 0;

		// Token: 0x040010E9 RID: 4329
		private const int INDEX_OUTPUT_IS_AVAILABLE = 1;

		// Token: 0x040010EA RID: 4330
		private const int INDEX_OUTPUT_IS_ON_EXPEDITION = 2;

		// Token: 0x040010EB RID: 4331
		private const int INDEX_OUTPUT_ACTUAL_LEVEL = 3;

		// Token: 0x040010EC RID: 4332
		private const int INDEX_OUTPUT_IS_MAX_LEVEL = 4;

		// Token: 0x040010ED RID: 4333
		private const int INDEX_OUTPUT_DURABILITY = 5;

		// Token: 0x040010EE RID: 4334
		private const int INDEX_OUTPUT_IS_DAMAGE = 6;

		// Token: 0x040010EF RID: 4335
		private const int INDEX_OUTPUT_AMOUNT = 7;

		// Token: 0x040010F0 RID: 4336
		private const int INDEX_OUTPUT_ICON_TERM = 8;

		// Token: 0x040010F1 RID: 4337
		[SerializeField]
		private IItem _item;
	}
}
