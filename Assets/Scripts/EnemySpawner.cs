using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int Count = 100;
    [SerializeField] private GameObject enemyPrefab;

    private Entity _prefab;
    private EntityManager _entityManager;
    private GameObjectConversionSettings _settings;
    private BlobAssetStore _blobAssetStore;
    
    void Start()
    {
        // Create entity prefab from the game object hierarchy once

        _blobAssetStore = new BlobAssetStore();
        
        _settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
        _prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyPrefab, _settings);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void OnDestroy()
    {       
        _settings.BlobAssetStore.Dispose();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        for (var x = 0; x < Count; x++)
        {
            // Efficiently instantiate a bunch of entities from the already converted entity prefab
            var instance = _entityManager.Instantiate(_prefab);

            // Place the instantiated entity in a grid with some noise
            var position = transform.TransformPoint(new Vector3(Random.Range(-5f, 5f), 0 , Random.Range(-5f, 5f)));
            _entityManager.SetComponentData(instance, new Translation {Value = position});
        }
    }
}
