
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Network/new config")]
public class NetworkConfigurator : ScriptableObject
{
    public int InputsCount;
    public int InputLayerCount;
    public List<int> HiddenLayersCount;
    public int OutputLayerCount;
}
