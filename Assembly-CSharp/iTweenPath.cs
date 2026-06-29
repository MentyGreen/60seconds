using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
[AddComponentMenu("Pixelplacement/iTweenPath")]
public class iTweenPath : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x000020A9 File Offset: 0x000002A9
	private void OnEnable()
	{
		if (!iTweenPath.paths.ContainsKey(this.pathName))
		{
			iTweenPath.paths.Add(this.pathName.ToLower(), this);
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020D3 File Offset: 0x000002D3
	private void OnDisable()
	{
		iTweenPath.paths.Remove(this.pathName.ToLower());
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020EB File Offset: 0x000002EB
	private void OnDrawGizmosSelected()
	{
		if (this.pathVisible && this.nodes.Count > 0)
		{
			iTween.DrawPath(this.nodes.ToArray(), this.pathColor);
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
	public static Vector3[] GetPath(string requestedName)
	{
		requestedName = requestedName.ToLower();
		if (iTweenPath.paths.ContainsKey(requestedName))
		{
			return iTweenPath.paths[requestedName].nodes.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
	public static Vector3[] GetPathReversed(string requestedName)
	{
		requestedName = requestedName.ToLower();
		if (iTweenPath.paths.ContainsKey(requestedName))
		{
			List<Vector3> range = iTweenPath.paths[requestedName].nodes.GetRange(0, iTweenPath.paths[requestedName].nodes.Count);
			range.Reverse();
			return range.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	// Token: 0x04000004 RID: 4
	public string pathName = "";

	// Token: 0x04000005 RID: 5
	public Color pathColor = Color.cyan;

	// Token: 0x04000006 RID: 6
	public List<Vector3> nodes = new List<Vector3>
	{
		Vector3.zero,
		Vector3.zero
	};

	// Token: 0x04000007 RID: 7
	public int nodeCount;

	// Token: 0x04000008 RID: 8
	public static Dictionary<string, iTweenPath> paths = new Dictionary<string, iTweenPath>();

	// Token: 0x04000009 RID: 9
	public bool initialized;

	// Token: 0x0400000A RID: 10
	public string initialName = "";

	// Token: 0x0400000B RID: 11
	public bool pathVisible = true;
}
