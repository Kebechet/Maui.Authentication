namespace Maui.Authentication.Platforms.Android.Listeners;

internal abstract class ListenerBase<TResult> : Java.Lang.Object
{
    protected readonly TaskCompletionSource<TResult> _taskCompletionSource;
    public Task<TResult> Task => _taskCompletionSource.Task;

    public ListenerBase(CancellationToken cancellationToken)
    {
        _taskCompletionSource = new TaskCompletionSource<TResult>();
        cancellationToken.Register(() => _taskCompletionSource.TrySetCanceled());
    }

    protected void ReportSuccess(TResult result)
    {
        _taskCompletionSource.TrySetResult(result);
    }

    protected void ReportException(Exception exception)
    {
        _taskCompletionSource.TrySetException(exception);
    }
}
