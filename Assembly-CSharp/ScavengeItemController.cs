using System;
using HighlightingSystem;
using RG.Parsecs.Common;
using RG.Remaster.Scavenge;
using RG.SecondsRemaster.Scavenge;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class ScavengeItemController : MonoBehaviour
{
	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0003D5FE File Offset: 0x0003B7FE
	public Transform RaycastTarget
	{
		get
		{
			return this._raycastTarget;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0003D606 File Offset: 0x0003B806
	public float MaxReachDistance
	{
		get
		{
			return this._maxReachDistance;
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0003D610 File Offset: 0x0003B810
	public void Start()
	{
		this._specialActionCounter += this._specialActionTimeout;
		this._childrenHighlighters = base.GetComponentsInChildren<Highlighter>();
		this._childHighlighter = base.GetComponentInChildren<Highlighter>();
		this._animator = base.GetComponent<Animator>();
		SphereCollider componentInChildren = base.GetComponentInChildren<SphereCollider>();
		if (componentInChildren != null)
		{
			this._maxReachDistance = componentInChildren.radius;
		}
		this.Highlight(true);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0003D677 File Offset: 0x0003B877
	public void OnEnable()
	{
		ScavengeItemManager.Instance.RegisterController(this);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0003D684 File Offset: 0x0003B884
	private void HighlightObject(Highlighter highlighter, bool on)
	{
		if (highlighter != null)
		{
			PlayerInteraction playerInteraction = GlobalTools.GetPlayerInteraction();
			if (playerInteraction != null)
			{
				if (on)
				{
					highlighter.ConstantOn(playerInteraction.HighlightColor, 0.25f);
					return;
				}
				highlighter.ConstantOff(0.25f);
			}
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0003D6CC File Offset: 0x0003B8CC
	public void Highlight(bool on)
	{
		if (this._canBePickedUp)
		{
			for (int i = 0; i < this._childrenHighlighters.Length; i++)
			{
				this.HighlightObject(this._childrenHighlighters[i], on);
			}
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0003D704 File Offset: 0x0003B904
	public void TargetHighlight(bool on)
	{
		if (this._canBePickedUp)
		{
			PlayerInteraction playerInteraction = GlobalTools.GetPlayerInteraction();
			GameFlow controller = GlobalTools.GetController<GameFlow>();
			if (this._childHighlighter != null)
			{
				this._childHighlighter.ConstantOffImmediate();
				if (on)
				{
					if (controller.HandsController.WillItemFit(this.ScavengeItem))
					{
						this._childHighlighter.ConstantOn(playerInteraction.TargetHighlightColor, 0.25f);
						return;
					}
					this._childHighlighter.ConstantOn(playerInteraction.TargetHighlightColorInventoryFull, 0.25f);
					return;
				}
				else
				{
					this._childHighlighter.ConstantOn(playerInteraction.HighlightColor, 0.25f);
				}
			}
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0003D798 File Offset: 0x0003B998
	public void PlaySound(string soundName)
	{
		AudioManager.PlaySoundAtPoint(soundName, base.transform, 1f, 1f, 0f);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0003D7B5 File Offset: 0x0003B9B5
	public void DoSpecialAction()
	{
		if (this._animator != null && this._specialActionCounter <= Time.time)
		{
			this._animator.SetTrigger("specialAction");
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0003D7E2 File Offset: 0x0003B9E2
	public void EndSpecialAction()
	{
		if (this._animator != null)
		{
			this._specialActionCounter = Time.time + this._specialActionTimeout;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003D804 File Offset: 0x0003BA04
	public string IconName
	{
		get
		{
			return this._iconName;
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0003D80C File Offset: 0x0003BA0C
	public string SurvivalName
	{
		get
		{
			return this._survivalName;
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0003D814 File Offset: 0x0003BA14
	public int Weight
	{
		get
		{
			return this._weight;
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003D81C File Offset: 0x0003BA1C
	public bool SpecialItem
	{
		get
		{
			return this._specialItem;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003D824 File Offset: 0x0003BA24
	// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0003D82C File Offset: 0x0003BA2C
	public bool CanBePickedUp
	{
		get
		{
			return this._canBePickedUp;
		}
		set
		{
			this._canBePickedUp = value;
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003D835 File Offset: 0x0003BA35
	public bool IsCharacter
	{
		get
		{
			return this._character > ECharacter.NONE;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0003D840 File Offset: 0x0003BA40
	public ECharacter Character
	{
		get
		{
			return this._character;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003D848 File Offset: 0x0003BA48
	public ScavengeItem ScavengeItem
	{
		get
		{
			return this._scavengeItem;
		}
	}

	// Token: 0x040008EF RID: 2287
	[SerializeField]
	private string _name = string.Empty;

	// Token: 0x040008F0 RID: 2288
	[SerializeField]
	private string _iconName = string.Empty;

	// Token: 0x040008F1 RID: 2289
	[SerializeField]
	private string _survivalName = string.Empty;

	// Token: 0x040008F2 RID: 2290
	[SerializeField]
	private int _weight;

	// Token: 0x040008F3 RID: 2291
	[SerializeField]
	private ScavengeItem _scavengeItem;

	// Token: 0x040008F4 RID: 2292
	[SerializeField]
	private ECharacter _character;

	// Token: 0x040008F5 RID: 2293
	[SerializeField]
	private bool _specialItem;

	// Token: 0x040008F6 RID: 2294
	[SerializeField]
	private bool _canBePickedUp = true;

	// Token: 0x040008F7 RID: 2295
	[SerializeField]
	private float _specialActionTimeout;

	// Token: 0x040008F8 RID: 2296
	private float _specialActionCounter;

	// Token: 0x040008F9 RID: 2297
	[SerializeField]
	private Animator _animator;

	// Token: 0x040008FA RID: 2298
	[SerializeField]
	private Transform _raycastTarget;

	// Token: 0x040008FB RID: 2299
	[SerializeField]
	private Highlighter[] _childrenHighlighters;

	// Token: 0x040008FC RID: 2300
	[SerializeField]
	private Highlighter _childHighlighter;

	// Token: 0x040008FD RID: 2301
	private float _maxReachDistance = 1.5f;
}
