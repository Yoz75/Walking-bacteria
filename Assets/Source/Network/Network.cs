
using System;
using System.Collections.Generic;

public class Network
{
     private int InputLayerCount;
     private List<int> HiddenLayersCount;
     private int OutputLayerCount;

    private NetworkLayer InputLayer;
    private NetworkLayer[] HiddenLayers;
    private NetworkLayer LastHiddenLayer;
    private NetworkLayer OutputLayer;

    private int InputsCount;

    private NetworkConfigurator Config;

    public Network(NetworkConfigurator config)
    {
        Config = config;

        InputLayerCount = config.InputLayerCount;
        HiddenLayersCount = config.HiddenLayersCount;
        OutputLayerCount = config.OutputLayerCount;
        InputsCount = config.InputsCount;

        InputLayer = new NetworkLayer(InputLayerCount, null, InputsCount);
        HiddenLayers = new NetworkLayer[HiddenLayersCount.Count];
        for(int i = 0; i < HiddenLayers.Length; i++)
        {

            int previousLayerLength;
            NetworkLayer previousLayer;

            if(i == 0)
            {
                previousLayerLength = InputLayerCount;
                previousLayer = InputLayer;
            }
            else
            {
                 previousLayer = HiddenLayers[i - 1];
                previousLayerLength = previousLayer.GetLength();
            }
            HiddenLayers[i] = new NetworkLayer(HiddenLayersCount[i], previousLayer, previousLayerLength);
        }

        LastHiddenLayer = HiddenLayers[HiddenLayersCount.Count - 1];
        OutputLayer = new NetworkLayer(OutputLayerCount, LastHiddenLayer, LastHiddenLayer.GetLength());

    }

    public float[] Process(params float[] inputs)
    {
        float[] result;

        InputLayer.ProcessData(inputs);

        float[] hiddenLayerInputs;
        for(int i = 0; i < HiddenLayersCount.Count; i++)
        {
            if(i == 0)
            {
                hiddenLayerInputs = InputLayer.Output;
            }
            else
            {
                hiddenLayerInputs = HiddenLayers[i - 1].Output;
            }
            HiddenLayers[i].ProcessData(hiddenLayerInputs);
        }

        OutputLayer.ProcessData(LastHiddenLayer.Output);

        result = OutputLayer.Output;

        return result;

    }

    public Network Merge(Network other)
    {
        Network kid = new Network(Config);

        kid.InputLayer = MergeLayers(InputLayer, other.InputLayer);

        for(int i = 0; i < HiddenLayers.Length; i++)
        {
            kid.HiddenLayers[i] = MergeLayers(HiddenLayers[i], other.HiddenLayers[i]);
        }

        kid.OutputLayer = MergeLayers(OutputLayer, other.OutputLayer);

        return kid;
        
        NetworkLayer MergeLayers(NetworkLayer baseLayer, NetworkLayer second)
        {
            return null;
        }
    }

    public void Save(string path)
    {
    }

    public void Mutate()
    {
        Random random = new Random();
        NetworkLayer mutatingLayer;
        switch(random.Next(0,3))
        {
            case 0:
                mutatingLayer = InputLayer;
                break;
            case 1:
                mutatingLayer = HiddenLayers[random.Next(0, HiddenLayersCount.Count)];
                break;
            case 2:
                mutatingLayer = OutputLayer;
                break;
            default:
                throw new ArgumentException();
        }

        int neuronIndex = random.Next(0, mutatingLayer.GetLength());
        int weightIndex = random.Next(0, mutatingLayer.WeightsPerNeuron);
        float weightValue = (float)Math.Tanh(random.NextDouble());
        mutatingLayer.SetNeuronWeightsAtPosition(neuronIndex, weightIndex, weightValue);
    }

}
