using System;
using System.Collections.Generic;
using NodeEditorFramework;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000273 RID: 627
	public abstract class ListNodeTemplate<InputType> : ParsecsNode
	{
		// Token: 0x06001730 RID: 5936
		public abstract override Node Create(Vector2 pos);

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001731 RID: 5937
		protected abstract string ConnectionType { get; }

		// Token: 0x06001732 RID: 5938 RVA: 0x00065B18 File Offset: 0x00063D18
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00065B3F File Offset: 0x00063D3F
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00065B41 File Offset: 0x00063D41
		private void AddNewElement()
		{
			base.CreateInput("Value " + this._counter.ToString(), this.ConnectionType);
			this._counter++;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00065B73 File Offset: 0x00063D73
		private void RemoveElement()
		{
			this.Inputs[this.Inputs.Count - 1].Delete();
			this._counter--;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00065BA0 File Offset: 0x00063DA0
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00065BA4 File Offset: 0x00063DA4
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			List<InputType> list = new List<InputType>();
			for (int i = 0; i < this.Inputs.Count; i++)
			{
				list.Add(base.GetInputValue<InputType>(this.Inputs[i], canvas));
			}
			return base.CastValue<T>(list);
		}

		// Token: 0x04001070 RID: 4208
		[SerializeField]
		private int _counter = 2;

		// Token: 0x04001071 RID: 4209
		private const int OUTPUT_INDEX = 0;

		// Token: 0x04001072 RID: 4210
		private const int MINIMAL_INPUTS = 1;
	}
}
