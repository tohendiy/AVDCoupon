﻿using ADVCoupon.Models;
using ADVCoupon.ViewModel.CouponViewModel;
using AVDCoupon.Data;
using AVDCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Services
{
    public interface ICouponService
    {
        UserCoupon GetUserCoupon(string userId, Guid couponId);
        Task<CouponCreateItemViewModel> GetCouponProductProvidersListItemViewModelAsync();

        Task<Coupon> GetCouponById(Guid id);

        Task<Coupon> GetCouponAsync(Guid Id);
        Task<CouponCreateItemViewModel> GetCouponCreateItemViewModelAsync(Guid Id);

        Task<List<Coupon>> GetCouponsAsync();
        Task<List<CouponCreateItemViewModel>> GetCouponCreateItemViewModelsAsync();

        Task<Coupon> CreateCouponAsync(Coupon coupon);
        Task<Coupon> CreateCouponAsync(CouponCreateItemViewModel couponModel);

        Task UpdateCouponAsync(Coupon coupon);
        Task UpdateCouponAsync(CouponCreateItemViewModel couponModel);

        Task DeleteCouponAsync(Guid Id);

        Task ActivateCouponAsync(Guid Id);
        Task DeactivateCouponAsync(Guid Id);

        bool IsExist(Guid Id);
        
    }
}
