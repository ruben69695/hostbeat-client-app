using System.Threading.Tasks;

namespace HostbeatClient.Features.Login;

public class LoginWindowViewModel
{
    public async void Login()
    {
        await Task.FromResult(true);
    }
}