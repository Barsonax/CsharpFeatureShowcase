namespace MemoryAndSpan
{
    public struct RGBA
    {
        public RGBA(byte red, byte green, byte blue, byte alpha) : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }
        public byte Alpha { get; }
    }
}
