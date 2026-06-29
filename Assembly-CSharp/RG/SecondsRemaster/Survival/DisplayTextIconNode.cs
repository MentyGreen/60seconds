using System;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D9 RID: 729
	[Node(false, "Text Nodes/Display Text Icon Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class DisplayTextIconNode : ResourceNode
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0006F21B File Offset: 0x0006D41B
		public override string GetID
		{
			get
			{
				return "EE_DisplayTextIconNode";
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006F224 File Offset: 0x0006D424
		public override Node Create(Vector2 pos)
		{
			DisplayTextIconNode displayTextIconNode = ScriptableObject.CreateInstance<DisplayTextIconNode>();
			displayTextIconNode.rect = new Rect(pos.x, pos.y, 250f, 130f);
			displayTextIconNode.name = "Display Text Icon";
			displayTextIconNode.CreateMutliInput("In", "Flow");
			displayTextIconNode.CreateInput("Icon Term", "LocalizedString");
			displayTextIconNode.CreateInput("Amount", "Int");
			displayTextIconNode.CreateInput("Display Priority", "Int");
			displayTextIconNode.CreateOutput("Out", "Flow");
			return displayTextIconNode;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
		public override Node Duplicate(Vector2 pos)
		{
			DisplayTextIconNode displayTextIconNode = (DisplayTextIconNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayTextIconNode._term = this._term;
			displayTextIconNode._amount = this._amount;
			displayTextIconNode._type = this._type;
			displayTextIconNode._priority = this._priority;
			return displayTextIconNode;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0006F31F File Offset: 0x0006D51F
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0006F321 File Offset: 0x0006D521
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0006F323 File Offset: 0x0006D523
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0006F328 File Offset: 0x0006D528
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._term, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._amount, canvas);
			base.GetInputValue<int>(this.Inputs[3], ref this._priority, canvas);
			TextIconJournalContent content = new TextIconJournalContent(this._term, this._amount, this._type, this._priority);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400137A RID: 4986
		public const string ID = "EE_DisplayTextIconNode";

		// Token: 0x0400137B RID: 4987
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400137C RID: 4988
		private const string INPUT_ICON_TERM_NAME = "Icon Term";

		// Token: 0x0400137D RID: 4989
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x0400137E RID: 4990
		private const string INPUT_PRIORITY_NAME = "Display Priority";

		// Token: 0x0400137F RID: 4991
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001380 RID: 4992
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001381 RID: 4993
		private const int INPUT_ICON_TERM_INDEX = 1;

		// Token: 0x04001382 RID: 4994
		private const int INPUT_AMOUNT_INDEX = 2;

		// Token: 0x04001383 RID: 4995
		private const int INPUT_PRIORITY_INDEX = 3;

		// Token: 0x04001384 RID: 4996
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001385 RID: 4997
		[SerializeField]
		private LocalizedString _term;

		// Token: 0x04001386 RID: 4998
		[SerializeField]
		private int _amount;

		// Token: 0x04001387 RID: 4999
		[SerializeField]
		private int _priority;

		// Token: 0x04001388 RID: 5000
		[SerializeField]
		private EventContentData.ETextIconContentType _type;

		// Token: 0x04001389 RID: 5001
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
