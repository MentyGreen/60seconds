using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000262 RID: 610
	[Node(false, "Text Nodes/Display Pure Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent)
	})]
	public class DisplayPureTextNode : MessageNode
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00063576 File Offset: 0x00061776
		public override string GetID
		{
			get
			{
				return "EE_DisplayPureTextNode";
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00063580 File Offset: 0x00061780
		public override Node Create(Vector2 pos)
		{
			DisplayPureTextNode displayPureTextNode = ScriptableObject.CreateInstance<DisplayPureTextNode>();
			displayPureTextNode.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayPureTextNode.name = "Display Pure Text";
			displayPureTextNode.CreateMutliInput("In", "Flow");
			displayPureTextNode.CreateInput("Term", "String");
			displayPureTextNode.CreateOutput("Out", "Flow");
			return displayPureTextNode;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000635F1 File Offset: 0x000617F1
		public override Node Duplicate(Vector2 pos)
		{
			DisplayPureTextNode displayPureTextNode = (DisplayPureTextNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayPureTextNode._text = this._text;
			return displayPureTextNode;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00063629 File Offset: 0x00061829
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0006362B File Offset: 0x0006182B
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0006362D File Offset: 0x0006182D
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00063630 File Offset: 0x00061830
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<string>(this.Inputs[1], ref this._text, canvas);
			TextJournalContent content = new TextJournalContent(this._text, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000FAA RID: 4010
		public const string ID = "EE_DisplayPureTextNode";

		// Token: 0x04000FAB RID: 4011
		[SerializeField]
		private string _text;
	}
}
