namespace GMTK22.Data
{
    public readonly struct Weight
    {
        public int FaceValue { get; }
        public float Percentage { get; }

        public Weight(int faceValue, float percentage)
        {
            FaceValue = faceValue;
            Percentage = percentage;
        }

        public bool IsEmpty => FaceValue == 0;
    }
}
