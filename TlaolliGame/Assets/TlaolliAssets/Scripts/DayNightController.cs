using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    public Light2D lightDay;
    private float transitionTime = 30f;
    private bool isDay = true;
    private AnimalsController animalsController;

    void Start()
    {
        animalsController= FindObjectOfType<AnimalsController>();
    }



    public IEnumerator CycleDayNight()
    {
        while (true)
        {
            // Colores extraídos de tus capturas
            Color dayColor = new Color32(255, 255, 255, 255);   // #FFFFFF
            Color nightColor = new Color32(10, 10, 73, 255);    // #0A0A49

            Color startColor = isDay ? dayColor : nightColor;
            Color endColor = isDay ? nightColor : dayColor;

            float t = 0f;
            while (t < transitionTime)
            {
                t += Time.deltaTime;
                float lerpT = t / transitionTime;
                lightDay.color = Color.Lerp(startColor, endColor, lerpT);
                yield return null;
            }

            isDay = !isDay;

            // Espera antes de volver a cambiar (ajustable)

            if(lightDay.color== nightColor )
            {
                StartCoroutine(animalsController.spawnerAnimal());

            }
            yield return new WaitForSeconds(60f);
        }
    }
}
