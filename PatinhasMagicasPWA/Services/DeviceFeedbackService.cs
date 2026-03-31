using Microsoft.JSInterop;

namespace PatinhasMagicasPWA.Services
{
    public class DeviceFeedbackService
    {
        private readonly IJSRuntime _jsRuntime;

        public DeviceFeedbackService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public ValueTask VibrateSelectionAsync()
        {
            return VibrateAsync(new[] { 30 });
        }

        public ValueTask VibrateSuccessAsync()
        {
            return VibrateAsync(new[] { 40, 30, 80 });
        }

        public ValueTask VibrateWarningAsync()
        {
            return VibrateAsync(new[] { 60, 40, 60 });
        }

        private ValueTask VibrateAsync(int[] pattern)
        {
            return _jsRuntime.InvokeVoidAsync("deviceFeedback.vibrate", pattern);
        }
    }
}
