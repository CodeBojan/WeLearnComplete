using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Services.Account;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : UserAuthorizedController
{
    private readonly IAccountStore _accountStore;

    public AccountsController(IAccountStore accountStore)
    {
        _accountStore = accountStore;
    }

    [HttpGet("me")]
    public async Task<ActionResult<GetAccountDto>> GetMeAsync()
    {
        try
        {
            var dto = await _accountStore.GetAccountAsync(UserId);
            return Ok(dto);
        }
        catch (AccountNotFoundException)
        {
            return NotFound();
        }
    }
}
