// Decompiled with JetBrains decompiler
// Type: HuionTablet.ImageHelper
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Huion;
using HuionTablet.utils;

namespace HuionTablet
{
    public class ImageHelper
    {
        public static Image GetIco(string deviceTypeName, string extensionStr)
        {
            return (Image) getImage(GetIconPath(deviceTypeName, extensionStr));
        }

        public static Image GetScaleIco(string deviceTypeName, string extensionStr)
        {
            return (Image) HuionRender.blowupImage(getImage(GetIconPath(deviceTypeName, extensionStr)),
                DpiHelper.getInstance().XDpi);
        }

        public static Image GetScaleIco(string deviceTypeName, string extensionStr, Control v)
        {
            return getScaleImage(GetIconPath(deviceTypeName, extensionStr), v);
        }

        public static Image GetResizeIco(string deviceTypeName, string extensionStr, Control v)
        {
            return (Image) HuionRender.resizeImage(new Bitmap(GetIco(deviceTypeName, extensionStr)), v.Width, v.Height);
        }

        public static Bitmap getImage(string imagePath)
        {
            FileStream fileStream = File.Open(imagePath, FileMode.Open);
            Image original = Image.FromStream((Stream) fileStream);
            fileStream.Close();
            fileStream.Dispose();
            Bitmap bitmap = new Bitmap(original);
            original.Dispose();
            return bitmap;
        }

        public static Bitmap getDllImage(string imageName)
        {
            Stream stream = new Depot().loadImage(imageName);
            Bitmap bitmap = new Bitmap(stream);
            stream.Close();
            stream.Dispose();
            return bitmap;
        }

        public static Bitmap getDllImage(string imageName, OEMType oemType)
        {
            Depot depot = new Depot();
            Stream stream;
            switch (oemType)
            {
                case OEMType.HUION:
                    stream = depot.loadHuionImage(imageName);
                    break;
                case OEMType.GAOMON:
                    stream = depot.loadGaomonImage(imageName);
                    break;
                default:
                    stream = depot.loadTalbetDriverImage(imageName);
                    break;
            }

            Bitmap bitmap = new Bitmap(stream);
            stream.Close();
            stream.Dispose();
            return bitmap;
        }

        public static Icon getDllIcon(string iconName, OEMType oemType)
        {
            Depot depot = new Depot();
            Stream stream;
            switch (oemType)
            {
                case OEMType.HUION:
                    stream = depot.loadHuionImage(iconName);
                    break;
                case OEMType.GAOMON:
                    stream = depot.loadGaomonImage(iconName);
                    break;
                default:
                    stream = depot.loadTalbetDriverImage(iconName);
                    break;
            }

            Icon icon = new Icon(stream);
            stream.Close();
            stream.Dispose();
            return icon;
        }

        public static Image getDllScaleImage(string imageName, Control v)
        {
            Stream stream = new Depot().loadImage(imageName);
            Console.WriteLine(imageName);
            Console.WriteLine(stream.ToString());
            Image original = Image.FromStream(stream);
            stream.Close();
            stream.Dispose();
            Bitmap bmp = new Bitmap(original);
            Bitmap bitmap = HuionRender.blowupImage(HuionRender.compressImageWithRate(bmp, v.Width, v.Height),
                DpiHelper.getInstance().XDpi);
            original.Dispose();
            if (bitmap == bmp)
                return (Image) bitmap;
            bmp.Dispose();
            return (Image) bitmap;
        }

        public static Image getScaleImage(string imagePath, Control v)
        {
            FileStream fileStream = File.Open(imagePath, FileMode.Open);
            Image original = Image.FromStream((Stream) fileStream);
            fileStream.Close();
            fileStream.Dispose();
            Bitmap bmp = new Bitmap(original);
            Bitmap bitmap = HuionRender.blowupImage(HuionRender.compressImageWithRate(bmp, v.Width, v.Height),
                DpiHelper.getInstance().XDpi);
            original.Dispose();
            if (bitmap == bmp)
                return (Image) bitmap;
            bmp.Dispose();
            return (Image) bitmap;
        }

        public static string GetIconPath(string devicetypeName, string extensionStr)
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "res\\" + devicetypeName + extensionStr;
        }
    }
}