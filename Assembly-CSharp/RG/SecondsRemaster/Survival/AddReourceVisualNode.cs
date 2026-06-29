using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D6 RID: 726
	[Node(false, "Supplies Nodes/Resources/Add Resource Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddReourceVisualNode : ResourceNode
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0006EC88 File Offset: 0x0006CE88
		public override string GetID
		{
			get
			{
				return "EE_AddResourceVisualNode";
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0006EC90 File Offset: 0x0006CE90
		public override Node Create(Vector2 pos)
		{
			AddReourceVisualNode addReourceVisualNode = ScriptableObject.CreateInstance<AddReourceVisualNode>();
			addReourceVisualNode.rect = new Rect(pos.x, pos.y, 180f, 40f);
			addReourceVisualNode.name = "Add Resource";
			addReourceVisualNode.CreateInput("In", "Flow");
			addReourceVisualNode.CreateInput("Resource", "Resources");
			addReourceVisualNode.CreateInput("Value", "Int");
			addReourceVisualNode.CreateInput("Show in Starlog", "Bool");
			addReourceVisualNode.CreateOutput("Out", "Flow");
			return addReourceVisualNode;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006ED24 File Offset: 0x0006CF24
		public override Node Duplicate(Vector2 pos)
		{
			AddReourceVisualNode addReourceVisualNode = (AddReourceVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addReourceVisualNode._resource = this._resource;
			addReourceVisualNode._value = this._value;
			addReourceVisualNode._showStarlogGraphic = this._showStarlogGraphic;
			return addReourceVisualNode;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0006ED7F File Offset: 0x0006CF7F
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006ED81 File Offset: 0x0006CF81
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0006ED83 File Offset: 0x0006CF83
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0006ED88 File Offset: 0x0006CF88
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Resource>(this.Inputs[1], ref this._resource, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._value, canvas);
			base.GetInputValue<bool>(this.Inputs[3], ref this._showStarlogGraphic, canvas);
			int amount = Singleton<ItemManager>.Instance.GetPlayerResources().AddResource(this._resource, this._value);
			if (this._showStarlogGraphic)
			{
				TextIconJournalContent content = new TextIconJournalContent(this._resource.IconTerm, amount, EventContentData.ETextIconContentType.ADDITION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001356 RID: 4950
		public const string ID = "EE_AddResourceVisualNode";

		// Token: 0x04001357 RID: 4951
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001358 RID: 4952
		private const string INPUT_RESOURCE_NAME = "Resource";

		// Token: 0x04001359 RID: 4953
		private const string INPUT_VALUE_NAME = "Value";

		// Token: 0x0400135A RID: 4954
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400135B RID: 4955
		private const string NODE_NAME = "Add Resource";

		// Token: 0x0400135C RID: 4956
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x0400135D RID: 4957
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400135E RID: 4958
		private const int INPUT_RESOURCE_INDEX = 1;

		// Token: 0x0400135F RID: 4959
		private const int INPUT_VALUE_INDEX = 2;

		// Token: 0x04001360 RID: 4960
		private const int INPUT_SHOW_GRAPHIC_INDEX = 3;

		// Token: 0x04001361 RID: 4961
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001362 RID: 4962
		[SerializeField]
		private Resource _resource;

		// Token: 0x04001363 RID: 4963
		[SerializeField]
		private int _value;

		// Token: 0x04001364 RID: 4964
		[SerializeField]
		private bool _showStarlogGraphic = true;
	}
}
