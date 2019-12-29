using System;

namespace AlphaDev.Paging
{
    public class PagesSettings
    {
        private static readonly Lazy<PagesSettings> Settings;

        static PagesSettings()
        {
            Settings = new Lazy<PagesSettings>(() => new PagesSettings(ushort.MaxValue, ushort.MaxValue));
        }

        public PagesSettings(ushort previousPagesLength, ushort nextPagesLength)
        {
            PreviousPagesLength = previousPagesLength;
            NextPagesLength = nextPagesLength;
        }

        public ushort PreviousPagesLength { get; }

        public ushort NextPagesLength { get; }

        public static PagesSettings Default => Settings.Value;
    }
}