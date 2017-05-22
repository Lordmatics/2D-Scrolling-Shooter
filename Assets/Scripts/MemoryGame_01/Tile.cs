using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/Tile")]
public class Tile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //private MeshRenderer meshRend;
    private Image tileImage;

    private Image maskedImage;

    private RectTransform rectTrans;

    private Material defaultMaterial;
    private Sprite defaultSprite;
    private Sprite colouredSprite;

    [SerializeField]
    private Material colourMaterial;

    [HideInInspector]
    public int tileIndex;

    [HideInInspector]
    public bool bTileFlipped = false;

    private float originalScale;

    private static float hoverScale;

    private float hoverModifier = 0.2f;

    private MemoryGame memoryGame;

    public static float tileWidth = 5.5f;

    public static float tileHeight = 5.5f;

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
        bool bMatch = false;

        if (MemoryGame.bUseColour)
        {
            bMatch = tileA.colourMaterial.name == tileB.colourMaterial.name;
        }
        else
        {
            bMatch = tileA.colouredSprite.name == tileB.colouredSprite.name;
        }
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

    public void UpdateHoverScale()
    {
        rectTrans = GetComponent<RectTransform>();
        hoverScale = rectTrans.localScale.x + hoverModifier;
    }

    public void SetDefaultMaterial()
    {
        //meshRend = GetComponent<MeshRenderer>();
        tileImage = GetComponent<Image>();
        // DEPENDS ON HAVING A CHILD
        maskedImage = transform.GetChild(0).GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();
        defaultMaterial = tileImage.material;
        defaultSprite = maskedImage.sprite;
        originalScale = rectTrans.localScale.x;
        hoverScale = rectTrans.localScale.x + hoverModifier;
    }

    public void ChooseColour()
    {
        if(MemoryGame.bUseColour)
        {
            if (tileImage != null)
            {
                Material newMat = ColourStash.instance.GetRandomMaterial().customMat;
                if (newMat != null)
                    colourMaterial = newMat;
            }
            else
            {
                Debug.Log("tileImage null");
            }
        }
        else
        {
            if(maskedImage != null)
            {
                TileImage data = ColourStash.instance.GetRandomMaterial().tileImageData;
                if(data != null)           
                    colouredSprite = data.tileSprite;
                
            }
            else
            {
                Debug.Log("TileImage DATA null");
            }
        }

    }

    public void FlipOver()
    {
        if(tileImage != null)
        {
            if (memoryGame.currentClickCount >= 2) return;

            if(MemoryGame.bUseColour)
            {
                tileImage.material = colourMaterial;
            }
            else
            {
                if (maskedImage != null)
                    maskedImage.sprite = colouredSprite;
            }
            bTileFlipped = true;
            memoryGame.AddToIndexes(tileIndex);
        }
    }

    public void FlipDown()
    {
        if (tileImage != null)
        {
            if(MemoryGame.bUseColour)
            {
                tileImage.material = defaultMaterial;

            }
            else
            {
                if (maskedImage != null)
                    maskedImage.sprite = defaultSprite;
            }
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
        rectTrans.SetScale(originalScale);
    }

    // Enterred Tile - Animate it towards the player
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (bTileFlipped) return;
        rectTrans.SetScale(hoverScale);

        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }

    // Exit tile, push back to default position
    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
        rectTrans.SetScale(originalScale);

    }
}
