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
        timer += Random.Range(0f, 0.1f); 
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
