using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {

	public const float MaxViewDist = 450;
	public Transform viewer;
	public Material mapMaterial;

	public static Vector2 ViewerPosition;
	static mapGenerator MapGenerator;
	int chunkSize;
	int chunksVisibleInView;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk> ();
	List<TerrainChunk> terrainChunkVisibleLastUpdate = new List<TerrainChunk>();

	void Start() {
		MapGenerator = GetComponent<mapGenerator> ();
		chunkSize = mapGenerator.mapChunkSize - 1;
		chunksVisibleInView = Mathf.RoundToInt(MaxViewDist / chunkSize);
	}

	void Update () {
		ViewerPosition = new Vector2 (viewer.position.x, viewer.position.z);
		UpdateVisibleChunks ();
	}

	void UpdateVisibleChunks() {
		for (int i = 0; i < terrainChunkVisibleLastUpdate.Count; i++) {
			terrainChunkVisibleLastUpdate [i].SetVisible (false);
		}
		terrainChunkVisibleLastUpdate.Clear ();

		int currentChunksCoordX = Mathf.RoundToInt (ViewerPosition.x / chunkSize);
		int currentChunksCoordY = Mathf.RoundToInt (ViewerPosition.y / chunkSize);

		for (int yOffset = -chunksVisibleInView; yOffset <= chunksVisibleInView; yOffset++) {
			for (int xOffset = -chunksVisibleInView; xOffset <= chunksVisibleInView; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunksCoordX + xOffset, currentChunksCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunks ();

					if (terrainChunkDictionary[viewedChunkCoord].isVisible()) {
						terrainChunkVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
					
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, transform, mapMaterial));
				}
			}
		}
	}

	public class TerrainChunk {
		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		MeshRenderer meshRenderer;
		MeshFilter meshFilter;

		public TerrainChunk(Vector2 coord, int size, Transform parent, Material material) {
			position = coord * size;
			bounds = new Bounds(position, Vector2.one*size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			meshObject = new GameObject("Terrain Chunk");
			meshRenderer = meshObject.AddComponent<MeshRenderer>();
			meshFilter = meshObject.AddComponent<MeshFilter>();
			meshRenderer.material = material;

			meshObject.transform.position = positionV3;
			meshObject.transform.parent = parent;
			SetVisible(false);

			MapGenerator.RequestMapData(OnMapDataReceived);
		}

		void OnMapDataReceived(MapData mapData) {
			MapGenerator.RequestMeshData (mapData, OnMeshDataReceived);
		}

		void OnMeshDataReceived (MeshData meshData) {
			meshFilter.mesh = meshData.CreateMesh ();
		}

		public void UpdateTerrainChunks() {
			float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (ViewerPosition));
			bool visible = viewerDistFromNearestEdge <= MaxViewDist;
			SetVisible (visible);
		}

		public void SetVisible (bool visible) {
			meshObject.SetActive (visible);
		}

		public bool isVisible() {
			return meshObject.activeSelf;
		}
	}
}
