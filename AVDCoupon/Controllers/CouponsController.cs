﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AVDCoupon.Data;
using AVDCoupon.Models;
using ADVCoupon.ViewModel.CouponViewModel;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Net.Http.Headers;
using ADVCoupon.Services;
using ADVCoupon.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ADVCoupon.Helpers;

namespace ADVCoupon.Controllers
{
    [Authorize(Roles = Constants.ADMIN_ROLE)]
    public class CouponsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICouponService _couponService;
        private readonly ITemplateService _templateService;


        public CouponsController(ApplicationDbContext context, ITemplateService templateService, UserManager<ApplicationUser> userManager, ICouponService couponService)
        {
            _templateService = templateService;
            _context = context;
            _userManager = userManager;
            _couponService = couponService;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
            //var coupons = await _context.Coupons.ToListAsync();
            //var couponsListViewModel = new List<CouponDetailItemViewModel>(coupons.Count);
            //couponsListViewModel = coupons.Select(item => new CouponDetailItemViewModel
            //{
            //    Id = item.Id,
            //    CouponName = item.CouponName,
            //    TotalCapacity = item.TotalCapacity,
            //    CurrentCapacity = item.CurrentCapacity,
            //    CouponImage = item.CouponImage
            //}).ToList();
            //return View(couponsListViewModel);

            //////////////////////////////////////////////
            //var couponModelList = await _couponService.GetCouponCreateItemViewModelsAsync();
            //return View(couponModelList);

            return View();

        }

        // GET: Coupons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponDetailItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponImage = coupon.CouponImage
            //};
            if (id == null)
            {
                return NotFound();
            }

            var couponModel = await _couponService.GetCouponCreateItemViewModelAsync(id.Value);
            if (couponModel == null)
            {
                return NotFound();
            }
            return View(couponModel);

        }

        // GET: Coupons/Create
        public async Task<IActionResult> Create()
        {
           

            var model = await _couponService.GetCouponProductsListItemViewModelAsync();

            return View(model);
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponCreateItemViewModel couponModel)
        {
            if(couponModel.DiscountTypeText.Equals(Constants.DISCOUNT_TYPE_PERCENT))
            {
                if(couponModel.Discount < 0 || couponModel.Discount > 100)
                {
                    ModelState.AddModelError("Discount", "Out of range of possible values");
                }
            }
            if(couponModel.Image == null)
            {
                ModelState.AddModelError("Image", "Please, provide image for coupon");
            }
            if (ModelState.IsValid)
            {
                var coupon = await _couponService.CreateCouponAsync(couponModel);
                // for test only! AA
            //    var location = Helpers.CookiesHelper.GetGeolocation(HttpContext);

            //    var coupon = new Coupon
            //    {
            //        TotalCapacity = couponItem.TotalCapacity,
            //        CurrentCapacity = couponItem.CurrentCapacity,
            //        CouponName = couponItem.CouponName,
            //        Id = Guid.NewGuid()
            //        //MerchantUser = _userManager.Users.FirstOrDefault(item => item.Id == couponItem.MerchantUserId),
                    
            //    };
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await couponItem.CouponImage.CopyToAsync(memoryStream);
            //        coupon.CouponImage = memoryStream.ToArray();
            //    }
            //    _context.Add(coupon);
            //    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            couponModel.Products = _couponService.GetSelectListProducts();
            couponModel.DiscountType = _couponService.GetSelectListDiscountTypes();
            couponModel.Networks = _couponService.GetMultiSelectListNetworks();
            return View(couponModel);
        }

        // GET: Coupons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponEditItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponViewImage = coupon.CouponImage
            //};
            if (id == null)
            {
                return NotFound();
            }

            var couponModel = await _couponService.GetCouponCreateItemViewModelAsync(id.Value);
            if (couponModel == null)
            {
                return NotFound();
            }
            return View(couponModel);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CouponCreateItemViewModel couponModel)
        {
            if (id != couponModel.Id)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
                try
                {
                    //    var coupon = new Coupon
                    //    {
                    //        TotalCapacity = couponItem.TotalCapacity,
                    //        CurrentCapacity = couponItem.CurrentCapacity,
                    //        CouponName = couponItem.CouponName,
                    //        Id = couponItem.Id
                    //    };
                    //    using (var memoryStream = new MemoryStream())
                    //    {
                    //        await couponItem.CouponImage.CopyToAsync(memoryStream);
                    //        if (memoryStream != null)
                    //        {
                    //            coupon.CouponImage = memoryStream.ToArray();
                    //        }
                    //    }

                    //    _context.Update(coupon);
                    //    await _context.SaveChangesAsync();
                    await _couponService.UpdateCouponAsync(couponModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(couponModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            couponModel.Products = _couponService.GetSelectListProducts();
            couponModel.DiscountType = _couponService.GetSelectListDiscountTypes();
            couponModel.Networks = _couponService.GetMultiSelectListNetworks();
            return View(couponModel);
        }

        // GET: Coupons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponDetailItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponImage = coupon.CouponImage
            //};
            if (id == null)
            {
                return NotFound();
            }

            var couponModel = await _couponService.GetCouponCreateItemViewModelAsync(id.Value);
            if (couponModel == null)
            {
                return NotFound();
            }
            return View(couponModel);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _couponService.DeleteCouponAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(Guid id)
        {
            return _couponService.IsExist(id);
        }

        [Route("pdf/test")]
        public async Task<IActionResult> CouponPdf(string couponId, string userId)
        {

            var currentUserCoupon = _couponService.GetUserCoupon(userId, new Guid(couponId));
            var currentCoupon = await _couponService.GetCouponById(new Guid(couponId));
            var model = CouponGenerator.ConvertUserCouponToPdfViewModel(currentUserCoupon, currentCoupon);

            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/pdf/coupon")]
        public async Task<IActionResult> GenerateCoupon(string couponId, string userId)
        {
            var output = await Helpers.CouponGenerator.GeneratePDF(_context, _templateService, _couponService, new Guid(couponId), userId);
            return File(output, "application/pdf");
        }

        public async Task<IActionResult> ActivateCoupon(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _couponService.ActivateCouponAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeactivateCoupon(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _couponService.DeactivateCouponAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var couponsList = await _couponService.GetCouponCreateItemViewModelsAsync();
            return PartialView("_IndexGrid", couponsList);
        }
        //[HttpGet]
        //public async Task<PartialViewResult> CreateProductGrid()
        //{
        //    // Only grid query values will be available here.
        //    //return PartialView("_IndexGrid", usersWithRoles.ToList());
        //}


    }
}
