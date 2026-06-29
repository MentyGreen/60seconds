using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D8 RID: 728
	[Node(false, "Supplies Nodes/Items/Damage Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class DamageItemNodeV2 : ResourceNode
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0006F00B File Offset: 0x0006D20B
		public override string GetID
		{
			get
			{
				return "EE_DamageItemNodeV2";
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006F014 File Offset: 0x0006D214
		public override Node Create(Vector2 pos)
		{
			DamageItemNodeV2 damageItemNodeV = ScriptableObject.CreateInstance<DamageItemNodeV2>();
			damageItemNodeV.rect = new Rect(pos.x, pos.y, 180f, 130f);
			damageItemNodeV.name = "Damage Item";
			damageItemNodeV.CreateMutliInput("In", "Flow");
			damageItemNodeV.CreateInput("Item", "Item");
			damageItemNodeV.CreateInput("Show In Starlog", "Bool");
			damageItemNodeV.CreateOutput("Out", "Flow");
			return damageItemNodeV;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006F098 File Offset: 0x0006D298
		public override Node Duplicate(Vector2 pos)
		{
			DamageItemNodeV2 damageItemNodeV = (DamageItemNodeV2)this.Create(this.rect.position + new Vector2(20f, 20f));
			damageItemNodeV._item = this._item;
			damageItemNodeV._showInStarlog = this._showInStarlog;
			return damageItemNodeV;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006F0E7 File Offset: 0x0006D2E7
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006F0E9 File Offset: 0x0006D2E9
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006F0EB File Offset: 0x0006D2EB
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006F0F0 File Offset: 0x0006D2F0
		public override void Execute(NodeCanvas canvas)
		{
			IItem item = this._item;
			base.GetInputValue<IItem>(this.Inputs[1], ref item, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._showInStarlog, canvas);
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
			if (this._showInStarlog && ((item.BaseRuntimeData.IsAvailable && flag2 && !flag) || (isAvailable && !item.BaseRuntimeData.IsAvailable)))
			{
				TextIconJournalContent content = new TextIconJournalContent(item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400136E RID: 4974
		public const string ID = "EE_DamageItemNodeV2";

		// Token: 0x0400136F RID: 4975
		[SerializeField]
		private Item _item;

		// Token: 0x04001370 RID: 4976
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001371 RID: 4977
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001372 RID: 4978
		private const string INPUT_SHOW_IN_STARLOG_NAME = "Show In Starlog";

		// Token: 0x04001373 RID: 4979
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001374 RID: 4980
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001375 RID: 4981
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x04001376 RID: 4982
		private const int INPUT_SHOW_IN_STARLOG_INDEX = 2;

		// Token: 0x04001377 RID: 4983
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001378 RID: 4984
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";

		// Token: 0x04001379 RID: 4985
		[SerializeField]
		private bool _showInStarlog = true;
	}
}
