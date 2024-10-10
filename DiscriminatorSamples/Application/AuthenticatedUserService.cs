using DiscriminatorSamples.Business.Interfaces;

namespace DiscriminatorSamples.Application;

//public class AuthenticatedUserService : IAuthenticatedUserService
//{
//    private readonly IHttpContextAccessor httpContextAccessor;

//    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
//    {
//        this.httpContextAccessor = httpContextAccessor;
//    }

//    public string GetAuthenticatedUserOrDefault()
//    {
//        return httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
//    }
//}


public class FakeAuthenticatedUserService : IAuthenticatedUserService
{
    //private readonly IHttpContextAccessor httpContextAccessor;

    public string GetAuthenticatedUserOrDefault()
    {
        return "gbarska";
    }
}