using System;

namespace AlphaDev.Paging
{
    public class PagesSettings
    {
        private static readonly Lazy<PagesSettings> Settings;

        static PagesSettings()
        {
            Settings = new Lazy<PagesSettings>(() =>
                new PagesSettings(ushort.MaxValue, ushort.MaxValue, 10));
        }

        public PagesSettings(ushort previousPagesLength, ushort nextPagesLength, uint itemsPerPage)
        {
            if (itemsPerPage == 0) throw new ArgumentException("Cannot be '0'.", nameof(itemsPerPage));

            PreviousPagesLength = previousPagesLength;
            NextPagesLength = nextPagesLength;
            ItemsPerPage = itemsPerPage;
        }

        public ushort PreviousPagesLength { get; }

        public ushort NextPagesLength { get; }

        public uint ItemsPerPage { get; }

        public static PagesSettings Default => Settings.Value;
    }
}