using UnityEngine;
using UnityEngine.UI;

public class MoodController : MonoBehaviour
{
    public Slider hungerBar;
    public Slider happinessBar;
    public Slider hygieneBar;

    public float hunger = 100f;
    public float happiness = 100f;
    public float hygiene = 100f;

    public float decreaseRate = 0.1f;
    public float feedBoost = 50f;
    public float hygieneBoost = 30f;
    public float happinessPenaltyFromBath = 10f;
    private void Start()
    {
        UpdateBars();
    }

    private void Update()
    {

        hunger -= (decreaseRate / 6f) * Time.deltaTime;
        happiness -= (decreaseRate / 6f) * Time.deltaTime;
        hygiene -= (decreaseRate / 6f) * Time.deltaTime;


        hunger = Mathf.Clamp(hunger, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
        hygiene = Mathf.Clamp(hygiene, 0f, 100f);

        UpdateBars();
    }

    public void Feed()
    {
        hunger = 100f;
        happiness += feedBoost / 2f;

        happiness = Mathf.Clamp(happiness, 0f, 100f);
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
        happiness = 100f;
  
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
}
