using UnityEngine;

public class TilesTyping : MonoBehaviour
{
    private TilesSO tilesSO;

    public void Init(TilesSO so)
    {
        tilesSO = so;

        // Esempio: cambia colore in base al nome
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            if (so.tileType == 0)
                renderer.material.color = Color.blue;
            else if (so.tileType == 1)
                renderer.material.color = Color.red;
                
            else
                Debug.Log("No tileType assigned");
        }

        // Puoi aggiungere qui altre logiche per aggiornare la tile
        // ad esempio cambiare mesh, sprite, ecc.
    }
}

