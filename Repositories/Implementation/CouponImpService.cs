using AutoMapper;
using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Response;
using BookMyMeal.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Numerics;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using static QRCoder.PayloadGenerator;

namespace BookMyMeal.Repositories.Implementation
{
    public class CouponImpService : ICouponService
    {
        private readonly BookMyMealContext _bookedMyMealDbContext;
        private readonly IMapper _mapper;
        public CouponImpService(BookMyMealContext bookedMyMealDbContext, IMapper mapper)
        {
            _bookedMyMealDbContext = bookedMyMealDbContext;
            _mapper = mapper;
        }
        public CouponDetailResponse GetCouponById(long couponId)
        {
           // throw new NotImplementedException();
            //Context.coupon.where coupon id
            //Context.coupons.where mealBookingid
              try
              {
                    var couponDetails = this._bookedMyMealDbContext.Coupons.Where(cpnId => cpnId.CouponId == couponId).FirstOrDefault();
                    var couponDetailResponse = _mapper.Map<Coupon, CouponDetailResponse>(couponDetails);
                    return couponDetailResponse;
              }
              catch (Exception)
              {
                  throw;
              }
        }

        public CouponDetailResponse GetCouponMealBookingId(long mealBookingId)
        {
            //throw new NotImplementedException();
           try
            {
                var mealbooking = this._bookedMyMealDbContext.Coupons.Where(mealbkId => mealbkId.MealBookingId == mealBookingId).FirstOrDefault();
                var mealBookingDto = _mapper.Map<Coupon, CouponDetailResponse>(mealbooking);
                return mealBookingDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
