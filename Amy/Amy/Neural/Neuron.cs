using System;

namespace Amy.Neural
{
    public class Neuron
    {
        private Random _random;
        private int _randomness;
        private int _colorVariation;

        public Neuron(int randomness, int colorVariation)
        {
            _random = new Random();
            _randomness = _random.Next(randomness);
            _colorVariation = _random.Next(colorVariation);
        }

        /// <summary>
        /// Keep mutating until it's a perfect neuron.
        /// </summary>
        /// <param name="randomness"></param>
        /// <param name="colorVariation"></param>
        public void Feedback(int randomness, int colorVariation)
        {
            int diff = Math.Abs(randomness - _randomness + colorVariation - _colorVariation);

            if (diff > 0) {
                _randomness = _random.Next(randomness);
                _colorVariation = _random.Next(colorVariation);
            }
        }

        public NeuralOutput Compute(NeuralInputs input)
        {
            return new NeuralOutput();
        }
    }
}
