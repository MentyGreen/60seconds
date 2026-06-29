using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002DC RID: 732
	[CreateAssetMenu(fileName = "New Character", menuName = "60 Seconds Remaster!/Characters/New Character")]
	public class SecondsCharacter : Character
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0006F59C File Offset: 0x0006D79C
		public IconSizeDefinition SizeDefinition
		{
			get
			{
				return this._iconSizeDefinition;
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0006F5A4 File Offset: 0x0006D7A4
		public override bool CanLock()
		{
			return CharacterManager.Instance.GetCharacterList().CharactersInGame.Contains(this) && (base.CanLock() && base.RuntimeData.IsDrawnOnShip()) && base.RuntimeData.IsAlive();
		}

		// Token: 0x04001394 RID: 5012
		[SerializeField]
		private IconSizeDefinition _iconSizeDefinition;
	}
}
