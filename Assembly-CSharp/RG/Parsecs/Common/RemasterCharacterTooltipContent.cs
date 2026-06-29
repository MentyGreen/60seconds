using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Parsecs.Common
{
	// Token: 0x0200020E RID: 526
	public class RemasterCharacterTooltipContent : TooltipContent
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0005BA94 File Offset: 0x00059C94
		public Character Character
		{
			get
			{
				if (this._useForcedCharacter)
				{
					if (this._forcedCharacter == null)
					{
						Debug.LogWarning("Bad setup in CharacterTooltipContent in " + base.gameObject.name);
						return null;
					}
					return this._forcedCharacter;
				}
				else
				{
					if (this._characterList == null || this._characterList.CharactersInGame.Count <= this._characterIndex)
					{
						Debug.LogWarning("Bad setup in CharacterTooltipContent in " + base.gameObject.name);
						return null;
					}
					return this._characterList.CharactersInGame[this._characterIndex];
				}
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005BB34 File Offset: 0x00059D34
		public override bool IsValid()
		{
			Character character = null;
			if (this._useForcedCharacter && this._forcedCharacter != null)
			{
				character = this._forcedCharacter;
			}
			else if (this._characterList != null && this._characterList.CharactersInGame.Count > this._characterIndex)
			{
				character = this._characterList.CharactersInGame[this._characterIndex];
			}
			return !(character == null) && character.RuntimeData.IsAlive() && !character.RuntimeData.IsOnExpedition() && this._characterList.CharactersInGame.Contains(character);
		}

		// Token: 0x04000D91 RID: 3473
		[SerializeField]
		[Range(0f, 3f)]
		private int _characterIndex;

		// Token: 0x04000D92 RID: 3474
		[SerializeField]
		private CharacterList _characterList;

		// Token: 0x04000D93 RID: 3475
		[SerializeField]
		private bool _useForcedCharacter;

		// Token: 0x04000D94 RID: 3476
		[Tooltip("If Use Forced Character is true, this character will be used. In that case it can't be null.")]
		[SerializeField]
		private Character _forcedCharacter;
	}
}
