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
	// Token: 0x0200026F RID: 623
	[Node(true, "Legacy/Remove Resource Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(ExpeditionEvent)
	})]
	public class RemoveResourceNode : ResourceNode
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0006541E File Offset: 0x0006361E
		public override string GetID
		{
			get
			{
				return "EE_RemoveResourceNode";
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00065428 File Offset: 0x00063628
		public override Node Create(Vector2 pos)
		{
			RemoveResourceNode removeResourceNode = ScriptableObject.CreateInstance<RemoveResourceNode>();
			removeResourceNode.rect = new Rect(pos.x, pos.y, 180f, 40f);
			removeResourceNode.name = "Remove Resource";
			removeResourceNode.CreateInput("In", "Flow");
			removeResourceNode.CreateInput("Resource", "Resources");
			removeResourceNode.CreateInput("Value", "Int");
			removeResourceNode.CreateOutput("Out", "Flow");
			return removeResourceNode;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000654AC File Offset: 0x000636AC
		public override Node Duplicate(Vector2 pos)
		{
			RemoveResourceNode removeResourceNode = (RemoveResourceNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeResourceNode._resource = this._resource;
			removeResourceNode._value = this._value;
			return removeResourceNode;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000654FB File Offset: 0x000636FB
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000654FD File Offset: 0x000636FD
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000654FF File Offset: 0x000636FF
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00065504 File Offset: 0x00063704
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Resource>(this.Inputs[1], ref this._resource, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._value, canvas);
			int amount = Singleton<ItemManager>.Instance.GetPlayerResources().RemoveResourceAndGetRemovedAmount(this._resource, this._value);
			TextIconJournalContent content = new TextIconJournalContent(this._resource.IconTerm, amount, EventContentData.ETextIconContentType.SUBTRACTION, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400103C RID: 4156
		public const string ID = "EE_RemoveResourceNode";

		// Token: 0x0400103D RID: 4157
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400103E RID: 4158
		private const string INPUT_RESOURCE_NAME = "Resource";

		// Token: 0x0400103F RID: 4159
		private const string INPUT_VALUE_NAME = "Value";

		// Token: 0x04001040 RID: 4160
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001041 RID: 4161
		private const string NODE_NAME = "Remove Resource";

		// Token: 0x04001042 RID: 4162
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001043 RID: 4163
		private const int INPUT_RESOURCE_INDEX = 1;

		// Token: 0x04001044 RID: 4164
		private const int INPUT_VALUE_INDEX = 2;

		// Token: 0x04001045 RID: 4165
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001046 RID: 4166
		[SerializeField]
		private Resource _resource;

		// Token: 0x04001047 RID: 4167
		[SerializeField]
		private int _value;
	}
}
