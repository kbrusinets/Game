using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject enemy;
    public Camera cam;
    public GameObject LifeBar;
    public GameObject gameOverText;
    public GameObject nextStageText;
    public GameObject restart;
    public GameObject girl;
    public GameObject food;
    public GameObject score;
    public GameObject ratedM;

    public Weapon stoneWeapon;
    public Weapon pistol;

    private Text scoreText;

    private float maxHeight, xSpawn, spawnTime;
    private float life;
    private int hazardCount;
    private float enemySpeed;
    private float exp;
    private int lvl;
    private int maxLvl;
    private bool gameOver;
    private bool nextStage;

    private Weapon chosenWeapon;
    private Weapon[] weapons;


    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        lvl = 0;
        life = 100;
        maxLvl = 1;
        spawnTime = 1.0f;
        scoreText = score.GetComponent<Text>();
        UpdateScore();
        initializeWeapons();
        takeWeapon();
        hazardCount = 10;
        enemySpeed = 3.0f;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targerHeight = cam.ScreenToWorldPoint(upperCorner);
        Vector3 enemyBonds = enemy.GetComponent<Renderer>().bounds.extents;
        maxHeight = targerHeight.y - enemyBonds.y;
        xSpawn = targerHeight.x + enemyBonds.x;

        StartCoroutine(ratedMForMature());
        StartCoroutine(Spawn());
    }

    IEnumerator ratedMForMature()
    {
        while (true)
        {
            ratedM.SetActive(true);
            yield return new WaitForSeconds(4.0f);
            ratedM.SetActive(false);
            break;
        }
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + exp;
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (gameOver)
                { break; }
                Vector3 spawnPosition = new Vector3(
                    xSpawn,
                    Random.Range(-maxHeight, maxHeight),
                    0.0f
                    );
                Instantiate(getUnit(), spawnPosition, Quaternion.Euler(0, 0, 180));
                yield return new WaitForSeconds(spawnTime);
            }

            while (true)
            {
                if (gameOver)
                { break; }
                if (checkForNextStage())
                {
                    StartCoroutine(nextStageBlinking());
                    hazardCount += 2;
                    addSpeed();
                    spawnTime -= 0.1f;
                    yield return new WaitForSeconds(3.0f);
                    break;
                }
                else
                    yield return new WaitForSeconds(1.0f);
            }
            if (gameOver)
            { break; }

        }
    }

    private GameObject getUnit()
    {
        //return enemy;

        int random = Random.Range(0, 10);
        if (random < 7)
            return enemy;
        else
            if (random < 8)
                return girl;
            else
                return food;
    }

    private bool checkForNextStage()
    {
        if (GameObject.Find("enemy(Clone)") != null || GameObject.Find("girl(Clone)") != null || GameObject.Find("food(Clone)") != null)
            return false;
        else
            return true;
    }

    private IEnumerator nextStageBlinking()
    {
        int nTimes = 5;
        while (nTimes > 0)
        {
            nextStageText.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            nextStageText.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            nTimes--;
        }
    }

    public float returnSpeed()
    {
        return enemySpeed;
    }

    private void addSpeed()
    {
        enemySpeed += 1 * Mathf.Sign(enemySpeed);
    }

    private void initializeWeapons()
    {
        weapons = new Weapon[maxLvl + 1];
        weapons[0] = stoneWeapon;
        weapons[1] = pistol;
    }

    private void takeWeapon()
    {
        chosenWeapon = weapons[Mathf.Min(lvl, maxLvl)];
    }

    public Weapon returnWeapon()
    {
        return chosenWeapon;
    }

    private int getLvl()
    {
        return Mathf.Min((int)exp / 500, maxLvl);
    }

    public void addExp(float amount)
    {
        exp += amount;
        UpdateScore();
        if (getLvl() != lvl)
        {
            lvl = getLvl();
            takeWeapon();
        }
    }

    public void changeHp(float amount)
    {
        life = Mathf.Min(life + amount, 100);
        var theBarRectTransform = LifeBar.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(300 * life / 100, theBarRectTransform.sizeDelta.y);
        if (life == 0)
            StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        gameOver = true;
        while (!Input.GetKeyDown(KeyCode.R))
        {
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(1f);
            restart.SetActive(true);
            gameOverText.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        Application.LoadLevel(Application.loadedLevel);
        //gameOverText.SetActive(true);        
    }
}
