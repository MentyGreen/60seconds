using System;
using System.Diagnostics;

namespace DunGen
{
	// Token: 0x020001DE RID: 478
	public sealed class GenerationStats
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00058ABB File Offset: 0x00056CBB
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00058AC3 File Offset: 0x00056CC3
		public int MainPathRoomCount { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00058ACC File Offset: 0x00056CCC
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00058AD4 File Offset: 0x00056CD4
		public int BranchPathRoomCount { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00058ADD File Offset: 0x00056CDD
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x00058AE5 File Offset: 0x00056CE5
		public int TotalRoomCount { get; private set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00058AEE File Offset: 0x00056CEE
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00058AF6 File Offset: 0x00056CF6
		public int MaxBranchDepth { get; private set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00058AFF File Offset: 0x00056CFF
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x00058B07 File Offset: 0x00056D07
		public int TotalRetries { get; private set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00058B10 File Offset: 0x00056D10
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x00058B18 File Offset: 0x00056D18
		public float PreProcessTime { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00058B21 File Offset: 0x00056D21
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00058B29 File Offset: 0x00056D29
		public float MainPathGenerationTime { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00058B32 File Offset: 0x00056D32
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00058B3A File Offset: 0x00056D3A
		public float BranchPathGenerationTime { get; private set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00058B43 File Offset: 0x00056D43
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00058B4B File Offset: 0x00056D4B
		public float PostProcessTime { get; private set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00058B54 File Offset: 0x00056D54
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x00058B5C File Offset: 0x00056D5C
		public float TotalTime { get; private set; }

		// Token: 0x060013AE RID: 5038 RVA: 0x00058B68 File Offset: 0x00056D68
		internal void Clear()
		{
			this.MainPathRoomCount = (this.BranchPathRoomCount = (this.TotalRoomCount = (this.MaxBranchDepth = (this.TotalRetries = 0))));
			this.PreProcessTime = (this.MainPathGenerationTime = (this.BranchPathGenerationTime = (this.PostProcessTime = (this.TotalTime = 0f))));
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00058BD8 File Offset: 0x00056DD8
		internal void IncrementRetryCount()
		{
			int totalRetries = this.TotalRetries;
			this.TotalRetries = totalRetries + 1;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00058BF5 File Offset: 0x00056DF5
		internal void SetRoomStatistics(int mainPathRoomCount, int branchPathRoomCount, int maxBranchDepth)
		{
			this.MainPathRoomCount = mainPathRoomCount;
			this.BranchPathRoomCount = branchPathRoomCount;
			this.MaxBranchDepth = maxBranchDepth;
			this.TotalRoomCount = this.MainPathRoomCount + this.BranchPathRoomCount;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00058C1F File Offset: 0x00056E1F
		internal void BeginTime(GenerationStatus status)
		{
			if (this.stopwatch.IsRunning)
			{
				this.EndTime();
			}
			this.generationStatus = status;
			this.stopwatch.Reset();
			this.stopwatch.Start();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00058C54 File Offset: 0x00056E54
		internal void EndTime()
		{
			this.stopwatch.Stop();
			float num = (float)this.stopwatch.Elapsed.TotalMilliseconds;
			switch (this.generationStatus)
			{
			case GenerationStatus.PreProcessing:
				this.PreProcessTime += num;
				break;
			case GenerationStatus.MainPath:
				this.MainPathGenerationTime += num;
				break;
			case GenerationStatus.Branching:
				this.BranchPathGenerationTime += num;
				break;
			case GenerationStatus.PostProcessing:
				this.PostProcessTime += num;
				break;
			}
			this.TotalTime = this.PreProcessTime + this.MainPathGenerationTime + this.BranchPathGenerationTime + this.PostProcessTime;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00058D04 File Offset: 0x00056F04
		public GenerationStats Clone()
		{
			return new GenerationStats
			{
				MainPathRoomCount = this.MainPathRoomCount,
				BranchPathRoomCount = this.BranchPathRoomCount,
				TotalRoomCount = this.TotalRoomCount,
				MaxBranchDepth = this.MaxBranchDepth,
				TotalRetries = this.TotalRetries,
				PreProcessTime = this.PreProcessTime,
				MainPathGenerationTime = this.MainPathGenerationTime,
				BranchPathGenerationTime = this.BranchPathGenerationTime,
				PostProcessTime = this.PostProcessTime,
				TotalTime = this.TotalTime
			};
		}

		// Token: 0x04000CF7 RID: 3319
		private Stopwatch stopwatch = new Stopwatch();

		// Token: 0x04000CF8 RID: 3320
		private GenerationStatus generationStatus;
	}
}
