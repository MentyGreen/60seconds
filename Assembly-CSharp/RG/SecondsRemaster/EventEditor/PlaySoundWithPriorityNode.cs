using System;
using FMODUnity;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.EventEditor
{
	// Token: 0x0200034B RID: 843
	[Node(false, "Utility Nodes/Play Sound With Priority", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ReportEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas)
	})]
	public class PlaySoundWithPriorityNode : FlowNode
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0007775F File Offset: 0x0007595F
		public override string GetID
		{
			get
			{
				return "EE_PlaySoundWithPriority";
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00077768 File Offset: 0x00075968
		public override Node Create(Vector2 pos)
		{
			PlaySoundWithPriorityNode playSoundWithPriorityNode = ScriptableObject.CreateInstance<PlaySoundWithPriorityNode>();
			playSoundWithPriorityNode.rect = new Rect(pos.x, pos.y, 340f, 300f);
			playSoundWithPriorityNode.name = "Play Sound With Priority Node";
			playSoundWithPriorityNode.CreateMutliInput("In", "Flow");
			playSoundWithPriorityNode.CreateOutput("Out", "Flow");
			return playSoundWithPriorityNode;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000777C8 File Offset: 0x000759C8
		public override Node Duplicate(Vector2 pos)
		{
			PlaySoundWithPriorityNode playSoundWithPriorityNode = (PlaySoundWithPriorityNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			playSoundWithPriorityNode._event = this._event;
			playSoundWithPriorityNode._volume = this._volume;
			playSoundWithPriorityNode._pan = this._pan;
			playSoundWithPriorityNode._pitch = this._pitch;
			playSoundWithPriorityNode._offset = this._offset;
			playSoundWithPriorityNode._currentSound = this._currentSound;
			playSoundWithPriorityNode._priority = this._priority;
			return playSoundWithPriorityNode;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00077853 File Offset: 0x00075A53
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00077855 File Offset: 0x00075A55
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00077857 File Offset: 0x00075A57
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007785C File Offset: 0x00075A5C
		public override void Execute(NodeCanvas canvas)
		{
			if (this._priority >= this._currentSound.EventPriority)
			{
				this._currentSound.EventName = this._event;
				this._currentSound.EventPriority = this._priority;
				this._currentSound.Volume = this._volume;
				this._currentSound.Pan = this._pan;
				this._currentSound.Pitch = this._pitch;
				this._currentSound.Offset = this._offset;
				this._currentSound.OffsetCheck = this._offsetCheck;
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x040015AA RID: 5546
		private const int INTPUT_IN_INDEX = 0;

		// Token: 0x040015AB RID: 5547
		private const int OUTTPUT_IN_INDEX = 0;

		// Token: 0x040015AC RID: 5548
		private const string NODE_NAME = "Play Sound With Priority Node";

		// Token: 0x040015AD RID: 5549
		[EventRef]
		[SerializeField]
		private string _event;

		// Token: 0x040015AE RID: 5550
		[SerializeField]
		[Range(0f, 1f)]
		private float _volume = 0.5f;

		// Token: 0x040015AF RID: 5551
		[SerializeField]
		[Range(-1f, 1f)]
		private float _pan;

		// Token: 0x040015B0 RID: 5552
		[SerializeField]
		[Range(-1f, 1f)]
		private float _pitch;

		// Token: 0x040015B1 RID: 5553
		[SerializeField]
		private int _offset;

		// Token: 0x040015B2 RID: 5554
		[SerializeField]
		private bool _offsetCheck;

		// Token: 0x040015B3 RID: 5555
		[SerializeField]
		private CurrentSoundToPlay _currentSound;

		// Token: 0x040015B4 RID: 5556
		[SerializeField]
		private int _priority;

		// Token: 0x040015B5 RID: 5557
		private const string ERROR_INFO = "No event selected.";

		// Token: 0x040015B6 RID: 5558
		private const string WARNING_INFO = "No offeset time was set.";

		// Token: 0x040015B7 RID: 5559
		private const string VALUE_PITCH = "Pitch";

		// Token: 0x040015B8 RID: 5560
		private const string VALUE_PAN = "Pan";

		// Token: 0x040015B9 RID: 5561
		private const string EMPTY_EVENT_INFO = "No current sound selected";

		// Token: 0x040015BA RID: 5562
		public const string ID = "EE_PlaySoundWithPriority";
	}
}
