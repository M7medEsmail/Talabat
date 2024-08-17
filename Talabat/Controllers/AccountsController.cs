using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Services;
using Talabat.Dto;
using Talabat.Errors;
using Talabat.Extensions;

namespace Talabat.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService TokenService;
        private readonly IMapper Mapper;
        private readonly IEmailService EmailService;

        public AccountsController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager , ITokenService tokenService , IMapper mapper , IEmailService emailService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            TokenService = tokenService;
            Mapper = mapper;
            EmailService = emailService;
        }
        [HttpPost("login")]
        [SwaggerOperation(summary: "تسجيل الدخول ")]

        public async Task<ActionResult<UserDto>> Login(LoginDto LoginDto)
        {
            var user = await userManager.FindByEmailAsync(LoginDto.Email);
            if (user == null) return Unauthorized(new ApiErrorResponse(401));

            // false value here (lookOutOfFailure) as if he write error in password , Don't block this account! 
            var result = await signInManager.CheckPasswordSignInAsync(user, LoginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiErrorResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await TokenService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        [SwaggerOperation(summary: "انشاء حساب ")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExist(registerDto.Email).Result.Value)
                return BadRequest(new ApiErrorResponse(400 , "This Email Is Existed"));

            var user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email, // m7med.esmail22@gmail.com
                UserName = registerDto.Email.Split('@')[0] //m7med.esmail22
            };

            var result = await userManager.CreateAsync(user , registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            return Ok(new UserDto()
            {

                DisplayName = user.UserName,
                Email = user.Email,
                Token = await TokenService.CreateToken(user)
            });


        }

        [HttpPost("SignOut")]
        [SwaggerOperation(summary: "تسجيل الخروج ")]

        [Authorize]
        public new async Task<ActionResult> SignOut() // we use new because the base of controller have signoit function 
        {
            await signInManager.SignOutAsync();
            return Ok();
        }


        [HttpPost("SendEmail")]
        [SwaggerOperation(summary: "  ارسال ميل للمستخدم ")]

        public async Task<ActionResult> SendEmail(Email request)
        {
            EmailService.SendEmail(request);
            return Ok();
        }
        //{
        //    if (ModelState.IsValid) 
        //    {
        //        var user = await userManager.FindByEmailAsync(forgetPasswordDto.Email);

        //        if(user != null)
        //        {
        //            var token = await userManager.GeneratePasswordResetTokenAsync(user); // Token is valid for only one time for this user 
        //            var ResetPasswordLink = Url.Action("ResetPassword", "Accounts", new { Email = forgetPasswordDto.Email , Token = token }, Request.Scheme);
        //            // scheme have Protocol + HostName +Port
        //            //https://localhost:7049/Accounts/ResetPassword?Email=som3a@gmail.com&token=jnbimgfkdgvopckfdcmkclx,
        //            var email = new Email()
        //            {
        //               Subject = "Reset Your Password",
        //               To = forgetPasswordDto.Email,
        //               Body = ""

        //            };

        //        }
        //        ModelState.AddModelError(string.Empty, "Email is not Exist");
        //    }
        //}

        [Authorize]
        [HttpGet("CurrentUser")] // Get : api/account
        [SwaggerOperation(summary: " ايجاد المستخدم الحالي")]

        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await TokenService.CreateToken(user)
            });
        }

        [HttpGet("EmailExist")]
        [SwaggerOperation(summary: " فحص اذا كان الايميل موجود ولا لا ")]

        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await userManager.FindByEmailAsync(email) !=null;
        }


        [Authorize]
        [HttpPut("UpdateAddress")] // Put : api/account/address
        [SwaggerOperation(summary: " تعديل عنوان المستخدم ")]

        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdatedAddress)
        {
            var address = Mapper.Map<AddressDto, Address>(UpdatedAddress);
            var AppUser = await userManager.FindUserWithAddressByEmailAsync(User);


            AppUser.Address = address;
            var result = await userManager.UpdateAsync(AppUser);    
            if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400, "An Error Occure when Updating the address"));
            return Ok(Mapper.Map<Address , AddressDto>(AppUser.Address));
        }
        [Authorize]
        [HttpGet("address")]
        [SwaggerOperation(summary: " ايجاد عنوان المستخدم ")]

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            return Ok(Mapper.Map<Address , AddressDto>(user.Address));

        }



    }
}
