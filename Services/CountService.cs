using System;
namespace web_blazor.Services;
public class CountService
{
    public int Count { get; set; } = 10;
    public void InCrease()
    {
        Count += 1;
        NotifyStateChange();
    }
    public void DeCrease()
    {
        Count -= 1;
        NotifyStateChange();
    }

    public event Action OnChange;

    public void NotifyStateChange() => OnChange?.Invoke();
}