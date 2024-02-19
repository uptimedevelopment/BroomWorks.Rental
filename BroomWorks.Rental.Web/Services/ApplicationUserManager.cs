using BroomWorks.Rental.Business.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BroomWorks.Rental.Web.Services;

public class ApplicationUserManager : UserManager<IdentityUser>
{
    private readonly ICustomerService _customerService;

    public ApplicationUserManager(
        IUserStore<IdentityUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher, 
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<IdentityUser>> logger,
        ICustomerService customerService)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _customerService = customerService;
    }

    public override async Task<IdentityResult> CreateAsync(IdentityUser user)
    {
        var result = await base.CreateAsync(user);

        if (result.Succeeded)
        {
            await _customerService.CreateCustomerAsync(user.UserName);
        }

        return result;
    }
}
