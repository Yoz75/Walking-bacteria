
public class NetworkLayer
{
    public NetworkLayer(int length, NetworkLayer previousLayer, int weightsCount)
    {
        Layer = new Neuron[length];
        WeightsPerNeuron = weightsCount;

        for (int i = 0; i < length; i++) 
        {
            Layer[i] = new Neuron(weightsCount);
        }

        if (previousLayer != null) 
        {
            PreviousLayer = previousLayer;
        }
    }

    public float[] Output
    {
        get;
        private set;
    }

    public int WeightsPerNeuron
    {
        get;
        private set;
    }

    private Neuron[] Layer;

    public NetworkLayer PreviousLayer
    {
        get;
        private set;
    }

    public int GetLength()
    {
        return Layer.Length;
    }

    public void SetNeuronWeightsAtPosition(int neuronIndex, int weightIndex, float weight)
    {
        Layer[neuronIndex].SetWeight(weightIndex, weight);
    }

    public void ProcessData(float[] inputs)
    {
        Output = new float[Layer.Length];
        for(int i = 0; i < Layer.Length; i++)
        {
            Layer[i].ProcessData(inputs);
            Output[i] = Layer[i].Output;
        }
    }

}

