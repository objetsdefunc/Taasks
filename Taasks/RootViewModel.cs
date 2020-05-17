namespace Taasks
{
    using Caliburn.Micro;
    using NodaTime;

    public sealed class RootViewModel : Screen
    {
        private readonly TaaskViewModel taask;
        private readonly TaaskViewModel taask2;

        public RootViewModel()
        {
            taask = new TaaskViewModel(new Taask(SystemClock.Instance));
            taask2 = new TaaskViewModel(new Taask(SystemClock.Instance));
        }

        public TaaskViewModel Taask => taask;

        public TaaskViewModel Taask2 => taask2;
    }
}
