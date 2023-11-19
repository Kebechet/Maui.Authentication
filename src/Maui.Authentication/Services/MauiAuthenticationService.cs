namespace Maui.Authentication.Services;

public partial class MauiAuthenticationService
{
    public partial void Initialize();
    public partial Task<object> SignIn(CancellationToken cancellationToken = default);
}
