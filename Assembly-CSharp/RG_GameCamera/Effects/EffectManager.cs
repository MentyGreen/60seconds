using System;
using System.Collections.Generic;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B4 RID: 436
	internal class EffectManager : MonoBehaviour
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x000502DF File Offset: 0x0004E4DF
		public static EffectManager Instance
		{
			get
			{
				if (!EffectManager.instance)
				{
					EffectManager.instance = CameraInstance.CreateInstance<EffectManager>("EffectManager");
				}
				return EffectManager.instance;
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00050301 File Offset: 0x0004E501
		private void Awake()
		{
			EffectManager.instance = this;
			this.effects = new List<Effect>();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00050314 File Offset: 0x0004E514
		public void Register(Effect effect)
		{
			if (effect != null)
			{
				this.effects.Add(effect);
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0005032C File Offset: 0x0004E52C
		public void StopAll()
		{
			foreach (Effect effect in this.effects)
			{
				effect.Stop();
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0005037C File Offset: 0x0004E57C
		public T Create<T>() where T : Effect
		{
			T t = base.gameObject.GetComponent<T>();
			if (!t)
			{
				t = base.gameObject.AddComponent<T>();
				if (t)
				{
					this.Register(t);
					t.Init();
				}
			}
			return t;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000503D4 File Offset: 0x0004E5D4
		public Effect Create(Type effectType)
		{
			switch (effectType)
			{
			case Type.Explosion:
				return this.Create<Explosion>();
			case Type.Stomp:
				return this.Create<Stomp>();
			case Type.Earthquake:
				return this.Create<Earthquake>();
			case Type.Yes:
				return this.Create<Yes>();
			case Type.No:
				return this.Create<No>();
			case Type.FireKick:
				return this.Create<FireKick>();
			case Type.SprintShake:
				return this.Create<SprintShake>();
			default:
				return null;
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00050437 File Offset: 0x0004E637
		public void Delete(Effect effect)
		{
			if (this.effects.Contains(effect))
			{
				this.effects.Remove(effect);
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00050454 File Offset: 0x0004E654
		public void PostUpdate()
		{
			foreach (Effect effect in this.effects)
			{
				if (effect.Playing)
				{
					effect.PostUpdate();
				}
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000504B0 File Offset: 0x0004E6B0
		private void OnGUI()
		{
		}

		// Token: 0x04000C3D RID: 3133
		private static EffectManager instance;

		// Token: 0x04000C3E RID: 3134
		private List<Effect> effects;
	}
}
