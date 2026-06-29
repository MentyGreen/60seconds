using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class RuntimeMeshBatcherController : MonoBehaviour
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000336BF File Offset: 0x000318BF
	// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000336C6 File Offset: 0x000318C6
	public static RuntimeMeshBatcherController instance { get; set; }

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000336CE File Offset: 0x000318CE
	protected void Awake()
	{
		RuntimeMeshBatcherController.instance = this;
		RuntimeMeshBatcher.rmbContainerGameObject = base.gameObject;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x000336E1 File Offset: 0x000318E1
	protected void Start()
	{
		if (this.autoRun)
		{
			this.CombineMeshes();
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x000336F4 File Offset: 0x000318F4
	public GameObject CombineMeshes()
	{
		return RuntimeMeshBatcher.CombineMeshes(this.rmbLayer, this.processObjectsByTag, this.rmbTag, this.processObjectsByLayer, this.destroyOriginalObjects, this.keepOriginalObjectReferences, this.combineByGrid, this.gridType, this.gridSize);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0003373C File Offset: 0x0003193C
	public void UncombineMeshes(GameObject rmbParent)
	{
		RuntimeMeshBatcher.UncombineMeshes(rmbParent);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00033744 File Offset: 0x00031944
	public GameObject CombineMeshes(GameObject[] objectsToBatch)
	{
		return RuntimeMeshBatcher.CombineMeshes(objectsToBatch, this.destroyOriginalObjects, this.keepOriginalObjectReferences, this.combineByGrid, this.gridType, this.gridSize);
	}

	// Token: 0x04000607 RID: 1543
	public bool processObjectsByLayer;

	// Token: 0x04000608 RID: 1544
	public bool processObjectsByTag;

	// Token: 0x04000609 RID: 1545
	public LayerMask rmbLayer;

	// Token: 0x0400060A RID: 1546
	public string rmbTag;

	// Token: 0x0400060B RID: 1547
	public bool destroyOriginalObjects;

	// Token: 0x0400060C RID: 1548
	public bool keepOriginalObjectReferences;

	// Token: 0x0400060D RID: 1549
	public bool combineByGrid;

	// Token: 0x0400060E RID: 1550
	public MeshBatcherGridType gridType;

	// Token: 0x0400060F RID: 1551
	public float gridSize;

	// Token: 0x04000610 RID: 1552
	public bool autoRun;
}
