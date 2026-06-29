using System;
using System.Collections;
using System.Collections.Generic;
using RG.Parsecs.Scavenge;
using UnityEngine;

namespace RG.Parsecs.Socials
{
	// Token: 0x02000210 RID: 528
	public class SharePromise
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x0005C2F5 File Offset: 0x0005A4F5
		public SharePromise()
		{
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005C31E File Offset: 0x0005A51E
		public SharePromise(Action<SharePromise> work)
		{
			this.Work = work;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005C34E File Offset: 0x0005A54E
		public SharePromise(Func<SharePromise, IEnumerator> work, MonoBehaviour coroutineHost)
		{
			this.WorkCoroutine = work;
			this.CoroutineHost = coroutineHost;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005C385 File Offset: 0x0005A585
		private void requiredSuccess(SharePromise req)
		{
			this._requirements.Remove(req);
			if (this._requirements.Count == 0)
			{
				this.Process();
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005C3A7 File Offset: 0x0005A5A7
		private void requiredFailure(SharePromise req)
		{
			this.Failure();
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0005C3AF File Offset: 0x0005A5AF
		public void Require(SharePromise promise)
		{
			promise.OnSuccess.AddOneTime(new Action<SharePromise>(this.requiredSuccess));
			promise.OnFailure.AddOneTime(new Action<SharePromise>(this.requiredFailure));
			this._requirements.Add(promise);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0005C3EB File Offset: 0x0005A5EB
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0005C3F3 File Offset: 0x0005A5F3
		public bool IsFinished { get; protected set; }

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005C3FC File Offset: 0x0005A5FC
		public void Process()
		{
			if (this._isProcessing)
			{
				return;
			}
			this._isProcessing = true;
			if (this.Work != null)
			{
				this.Work(this);
				return;
			}
			if (this.WorkCoroutine != null)
			{
				this.CoroutineHost.StartCoroutine(this.WorkCoroutine(this));
				return;
			}
			this.Success();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005C455 File Offset: 0x0005A655
		public void Success()
		{
			if (this.IsFinished)
			{
				return;
			}
			this.IsFinished = true;
			this.OnSuccess.Invoke(this);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005C474 File Offset: 0x0005A674
		public void Failure()
		{
			if (this.IsFinished)
			{
				return;
			}
			this.IsFinished = true;
			foreach (SharePromise sharePromise in this._requirements)
			{
				sharePromise.OnSuccess.Remove(new Action<SharePromise>(this.requiredSuccess));
				sharePromise.OnFailure.Remove(new Action<SharePromise>(this.requiredFailure));
			}
			this._requirements.Clear();
			this.OnFailure.Invoke(this);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005C518 File Offset: 0x0005A718
		public void Resolve(bool s)
		{
			if (s)
			{
				this.Success();
				return;
			}
			this.Failure();
		}

		// Token: 0x04000DA8 RID: 3496
		public float Progress;

		// Token: 0x04000DA9 RID: 3497
		public GenericEvent<SharePromise> OnSuccess = new GenericEvent<SharePromise>();

		// Token: 0x04000DAA RID: 3498
		public GenericEvent<SharePromise> OnFailure = new GenericEvent<SharePromise>();

		// Token: 0x04000DAB RID: 3499
		private List<SharePromise> _requirements = new List<SharePromise>();

		// Token: 0x04000DAC RID: 3500
		private Func<SharePromise, IEnumerator> WorkCoroutine;

		// Token: 0x04000DAD RID: 3501
		private Action<SharePromise> Work;

		// Token: 0x04000DAE RID: 3502
		private MonoBehaviour CoroutineHost;

		// Token: 0x04000DAF RID: 3503
		private bool _isProcessing;
	}
}
