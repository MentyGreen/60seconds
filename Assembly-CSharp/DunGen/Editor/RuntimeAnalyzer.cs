using System;
using System.Diagnostics;
using System.Text;
using DunGen.Analysis;
using DunGen.Graph;
using UnityEngine;

namespace DunGen.Editor
{
	// Token: 0x02000202 RID: 514
	[AddComponentMenu("DunGen/Analysis/Runtime Analyzer")]
	public sealed class RuntimeAnalyzer : MonoBehaviour
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x0005A603 File Offset: 0x00058803
		private void Start()
		{
			if (this.RunOnStart)
			{
				this.Analyze();
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0005A614 File Offset: 0x00058814
		public void Analyze()
		{
			bool flag = false;
			if (this.DungeonFlow == null)
			{
				Debug.LogError("No DungeonFlow assigned to analyzer");
			}
			else if (this.Iterations <= 0)
			{
				Debug.LogError("Iteration count must be greater than 0");
			}
			else if (this.MaxFailedAttempts <= 0)
			{
				Debug.LogError("Max failed attempt count must be greater than 0");
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			this.prevShouldRandomizeSeed = this.generator.ShouldRandomizeSeed;
			this.generator.DungeonFlow = this.DungeonFlow;
			this.generator.MaxAttemptCount = this.MaxFailedAttempts;
			this.generator.ShouldRandomizeSeed = true;
			this.analysis = new GenerationAnalysis(this.Iterations);
			this.analysisTime = 0.0;
			this.currentIterations = 0;
			this.targetIterations = this.Iterations;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0005A6E0 File Offset: 0x000588E0
		private void Update()
		{
			if (this.targetIterations <= 0)
			{
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = 0;
			int num2 = this.targetIterations - this.currentIterations;
			int num3 = 0;
			while (num3 < num2 && stopwatch.Elapsed.TotalSeconds < (double)this.PerFrameAnalysisTime)
			{
				if (this.generator.Generate())
				{
					this.analysis.IncrementSuccessCount();
					this.analysis.Add(this.generator.GenerationStats);
				}
				this.currentIterations++;
				num++;
				num3++;
			}
			this.analysisTime += stopwatch.Elapsed.TotalSeconds;
			if (this.MaximumAnalysisTime > 0f && this.analysisTime >= (double)this.MaximumAnalysisTime)
			{
				this.targetIterations = this.currentIterations;
				this.finishedEarly = true;
			}
			if (this.currentIterations >= this.targetIterations)
			{
				this.targetIterations = 0;
				this.analysis.Analyze();
				Object.Destroy(this.generator.Root);
				this.OnAnalysisComplete();
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0005A7F4 File Offset: 0x000589F4
		private void OnAnalysisComplete()
		{
			this.generator.ShouldRandomizeSeed = this.prevShouldRandomizeSeed;
			this.infoText.Length = 0;
			Debug.Log(this.analysis.MaxBranchDepth);
			if (this.finishedEarly)
			{
				this.infoText.AppendLine("[ Reached maximum analysis time before the target number of iterations was reached ]");
			}
			this.infoText.AppendFormat("Iterations: {0}, Max Failed Attempts: {1}", this.finishedEarly ? this.analysis.IterationCount : this.analysis.TargetIterationCount, this.MaxFailedAttempts);
			this.infoText.AppendFormat("\nTotal Analysis Time: {0:0.00} seconds", this.analysisTime);
			this.infoText.AppendFormat("\nDungeons successfully generated: {0}% ({1} failed)", Mathf.RoundToInt(this.analysis.SuccessPercentage), this.analysis.TargetIterationCount - this.analysis.SuccessCount);
			this.infoText.AppendLine();
			this.infoText.AppendLine();
			this.infoText.Append("## TIME TAKEN (in milliseconds) ##");
			this.infoText.AppendFormat("\n\tPre-Processing:\t\t\t\t\t{0}", this.analysis.PreProcessTime);
			this.infoText.AppendFormat("\n\tMain Path Generation:\t\t{0}", this.analysis.MainPathGenerationTime);
			this.infoText.AppendFormat("\n\tBranch Path Generation:\t\t{0}", this.analysis.BranchPathGenerationTime);
			this.infoText.AppendFormat("\n\tPost-Processing:\t\t\t\t{0}", this.analysis.PostProcessTime);
			this.infoText.Append("\n\t-------------------------------------------------------");
			this.infoText.AppendFormat("\n\tTotal:\t\t\t\t\t\t\t\t{0}", this.analysis.TotalTime);
			this.infoText.AppendLine();
			this.infoText.AppendLine();
			this.infoText.AppendLine("## ROOM DATA ##");
			this.infoText.AppendFormat("\n\tMain Path Rooms: {0}", this.analysis.MainPathRoomCount);
			this.infoText.AppendFormat("\n\tBranch Path Rooms: {0}", this.analysis.BranchPathRoomCount);
			this.infoText.Append("\n\t-------------------");
			this.infoText.AppendFormat("\n\tTotal: {0}", this.analysis.TotalRoomCount);
			this.infoText.AppendLine();
			this.infoText.AppendLine();
			this.infoText.AppendFormat("Retry Count: {0}", this.analysis.TotalRetries);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0005AA70 File Offset: 0x00058C70
		private void OnGUI()
		{
			if (this.analysis == null || this.infoText == null || this.infoText.Length == 0)
			{
				string text = (this.analysis.SuccessCount < this.analysis.IterationCount) ? ("\nFailed Dungeons: " + (this.analysis.IterationCount - this.analysis.SuccessCount).ToString()) : "";
				GUILayout.Label(string.Format("Analyzing... {0} / {1} ({2:0.0}%){3}", new object[]
				{
					this.currentIterations,
					this.targetIterations,
					(float)this.currentIterations / (float)this.targetIterations * 100f,
					text
				}), Array.Empty<GUILayoutOption>());
				return;
			}
			GUILayout.Label(this.infoText.ToString(), Array.Empty<GUILayoutOption>());
		}

		// Token: 0x04000D59 RID: 3417
		public DungeonFlow DungeonFlow;

		// Token: 0x04000D5A RID: 3418
		public int Iterations = 100;

		// Token: 0x04000D5B RID: 3419
		public int MaxFailedAttempts = 20;

		// Token: 0x04000D5C RID: 3420
		public bool RunOnStart = true;

		// Token: 0x04000D5D RID: 3421
		public float MaximumAnalysisTime;

		// Token: 0x04000D5E RID: 3422
		public float PerFrameAnalysisTime = 0.1f;

		// Token: 0x04000D5F RID: 3423
		private DungeonGenerator generator = new DungeonGenerator();

		// Token: 0x04000D60 RID: 3424
		private GenerationAnalysis analysis;

		// Token: 0x04000D61 RID: 3425
		private StringBuilder infoText = new StringBuilder();

		// Token: 0x04000D62 RID: 3426
		private int targetIterations;

		// Token: 0x04000D63 RID: 3427
		private int currentIterations;

		// Token: 0x04000D64 RID: 3428
		private double analysisTime;

		// Token: 0x04000D65 RID: 3429
		private bool finishedEarly;

		// Token: 0x04000D66 RID: 3430
		private bool prevShouldRandomizeSeed;
	}
}
