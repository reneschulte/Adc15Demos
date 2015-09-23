namespace WpDxBandSample.Helpers
{
    public class Smoothen
    {
        private readonly double[] _inputs;

        public Smoothen()
            : this(5)
        {
        }

        public Smoothen(int factor)
        {
            _inputs = new double[factor];
        }

        public double GetValue(double input)
        {
            var sum = input;

            for (var index = _inputs.Length - 2; index >= 0; index--)
            {
                sum += _inputs[index];
                _inputs[index + 1] = _inputs[index];
            }

            _inputs[0] = input;
            return sum / _inputs.Length;
        }
    }
}
