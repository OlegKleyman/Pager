using System;

namespace AlphaDev.Paging
{
    public class PagesSettings
    {
        private static readonly Lazy<PagesSettings> Settings;

        static PagesSettings()
        {
            Settings = new Lazy<PagesSettings>(() =>
                new PagesSettings(int.MaxValue, int.MaxValue, 10));
        }

        public PagesSettings(in int previousPagesLength, in int nextPagesLength, in int itemsPerPage)
        {
            if (previousPagesLength < 0)
            {
                throw new ArgumentException("Cannot be less than '0'.", nameof(previousPagesLength));
            }

            if (nextPagesLength < 0) throw new ArgumentException("Cannot be less than '0'.", nameof(nextPagesLength));
            if (itemsPerPage < 1) throw new ArgumentException("Cannot be less than '1'.", nameof(itemsPerPage));

            PreviousPagesLength = previousPagesLength;
            NextPagesLength = nextPagesLength;
            ItemsPerPage = itemsPerPage;
        }

        public int PreviousPagesLength { get; }

        public int NextPagesLength { get; }

        public int ItemsPerPage { get; }

        public static PagesSettings Default => Settings.Value;
    }
}