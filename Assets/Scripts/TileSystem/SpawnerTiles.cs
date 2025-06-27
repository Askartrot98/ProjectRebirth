using UnityEngine;

public class SpawnerTiles : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] Transform spawnPos;
    [SerializeField] TilesSO[] tilesSO;

    [SerializeField] private int rows = 10;
    [SerializeField] private int columns = 10;


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
            
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    Vector3 newPos = new Vector3(spawnPos.position.x + i * tile.transform.localScale.x, spawnPos.position.y, spawnPos.position.z + j * tile.transform.localScale.z);
                    GameObject spawnedTile = Instantiate(tile, newPos, Quaternion.identity);

                    // Scegli un TilesSO casuale
                    TilesSO so = tilesSO[Random.Range(0, tilesSO.Length)];

                    // Inizializza la tile con il TilesSO scelto
                    TilesTyping typing = spawnedTile.GetComponent<TilesTyping>();
                    if (typing != null)
                    {
                        typing.Init(so);
                    }
                }
               
            }
        }
    }
}
