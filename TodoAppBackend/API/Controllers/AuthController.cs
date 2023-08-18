using API.Models;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("account")]
public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    public AuthController(UserManager<ApplicationUser> userManager, IJwtService jwtService, IMapper mapper)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ApplicationUser user = new ApplicationUser {
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model!.Password!);

        if (result.Succeeded)
        {
            string defaultRegisteredUserRole = "Client";
            await _userManager.AddToRoleAsync(user, defaultRegisteredUserRole);
            List<string> currentClientRoles = new() { defaultRegisteredUserRole };

            var token = _jwtService.GenerateToken(user, currentClientRoles);

            ApplicationUserDto userDto = _mapper.Map<ApplicationUserDto>(user);
            userDto.Roles = currentClientRoles;
            userDto.Token = token;
            return Ok(userDto);
        }
        else
        {
            return BadRequest(new { message = result.Errors.ElementAt(0).Description});
        }

        
;    }


    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel model)
    {
        // Check if the model is valid
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        // Find the user by username
        var user = await _userManager.FindByEmailAsync(model.Email!);

        // Check if the user exists and the password is correct
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            // Return unauthorized if the credentials are invalid
            return Unauthorized(new { message = "Invalid username or password" });
        }

        IList<string> userRoles = await _userManager.GetRolesAsync(user);

        // Generate a JWT for the user
        var token = _jwtService.GenerateToken(user, userRoles);

        ApplicationUserDto userDto = _mapper.Map<ApplicationUserDto>(user);
        userDto.Roles = userRoles;
        userDto.Token = token;
        return Ok(userDto);
    }
}
