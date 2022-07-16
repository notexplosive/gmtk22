namespace GMTK22.Data
{
    public readonly struct ProbableWeight
    {
        public int FaceValue { get; }
        public float Percentage { get; }

        public ProbableWeight(int faceValue, float percentage)
        {
            FaceValue = faceValue;
            Percentage = percentage;
        }

        public bool IsEmpty => FaceValue == 0;
    }
}
