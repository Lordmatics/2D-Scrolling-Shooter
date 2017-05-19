using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Scripts/Game/Tile")]
public class Tile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MeshRenderer meshRend;

    private Material defaultMaterial;

    [SerializeField]
    private Material colourMaterial;

    [HideInInspector]
    public int tileIndex;

    [HideInInspector]
    public bool bTileFlipped = false;

    private float startingY;

    private MemoryGame memoryGame;

    public static bool operator ==(Tile tileA, Tile tileB)
    {
        if(object.ReferenceEquals(tileA,tileB))
        {
            return true;
        }
        if(object.ReferenceEquals(tileA, null) || object.ReferenceEquals(tileB, null))
        {
            return false;
        } 

        // Do actual check
        bool bMatch = tileA.colourMaterial.name == tileB.colourMaterial.name;
        return bMatch;
    }

    public static bool operator !=(Tile tileA, Tile tileB)
    {
        //if (tileA.gameObject == null || tileB.gameObject == null) return false;
        return !(tileA == tileB);

        //bool bMatch = tileA.colourMaterial.name != tileB.colourMaterial.name;
        //return bMatch;
    }

    public override bool Equals(object other)
    {
        return this == (other as Tile);
    }

    public bool Equals(Tile other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void SetGame(MemoryGame game)
    {
        memoryGame = game;
    }

    public void SetDefaultMaterial()
    {
        meshRend = GetComponent<MeshRenderer>();
        defaultMaterial = meshRend.material;
        startingY = transform.position.y;
    }

    public void ChooseColour()
    {
        if (meshRend != null)
        {
            Material newMat = ColourStash.instance.GetRandomMaterial().customMat;
            if(newMat != null)
                colourMaterial = newMat;
        }
        else
        {
            Debug.Log("MeshRend null");
        }
    }

    public void FlipOver()
    {
        if(meshRend != null)
        {
            if (memoryGame.currentClickCount >= 2) return;

            meshRend.material = colourMaterial;
            bTileFlipped = true;
            memoryGame.AddToIndexes(tileIndex);
        }
    }

    public void FlipDown()
    {
        if (meshRend != null)
        {
            meshRend.material = defaultMaterial;
            bTileFlipped = false;
        }
    }

    public void SetTileIndex(int index)
    {
        tileIndex = index;
    }

    // Pressed tile
    public void OnPointerClick(PointerEventData eventData)
    {
        if (memoryGame.currentClickCount >= 2 || bTileFlipped) return;
        FlipOver();
        transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
    }

    // Enterred Tile - Animate it towards the player
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (bTileFlipped) return;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }

    // Exit tile, push back to default position
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = new Vector3(transform.position.x, startingY, transform.position.z);

    }
}
