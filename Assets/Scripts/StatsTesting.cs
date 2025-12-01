using UnityEngine;

public class StatsTesting : MonoBehaviour
{
    public Stats playerStats = new Stats(100f, 100f, 100f, "name");
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerStats.damage);
        Debug.Log(playerStats.health);
        Debug.Log(playerStats.speed);
        Debug.Log(playerStats.name);
    }
}
