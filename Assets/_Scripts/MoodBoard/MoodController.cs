using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MoodController : MonoBehaviour
{
    public Slider hungerBar;
    public Slider happinessBar;
    public Slider hygieneBar;

    public Gradient barGradient;

    public Image barFillHunger;
    public Image barFillHappiness;
    public Image barFillHygene;

    public float hunger = 100f;
    public float happiness = 100f;
    public float hygiene = 100f;

    public GameObject smiley;
    public GameObject neutral;
    public GameObject sad;

    public float decreaseRate = 0.5f;
    public float feedBoost = 50f;
    public float hygieneBoost = 30f;
    public float happinessPenaltyFromBath = 10f;
    private void Start()
    {
        UpdateBars();
        
    }

    private void Update()
    {
        hunger -= (decreaseRate / 1f) * Time.deltaTime;
        happiness -= (decreaseRate / 1f) * Time.deltaTime;
        hygiene -= (decreaseRate / 1f) * Time.deltaTime;


        hunger = Mathf.Clamp(hunger, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene, 0f, 100f);

        barFillHappiness.color = barGradient.Evaluate(happiness);
        barFillHygene.color = barGradient.Evaluate(hygiene);
        barFillHunger.color = barGradient.Evaluate(hunger);



        UpdateBars();
        UpdateMoodFaces();
    }

    public void Feed()
    {
        hunger += 20f;
        happiness += feedBoost / 2f;

        hygiene -= 15f;

        happiness = Mathf.Clamp(happiness, 0f, 100f);
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene, 0f, 100f);

        UpdateBars();
    }
    public void Bathe()
    {
        hygiene += hygieneBoost;
        happiness -= happinessPenaltyFromBath;

        hygiene = Mathf.Clamp(hygiene, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        UpdateBars();
    }

    public void Play()
    {
        happiness += 20f;

        hygiene -= 10f;

        hunger -= 15f;

        happiness = Mathf.Clamp(happiness, 0f, 100f);
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene, 0f, 100f);

        UpdateBars();
    }
    public void Pet()
    {
        happiness = 100f;

        UpdateBars();
    }

    private void UpdateBars()
    {
        if (hungerBar) hungerBar.value = hunger / 100f;
        if (happinessBar) happinessBar.value = happiness / 100f;
        if (hygieneBar) hygieneBar.value = hygiene / 100f;
    }
    private void UpdateMoodFaces()
    {
        if (happiness > 60f)
        {
            smiley.SetActive(true);
            neutral.SetActive(false);
            sad.SetActive(false);
        }
        else if (happiness > 30f)
        {
            smiley.SetActive(false);
            neutral.SetActive(true);
            sad.SetActive(false);
        }
        else
        {
            smiley.SetActive(false);
            neutral.SetActive(false);
            sad.SetActive(true);
        }
    }
}
