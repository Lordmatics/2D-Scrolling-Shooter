using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    public float rows;
    public float columns;
    public Vector3 gridStart;

    public Grid(Vector3 offset = new Vector3(), float r = 6.0f, float c = 6.0f)
    {
        offset = new Vector3(14.5f, 0.0f, -14.5f);
        rows = r;
        columns = c;
    }
}

[System.Serializable]
public class ClickedTilesContainer
{
    public int tileA;
    public bool bTileAUsed;

    public int tileB;

    public ClickedTilesContainer(int a = 0, int b = 0)
    {
        tileA = a;
        bTileAUsed = false;
        tileB = b;
    }
}

[AddComponentMenu("Scripts/Game/MemoryGame")]
public class MemoryGame : MonoBehaviour
{

    [SerializeField]
    private Grid grid = new Grid();

    [SerializeField]
    private float tileWidth = 5.5f;

    [SerializeField]
    private float tileHeight = 5.5f;

    [SerializeField]
    private List<GameObject> tileArray = new List<GameObject>();

    public ClickedTilesContainer clickedTileIndexes = new ClickedTilesContainer();

    public int currentClickCount = 0;

    [SerializeField]
    private int correctValueScore = 25;

    [SerializeField]
    private int successStreak = 0;

    public static float delay = 0.8f;

    public static int matchesMade = 0;

    private GameObject playAgainButton;

    private GameObject settingsButton;

    [SerializeField]
    private Difficulty difficultyScript;

    void Start()
    {
        playAgainButton = GameObject.FindObjectOfType<PlayAgain>().gameObject;
        settingsButton = GameObject.FindObjectOfType<Difficulty>().gameObject;

        //CreateGrid();
        ShowButton();
    }

    #region  _PLAY_AGAIN_BUTTON_
    void HideButton()
    {
        if (playAgainButton != null)
        {
            playAgainButton.gameObject.SetActive(false);
        }
        if(settingsButton != null)
        {
            settingsButton.gameObject.SetActive(false);
        }
    }

    void ShowButton()
    {
        if (playAgainButton != null)
        {
            playAgainButton.gameObject.SetActive(true);
        }
        if (settingsButton != null)
        {
            settingsButton.gameObject.SetActive(true);
        }
    }
    #endregion

    #region _GRID_STUFF_
    void ClearExistingGrid()
    {
        if(tileArray.Count > 0)
        {
            // Tiles exist
            for (int i = tileArray.Count - 1; i >= 0; i--)
            {
                GameObject temp = tileArray[i];
                tileArray.Remove(temp);
                Destroy(temp);
            }
        }
    }

    public void CreateGrid()
    {
        HideButton();
        ClearExistingGrid();

        if (difficultyScript != null)
        {
            Debug.Log("SCRIPT");

            Vector3 gridStart = difficultyScript.GetGridSettings().gridStart;

            int counter = 0;
            for (int i = 0; i < difficultyScript.GetGridSettings().rows; i++)
            {
                for (int j = 0; j < difficultyScript.GetGridSettings().columns; j++)
                {
                    Vector3 pos = gridStart + new Vector3(j * tileWidth, 0.0f, i * -tileHeight);
                    GameObject prefab = BuildTile.instance.InstantiateTileAt(pos);
                    if (prefab != null)
                    {
                        tileArray.Add(prefab);
                        Tile tileScript = prefab.GetComponent<Tile>();
                        if (tileScript != null)
                        {
                            tileScript.SetGame(this);
                            tileScript.SetDefaultMaterial();
                            tileScript.SetTileIndex(counter);
                            tileScript.ChooseColour();
                            counter++;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("NOSCRIPT");
        }
    }
    #endregion

    #region _TILE_STUFF
    public void AddToIndexes(int index)
    {
        currentClickCount++;

        if(clickedTileIndexes.bTileAUsed)
        {
            clickedTileIndexes.tileB = index;
            clickedTileIndexes.bTileAUsed = false;
            //Invoke("EvaluateTiles", 1.0f);
            StartCoroutine(EvaluateTiles());
        }
        else
        {
            clickedTileIndexes.tileA = index;
            clickedTileIndexes.bTileAUsed = true;
        }
    }

    public IEnumerator EvaluateTiles()
    {


        Tile tileScriptA = tileArray[clickedTileIndexes.tileA].GetComponent<Tile>();
        Tile tileScriptB = tileArray[clickedTileIndexes.tileB].GetComponent<Tile>();


        if (tileScriptA == tileScriptB)
        {
            // Gain Score
            // Lock Tiles
            Debug.Log("CORRECT");
            int affectedScore = correctValueScore + (successStreak * correctValueScore);
            Score.instance.ModifyScore(affectedScore);
            TileOutcome.instance.UpdateText(affectedScore, true);
            successStreak++;
            matchesMade++;
            CheckForEndOfGame();
        }
        else
        {
            Score.instance.ModifyScore(-correctValueScore);
            TileOutcome.instance.UpdateText(correctValueScore);

            yield return new WaitForSeconds(delay);
            tileScriptA.FlipDown();
            tileScriptB.FlipDown();
        }
        currentClickCount = 0;

        clickedTileIndexes.tileA = 0;
        clickedTileIndexes.tileB = 0;
    }
    #endregion

    #region _END_CONDITION_
    void CheckForEndOfGame()
    {
        int condition = ((int)difficultyScript.currentSelectedDimension.grid.rows * (int)difficultyScript.currentSelectedDimension.grid.columns ) / 2;
        if(matchesMade >= condition)
        {
            TileOutcome.instance.EndOfGameText();
            matchesMade = 0;
            ShowButton();
            ColourStash.instance.RefillMaterialArray();
        }
    }
    #endregion
}
