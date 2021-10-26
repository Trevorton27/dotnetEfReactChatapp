using DotnetEFReactChatApp.Auth;
using DotnetEFReactChatApp.Dtos;
using DotnetEFReactChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetEFReactChatApp.Models;

namespace DotnetEFReactChatApp.Controllers
{


        [ApiController]
        [Route("api")]
        public class AuthController : Controller
        {
            private readonly IAuthorizeUserService _userService;
            private readonly JwtService _jwtService;
            public AuthController(IAuthorizeUserService userService, JwtService jwtService)
            {
                _userService = userService;
                _jwtService = jwtService;
            }
            [HttpPost("register")]
            public IActionResult Register(RegisterDto dto)
            {
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)

                };


                return Created(uri: "Success bananas!", value: _userService.Create(user));
            }

            [HttpPost("login")]
            public IActionResult Login(LoginDto dto)
            {
                var user = _userService.GetByEmail(dto.Email);
                if (user == null) return BadRequest(error: new { message = "Invalid Credentials" });
                if (!BCrypt.Net.BCrypt.Verify(text: dto.Password, hash: user.Password))
                {
                    return BadRequest(error: new { message = "Invalid Credentials" });
                }

                var jwt = _jwtService.Generate(user.Id);

                return Ok(jwt);
            }

            [HttpGet("user")]
            public new IActionResult User()
            {
                try
                {
                    var jwt = Request.Cookies["jwt"];
                    var token = _jwtService.Verify(jwt);

                    int userId = int.Parse(token.Issuer);
                    var user = _userService.GetById(userId);

                    return Ok(user);
                }
                catch (Exception)
                {
                    return Unauthorized();
                }
            }



            [HttpPost("logout")]
            public IActionResult Logout()
            {
                Response.Cookies.Delete("jwt");
                return Ok(new
                {
                    message = "Successfully logged out."
                });
            }

        }
    }

