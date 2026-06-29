using System;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x0200024D RID: 589
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Crafting/New Remedium", fileName = "New Remedium")]
	public class SecondsRemedium : Remedium
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00060E72 File Offset: 0x0005F072
		public SecondsRemediumStaticData SecondsRemediumStaticData
		{
			get
			{
				return this._secondsRemediumStaticData;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00060E7A File Offset: 0x0005F07A
		public SecondsRemediumRuntimeData SecondsRemediumRuntimeData
		{
			get
			{
				return this._secondsRemediumRuntimeData;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00060E82 File Offset: 0x0005F082
		public IconSizeDefinition IconSizeDefinition
		{
			get
			{
				return this._iconSizeDefinition;
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00060E8A File Offset: 0x0005F08A
		public override void CreateNewRuntimeData()
		{
			base.CreateNewRuntimeData();
			this._secondsRemediumRuntimeData = new SecondsRemediumRuntimeData();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00060E9D File Offset: 0x0005F09D
		public override bool IsUpgradable()
		{
			return false;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		public override void Upgrade()
		{
			throw new UnityException("Cannot use Upgrade method in SecondsRemedium object");
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00060EAC File Offset: 0x0005F0AC
		public override void Downgrade()
		{
			throw new UnityException("Cannot use Downgrade method in SecondsRemedium");
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00060EB8 File Offset: 0x0005F0B8
		public override void InitializeWithDefaultData()
		{
			base.InitializeWithDefaultData();
			this._secondsRemediumRuntimeData.IsDamaged = this._secondsRemediumStaticData.IsDamaged;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00060ED6 File Offset: 0x0005F0D6
		public override void Add()
		{
			if (!this.BaseRuntimeData.IsAvailable || this._secondsRemediumRuntimeData.IsDamaged)
			{
				this.InitializeWithDefaultData();
			}
			this.BaseRuntimeData.IsAvailable = true;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00060F04 File Offset: 0x0005F104
		public virtual void SetDamage()
		{
			this._secondsRemediumRuntimeData.IsDamaged = true;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00060F12 File Offset: 0x0005F112
		public virtual void Repair()
		{
			this._secondsRemediumRuntimeData.IsDamaged = false;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00060F20 File Offset: 0x0005F120
		public override void Use()
		{
			if (!this.SecondsRemediumRuntimeData.IsDamaged)
			{
				this._secondsRemediumRuntimeData.IsDamaged = true;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00060F3B File Offset: 0x0005F13B
		public override bool IsDamaged()
		{
			return this.SecondsRemediumRuntimeData.IsDamaged;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00060F48 File Offset: 0x0005F148
		public override bool IsLockable()
		{
			return base.IsLockable() && !this.IsDamaged();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00060F60 File Offset: 0x0005F160
		protected override string InnerSerialize()
		{
			return JsonUtility.ToJson(new SecondsItemWrapper
			{
				IsAvailable = this.RuntimeData.IsAvailable,
				IsDamage = this._secondsRemediumRuntimeData.IsDamaged,
				Level = this.RuntimeData.Level,
				Lock = this.RuntimeData.Lock,
				OnExpedition = this.RuntimeData.IsOnExpedition,
				IsEnable = this.RuntimeData.IsEnabled
			});
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00060FE0 File Offset: 0x0005F1E0
		protected override void InnerDeserialize(string jsonData)
		{
			SecondsItemWrapper secondsItemWrapper = JsonUtility.FromJson<SecondsItemWrapper>(jsonData);
			this.RuntimeData.IsAvailable = secondsItemWrapper.IsAvailable;
			this.RuntimeData.Level = secondsItemWrapper.Level;
			this.RuntimeData.Lock = secondsItemWrapper.Lock;
			this.RuntimeData.IsOnExpedition = secondsItemWrapper.OnExpedition;
			this._secondsRemediumRuntimeData.IsDamaged = secondsItemWrapper.IsDamage;
			this.RuntimeData.IsEnabled = secondsItemWrapper.IsEnable;
		}

		// Token: 0x04000ED8 RID: 3800
		[SerializeField]
		private SecondsRemediumStaticData _secondsRemediumStaticData;

		// Token: 0x04000ED9 RID: 3801
		[SerializeField]
		private SecondsRemediumRuntimeData _secondsRemediumRuntimeData;

		// Token: 0x04000EDA RID: 3802
		[SerializeField]
		private IconSizeDefinition _iconSizeDefinition;
	}
}
