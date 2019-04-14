using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using QuickstartIdentityServer.Data;
using QuickstartIdentityServer.Data.Dtos;

namespace QuickstartIdentityServer
{
    public class AdditionalClaimsProfileService: IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        
        public AdditionalClaimsProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<User>(hasCustomRepository: true);
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                string clientId = context.Client.ClientId;
                string sub = context.Subject.GetSubjectId();
              
                if (string.IsNullOrEmpty(sub))
                {
                    UserDto userDto = null;
                    var user = _userRepository.Find(sub);
                    userDto = AutoMapper.Mapper.Map<UserDto>(user);
                    if (userDto != null)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("id", userDto.Id.ToString()),
                            new Claim("email", userDto.UserName),
                            new Claim("name", userDto.Id.ToString()),//返回的id_token包含的信息
                            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                            new Claim(ClaimTypes.Email, userDto.UserName),
                            new Claim(ClaimTypes.Name, userDto.Id.ToString())//返回的id_token包含的信息
                        };
                        context.IssuedClaims = claims;
                    }
                }
            }catch (Exception ex)
            {

            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                string clientId = context.Client.ClientId;
                string sub = context.Subject.GetSubjectId();
              
                if (string.IsNullOrEmpty(sub))
                {
                    UserDto userDto = null;
                    var user = _userRepository.Find(sub);
                    userDto = AutoMapper.Mapper.Map<UserDto>(user);
                    if (userDto != null)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("id", userDto.Id.ToString()),
                            new Claim("email", userDto.UserName),
                            new Claim("name", userDto.Id.ToString()),//返回的id_token包含的信息
                            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                            new Claim(ClaimTypes.Email, userDto.UserName),
                            new Claim(ClaimTypes.Name, userDto.Id.ToString())//返回的id_token包含的信息
                        };
                    }
                    
                       
                    context.IsActive = userDto != null;
                }
                
            }catch (Exception ex)
            {

            }
        }
    }
}
