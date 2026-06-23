using Unity.VisualScripting;
using UnityEngine;

public class Lightsoff : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    float timer;
    void Start()
    {
        
    }

    void Update()
    {
        RandomLight();
        if (lights[0].enabled)
        {
            timer += Random.Range(0f, 0.2f);
        }
    }



    private void RandomLight() 
    {
        if (timer >= 180) 
        {
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            timer = 0;
        }
    }
}
