using System;
using System.Collections.Generic;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000264 RID: 612
	[Node(false, "Text Nodes/Display Top Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent)
	})]
	public class DisplayTopTextNode : MessageNode
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000637E6 File Offset: 0x000619E6
		public override string GetID
		{
			get
			{
				return "DisplayTopTextNode";
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000637F0 File Offset: 0x000619F0
		public override Node Create(Vector2 pos)
		{
			DisplayTopTextNode displayTopTextNode = ScriptableObject.CreateInstance<DisplayTopTextNode>();
			displayTopTextNode.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayTopTextNode.name = "Display Top Text";
			displayTopTextNode.CreateMutliInput("In", "Flow");
			displayTopTextNode.CreateInput("Term", "LocalizedString");
			displayTopTextNode.CreateInput("Character", "Character");
			displayTopTextNode.CreateInput("Item", "Item");
			displayTopTextNode.CreateOutput("Out", "Flow");
			return displayTopTextNode;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00063884 File Offset: 0x00061A84
		public override Node Duplicate(Vector2 pos)
		{
			DisplayTopTextNode displayTopTextNode = (DisplayTopTextNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayTopTextNode._text = this._text;
			displayTopTextNode._character = this._character;
			displayTopTextNode._item = this._item;
			return displayTopTextNode;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000638DF File Offset: 0x00061ADF
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000638E1 File Offset: 0x00061AE1
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000638E3 File Offset: 0x00061AE3
		private void AddNewVariable()
		{
			base.CreateInput("Var " + this._counter.ToString(), ObjectConnection.ID);
			this._counter++;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00063914 File Offset: 0x00061B14
		private void RemoveVariable()
		{
			if (this.Inputs.Count > 4)
			{
				this.Inputs[this.Inputs.Count - 1].Delete();
				this._counter--;
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0006394F File Offset: 0x00061B4F
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00063954 File Offset: 0x00061B54
		private List<int> GetLocalVariables(NodeCanvas canvas)
		{
			if (this.Inputs.Count > 4)
			{
				List<int> list = new List<int>(this.Inputs.Count - 4);
				for (int i = 4; i < this.Inputs.Count; i++)
				{
					if (this.Inputs[i].connection.typeID == "Bool")
					{
						bool value = false;
						base.GetInputValue<bool>(this.Inputs[i], ref value, canvas);
						list.Add(Convert.ToInt32(value));
					}
					else if (this.Inputs[i].connection.typeID == "Int")
					{
						int item = 0;
						base.GetInputValue<int>(this.Inputs[i], ref item, canvas);
						list.Add(item);
					}
					else if (this.Inputs[i].connection.typeID == "PlayerDecision")
					{
						PlayerDecision playerDecision = null;
						base.GetInputValue<PlayerDecision>(this.Inputs[i], ref playerDecision, canvas);
						list.Add(playerDecision.ChoosenNumber);
					}
					else
					{
						list.Add(-1);
						Debug.LogError("Error in DisplayTextNode - wrong connection!!!");
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00063A8C File Offset: 0x00061C8C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._text, canvas);
			base.GetInputValue<Character>(this.Inputs[2], ref this._character, canvas);
			base.GetInputValue<IItem>(this.Inputs[3], ref this._item, canvas);
			TextJournalContent textJournalContent = new TextJournalContent(this._text, int.MaxValue);
			textJournalContent.Characters = new List<Character>
			{
				this._character
			};
			textJournalContent.Items = new List<IItem>
			{
				this._item
			};
			textJournalContent.LocalVariablesInts = this.GetLocalVariables(canvas);
			if (this._text.ToString().Contains("EXPEDITION"))
			{
				textJournalContent.ExpeditionCharacter = ExpeditionManager.Instance.GetExpeditionCharacter();
			}
			SecondsEventManager.AddJournalContent(base.ParentCanvas, textJournalContent);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000FB7 RID: 4023
		public const string ID = "DisplayTopTextNode";

		// Token: 0x04000FB8 RID: 4024
		private const int OFFSET = 4;

		// Token: 0x04000FB9 RID: 4025
		[SerializeField]
		private LocalizedString _text;

		// Token: 0x04000FBA RID: 4026
		[SerializeField]
		private Character _character;

		// Token: 0x04000FBB RID: 4027
		[SerializeField]
		private IItem _item;

		// Token: 0x04000FBC RID: 4028
		[SerializeField]
		private int _counter;
	}
}
