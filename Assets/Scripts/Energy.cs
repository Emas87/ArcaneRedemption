using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    [SerializeField]
    Texture2D empty;
    [SerializeField]
    Texture2D filled;
    [SerializeField]
    Texture2D half;

    // Update is called once per frame
    void Update()
    {
        float energy = FindObjectOfType<PlayerStats>().energyPoints;
        float capacity = FindObjectOfType<PlayerStats>().energyPointsCapacity;

        Slider temp = GetComponent<Slider>();
        temp.value = energy*100/capacity;
    }
}
