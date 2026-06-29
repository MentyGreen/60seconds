using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.Remaster.Menu
{
	// Token: 0x02000228 RID: 552
	public class FlyingObjectBackgroundSoundPlayer : MonoBehaviour
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0005E87C File Offset: 0x0005CA7C
		public void PlaySound()
		{
			if (!this._defaultFlyingObject.activeInHierarchy)
			{
				if (this._alternativeFlyingObject.activeInHierarchy)
				{
					if (this._isObjectInverted.Value)
					{
						AudioManager.PlaySoundAndReturnInstance(this._invertedAlternativeFlyingObjectSoundSlot.SoundEventName, 1f, 1f, 0f);
						return;
					}
					AudioManager.PlaySoundAndReturnInstance(this._alternativeFlyingObjectSoundSlot.SoundEventName, 1f, 1f, 0f);
				}
				return;
			}
			if (this._isObjectInverted.Value)
			{
				AudioManager.PlaySoundAndReturnInstance(this._invertedDefaultFlyingObjectSoundSlot.SoundEventName, 1f, 1f, 0f);
				return;
			}
			AudioManager.PlaySoundAndReturnInstance(this._defaultFlyingObjectSoundSlot.SoundEventName, 1f, 1f, 0f);
		}

		// Token: 0x04000E45 RID: 3653
		[SerializeField]
		private SoundSlot _defaultFlyingObjectSoundSlot;

		// Token: 0x04000E46 RID: 3654
		[SerializeField]
		private SoundSlot _alternativeFlyingObjectSoundSlot;

		// Token: 0x04000E47 RID: 3655
		[SerializeField]
		private SoundSlot _invertedDefaultFlyingObjectSoundSlot;

		// Token: 0x04000E48 RID: 3656
		[SerializeField]
		private SoundSlot _invertedAlternativeFlyingObjectSoundSlot;

		// Token: 0x04000E49 RID: 3657
		[SerializeField]
		private GameObject _defaultFlyingObject;

		// Token: 0x04000E4A RID: 3658
		[SerializeField]
		private GameObject _alternativeFlyingObject;

		// Token: 0x04000E4B RID: 3659
		[SerializeField]
		private GlobalBoolVariable _isObjectInverted;
	}
}
