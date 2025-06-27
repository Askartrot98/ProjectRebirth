using UnityEngine;

public class SpawnerTiles : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] Transform spawnPos;
    [SerializeField] TilesSO[] tilesSO;


    void Start()
    {
        SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTile()
    {
        if (tile != null)
        {
            
            for (int i = 0; i< 10; i++)
            {
                for(int j = 0; j<10; j++)
                {
                    Vector3 NewPos = new Vector3(spawnPos.position.x + i * 3, spawnPos.position.y, spawnPos.position.z + j * 3);
                    tile = Instantiate(tile, NewPos, Quaternion.identity);
                }
               
            }
        }
    }
}
