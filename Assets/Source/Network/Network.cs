
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
public class Network
{
     private int InputLayerCount;
     private List<int> HiddenLayersCount;
     private int OutputLayerCount;

    private Neuron[] InputLayer;
    private Neuron[][] HiddenLayers;
    private Neuron[] OutputLayer;

    private const string INPUT_LAYER_FILE_NAME = "InputLayer";

    private NetworkConfigurator Config;

    public Network(NetworkConfigurator config)
    {
        Config = config;

        InputLayerCount = config.InputLayerCount;
        HiddenLayersCount = config.HiddenLayersCount;
        OutputLayerCount = config.OutputLayerCount;
        
        InputLayer = new Neuron[InputLayerCount];
        OutputLayer = new Neuron[OutputLayerCount];
        System.Random random = new System.Random();

        for(int i = 0; i < InputLayer.Length; i++)
        {
            InputLayer[i] = new Neuron((float)Math.Tanh(random.NextDouble()));
        }

        HiddenLayers = new Neuron[HiddenLayersCount.Count][];

        for(int i = 0; i < HiddenLayersCount.Count; i++)
        {
            HiddenLayers[i] = new Neuron[HiddenLayersCount[i]];
            for(int j = 0; j < HiddenLayers[i].Length; j++)
            {
                HiddenLayers[i][j] = new Neuron((float)Math.Tanh(random.NextDouble()));
            }
        }

        for(int i = 0; i < OutputLayer.Length; i++)
        {
            OutputLayer[i] = new Neuron((float)Math.Tanh(random.NextDouble()));
        }
    }

    private void ProcessLayer(Neuron[] inputLayer, Neuron[] outputLayer)
    {
        for(int i = 0; i < outputLayer.Length; i++)
        {
            float sum = 0;
            for(int j = 0; j < inputLayer.Length; j++)
            {
                sum += inputLayer[j].Output;
            }
            outputLayer[i].ProcessData(sum);
        }
    }

    public float[] Process(params float[] inputs)
    {
        for(int i = 0; i < InputLayer.Length; i++)
        {
            InputLayer[i].ProcessData(inputs[i]);
        }

        for(int i = 0; i < HiddenLayers.Length; i++)
        {
            if(i == 0)
            {
                ProcessLayer(InputLayer, HiddenLayers[i]);
            }
            else
            {
                ProcessLayer(HiddenLayers[i - 1], HiddenLayers[i]);
            }  
        }

        ProcessLayer(HiddenLayers[HiddenLayers.Length - 1], OutputLayer);

        float[] results = new float[OutputLayerCount];
        for(int i = 0; i < results.Length; i++)
        {
            results[i] = OutputLayer[i].Output;
        }
        return results;
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
        
        Neuron[] MergeLayers(Neuron[] first, Neuron[] second)
        {
            Neuron[] result = new Neuron[first.Length];

            for(int i = 0; i < first.Length; i++)
            {
                if(i % 2 == 0)
                {
                    result[i] = new Neuron(first[i].Weight);
                }
                else
                {
                    result[i] = new Neuron(second[i].Weight);
                }
            }
            return result;
        }
    }

    public void Save(string path)
    {
        string result;

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        StringBuilder builder = new StringBuilder(InputLayer.Length);

        foreach(var item in InputLayer)
        {
            builder.AppendLine(item.Weight.ToString());
        }

        result = builder.ToString();

        File.WriteAllText(path + INPUT_LAYER_FILE_NAME, result);

    }

    public void Mutate()
    {
        Random random = new Random();
        Neuron[] mutatingLayer;
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

        mutatingLayer[random.Next(0, mutatingLayer.Length)].Weight = (float)Math.Tanh(random.NextDouble());
    }

}
