namespace Taasks
{
    using Caliburn.Micro;
    using NodaTime;

    public sealed class RootViewModel : Screen
    {
        private readonly TaaskViewModel taask1;
        private readonly TaaskViewModel taask2;

        public RootViewModel()
        {
            taask1 = new TaaskViewModel(new Taask("Tassk 1", SystemClock.Instance));
            taask2 = new TaaskViewModel(new Taask("Tassk 2", SystemClock.Instance));
        }

        public TaaskViewModel Taask => taask1;

        public TaaskViewModel Taask2 => taask2;
    }
}
