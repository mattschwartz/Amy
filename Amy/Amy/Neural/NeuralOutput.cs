using System.Drawing;

namespace Amy.Neural
{
    public class NeuralOutput
    {
        public int Alpha { get; set; }
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }

        public Color ToColor()
        {
            return Color.FromArgb(Alpha, Red, Green, Blue);
        }
    }
}
