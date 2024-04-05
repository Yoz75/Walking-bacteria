
using System;

public class Neuron
{

    public float Output
    {
        get;
        private set;
    }

    public float Weight;

    public Neuron(float weight)
    {
        Weight = weight;
    }

    public void ProcessData(float input)
    {
        Output = (float)Math.Tanh(input * Weight);
    }
}