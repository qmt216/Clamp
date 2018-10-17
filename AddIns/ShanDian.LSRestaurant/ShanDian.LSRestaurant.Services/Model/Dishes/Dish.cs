﻿using System;
using Dapper.Contrib.Extensions;
using ShanDian.LSRestaurant.Com;
using ShanDian.LSRestaurant.Factories;

namespace ShanDian.LSRestaurant.Model.Dishes
{
    /// <summary>
    /// 菜品
    /// </summary>
    [Table("tb_dish")]
    public class Dish
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [ExplicitKey]
        public long Id { get; set; } = KeyFactory.GetPrimaryKey(DishKeyEnum.Dish);

        /// <summary>
        /// 菜品编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜品名称(唯一，【菜品与加料菜之间可重名】)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜品名称拼音头字母简写
        /// </summary>
        public string PinYin { get; set; }

        /// <summary>
        /// 菜品单价，单位[分]
        /// </summary>
        public int Price { get; set; } = 0;

        /// <summary>
        /// 菜品类型： 10：普通菜品 15：加料辅菜 20：计重菜 30：加料菜（有加料辅菜的普通菜） 40：固定套餐 50：换菜套餐 60：多选套餐 70：拼合菜
        /// </summary>
        public int DishType { get; set; } = 10;

        /// <summary>
        /// 菜品分类Id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 菜品单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 是否上架状态：0 否，1是；默认1
        /// </summary>
        public int IsOnline { get; set; } = 1;

        /// <summary>
        /// 是否参加全场优惠：0 否，1 是  ；默认1
        /// </summary>
        public int IsDiscount { get; set; } = 1;

        /// <summary>
        /// 排序:按升序排
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 排序修改时间
        /// </summary>
        public DateTime SortTime { get; set; }

        /// <summary>
        /// 餐盒费
        /// </summary>
        public int BoxFee { get; set; } = 0;

        /// <summary>
        /// 是否启用份量：0 否，1 是
        /// </summary>
        public int IsSkus { get; set; } = 0;

        /// <summary>
        /// 是否启用做法：0 关，1 公共做法，2私有做法；默认0
        /// </summary>
        public int IsPractice { get; set; } = 0;


        /// <summary>
        /// 是否启用加料菜：0 关，1 公共加料，2 私有加料；默认0
        /// </summary>
        public int IsCharging { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModificationTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifitor { get; set; }
    }
}
