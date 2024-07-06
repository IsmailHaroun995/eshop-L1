using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;
public class DiscountService(DiscountContext dbContext , ILogger<DiscountService> Logger)
    : DiscountProtoServoce.DiscountProtoServoceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if(coupon is null)
            coupon = new Models.Coupon { ProductName = "No discount", Amount = 0, Description = "No description" };

        Logger.LogInformation("Discount is retrieved for product name : {prodcutName} , Amount :{amount}", coupon.ProductName,coupon.Amount);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
        
    }
    public override async Task<CouponModel> CraeteDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        Logger.LogInformation("Discout is successfullt created {productName}", coupon.ProductName);
        CouponModel coupoModel = coupon.Adapt<CouponModel>();

        return coupoModel;

    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        Logger.LogInformation("Discout is successfully updated {productName}", coupon.ProductName);
        CouponModel coupoModel = coupon.Adapt<CouponModel>();

        return coupoModel;
    }
    public override async Task<DeleteDiscountReseponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found"));
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        Logger.LogInformation("Discount is successfully deleted");

        return new DeleteDiscountReseponse { Success = true };  

    }
}

