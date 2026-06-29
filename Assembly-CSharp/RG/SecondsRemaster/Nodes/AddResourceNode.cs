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
	// Token: 0x0200026C RID: 620
	[Node(true, "Legacy/Add Resource Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddResourceNode : ResourceNode
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00064FBD File Offset: 0x000631BD
		public override string GetID
		{
			get
			{
				return "EE_AddResourceNode";
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00064FC4 File Offset: 0x000631C4
		public override Node Create(Vector2 pos)
		{
			AddResourceNode addResourceNode = ScriptableObject.CreateInstance<AddResourceNode>();
			addResourceNode.rect = new Rect(pos.x, pos.y, 180f, 40f);
			addResourceNode.name = "Add Resource";
			addResourceNode.CreateInput("In", "Flow");
			addResourceNode.CreateInput("Resource", "Resources");
			addResourceNode.CreateInput("Value", "Int");
			addResourceNode.CreateOutput("Out", "Flow");
			return addResourceNode;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00065048 File Offset: 0x00063248
		public override Node Duplicate(Vector2 pos)
		{
			AddResourceNode addResourceNode = (AddResourceNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addResourceNode._resource = this._resource;
			addResourceNode._value = this._value;
			return addResourceNode;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00065097 File Offset: 0x00063297
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00065099 File Offset: 0x00063299
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0006509B File Offset: 0x0006329B
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000650A0 File Offset: 0x000632A0
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Resource>(this.Inputs[1], ref this._resource, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._value, canvas);
			int amount = Singleton<ItemManager>.Instance.GetPlayerResources().AddResource(this._resource, this._value);
			TextIconJournalContent content = new TextIconJournalContent(this._resource.IconTerm, amount, EventContentData.ETextIconContentType.ADDITION, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400101B RID: 4123
		public const string ID = "EE_AddResourceNode";

		// Token: 0x0400101C RID: 4124
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400101D RID: 4125
		private const string INPUT_RESOURCE_NAME = "Resource";

		// Token: 0x0400101E RID: 4126
		private const string INPUT_VALUE_NAME = "Value";

		// Token: 0x0400101F RID: 4127
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001020 RID: 4128
		private const string NODE_NAME = "Add Resource";

		// Token: 0x04001021 RID: 4129
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001022 RID: 4130
		private const int INPUT_RESOURCE_INDEX = 1;

		// Token: 0x04001023 RID: 4131
		private const int INPUT_VALUE_INDEX = 2;

		// Token: 0x04001024 RID: 4132
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001025 RID: 4133
		[SerializeField]
		private Resource _resource;

		// Token: 0x04001026 RID: 4134
		[SerializeField]
		private int _value;
	}
}
