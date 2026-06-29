using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D7 RID: 727
	[Node(true, "Supplies Nodes/Items/Damage Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class DamageItemNode : ResourceNode
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0006EE50 File Offset: 0x0006D050
		public override string GetID
		{
			get
			{
				return "EE_DamageItemNode";
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0006EE58 File Offset: 0x0006D058
		public override Node Create(Vector2 pos)
		{
			DamageItemNode damageItemNode = ScriptableObject.CreateInstance<DamageItemNode>();
			damageItemNode.rect = new Rect(pos.x, pos.y, 180f, 130f);
			damageItemNode.name = "Damage Item";
			damageItemNode.CreateMutliInput("In", "Flow");
			damageItemNode.CreateInput("Item", "Item");
			damageItemNode.CreateOutput("Out", "Flow");
			return damageItemNode;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006EEC9 File Offset: 0x0006D0C9
		public override Node Duplicate(Vector2 pos)
		{
			DamageItemNode damageItemNode = (DamageItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			damageItemNode._item = this._item;
			return damageItemNode;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006EF01 File Offset: 0x0006D101
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0006EF03 File Offset: 0x0006D103
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006EF05 File Offset: 0x0006D105
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006EF08 File Offset: 0x0006D108
		public override void Execute(NodeCanvas canvas)
		{
			IItem item = this._item;
			base.GetInputValue<IItem>(this.Inputs[1], ref item, canvas);
			bool isAvailable = item.BaseRuntimeData.IsAvailable;
			bool flag;
			bool flag2;
			if (item is Item)
			{
				Item item2 = item as Item;
				flag = item2.IsDamaged();
				item2.SetDamage();
				flag2 = item2.IsDamaged();
			}
			else
			{
				if (!(item is SecondsRemedium))
				{
					throw new UnityException("Cannot damage item: " + item.BaseStaticData.ItemId);
				}
				SecondsRemedium secondsRemedium = item as SecondsRemedium;
				flag = secondsRemedium.IsDamaged();
				secondsRemedium.SetDamage();
				flag2 = secondsRemedium.IsDamaged();
			}
			if ((item.BaseRuntimeData.IsAvailable && flag2 && !flag) || (isAvailable && !item.BaseRuntimeData.IsAvailable))
			{
				TextIconJournalContent content = new TextIconJournalContent(item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001365 RID: 4965
		public const string ID = "EE_DamageItemNode";

		// Token: 0x04001366 RID: 4966
		[SerializeField]
		private Item _item;

		// Token: 0x04001367 RID: 4967
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001368 RID: 4968
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001369 RID: 4969
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400136A RID: 4970
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400136B RID: 4971
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x0400136C RID: 4972
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400136D RID: 4973
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
