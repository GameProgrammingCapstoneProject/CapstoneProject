using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Transform prefabHealthbar;
    public static HealthManager Instance { get; private set; }

    PlayerHealth playerHealth;

    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            Debug.Log("Heath: " + playerHealth.GetHealth());
            playerHealth.TakeDamage(70);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {

        Transform healthBarTransform = Instantiate(prefabHealthbar, new Vector3(-5, 1), Quaternion.identity);
        HealthBar healthbar = healthBarTransform.GetComponent<HealthBar>();
        playerHealth = new PlayerHealth(100);

        healthbar.Setup(playerHealth);

/*        Debug.Log("Heath: " + playerHealth.GetHealth());
        playerHealth.TakeDamage(70);
        Debug.Log("Heath: " + playerHealth.GetHealth());
        playerHealth.TakeHealing(30);
        Debug.Log("Heath: " + playerHealth.GetHealth());*/

       
    }


}