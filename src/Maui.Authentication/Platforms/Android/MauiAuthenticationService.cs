using Android.Gms.Auth.Api.Identity;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Maui.Authentication.Platforms.Android.Listeners;
using static Android.Gms.Auth.Api.Identity.BeginSignInRequest;

namespace Maui.Authentication.Services;

public partial class MauiAuthenticationService
{
    private ISignInClient _signInClient;
    private BeginSignInRequest _signInRequest;

    public partial void Initialize()
    {
        // Your server's client ID, not your Android client ID.
        var webApplicationClientId = "754156486387-898pihgt1o3ajtjtt2gs4v1ak8h4cuqd.apps.googleusercontent.com";

        _signInClient = Identity.GetSignInClient(Platform.CurrentActivity);
        _signInRequest = new BeginSignInRequest.Builder()
            .SetPasswordRequestOptions(new PasswordRequestOptions.Builder()
                    .SetSupported(true)
                    .Build())
            .SetGoogleIdTokenRequestOptions(new GoogleIdTokenRequestOptions.Builder()
                    .SetSupported(true)
                    .SetServerClientId(webApplicationClientId)
                    // Only show accounts previously used to sign in.
                    //.SetFilterByAuthorizedAccounts(true)
                    .Build())
            // Automatically sign in when exactly one credential is retrieved.
            .SetAutoSelectEnabled(true)
            .Build();
    }

    public partial async Task<object> SignIn(System.Threading.CancellationToken cancellationToken)
    {
        var listener = new BeginSignInListener(cancellationToken);

        await _signInClient.BeginSignIn(_signInRequest)
            .AddOnSuccessListener(Platform.CurrentActivity, listener)
            .AddOnFailureListener(Platform.CurrentActivity, listener)
            .AddOnCanceledListener(Platform.CurrentActivity, listener);

        var result = await listener.Task;

        return null;//temp
    }
}

internal sealed class BeginSignInListener : ListenerBase<BeginSignInResult>, IOnSuccessListener, IOnFailureListener, IOnCanceledListener
{
    public BeginSignInListener(System.Threading.CancellationToken cancellationToken) : base(cancellationToken)
    {
    }

    public void OnSuccess(Java.Lang.Object result)
    {
        var casted = (BeginSignInResult) result;
        _taskCompletionSource.TrySetResult(casted);
    }

    public void OnFailure(Java.Lang.Exception e)
    {
        _taskCompletionSource.TrySetException(e);
    }

    public void OnCanceled()
    {
        _taskCompletionSource.TrySetCanceled();
    }
}