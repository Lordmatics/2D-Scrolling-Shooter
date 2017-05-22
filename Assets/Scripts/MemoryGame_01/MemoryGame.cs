using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]

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

    public static bool bUseColour = true;

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

    public GameObject backToMenuButton;

    void Start()
    {
        playAgainButton = GameObject.FindObjectOfType<PlayAgain>().gameObject;
        settingsButton = GameObject.FindObjectOfType<Difficulty>().gameObject;
        backToMenuButton = GameObject.FindObjectOfType<BackButton>().gameObject;
        //CreateGrid();
        ShowButton();
    }
    
    public void CustomiseSettings(bool _settings)
    {
        bUseColour = _settings;
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
        if(backToMenuButton != null)
        {
            backToMenuButton.gameObject.SetActive(true);
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
        if (backToMenuButton != null)
        {
            backToMenuButton.gameObject.SetActive(false);
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

    void AdjustGridLayout()
    {
        GameObject gridTilesContainer = GameObject.FindGameObjectWithTag("GameTiles");
        UnityEngine.UI.GridLayoutGroup layoutGroup = gridTilesContainer.GetComponent<UnityEngine.UI.GridLayoutGroup>();
        layoutGroup.constraint = UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = difficultyScript.GetGridSettings().columns;
    }

    public void CreateGrid()
    {
        HideButton();
        ClearExistingGrid();
        AdjustGridLayout();
        if (difficultyScript != null)
        {
            //Debug.Log("SCRIPT");

            //Vector3 gridStart = difficultyScript.GetGridSettings().gridStart;

            int counter = 0;
            for (int i = 0; i < difficultyScript.GetGridSettings().rows; i++)
            {
                for (int j = 0; j < difficultyScript.GetGridSettings().columns; j++)
                {
                    GameObject prefab = BuildTile.instance.InstantiateTileAt(new Vector3(), true);
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
            successStreak = 0;

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
            ResetGame();
        }
    }

    public void ResetGame()
    {
        TileOutcome.instance.EndOfGameText();
        matchesMade = 0;
        ShowButton();
        ColourStash.instance.RefillMaterialArray();
       
        //difficultyScript = null;
    }
    #endregion
}
