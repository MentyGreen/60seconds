using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000225 RID: 549
	[Node(false, "Supplies Nodes/Items/Is Item Damaged Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas),
		typeof(ConditionEvent)
	})]
	public class IsItemDamagedNode : ResourceNode
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0005E5B6 File Offset: 0x0005C7B6
		public override string GetID
		{
			get
			{
				return "EE_IsDamagedItemNode";
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005E5C0 File Offset: 0x0005C7C0
		public override Node Create(Vector2 pos)
		{
			IsItemDamagedNode isItemDamagedNode = ScriptableObject.CreateInstance<IsItemDamagedNode>();
			isItemDamagedNode.rect = new Rect(pos.x, pos.y, 180f, 120f);
			isItemDamagedNode.name = "Is Item Damaged";
			isItemDamagedNode.CreateInput("Item", "Item");
			isItemDamagedNode.CreateOutput("Is Damaged", "Bool");
			return isItemDamagedNode;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005E620 File Offset: 0x0005C820
		public override Node Duplicate(Vector2 pos)
		{
			IsItemDamagedNode isItemDamagedNode = (IsItemDamagedNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			isItemDamagedNode._item = this._item;
			return isItemDamagedNode;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005E658 File Offset: 0x0005C858
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005E65A File Offset: 0x0005C85A
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0005E65C File Offset: 0x0005C85C
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005E660 File Offset: 0x0005C860
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			IItem item = this._item;
			base.GetInputValue<IItem>(this.Inputs[0], ref item, canvas);
			if (item is Item)
			{
				return base.CastValue<T>(((Item)item).RuntimeData.IsDamaged);
			}
			if (item is SecondsRemedium)
			{
				return base.CastValue<T>(((SecondsRemedium)item).SecondsRemediumRuntimeData.IsDamaged);
			}
			throw new UnityException("Non damageable item in IsItemDamageNode");
		}

		// Token: 0x04000E34 RID: 3636
		public const string ID = "EE_IsDamagedItemNode";

		// Token: 0x04000E35 RID: 3637
		[SerializeField]
		private Item _item;

		// Token: 0x04000E36 RID: 3638
		private const int INPUT_ITEM_INDEX = 0;

		// Token: 0x04000E37 RID: 3639
		private const int OUTPUT_IS_AVAILABLE_INDEX = 0;

		// Token: 0x04000E38 RID: 3640
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04000E39 RID: 3641
		private const string OUTPUT_IS_AVAILABLE_NAME = "Is Damaged";
	}
}
