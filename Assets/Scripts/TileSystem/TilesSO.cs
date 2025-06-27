using UnityEngine;

[CreateAssetMenu(fileName = "Tiles", menuName = "Scriptable Objects/Tiles")]
public class TilesSO : ScriptableObject
{
    public string tileName;
    public GameObject tilePrefab;
    public int tileType;

}
