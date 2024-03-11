namespace McGurkin
{
    public readonly struct DigitalSize : IEquatable<DigitalSize>
    {
        private readonly long bits;

        private DigitalSize(long bits) { this.bits = bits; }

        public static DigitalSize FromBits(long bits) =>
            new(bits);

        public static DigitalSize FromBytes(double bytes) =>
            new((long)(bytes * 8));

        public static DigitalSize FromKilobytes(double kilobytes) =>
            new((long)(kilobytes * 8 * 1024));

        public static DigitalSize FromMegabytes(double megabytes) =>
            new((long)(megabytes * 8 * 1024 * 1024));

        public static DigitalSize FromGigabytes(double gigabytes) =>
            new((long)(gigabytes * 8 * 1024 * 1024 * 1024));

        public readonly long Bits =>
            bits;

        public readonly double Bytes =>
            bits / 8.0;

        public readonly double Kilobytes =>
            bits / 8.0 / 1024.0;

        public readonly double Megabytes =>
            bits / 8.0 / 1024.0 / 1024.0;

        public readonly double Gigabytes =>
            bits / 8.0 / 1024.0 / 1024.0 / 1024.0;

        public static DigitalSize operator +(DigitalSize a, DigitalSize b)
        {
            return new DigitalSize(a.bits + b.bits);
        }

        public static DigitalSize operator -(DigitalSize a, DigitalSize b)
        {
            return new DigitalSize(a.bits - b.bits);
        }

        public override string ToString()
        {
            if (bits >= 8L * 1024 * 1024 * 1024)
                return $"{Gigabytes} GB";
            else if (bits >= 8L * 1024 * 1024)
                return $"{Megabytes} MB";
            else if (bits >= 8L * 1024)
                return $"{Kilobytes} KB";
            else if (bits >= 8)
                return $"{Bytes} Bytes";
            else
                return $"{Bits} Bits";
        }

        public bool Equals(DigitalSize other)
        {
            return bits == other.bits;
        }

        public override bool Equals(object? obj)
        {
            if (obj is DigitalSize size)
                return Equals(size);
            return false;
        }

        public static bool operator ==(DigitalSize a, DigitalSize b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DigitalSize a, DigitalSize b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return bits.GetHashCode();
        }
    }
}
