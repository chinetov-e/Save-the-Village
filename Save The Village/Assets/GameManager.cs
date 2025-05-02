using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioSource battleSound;
    public AudioSource peasantSound;
    public AudioSource warriorSound;

    public GameObject gameOverScreen;
    public GameObject winScreen;

    public Image peasantTimerImg;
    public Image warriorTimerImg;
    public Image raidTimerImg;

    public Button peasantButton;
    public Button warriorButton;

    public Timer harvestTimer;
    public Timer eatingTimer;

    public Text resourcesText;
    public Text raidWaveText;
    public Text enemiesCountText;
    public Text gameOverStats;
    public Text gameWinStats;

    public int peasantCount;
    public int warriorsCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarrior;

    public int peasantCost;
    public int warriorCost;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;

    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    private int raidWave = 1;

    public float peasantCreateTime;
    public float warriorCreatTime;

    private int totalWheatProduced;
    private int wheatProduction;
    private int warriorsProduced;
    private int peasantProduced;

    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
    }

    void Update()
    {
        if(wheatCount < peasantCost)
        {
            peasantButton.interactable = false;
        }
        else if(wheatCount >= peasantCost && peasantTimer < -1)
        {
            peasantButton.interactable = true;
        }

        if(wheatCount < warriorCost)
        {
            warriorButton.interactable = false;
        }
        else if(wheatCount >= warriorCost && warriorTimer < -1)
        {
            warriorButton.interactable = true;
        }

        raidTimer -= Time.deltaTime;
        raidTimerImg.fillAmount = raidTimer / raidMaxTime;

        if(raidTimer <= 0)
        {
            raidWave += 1;
            raidTimer = raidMaxTime;
            warriorsCount -= nextRaid;
            battleSound.Play();
            if(raidWave >= 4)
            {
                nextRaid += raidIncrease;
            }
        }

        if(harvestTimer.Tick)
        {
            wheatProduction = peasantCount * wheatPerPeasant;
            wheatCount += wheatProduction;
            totalWheatProduced += wheatProduction;
        }

        if(eatingTimer.Tick)
        {
            wheatCount -= warriorsCount * wheatToWarrior;
        }

        if(peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            peasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if(peasantTimer > -1)
        {
            peasantTimerImg.fillAmount = 1;
            peasantCount += 1;
            peasantTimer = -2;
            peasantProduced += 1;
            peasantSound.Play();
        }

        if(warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            warriorTimerImg.fillAmount = warriorTimer / warriorCreatTime;
        }
        else if(warriorTimer > -1)
        {
            warriorTimerImg.fillAmount = 1;
            warriorsCount += 1;
            warriorTimer = -2;
            warriorsProduced += 1;
            warriorSound.Play();
        }

        UpdateText();

        if(warriorsCount < 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            gameOverStats.text = $"Всего собрано пшеницы: {totalWheatProduced} \n Пережито набегов: {raidWave - 1} \n Произведено войнов: {warriorsProduced} \n Произведено крестьян: {peasantProduced}";
        }

        if(wheatCount > 500)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            gameWinStats.text = $"Вы собрали достаточно пшеницы \n Всего собрано пшеницы: {totalWheatProduced} \n Пережито набегов: {raidWave - 1} \n Произведено войнов: {warriorsProduced} \n Произведено крестьян: {peasantProduced}";

        }
    }

    public void CreatePeasant()
    {
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
        clickSound.Play();
    }

    public void CreateWarrior()
    {
        wheatCount -= warriorCost;
        warriorTimer = warriorCreatTime;
        warriorButton.interactable = false;
        clickSound.Play();
    }

    private void UpdateText()
    {
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount;
        raidWaveText.text = raidWave.ToString();
        enemiesCountText.text = nextRaid.ToString();
    }
}
