using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000270 RID: 624
	[Node(false, "Supplies Nodes/Resources/Remove Resource Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(ExpeditionEvent)
	})]
	public class RemoveResourceVisualNode : ResourceNode
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x000655A4 File Offset: 0x000637A4
		public override string GetID
		{
			get
			{
				return "EE_RemoveResourceVisualNode";
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000655AC File Offset: 0x000637AC
		public override Node Create(Vector2 pos)
		{
			RemoveResourceVisualNode removeResourceVisualNode = ScriptableObject.CreateInstance<RemoveResourceVisualNode>();
			removeResourceVisualNode.rect = new Rect(pos.x, pos.y, 180f, 40f);
			removeResourceVisualNode.name = "Remove Resource";
			removeResourceVisualNode.CreateInput("In", "Flow");
			removeResourceVisualNode.CreateInput("Resource", "Resources");
			removeResourceVisualNode.CreateInput("Value", "Int");
			removeResourceVisualNode.CreateInput("Show in Starlog", "Bool");
			removeResourceVisualNode.CreateOutput("Out", "Flow");
			return removeResourceVisualNode;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00065640 File Offset: 0x00063840
		public override Node Duplicate(Vector2 pos)
		{
			RemoveResourceVisualNode removeResourceVisualNode = (RemoveResourceVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeResourceVisualNode._resource = this._resource;
			removeResourceVisualNode._value = this._value;
			removeResourceVisualNode._showStarlogGraphic = this._showStarlogGraphic;
			return removeResourceVisualNode;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0006569B File Offset: 0x0006389B
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0006569D File Offset: 0x0006389D
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0006569F File Offset: 0x0006389F
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000656A4 File Offset: 0x000638A4
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Resource>(this.Inputs[1], ref this._resource, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._value, canvas);
			base.GetInputValue<bool>(this.Inputs[3], ref this._showStarlogGraphic, canvas);
			int amount = Singleton<ItemManager>.Instance.GetPlayerResources().RemoveResourceAndGetRemovedAmount(this._resource, this._value);
			if (this._showStarlogGraphic)
			{
				TextIconJournalContent content = new TextIconJournalContent(this._resource.IconTerm, amount, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001048 RID: 4168
		public const string ID = "EE_RemoveResourceVisualNode";

		// Token: 0x04001049 RID: 4169
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400104A RID: 4170
		private const string INPUT_RESOURCE_NAME = "Resource";

		// Token: 0x0400104B RID: 4171
		private const string INPUT_VALUE_NAME = "Value";

		// Token: 0x0400104C RID: 4172
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400104D RID: 4173
		private const string NODE_NAME = "Remove Resource";

		// Token: 0x0400104E RID: 4174
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x0400104F RID: 4175
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001050 RID: 4176
		private const int INPUT_RESOURCE_INDEX = 1;

		// Token: 0x04001051 RID: 4177
		private const int INPUT_VALUE_INDEX = 2;

		// Token: 0x04001052 RID: 4178
		private const int INPUT_SHOW_GRAPHIC_INDEX = 3;

		// Token: 0x04001053 RID: 4179
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001054 RID: 4180
		[SerializeField]
		private Resource _resource;

		// Token: 0x04001055 RID: 4181
		[SerializeField]
		private int _value;

		// Token: 0x04001056 RID: 4182
		[SerializeField]
		private bool _showStarlogGraphic = true;
	}
}
