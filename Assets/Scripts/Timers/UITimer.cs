namespace Timers
{
    public class UITimer : Timer
    {
        private void Show()
        {
            
        }

        private void Hide()
        {
            
        }

        public override void StartTimer(float newDuration = -1f)
        {
            base.StartTimer(newDuration);
            Show();
        }


        protected override void FinishTimer()
        {
            base.FinishTimer();
            Hide();
        }
    }
}