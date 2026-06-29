using System;
using System.Collections.Generic;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D3 RID: 723
	[Node(true, "Legacy/Display Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class DisplayTextNode : MessageNode
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0006E32A File Offset: 0x0006C52A
		public override string GetID
		{
			get
			{
				return "DisplayTextNode";
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006E334 File Offset: 0x0006C534
		public override Node Create(Vector2 pos)
		{
			DisplayTextNode displayTextNode = ScriptableObject.CreateInstance<DisplayTextNode>();
			displayTextNode.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayTextNode.name = "Display Text";
			displayTextNode.CreateMutliInput("In", "Flow");
			displayTextNode.CreateInput("Term", "LocalizedString");
			displayTextNode.CreateInput("Character", "Character");
			displayTextNode.CreateInput("Item", "Item");
			displayTextNode.CreateOutput("Out", "Flow");
			return displayTextNode;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0006E3C8 File Offset: 0x0006C5C8
		public override Node Duplicate(Vector2 pos)
		{
			DisplayTextNode displayTextNode = (DisplayTextNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayTextNode._text = this._text;
			displayTextNode._character = this._character;
			displayTextNode._item = this._item;
			return displayTextNode;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0006E423 File Offset: 0x0006C623
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0006E425 File Offset: 0x0006C625
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0006E427 File Offset: 0x0006C627
		private void AddNewVariable()
		{
			base.CreateInput("Var " + this._counter.ToString(), ObjectConnection.ID);
			this._counter++;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0006E458 File Offset: 0x0006C658
		private void RemoveVariable()
		{
			if (this.Inputs.Count > 4)
			{
				this.Inputs[this.Inputs.Count - 1].Delete();
				this._counter--;
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0006E493 File Offset: 0x0006C693
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006E498 File Offset: 0x0006C698
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

		// Token: 0x0600195E RID: 6494 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._text, canvas);
			base.GetInputValue<Character>(this.Inputs[2], ref this._character, canvas);
			base.GetInputValue<IItem>(this.Inputs[3], ref this._item, canvas);
			TextJournalContent textJournalContent = new TextJournalContent(this._text, 0);
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

		// Token: 0x04001321 RID: 4897
		public const string ID = "DisplayTextNode";

		// Token: 0x04001322 RID: 4898
		private const int OFFSET = 4;

		// Token: 0x04001323 RID: 4899
		[SerializeField]
		private LocalizedString _text;

		// Token: 0x04001324 RID: 4900
		[SerializeField]
		private Character _character;

		// Token: 0x04001325 RID: 4901
		[SerializeField]
		private IItem _item;

		// Token: 0x04001326 RID: 4902
		[SerializeField]
		private int _counter;
	}
}
