
using System;

public class Neuron
{

    public float Output
    {
        get;
        private set;
    }

    public int WeightsCount
    {
        get
        {
            return Weights.Length;
        }
    }

    private float[] Weights;

    public Neuron(int weightsCount)
    {
        Random random = new Random();
        Weights = new float[weightsCount];
        for (int i = 0; i < weightsCount; i++) 
        {
            Weights[i] = (float)Math.Sin(random.NextDouble());
        }
    }

    public void ProcessData(float[] inputs)
    {
        for(int i = 0; i < Weights.Length; i++)
        {
            Output += inputs[i] * Weights[i];
        }
        Output = (float)Math.Tanh(Output);
    }

    public void SetWeight(int weightIndex, float value)
    {
        Weights[weightIndex] = value;
    }

}