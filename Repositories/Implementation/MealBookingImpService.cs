using BookMyMeal.Data.DataLayer;
using BookMyMeal.Models.Request;
using BookMyMeal.Models.Response;
using BookMyMeal.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using static QRCoder.PayloadGenerator;

namespace BookMyMeal.Repositories.Implementation
{
    public class MealBookingImpService : IMealBookingService
    {
        private static List<DateTime> HOLIDAYS = new List<DateTime>() {
            new DateTime(2024, 12, 25),
            new DateTime(2024, 1, 26),
            new DateTime(2024, 1, 14),
            new DateTime(2024, 1, 1),
            new DateTime(2023, 12, 25)
        };

        private readonly BookMyMealContext _bookedMyMealDbContext;

        public MealBookingImpService(BookMyMealContext bookedMyMealDbContext)
        {
            _bookedMyMealDbContext = bookedMyMealDbContext;
        }

        public List<MealBookingResponse> BookMyMeal(MealBookingRequest mealBookingRequest)
        {
            try
            {
                var mealBookingDetails = this._bookedMyMealDbContext.Bookmymeals.Where(x=>x.EmployeeLoginId == mealBookingRequest.EmployeeLoginID && x.IsBooked == true).ToList();

                // Start date from the request
                DateTime sDate = mealBookingRequest.StartDate.Date;

                // End date from the request
                DateTime eDate = mealBookingRequest.EndDate.Date;

                // Loop through the date range
                while (sDate <= eDate)
                {
                    bool isNewRecord = false; // Flag for a new meal booking
                    DateTime finalSDate = sDate;
                    string strFinalSDate = finalSDate.ToString("MM/dd/yyyy");

                    if (!HOLIDAYS.Any(x => x.ToString("MM/dd/yyyy") == strFinalSDate) && finalSDate.DayOfWeek != DayOfWeek.Saturday && finalSDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (mealBookingDetails?.Count > 0)
                        {
                            if(mealBookingDetails.Any(x=>
                            x.BookingDate.ToString("MM/dd/yyyy") == strFinalSDate && 
                            x.MealType.ToLower() == mealBookingRequest.MealType.ToLower()))
                            {
                                throw new Exception($"Meal is already booked on " + strFinalSDate);                                
                            }
                            else
                            {
                                isNewRecord = true; // Set flag if there are no existing bookings
                            }
                        }
                        else
                        {
                            isNewRecord = true; // Set flag if there are no existing bookings
                        }
                    }

                    if(isNewRecord)
                    {
                        this._bookedMyMealDbContext.Database.ExecuteSqlRaw($"EXEC Booked_My_Meal @empId = {mealBookingRequest.EmployeeLoginID}, @mealType = '{mealBookingRequest.MealType}', @bookingDate= {finalSDate}, @createdBy = {mealBookingRequest.EmployeeLoginID}");

                        //QRCodeGenerator QrGenerator = new QRCodeGenerator();
                        //QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q);
                        //QRCode QrCode = new QRCode(QrCodeInfo);
                        //Bitmap QrBitmap = QrCode.GetGraphic(60);
                        //byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                        //string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                        //this._bookedMyMealDbContext.Database.ExecuteSqlRaw($"EXEC Create_Coupon_For_BookMyMeal @mealBookingId={ },@qrCodeUri={QrUri}");

                        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        //QRCodeData qrCodeData = qrGenerator.CreateQrCode(mealBookingRequest);
                        //QRCode qrCode = new QRCode(qrCodeData);
                        //var qrCodeAsBitmap = qrCode.GetGraphic(20);                       
                        //string base64String = Convert.ToBase64String(BitmapToByteArray(qrCodeAsBitmap));
                        //string QRImageURL = "data:image/png;base64," + base64String;
                    }
                    sDate.AddDays(1);
                }

                return this.GetMealBookingDetailsByEmployeeId(mealBookingRequest.EmployeeLoginID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MealBookingResponse> CancelBookMyMeal(long mealBookingDetailsId)
        {
            try
            {
                var mealBookingDetails = this._bookedMyMealDbContext.Bookmymeals.Where(x => x.MealBookingId == mealBookingDetailsId).FirstOrDefault();
                if (mealBookingDetails != null)
                {
                    var data = this._bookedMyMealDbContext.Database.ExecuteSqlRaw($"EXEC Cancelled_MealBooking_By_MealBookingId @meal_booking_id = {mealBookingDetailsId}");
                    return this.GetMealBookingDetailsByEmployeeId(mealBookingDetails.EmployeeLoginId);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MealBookingResponse> GetMealBookingDetailsByEmployeeId(long employeeLoginId)
        {
            List<MealBookingResponse> mealBookingResponses = new List<MealBookingResponse>();
            try
            {
                var getMyBookedMealData = this._bookedMyMealDbContext.Bookmymeals.Where(x => x.EmployeeLoginId == employeeLoginId).ToList();
                if(getMyBookedMealData?.Count > 0)
                {
                    foreach (var item in getMyBookedMealData)
                    {
                        MealBookingResponse mealBookingResponse = new MealBookingResponse();
                        mealBookingResponse.BookingDate = item.BookingDate;
                        mealBookingResponse.IsBooked = item.IsBooked ?? false;
                        mealBookingResponse.MealBookingId = item.MealBookingId;
                        mealBookingResponse.MealType = item.MealType;
                        mealBookingResponse.EmployeeLoginId = item.EmployeeLoginId;

                        var mealCouponDetail = this._bookedMyMealDbContext.Coupons.Where(y => y.MealBookingId == item.MealBookingId).FirstOrDefault();
                        if (mealCouponDetail != null)
                        {
                            mealBookingResponse.couponDetailResponse = new CouponDetailResponse();
                            mealBookingResponse.couponDetailResponse.MealBookingId = item.MealBookingId;
                            mealBookingResponse.couponDetailResponse.CouponId = mealCouponDetail.CouponId;
                            mealBookingResponse.couponDetailResponse.QRCodeUri = mealCouponDetail.QrcodeUri;
                            mealBookingResponse.couponDetailResponse.IsRedeem = mealCouponDetail.IsRedeem;
                            mealBookingResponse.couponDetailResponse.IsActive = mealCouponDetail.IsActive ?? false;
                        }
                        mealBookingResponses.Add(mealBookingResponse);
                    }

                    return mealBookingResponses.OrderByDescending(x=>x.BookingDate).ToList();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Extension method to convert Bitmap to Byte Array
        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
