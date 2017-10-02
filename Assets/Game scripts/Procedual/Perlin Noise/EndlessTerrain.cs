using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {

	const float viewerMoveThreasholdForChunkUpdate = 25f;
	const float sqrviewerMoveThreasholdForChunkUpdate = viewerMoveThreasholdForChunkUpdate * viewerMoveThreasholdForChunkUpdate;
	const float scale = 5f;

	public LODInfo[] detailLevels;
	public static float MaxViewDist;
	public Transform viewer;
	public Material mapMaterial;

	public static Vector2 ViewerPosition;
	Vector2 viewPositionOld;
	static mapGenerator MapGenerator;
	int chunkSize;
	int chunksVisibleInView;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk> ();
	static List<TerrainChunk> terrainChunkVisibleLastUpdate = new List<TerrainChunk>();

	void Start() {
		MapGenerator = GetComponent<mapGenerator> ();
		MaxViewDist = detailLevels [detailLevels.Length - 1].visibleDstThreadhold;
		chunkSize = mapGenerator.mapChunkSize - 1;
		chunksVisibleInView = Mathf.RoundToInt(MaxViewDist / chunkSize);

		UpdateVisibleChunks();
	}

	void Update () {
		ViewerPosition = new Vector2 (viewer.position.x, viewer.position.z) / scale;

		if ((viewPositionOld - ViewerPosition).sqrMagnitude > sqrviewerMoveThreasholdForChunkUpdate) {
			viewPositionOld = ViewerPosition;
			UpdateVisibleChunks ();
		}
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
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, detailLevels, transform, mapMaterial));
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

		LODInfo[] detailLevels;
		LODMesh[] lodMeshes;

		MapData mapData;
		bool mapDataReceived;
		int previousLODIndex = -1;

		public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material) {
			this.detailLevels = detailLevels;

			position = coord * size;
			bounds = new Bounds(position, Vector2.one*size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			meshObject = new GameObject("Terrain Chunk");
			meshRenderer = meshObject.AddComponent<MeshRenderer>();
			meshFilter = meshObject.AddComponent<MeshFilter>();
			meshRenderer.material = material;

			meshObject.transform.position = positionV3*scale;
			meshObject.transform.parent = parent;
			meshObject.transform.localScale = Vector3.one * scale;;
			SetVisible(false);

			lodMeshes = new LODMesh[detailLevels.Length];
			for (int i = 0; i <detailLevels.Length; i++) {
				lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrainChunks);
			}

			MapGenerator.RequestMapData(position, OnMapDataReceived);
		}

		void OnMapDataReceived(MapData mapData) {
			this.mapData = mapData;
			mapDataReceived = true;

			Texture2D texture = TextureGenerator.TextureFromColourMap (mapData.colourMap, mapGenerator.mapChunkSize, mapGenerator.mapChunkSize);
			meshRenderer.material.mainTexture = texture;

			UpdateTerrainChunks ();
		}

		public void UpdateTerrainChunks() {
			if (mapDataReceived) {
				float viewerDistFromNearestEdge = Mathf.Sqrt (bounds.SqrDistance (ViewerPosition));
				bool visible = viewerDistFromNearestEdge <= MaxViewDist;

				if (visible) {
					int lodIndex = 0;

					for (int i = 0; i < detailLevels.Length - 1; i++) {
					
						if (viewerDistFromNearestEdge > detailLevels [i].visibleDstThreadhold) {
							lodIndex = i + 1;

						} else {
							break;
						}
					}

					if (lodIndex != previousLODIndex) {
						LODMesh lodMesh = lodMeshes [lodIndex];

						if (lodMesh.hasMesh) {
							previousLODIndex = lodIndex;
							meshFilter.mesh = lodMesh.mesh;
						} else if (!lodMesh.hasRequestedMesh) {
							lodMesh.RequestMesh (mapData);
						}
					}

					terrainChunkVisibleLastUpdate.Add (this);
				}

				SetVisible (visible);
			}
		}

		public void SetVisible (bool visible) {
			meshObject.SetActive (visible);
		}

		public bool isVisible() {
			return meshObject.activeSelf;
		}
	}

	class LODMesh {

		public Mesh mesh;
		public bool hasRequestedMesh;
		public bool hasMesh;
		int lod;
		System.Action updateCallback;

		public LODMesh(int lod, System.Action updateCallback) {
			this.lod = lod;
			this.updateCallback = updateCallback;
		}

		void OnMeshDataReceived(MeshData meshData){
			mesh = meshData.CreateMesh ();
			hasMesh = true;

			updateCallback ();
		}

		public void RequestMesh(MapData mapData) {
			hasRequestedMesh = true;
			MapGenerator.RequestMeshData (mapData, lod, OnMeshDataReceived);
		}
	}

	[System.Serializable]
	public struct LODInfo {
		public int lod;
		public float visibleDstThreadhold;

	}
}
