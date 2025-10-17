using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject cheese;
    public GameObject sauce;
    public GameObject soldCheese;
    public GameObject soldSauce;

    public void SpawnCheese()
    {
        Instantiate(cheese);

    }

    public void SpawnSauce()
    {
        Instantiate (sauce);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z");
            cheese.SetActive(true);
            soldCheese.SetActive(true);
            //SpawnCheese();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X");
            sauce.SetActive(true);
            soldSauce.SetActive(true);
           // SpawnSauce();
        }
    }

}
