using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Daikon Forge/User Interface/GUI Camera")]
[Serializable]
public class dfGUICamera : MonoBehaviour
{
	// Token: 0x060006EC RID: 1772 RVA: 0x0001DB70 File Offset: 0x0001BD70
	public void Awake()
	{
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0001DB72 File Offset: 0x0001BD72
	public void OnEnable()
	{
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0001DB74 File Offset: 0x0001BD74
	public void Start()
	{
		Camera component = base.GetComponent<Camera>();
		if (component.orthographicSize <= 0.01f)
		{
			component.orthographicSize = 1f;
		}
		component.transparencySortMode = TransparencySortMode.Orthographic;
		component.useOcclusionCulling = false;
		base.GetComponent<Camera>().eventMask &= ~base.GetComponent<Camera>().cullingMask;
	}
}
