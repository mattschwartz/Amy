using System.Collections.Generic;
using System.Drawing;

namespace Amy.Neural
{
    public class NeuralMap
    {
        private int _randomness;
        private int _colorVariation;

        private List<Neuron> _neuralNetwork;

        public NeuralMap(int numNeurons, int randomness, int colorVariation)
        {
            _neuralNetwork = new List<Neuron>();
            _randomness = randomness;
            _colorVariation = colorVariation;

            for (int i = 0; i < numNeurons; ++i) {
                _neuralNetwork.Add(new Neuron(randomness, colorVariation));
            }
        }

        public Color Do(Color color)
        {
            var input = new NeuralInputs {
                Alpha = color.A,
                Red = color.R,
                Green = color.G,
                Blue = color.B
            };
            var output = new NeuralOutput();

            _neuralNetwork.ForEach(n =>
            {
                output = n.Compute(input);
                n.Feedback(_randomness, _colorVariation);
                input.Alpha = output.Alpha;
                input.Red = output.Red;
                input.Green = output.Green;
                input.Blue = output.Blue;
            });

            return output.ToColor();
        }
    }
}
