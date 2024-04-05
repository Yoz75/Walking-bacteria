
using UnityEngine;

public class BacteriaCreator : MonoBehaviour
{
    [SerializeField] private int SwarmSize;
    [SerializeField] private GameObject BacteriaPrefab;

    private void Start()
    {
        for(int i = 0; i < SwarmSize; i++)
        {
            var bacteria = Instantiate(BacteriaPrefab).GetComponent<Bacteria>();
            bacteria.SetColor(new Color(Random.value, Random.value, Random.value));
        }

    }
}
