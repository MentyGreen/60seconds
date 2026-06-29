using System;
using System.Collections.Generic;
using System.Linq;

namespace DunGen.Analysis
{
	// Token: 0x02000203 RID: 515
	public class GenerationAnalysis
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0005AB93 File Offset: 0x00058D93
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0005AB9B File Offset: 0x00058D9B
		public int TargetIterationCount { get; private set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0005ABA4 File Offset: 0x00058DA4
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x0005ABAC File Offset: 0x00058DAC
		public int IterationCount { get; private set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005ABB5 File Offset: 0x00058DB5
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x0005ABBD File Offset: 0x00058DBD
		public NumberSetData MainPathRoomCount { get; private set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005ABC6 File Offset: 0x00058DC6
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0005ABCE File Offset: 0x00058DCE
		public NumberSetData BranchPathRoomCount { get; private set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0005ABD7 File Offset: 0x00058DD7
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x0005ABDF File Offset: 0x00058DDF
		public NumberSetData TotalRoomCount { get; private set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0005ABF0 File Offset: 0x00058DF0
		public NumberSetData MaxBranchDepth { get; private set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0005ABF9 File Offset: 0x00058DF9
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x0005AC01 File Offset: 0x00058E01
		public NumberSetData TotalRetries { get; private set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0005AC0A File Offset: 0x00058E0A
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x0005AC12 File Offset: 0x00058E12
		public NumberSetData PreProcessTime { get; private set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0005AC1B File Offset: 0x00058E1B
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0005AC23 File Offset: 0x00058E23
		public NumberSetData MainPathGenerationTime { get; private set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0005AC2C File Offset: 0x00058E2C
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x0005AC34 File Offset: 0x00058E34
		public NumberSetData BranchPathGenerationTime { get; private set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0005AC3D File Offset: 0x00058E3D
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x0005AC45 File Offset: 0x00058E45
		public NumberSetData PostProcessTime { get; private set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0005AC4E File Offset: 0x00058E4E
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x0005AC56 File Offset: 0x00058E56
		public NumberSetData TotalTime { get; private set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0005AC5F File Offset: 0x00058E5F
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x0005AC67 File Offset: 0x00058E67
		public float AnalysisTime { get; private set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0005AC70 File Offset: 0x00058E70
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x0005AC78 File Offset: 0x00058E78
		public int SuccessCount { get; private set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005AC81 File Offset: 0x00058E81
		public float SuccessPercentage
		{
			get
			{
				return (float)this.SuccessCount / (float)this.TargetIterationCount * 100f;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0005AC98 File Offset: 0x00058E98
		public GenerationAnalysis(int targetIterationCount)
		{
			this.TargetIterationCount = targetIterationCount;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005ACB2 File Offset: 0x00058EB2
		public void Clear()
		{
			this.IterationCount = 0;
			this.AnalysisTime = 0f;
			this.SuccessCount = 0;
			this.statsSet.Clear();
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0005ACD8 File Offset: 0x00058ED8
		public void Add(GenerationStats stats)
		{
			this.statsSet.Add(stats.Clone());
			this.AnalysisTime += stats.TotalTime;
			int iterationCount = this.IterationCount;
			this.IterationCount = iterationCount + 1;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0005AD1C File Offset: 0x00058F1C
		public void IncrementSuccessCount()
		{
			int successCount = this.SuccessCount;
			this.SuccessCount = successCount + 1;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0005AD3C File Offset: 0x00058F3C
		public void Analyze()
		{
			this.MainPathRoomCount = new NumberSetData(from x in this.statsSet
			select (float)x.MainPathRoomCount);
			this.BranchPathRoomCount = new NumberSetData(from x in this.statsSet
			select (float)x.BranchPathRoomCount);
			this.TotalRoomCount = new NumberSetData(from x in this.statsSet
			select (float)x.TotalRoomCount);
			this.MaxBranchDepth = new NumberSetData(from x in this.statsSet
			select (float)x.MaxBranchDepth);
			this.TotalRetries = new NumberSetData(from x in this.statsSet
			select (float)x.TotalRetries);
			this.PreProcessTime = new NumberSetData(from x in this.statsSet
			select x.PreProcessTime);
			this.MainPathGenerationTime = new NumberSetData(from x in this.statsSet
			select x.MainPathGenerationTime);
			this.BranchPathGenerationTime = new NumberSetData(from x in this.statsSet
			select x.BranchPathGenerationTime);
			this.PostProcessTime = new NumberSetData(from x in this.statsSet
			select x.PostProcessTime);
			this.TotalTime = new NumberSetData(from x in this.statsSet
			select x.TotalTime);
		}

		// Token: 0x04000D75 RID: 3445
		private readonly List<GenerationStats> statsSet = new List<GenerationStats>();
	}
}
