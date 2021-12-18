using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToSpawn;
    [SerializeField]
    private float[] percentages;
    private float period;
    [SerializeField]
    private Transform spawnPoint;
    private void Update()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetCurrentGameEnded())
        {
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetWeaponPickedUp())
            {
                if (period > Random.Range(1f, 5f))
                {
                    Instantiate(objectsToSpawn[GetRandomSpawn()], new Vector3(spawnPoint.position.x + Random.Range(-5f, 5f), spawnPoint.position.y, spawnPoint.position.z + Random.Range(-2f, 2f)), spawnPoint.rotation);
                    period = 0;
                }
                period += Time.deltaTime;
            }
        }
    }

    private int GetRandomSpawn()
    {
        var random = Random.Range(0f, 1f);
        float numForAdding = 0;
        float total = 0;

        for (var i = 0; i < percentages.Length; i++)
        {
            total += percentages[i];
        }

        for (var i = 0; i < objectsToSpawn.Length; i++)
        {
            if (percentages[i] / total + numForAdding >= random)
            {
                return i;
            }

            numForAdding += percentages[i] / total;
        }

        return 0;
    }
}