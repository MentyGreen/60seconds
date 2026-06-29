using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000EB RID: 235
public static class RuntimeMeshBatcher
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x00032D84 File Offset: 0x00030F84
	public static GameObject CombineMeshes(LayerMask rmbLayer, bool processObjectsByTag = false, string rmbTag = null, bool processObjectsByLayer = false, bool destroyOriginalObjects = false, bool keepOriginalObjectReferences = false, bool combineByGrid = false, MeshBatcherGridType gridType = MeshBatcherGridType.Grid2D, float gridSize = 0f)
	{
		List<GameObject> list = new List<GameObject>();
		if (processObjectsByTag && rmbTag != null)
		{
			list.AddRange(GameObject.FindGameObjectsWithTag(rmbTag));
		}
		if (processObjectsByLayer)
		{
			foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
			{
				if (RuntimeMeshBatcher.IsInLayerMask(gameObject.layer, rmbLayer))
				{
					list.Add(gameObject);
				}
			}
		}
		return RuntimeMeshBatcher.CombineMeshes(list.ToArray(), destroyOriginalObjects, keepOriginalObjectReferences, combineByGrid, gridType, gridSize);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00032DF0 File Offset: 0x00030FF0
	private static bool IsInLayerMask(int layer, LayerMask layerMask)
	{
		int num = 1 << layer;
		return (layerMask.value & num) > 0;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00032E14 File Offset: 0x00031014
	public static GameObject CombineMeshes(GameObject[] objectsToBatch, bool destroyOriginalObjects = false, bool keepOriginalObjectReferences = false, bool combineByGrid = false, MeshBatcherGridType gridType = MeshBatcherGridType.Grid2D, float gridSize = 0f)
	{
		if (objectsToBatch == null || objectsToBatch.Length == 0)
		{
			Debug.LogWarning("Runtime Mesh Batcher warning: no objects found to be combined.");
			return null;
		}
		if (combineByGrid && gridSize <= 0f)
		{
			Debug.LogWarning("Runtime Mesh Batcher warning: Grid Size must be superior to 0. Continuing batching without grid.");
			combineByGrid = false;
		}
		GameObject gameObject3;
		if (combineByGrid)
		{
			Dictionary<string, List<GameObject>> dictionary = new Dictionary<string, List<GameObject>>();
			if (gridType != MeshBatcherGridType.Grid2D)
			{
				if (gridType == MeshBatcherGridType.Grid3D)
				{
					foreach (GameObject gameObject in objectsToBatch)
					{
						Vector3 position = gameObject.transform.position;
						int gridIndex = RuntimeMeshBatcher.GetGridIndex(position.x, gridSize);
						int gridIndex2 = RuntimeMeshBatcher.GetGridIndex(position.y, gridSize);
						int gridIndex3 = RuntimeMeshBatcher.GetGridIndex(position.z, gridSize);
						string key = string.Concat(new string[]
						{
							gridIndex.ToString(),
							"_",
							gridIndex2.ToString(),
							"_",
							gridIndex3.ToString()
						});
						if (!dictionary.ContainsKey(key))
						{
							dictionary[key] = new List<GameObject>();
						}
						dictionary[key].Add(gameObject);
					}
				}
			}
			else
			{
				foreach (GameObject gameObject2 in objectsToBatch)
				{
					Vector3 position2 = gameObject2.transform.position;
					int gridIndex4 = RuntimeMeshBatcher.GetGridIndex(position2.x, gridSize);
					int gridIndex5 = RuntimeMeshBatcher.GetGridIndex(position2.z, gridSize);
					string key2 = gridIndex4.ToString() + "_" + gridIndex5.ToString();
					if (!dictionary.ContainsKey(key2))
					{
						dictionary[key2] = new List<GameObject>();
					}
					dictionary[key2].Add(gameObject2);
				}
			}
			gameObject3 = RuntimeMeshBatcher.CreateParent();
			using (Dictionary<string, List<GameObject>>.ValueCollection.Enumerator enumerator = dictionary.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					List<GameObject> list = enumerator.Current;
					RuntimeMeshBatcher.SetParent(RuntimeMeshBatcher.CreateStaticMesh(list.ToArray(), destroyOriginalObjects, true), gameObject3);
				}
				goto IL_1D0;
			}
		}
		gameObject3 = RuntimeMeshBatcher.CreateStaticMesh(objectsToBatch, destroyOriginalObjects, false);
		IL_1D0:
		if (keepOriginalObjectReferences)
		{
			if (RuntimeMeshBatcher.originalObjetReferences == null)
			{
				RuntimeMeshBatcher.originalObjetReferences = new Dictionary<GameObject, GameObject[]>();
			}
			RuntimeMeshBatcher.originalObjetReferences[gameObject3] = objectsToBatch;
		}
		return gameObject3;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00033024 File Offset: 0x00031224
	private static int GetGridIndex(float position, float gridSize)
	{
		return (int)Mathf.Floor(position / gridSize);
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00033030 File Offset: 0x00031230
	private static GameObject CreateParent()
	{
		if (RuntimeMeshBatcher.rmbParents == null)
		{
			RuntimeMeshBatcher.rmbParents = new List<GameObject>();
		}
		GameObject gameObject = new GameObject("RuntimeMeshBatcherParent_" + RuntimeMeshBatcher.rmbParents.Count.ToString());
		RuntimeMeshBatcher.rmbParents.Add(gameObject);
		if (RuntimeMeshBatcher.rmbContainerGameObject == null)
		{
			RuntimeMeshBatcher.rmbContainerGameObject = new GameObject("RuntimeMeshBatcherContainer");
		}
		RuntimeMeshBatcher.SetParent(gameObject, RuntimeMeshBatcher.rmbContainerGameObject);
		return gameObject;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x000330A4 File Offset: 0x000312A4
	private static GameObject CreateStaticMesh(GameObject[] gameObjects, bool destroyOriginalObjects, bool combineByGrid = false)
	{
		GameObject gameObject4 = combineByGrid ? new GameObject("SubParent") : RuntimeMeshBatcher.CreateParent();
		Transform transform = gameObject4.transform;
		Dictionary<Transform, Transform> originalTransforms = new Dictionary<Transform, Transform>();
		foreach (GameObject gameObject2 in gameObjects)
		{
			if (!(gameObject2 == null) && gameObject2.GetComponentsInChildren<Renderer>().Length != 0 && !gameObject2.GetComponentsInChildren<Renderer>()[0].isPartOfStaticBatch)
			{
				Transform transform2 = gameObject2.transform;
				if (!destroyOriginalObjects)
				{
					originalTransforms[transform2] = transform2.parent;
				}
				RuntimeMeshBatcher.SetParent(transform2, transform);
			}
		}
		Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
		Dictionary<Material, List<List<CombineInstance>>> dictionary = new Dictionary<Material, List<List<CombineInstance>>>();
		MeshRenderer[] componentsInChildren = gameObject4.GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (Material material in array[i].sharedMaterials)
			{
				if (material != null && !dictionary.ContainsKey(material))
				{
					dictionary.Add(material, new List<List<CombineInstance>>());
				}
			}
		}
		Dictionary<Material, int> dictionary2 = new Dictionary<Material, int>();
		foreach (MeshFilter meshFilter in gameObject4.GetComponentsInChildren<MeshFilter>())
		{
			if (!(meshFilter.sharedMesh == null))
			{
				if (meshFilter.sharedMesh.subMeshCount > 1)
				{
					MeshRenderer component = meshFilter.GetComponent<MeshRenderer>();
					for (int k = 0; k < component.sharedMaterials.Count<Material>(); k++)
					{
						CombineInstance item = new CombineInstance
						{
							mesh = meshFilter.sharedMesh,
							subMeshIndex = k,
							transform = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix
						};
						Material material2 = component.sharedMaterials[k];
						int vertexCount = item.mesh.vertexCount;
						if (!dictionary2.ContainsKey(material2) || dictionary2[material2] + vertexCount > 65536)
						{
							dictionary2[material2] = 0;
							dictionary[material2].Add(new List<CombineInstance>());
						}
						int count = dictionary[material2].Count;
						List<CombineInstance> list = dictionary[material2][count - 1];
						Dictionary<Material, int> dictionary3 = dictionary2;
						Material key = material2;
						dictionary3[key] += vertexCount;
						list.Add(item);
					}
				}
				else
				{
					CombineInstance item2 = new CombineInstance
					{
						mesh = meshFilter.sharedMesh,
						transform = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix
					};
					Material sharedMaterial = meshFilter.GetComponent<Renderer>().sharedMaterial;
					int vertexCount2 = item2.mesh.vertexCount;
					if (!dictionary2.ContainsKey(sharedMaterial) || dictionary2[sharedMaterial] + vertexCount2 > 65536)
					{
						dictionary2[sharedMaterial] = 0;
						dictionary[sharedMaterial].Add(new List<CombineInstance>());
					}
					int count2 = dictionary[sharedMaterial].Count;
					List<CombineInstance> list2 = dictionary[sharedMaterial][count2 - 1];
					Dictionary<Material, int> dictionary3 = dictionary2;
					Material key = sharedMaterial;
					dictionary3[key] += vertexCount2;
					list2.Add(item2);
				}
				meshFilter.GetComponent<Renderer>().enabled = false;
			}
		}
		foreach (Material material3 in dictionary.Keys)
		{
			for (int l = 0; l < dictionary[material3].Count; l++)
			{
				List<CombineInstance> list3 = dictionary[material3][l];
				GameObject gameObject3 = new GameObject("CombinedMesh_" + material3.name + "_" + l.ToString());
				Transform transform3 = gameObject3.transform;
				RuntimeMeshBatcher.SetParent(transform3, gameObject4.transform);
				transform3.localPosition = Vector3.zero;
				transform3.localRotation = Quaternion.identity;
				transform3.localScale = Vector3.one;
				gameObject3.AddComponent<MeshFilter>().mesh.CombineMeshes(list3.ToArray(), true, true);
				gameObject3.AddComponent<MeshRenderer>().material = material3;
			}
		}
		if (destroyOriginalObjects)
		{
			for (int m = 0; m < gameObjects.Length; m++)
			{
				Object.Destroy(gameObjects[m]);
			}
		}
		else
		{
			array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			IEnumerable<Transform> source = from gameObject in gameObjects
			select gameObject.transform;
			Func<Transform, bool> <>9__1;
			Func<Transform, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((Transform goTransform) => originalTransforms.ContainsKey(goTransform)));
			}
			foreach (Transform transform4 in source.Where(predicate))
			{
				if (transform4 != null && originalTransforms[transform4] != null)
				{
					RuntimeMeshBatcher.SetParent(transform4, originalTransforms[transform4]);
				}
			}
		}
		return gameObject4;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x000335E4 File Offset: 0x000317E4
	public static void UncombineMeshes(GameObject rmbParent)
	{
		if (rmbParent == null)
		{
			Debug.LogWarning("Runtime Mesh Batcher warning: Calling UncombineMeshes with null parent GameObject.");
			return;
		}
		if (RuntimeMeshBatcher.originalObjetReferences == null)
		{
			Debug.LogWarning("Runtime Mesh Batcher warning: Calling UncombineMeshes without having kept object references.");
			return;
		}
		if (!RuntimeMeshBatcher.originalObjetReferences.ContainsKey(rmbParent))
		{
			Debug.LogWarning("Runtime Mesh Batcher warning: Calling UncombineMeshes with undefined parent GameObject.");
			return;
		}
		GameObject[] array = RuntimeMeshBatcher.originalObjetReferences[rmbParent];
		Object.Destroy(rmbParent);
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				MeshRenderer[] array3 = gameObject.GetComponents<MeshRenderer>();
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j].enabled = true;
				}
				array3 = gameObject.GetComponentsInChildren<MeshRenderer>();
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j].enabled = true;
				}
			}
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x000336A2 File Offset: 0x000318A2
	private static void SetParent(GameObject gameObject, GameObject parent)
	{
		RuntimeMeshBatcher.SetParent(gameObject.transform, parent.transform);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x000336B5 File Offset: 0x000318B5
	private static void SetParent(Transform gameObjectTransform, Transform parentTransform)
	{
		gameObjectTransform.SetParent(parentTransform, true);
	}

	// Token: 0x04000604 RID: 1540
	public static List<GameObject> rmbParents;

	// Token: 0x04000605 RID: 1541
	public static Dictionary<GameObject, GameObject[]> originalObjetReferences;

	// Token: 0x04000606 RID: 1542
	public static GameObject rmbContainerGameObject;
}
