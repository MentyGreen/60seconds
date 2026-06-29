using System;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B3 RID: 435
	public abstract class Effect : MonoBehaviour
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000500EA File Offset: 0x0004E2EA
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x000500F2 File Offset: 0x0004E2F2
		public bool Playing { get; protected set; }

		// Token: 0x0600128D RID: 4749 RVA: 0x000500FB File Offset: 0x0004E2FB
		private void Start()
		{
			if (!this.unityCamera)
			{
				EffectManager.Instance.Register(this);
				this.Init();
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0005011B File Offset: 0x0004E31B
		public virtual void Init()
		{
			this.Playing = false;
			this.unityCamera = CameraManager.Instance.UnityCamera;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00050134 File Offset: 0x0004E334
		public void Play()
		{
			this.Playing = true;
			this.timeout = 0f;
			this.FadeIn = Mathf.Clamp(this.FadeIn, 0f, this.Length);
			this.FadeOut = Mathf.Clamp(this.FadeOut, 0f, this.Length);
			this.OnPlay();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00050191 File Offset: 0x0004E391
		public void Stop()
		{
			this.Playing = false;
			this.OnStop();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000501A0 File Offset: 0x0004E3A0
		public virtual void OnPlay()
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000501A2 File Offset: 0x0004E3A2
		public virtual void OnStop()
		{
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000501A4 File Offset: 0x0004E3A4
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000501A8 File Offset: 0x0004E3A8
		public void PostUpdate()
		{
			this.timeout += Time.deltaTime;
			this.timeoutNormalized = Mathf.Clamp01(this.timeout / this.Length);
			this.fadeState = Effect.FadeState.Full;
			if (this.FadeIn > 0f)
			{
				if (this.timeout < this.FadeIn)
				{
					this.fadeInNormalized = this.timeout / this.FadeIn;
					this.fadeState = Effect.FadeState.FadeIn;
				}
				else
				{
					this.fadeInNormalized = 1f;
				}
			}
			if (this.FadeOut > 0f)
			{
				if (this.timeout > this.Length - this.FadeOut)
				{
					this.fadeOutNormalized = (this.timeout - (this.Length - this.FadeOut)) / this.FadeOut;
					this.fadeState = Effect.FadeState.FadeOut;
				}
				else
				{
					this.fadeOutNormalized = 0f;
				}
			}
			if (this.timeout > this.Length)
			{
				if (this.Loop)
				{
					this.Play();
				}
				else
				{
					this.Stop();
				}
			}
			this.OnUpdate();
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000502A9 File Offset: 0x0004E4A9
		public void Delete()
		{
			EffectManager.Instance.Delete(this);
		}

		// Token: 0x04000C32 RID: 3122
		public bool Loop;

		// Token: 0x04000C33 RID: 3123
		public float Length = 1f;

		// Token: 0x04000C34 RID: 3124
		public float FadeIn = 0.5f;

		// Token: 0x04000C35 RID: 3125
		public float FadeOut = 0.5f;

		// Token: 0x04000C37 RID: 3127
		protected float timeout;

		// Token: 0x04000C38 RID: 3128
		protected float timeoutNormalized;

		// Token: 0x04000C39 RID: 3129
		protected float fadeInNormalized;

		// Token: 0x04000C3A RID: 3130
		protected float fadeOutNormalized;

		// Token: 0x04000C3B RID: 3131
		protected Effect.FadeState fadeState;

		// Token: 0x04000C3C RID: 3132
		protected Camera unityCamera;

		// Token: 0x020003E7 RID: 999
		protected enum FadeState
		{
			// Token: 0x0400181D RID: 6173
			FadeIn,
			// Token: 0x0400181E RID: 6174
			Full,
			// Token: 0x0400181F RID: 6175
			FadeOut
		}
	}
}
