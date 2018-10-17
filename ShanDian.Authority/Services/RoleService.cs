﻿using ShanDian.Machine.Services;
using ShanDian.Machine.Services.Data;
using System;
using System.Collections.Generic;
using ShanDian.Machine.Model;
using ShanDian.Machine.Dto.ShanDianView;
using ShanDian.Machine.Services.Cloud;
using ShanDian.Machine.Services.Factory;
using ShanDian.SDK.Framework.Model;
using ShanDian.Common.HTTP;
using ShanDian.SDK.Framework.Helpers;
using Newtonsoft.Json;
using ShanDian.Machine.Dto;

namespace ShanDian.Machine.Services
{
    public class RoleService : IRoleService
    {
        //    //private readonly LocalRoleServices _localRoleServices;
        //    private readonly IHydraCloudRole _hydraCloudRole;
        //    //private readonly IHydraCloudUser _hydraCloudUser;
        //    public RoleServices()
        //    {
        //        _hydraCloudRole = CloudLoader.Load<IHydraCloudRole>();
        //        //_hydraCloudUser = CloudLoader.Load<IHydraCloudUser>();
        //        //_localRoleServices = new LocalRoleServices();
        //    }


        /// <summary>
        /// 获取门店的组件权限配置列表
        /// </summary>
        /// <returns></returns>
        public SDApiResponse<List<CPermissionCategoryDto>> GetPermissionsConfigure()
        {
            SDApiResponse<List<CPermissionCategoryDto>> apiResponse = new SDApiResponse<List<CPermissionCategoryDto>>();

            try
            {
                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest("/v1/permissions", HttpMethod.GET, null);

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<List<CPermissionCategoryDto>>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<List<CPermissionCategoryDto>>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }
            return apiResponse;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public SDApiResponse<List<CRoleSummaryDto>> GetRoleSummary()
        {
            SDApiResponse<List<CRoleSummaryDto>> apiResponse = new SDApiResponse<List<CRoleSummaryDto>>();

            try
            {
                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest("/v1/roles", HttpMethod.GET, null);

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<List<CRoleSummaryDto>>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<List<CRoleSummaryDto>>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }
            return apiResponse;
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <returns></returns>
        public SDApiResponse<List<CPermissionCategoryDto>> GetRolePermissionsCategory(int roleId)
        {

            SDApiResponse<List<CPermissionCategoryDto>> apiResponse = new SDApiResponse<List<CPermissionCategoryDto>>();

            try
            {
                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest($"/v1/roles/{roleId}/permissions", HttpMethod.GET, null);

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<List<CPermissionCategoryDto>>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<List<CPermissionCategoryDto>>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }
            return apiResponse;


        }


        /// <summary>
        /// 新增角色及权限
        /// </summary>
        /// <param name="rolePermissionInDto"></param>
        public SDApiResponse<CRoleSummaryDto> AddRolePermission(RolePermissionInDto rolePermissionInDto)
        {
            SDApiResponse<CRoleSummaryDto> apiResponse = new SDApiResponse<CRoleSummaryDto>();

            try
            {
                var cRolePermissionInDto = new CRolePermissionInDto()
                {
                    Name = rolePermissionInDto.RoleName,
                    IsAdmin = rolePermissionInDto.IsAdmin,
                    Permissions = rolePermissionInDto.Permissions,
                    OperatorId = rolePermissionInDto.OperatorId,
                    OperatorName = rolePermissionInDto.OperatorName
                };

                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest("/v1/roles", HttpMethod.POST, JsonConvert.SerializeObject(cRolePermissionInDto));

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<CRoleSummaryDto>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<CRoleSummaryDto>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }

            return apiResponse;
        }

        /// <summary>
        /// 修改角色及权限
        /// </summary>
        /// <param name="rolePermissionInDto"></param>
        public SDApiResponse<object> AlterRolePermission(RolePermissionInDto rolePermissionInDto)
        {
            SDApiResponse<object> apiResponse = new SDApiResponse<object>();

            try
            {
                var cRolePermissionInDto = new CRolePermissionInDto()
                {
                    Name = rolePermissionInDto.RoleName,
                    IsAdmin = rolePermissionInDto.IsAdmin,
                    Permissions = rolePermissionInDto.Permissions,
                    OperatorId = rolePermissionInDto.OperatorId,
                    OperatorName = rolePermissionInDto.OperatorName
                };

                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest($"/v1/roles/{rolePermissionInDto.RoleId}", HttpMethod.PUT, JsonConvert.SerializeObject(cRolePermissionInDto));

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<object>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<object>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }

            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }

            return apiResponse;

        }

        /// <summary>
        /// 修改角色账号
        /// </summary>
        /// <param name="useArray"></param>
        public SDApiResponse<object> AlterRoleUsers(UseArray useArray)
        {
            SDApiResponse<object> apiResponse = new SDApiResponse<object>();

            try
            {
                var cUseArray = new CUseArray()
                {
                    Users = useArray.UserIds,
                    OperatorId = useArray.OperatorId,
                    OperatorName = useArray.OperatorName
                };

                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest($"/v1/roles/{useArray.RoleId}/users", HttpMethod.PUT, JsonConvert.SerializeObject(cUseArray));

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<object>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<object>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }

            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }

            return apiResponse;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="operatorId"></param>
        /// <param name="operatorName"></param>
        public SDApiResponse<object> DeleteRole(int roleId, int operatorId, string operatorName)
        {
            SDApiResponse<object> apiResponse = new SDApiResponse<object>();

            try
            {
                var inputDto = new InputDto()
                {
                    OperatorId = operatorId,
                    OperatorName = operatorName
                };


                HttpResponse httpResponse = SDApiHelper.SDFragmentRequest($"/v1/roles/{roleId}", HttpMethod.DELETE, JsonConvert.SerializeObject(inputDto));

                if (httpResponse != null)
                {
                    apiResponse = httpResponse.AsDeserializeBody<SDApiResponse<object>>();
                }
                else
                {
                    apiResponse = new SDApiResponse<object>();
                    apiResponse.Code = AccountError.Code.ComponentError;
                    apiResponse.Msg = "远程服务器不能访问";
                    apiResponse.Ts = DateTime.Now.Ticks;
                }

            }
            catch (Exception ex)
            {
                apiResponse.Code = AccountError.Code.ComponentError;
                apiResponse.Msg = "系统繁忙，请稍后再试";
            }

            return apiResponse;
        }

    }
}