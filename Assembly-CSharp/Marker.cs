using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class Marker : MonoBehaviour
{
	// Token: 0x14000066 RID: 102
	// (add) Token: 0x06000E62 RID: 3682 RVA: 0x0003B7E8 File Offset: 0x000399E8
	// (remove) Token: 0x06000E63 RID: 3683 RVA: 0x0003B820 File Offset: 0x00039A20
	public event Marker.Report OnEnter;

	// Token: 0x06000E64 RID: 3684 RVA: 0x0003B855 File Offset: 0x00039A55
	private void Start()
	{
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0003B857 File Offset: 0x00039A57
	private void Update()
	{
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0003B85C File Offset: 0x00039A5C
	public void Show(bool show)
	{
		if (this._markerVisual != null)
		{
			iTween.Stop(this._markerVisual);
			Hashtable hashtable = new Hashtable();
			hashtable.Add("scale", show ? this._nominalScale : Vector3.zero);
			hashtable.Add("time", this._showTime);
			hashtable.Add("looptype", iTween.LoopType.none);
			hashtable.Add("easeType", "easeInOutSine");
			iTween.ScaleTo(this._markerVisual, hashtable);
		}
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0003B8EC File Offset: 0x00039AEC
	public void Animate()
	{
		if (this._markerVisual != null)
		{
			iTween.Stop(this._markerVisual);
			if (this._animations != null)
			{
				for (int i = 0; i < this._animations.Length; i++)
				{
					if (this._animations[i] != Marker.EMarkerAnimation.NONE)
					{
						Hashtable hashtable = new Hashtable();
						Marker.EMarkerAnimation emarkerAnimation = this._animations[i];
						if (emarkerAnimation != Marker.EMarkerAnimation.ROTATION)
						{
							if (emarkerAnimation == Marker.EMarkerAnimation.PULSE)
							{
								hashtable.Add("amount", this._scale);
								hashtable.Add("time", this._scaleTime);
								hashtable.Add("looptype", iTween.LoopType.pingPong);
								hashtable.Add("easeType", "easeInOutSine");
								iTween.ScaleBy(this._markerVisual, hashtable);
							}
						}
						else
						{
							hashtable = new Hashtable();
							hashtable.Add("amount", this._rotation);
							hashtable.Add("time", this._rotationTime);
							hashtable.Add("looptype", iTween.LoopType.loop);
							hashtable.Add("easeType", "linear");
							iTween.RotateBy(this._markerVisual, hashtable);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0003BA1C File Offset: 0x00039C1C
	private void OnTriggerEnter(Collider collider)
	{
		if (this.OnEnter != null)
		{
			this.OnEnter(collider.gameObject, this);
		}
		this._currentUser = collider.gameObject;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0003BA44 File Offset: 0x00039C44
	private void OnTriggerExit(Collider collider)
	{
		if (this._currentUser == collider.gameObject)
		{
			this._currentUser = null;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003BA60 File Offset: 0x00039C60
	public GameObject CurrentUser
	{
		get
		{
			return this._currentUser;
		}
	}

	// Token: 0x0400088F RID: 2191
	[SerializeField]
	private GameObject _markerVisual;

	// Token: 0x04000890 RID: 2192
	[SerializeField]
	private Marker.EMarkerAnimation[] _animations;

	// Token: 0x04000891 RID: 2193
	[SerializeField]
	private float _scaleTime = 1f;

	// Token: 0x04000892 RID: 2194
	[SerializeField]
	private float _rotationTime = 1f;

	// Token: 0x04000893 RID: 2195
	[SerializeField]
	private float _showTime = 0.5f;

	// Token: 0x04000894 RID: 2196
	[SerializeField]
	private Vector3 _rotation = Vector3.zero;

	// Token: 0x04000895 RID: 2197
	[SerializeField]
	private Vector3 _nominalScale = Vector3.zero;

	// Token: 0x04000896 RID: 2198
	[SerializeField]
	private Vector3 _scale = Vector3.zero;

	// Token: 0x04000897 RID: 2199
	private GameObject _currentUser;

	// Token: 0x020003B4 RID: 948
	public enum EMarkerAnimation
	{
		// Token: 0x0400171C RID: 5916
		NONE,
		// Token: 0x0400171D RID: 5917
		ROTATION,
		// Token: 0x0400171E RID: 5918
		PULSE
	}

	// Token: 0x020003B5 RID: 949
	// (Invoke) Token: 0x06001DE4 RID: 7652
	public delegate void Report(GameObject entrant, Marker where);
}
