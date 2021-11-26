using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>(); //List of spawn points
    public int[] spawned = new int[10]; //Stores the already used spawn points
    [SerializeField] private GameObject Target;
    public bool isFull = false;

    [Header("Economy")]
    public int points = 0;
    public float timeRemaining = 20;
    private bool isGame;

    [Header("UI")]
    public Text pointsText;
    public Text countdownTimer;
    public GameObject overPanel;

    [Header("Sounds")]
    public AudioSource CannonSound;
    public AudioSource TimeUpSound;
    public AudioSource CollectSound;

    void Start()
    {
        isGame = true;
        StartCoroutine(StartRoutine());
    }

    void Update()
    {
        if (timeRemaining >= 0) //Game Countdown
        {
            timeRemaining -= Time.deltaTime;
            countdownTimer.text = timeRemaining.ToString("00");
        }
        else
        {
            TimeUpSound.Play(0);
            EndGame();
        }
    }

    private void EndGame() //When countdown is over
    {
        isGame = false;
        overPanel.SetActive(true);
    }

    public void PlayAgain() //Event occurs after game over
    {
        SceneManager.LoadScene("CannonScene");
    }

    #region [SPAWN]

    //This spawns the target GameObject in a random spawn point
    void Spawn()
    {
        int a = Random.Range(0, 10); //Spawn between 10 spawn points

        if (spawned[a] != 1)
        {
            spawned[a] = 1;
        }
        else
        {
            bool isExisting = true;
            while (isExisting)
            {
                a = Random.Range(0, 10);
                if (spawned[a] != 1)
                {
                    isExisting = false;
                    spawned[a] = 1;
                }
            }
        }

        GameObject obj_Target = Instantiate(Target, SpawnPoints[a].transform.position, Quaternion.identity); //Instantiate target in spawn point
        obj_Target.gameObject.name = a.ToString();
    }

    //Check if all the spawn points are already used
    void checkAllSpawned()
    {
        isFull = true;

        foreach (int item in spawned)
        {
            if (item == 0)
            {
                isFull = false;
                break;
            }
        }
    }

    #endregion

    #region [COOROUTINES]

    //Coourutine that spawns targets every 6 seconds 
    IEnumerator StartRoutine()
    {
        while (isGame == true)
        {
            checkAllSpawned();

            if (isFull == false)
            {
                Spawn();
            }
            yield return new WaitForSeconds(6);
        }
    }

    #endregion
}
