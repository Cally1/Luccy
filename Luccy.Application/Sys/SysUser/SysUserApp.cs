﻿using Abp.Domain.Repositories;
using Luccy.Core.CommonModel;
using Luccy.Entity.Sys;
using Luccy.IRepository.Sys;
using Luccy.Sys.SysUser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Luccy.Sys.SysUser
{
    public  class SysUserApp : LuccyAppServiceBase, ISysUserApp
    {
        private readonly ISysUserRepository _sysUserRepository;
        public SysUserApp(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }

        public UserOutputDto GetUserList(Pagination pagination, string keyword)
        {
            List<SysUserEntity> userEntityList= _sysUserRepository.GetUserListByPage(pagination, keyword);
            List<UserDto> userDtoList= AutoMapper.Mapper.Map<List<UserDto>>(userEntityList);
            UserOutputDto outputDto = new UserOutputDto();
            outputDto.UserDtoList = userDtoList;
            return outputDto;
        }




        /// <summary>
        /// 根据名称和密码获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserDto CheckLogin(LoginInputDto inputDto)
        {
            SysUserEntity entity = _sysUserRepository.GetAll().Where(b => b.UserName.Equals(inputDto.Name) && b.Password.Equals(inputDto.Pwd)).FirstOrDefault();
            UserDto dto = AutoMapper.Mapper.Map<UserDto>(entity);
            return dto;
        }


    }
}
