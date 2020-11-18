using System;

namespace SamuraiGame
{
    public class GameEventsManager : Singleton<GameEventsManager>
    {
        public event Action<int> showTooltip;
        public void ShowTooltipEvent(int index)
        {
            showTooltip?.Invoke(index);
        }

        public event Action showDummyTooltip;
        public void ShowDummyTooltipEvent()
        {
            showDummyTooltip?.Invoke();
        }

        public event Action hideTooltip;
        public void HideTooltipEvent()
        {
            hideTooltip?.Invoke();
        }
    }
}