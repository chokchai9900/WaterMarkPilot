using SkiaSharp;
using SkiaSharp.HarfBuzz;
using System;
using System.IO;
using System.Linq;
using Topten.RichTextKit;
using WaterMarkPilot.Models;

namespace WaterMarkPilot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new RecipeDetailModel()
            {
                DisplayName = "Display Name",
                WalletName = "WalletName",
                ReceiverName = "Receiver",
                Amount = "4.00",
                Currency = "USD",
                Coupon = "10.00+140.00",
                CouponCurrencyType = "THB",
                RefCode = "A009",
                RefDateTime = "1/8/2020 (12.00)",
                OptionalDetail = "[Optional Detail]",
                ProfileImgSenderUrl = "Url",
                ProfileImgReceiverUrl = "Url"
            };

            var savePath = @"E:\PilotWaterMark\final.png";

            var recipeImg = GenerateRecipe(data);
            using (FileStream outputFileStream = new FileStream(savePath, FileMode.Create))
            {
                recipeImg.CopyTo(outputFileStream);
            }
        }

        static Stream GenerateRecipe(RecipeDetailModel data)
        {
            var fontRegularPath = @"E:\PilotWaterMark\element\Kanit-Regular.ttf";
            var fontBoldPath = @"E:\PilotWaterMark\element\Kanit-Bold.ttf";
            var fontLightPath = @"E:\PilotWaterMark\element\Kanit-Light.ttf";

            var reseverIconPic = @"E:\PilotWaterMark\element\400.jpg";
            var senderIconPic = @"E:\PilotWaterMark\element\rcmtmwit.png";

            //var reseverIconPic = data.ProfileImgReceiverUrl;
            //var senderIconPic = data.ProfileImgSenderUrl;

            var waterMarkIcon = @"E:\PilotWaterMark\element\logomana.png";
            var qriconPic = @"E:\PilotWaterMark\element\iconqr@3x.png";
            var sendToIconPic = @"E:\PilotWaterMark\element\rcin.png";
            var successIocnPic = @"E:\PilotWaterMark\element\iconsuccess.png";
            var mPlusIconPic = @"E:\PilotWaterMark\element\fntopup@3x.png";


            SKBitmap bmp = new(800, 1000);
            using SKCanvas canvas = new(bmp);
            canvas.Clear(SKColor.Parse("#ffffff"));
            var CustomLightfont = SKTypeface.FromFile(fontLightPath);
            var CustomRegularfont = SKTypeface.FromFile(fontRegularPath);
            var CustomBoldfont = SKTypeface.FromFile(fontBoldPath);

            var bm = SKBitmap.Decode(waterMarkIcon).Resize(new SKImageInfo(100, 100), SKFilterQuality.High);
            var qricon = SKBitmap.Decode(qriconPic).Resize(new SKImageInfo(100, 100), SKFilterQuality.High);
            var sendToIcon = SKBitmap.Decode(sendToIconPic).Resize(new SKImageInfo(100, 100), SKFilterQuality.High);
            var senderIcon = SKBitmap.Decode(senderIconPic).Resize(new SKImageInfo(100, 100), SKFilterQuality.High);
            var reseverIcon = SKBitmap.Decode(reseverIconPic).Resize(new SKImageInfo(100, 100), SKFilterQuality.High);
            var successIocn = SKBitmap.Decode(successIocnPic).Resize(new SKImageInfo(20, 20), SKFilterQuality.High);
            var mPlusIcon = SKBitmap.Decode(mPlusIconPic).Resize(new SKImageInfo(20, 20), SKFilterQuality.High);

            var paintLightText = new SKPaint()
            {
                TextSize = 22,
                Color = SKColors.Black,
                Typeface = CustomLightfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularText = new SKPaint()
            {
                TextSize = 22,
                Color = SKColors.Black,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintBoldText = new SKPaint()
            {
                TextSize = 22,
                Color = SKColors.Black,
                Typeface = CustomBoldfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextAmount = new SKPaint()
            {
                TextSize = 36,
                Color = SKColors.Red,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextUSD = new SKPaint()
            {
                TextSize = 22,
                Color = SKColors.Red,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextCoupon = new SKPaint()
            {
                TextSize = 20,
                Color = SKColors.Gray,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextRefCode = new SKPaint()
            {
                TextSize = 36,
                Color = SKColors.Black,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextTHB = new SKPaint()
            {
                TextSize = 14,
                Color = SKColors.DarkGray,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextRefDate = new SKPaint()
            {
                TextSize = 26,
                Color = SKColors.Gray,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };
            var paintRegularTextInfo = new SKPaint()
            {
                TextSize = 26,
                Color = SKColors.Black,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf16,
                IsAntialias = true,
                SubpixelText = true
            };
            var paintRegularTextOptionalInfo = new SKPaint()
            {
                TextSize = 26,
                Color = SKColors.DarkGray,
                Typeface = CustomRegularfont,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = true
            };

            var paintLine = new SKPaint()
            {
                Color = SKColors.Gray.WithAlpha(100),
                StrokeWidth = 5
            };
            var paintImg = new SKPaint()
            {
                TextAlign = SKTextAlign.Center,
                Color = SKColors.Black,
            };

            var OpacityOption = new SKPaint()
            {
                Color = SKColors.Black.WithAlpha(20),
            };

            var firstPosX = 0;
            var firstPosY = 0;

            var bmpWidthSize = bmp.Width;
            var bmpHeightSize = bmp.Height;

            var sharkLine = true;

            for (int y = firstPosY; y < bmpHeightSize; y += bm.Height)
            {
                for (int x = firstPosX; x < bmpWidthSize; x += bm.Width)
                {
                    canvas.DrawBitmap(bm, new SKPoint(x, y), OpacityOption);
                }
                if (sharkLine)
                {
                    firstPosX -= 50;
                    sharkLine = false;
                }
                else
                {
                    firstPosX += 50;
                    sharkLine = true;
                }
            }

            var centerPicX = GetPercentage(bmp.Width, 50);
            var centerPicY = GetPercentage(bmp.Height, 50);

            var centerSenderContentX = GetPercentage(centerPicX, 50);
            var centerSenderContentY = GetPercentage(centerPicY, 50);

            canvas.DrawBitmap(senderIcon, new SKPoint(centerSenderContentX - (senderIcon.Width / 2), centerSenderContentY - 120), paintImg);
            canvas.DrawBitmap(sendToIcon, new SKPoint((centerSenderContentX * 2) - (sendToIcon.Width / 2), centerSenderContentY - 120), paintImg);
            canvas.DrawBitmap(reseverIcon, new SKPoint((centerSenderContentX * 3) - (reseverIcon.Width / 2), centerSenderContentY - 120), paintImg);

            canvas.DrawShapedText(data.DisplayName, centerSenderContentX - (paintRegularText.MeasureText(data.DisplayName) / 2), centerSenderContentY + 50, paintRegularText);
            canvas.DrawShapedText(data.WalletName, centerSenderContentX - (paintRegularText.MeasureText(data.WalletName) / 2), centerSenderContentY + 90, paintLightText);
            canvas.DrawShapedText(data.ReceiverName, (centerSenderContentX * 3) - (paintRegularText.MeasureText(data.ReceiverName) / 2), centerSenderContentY + 50, paintRegularText);

            var centerAmountPointX = centerPicX - (paintRegularTextAmount.MeasureText(data.Amount) / 2);
            var pointStartAmountText = (centerPicX - (paintRegularTextAmount.MeasureText(data.Amount) / 2)) - (successIocn.Width + 10);
            var pointEndAmountText = (centerPicX + (paintRegularTextAmount.MeasureText(data.Amount) / 2)) + 10;

            var centercouponAmountePointX = centerPicX - (paintRegularTextCoupon.MeasureText(data.Coupon) / 2);
            var pointStartMplusText = (centerPicX - (paintRegularTextCoupon.MeasureText(data.Coupon) / 2)) - (mPlusIcon.Width + 10);
            var pointEndMplusText = (centerPicX + (paintRegularTextCoupon.MeasureText(data.Coupon) / 2)) + 5;

            canvas.DrawBitmap(successIocn, pointStartAmountText, centerPicY - 120, paintImg);
            canvas.DrawShapedText(data.Amount, centerAmountPointX, centerPicY - 100, paintRegularTextAmount);
            canvas.DrawShapedText(data.Currency, pointEndAmountText, centerPicY - 100, paintRegularTextUSD);

            canvas.DrawBitmap(mPlusIcon, pointStartMplusText, centerPicY - 88, paintImg);
            canvas.DrawShapedText(data.Coupon, centercouponAmountePointX, centerPicY - 70, paintRegularTextCoupon);
            canvas.DrawShapedText(data.CouponCurrencyType, pointEndMplusText, centerPicY - 70, paintRegularTextTHB);

            canvas.DrawLine(new SKPoint(0, centerPicY + 50), new SKPoint(bmp.Width, centerPicY + 50), paintLine);
            canvas.DrawLine(new SKPoint(0, centerPicY + 250), new SKPoint(bmp.Width, centerPicY + 250), paintLine);

            canvas.DrawShapedText($"รหัสอ้างอิง : {data.RefCode}", 180, centerPicY + 130, paintRegularTextRefCode);
            canvas.DrawShapedText($"วันที่ : {data.RefDateTime}", 180, centerPicY + 190, paintRegularTextRefDate);
            canvas.DrawBitmap(qricon, new SKPoint(40, centerPicY + 100), paintImg);

            canvas.DrawShapedText("เพิ่มเติม", 40, centerPicY + 300, paintRegularTextInfo);
            canvas.DrawShapedText(data.OptionalDetail, 40, centerPicY + 340, paintRegularTextOptionalInfo);

            var result = bmp.Encode(SKEncodedImageFormat.Png, quality: 100);
            var stream = new MemoryStream();
            result.SaveTo(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        static float GetPercentage(float value,float percen)
        {
            return (value * percen) / 100;
        }

    }
}