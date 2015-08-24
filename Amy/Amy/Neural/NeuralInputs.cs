using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amy.Neural
{
    public class NeuralInputs
    {
        // INPUTS

        public int Alpha { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        // FEEDBACK

        /// <summary>
        /// From 1 to 100, this lets the neuron know if it did a good job
        /// </summary>
        public int Reward { get; set; }
        /// <summary>
        /// From 1 to 100, this lets the neuron know if it did a bad job
        /// </summary>
        public int Punishment { get; set; }
    }
}
