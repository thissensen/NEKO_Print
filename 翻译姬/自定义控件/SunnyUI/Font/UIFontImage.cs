/******************************************************************************
 * SunnyUI ��Դ�ؼ��⡢������⡢��չ��⡢��ҳ�濪����ܡ�
 * CopyRight (C) 2012-2023 ShenYongHua(������).
 * QQȺ��56829229 QQ��17612584 EMail��SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * �����ʹ�ô˴��룬�뱣����˵����
 ******************************************************************************
 * �ļ�����: UFontImage.cs
 * �ļ�˵��: ����ͼƬ������
 * ��ǰ�汾: V3.1
 * ��������: 2020-01-01
 *
 * 2020-01-01: V2.2.0 �����ļ�˵��
 * 2020-05-21: V2.2.5 ��������Դ�ļ��м������壬�������Ϊ�ļ���
 *                    ��л����Ǳ� https://gitee.com/maikebing
 * 2021-06-15: V3.0.4 ����FontAwesomeV5������ͼ�꣬�ع�����
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// ����ͼƬ������
    /// </summary>
    public static class FontImageHelper
    {
        /// <summary>
        /// AwesomeFont
        /// </summary>
        public static readonly FontImages FontAwesomeV4;

        /// <summary>
        /// ElegantFont
        /// </summary>
        public static readonly FontImages ElegantIcons;

        /// <summary>
        /// FontAwesomeV5Brands
        /// </summary>
        public static readonly FontImages FontAwesomeV5Brands;

        /// <summary>
        /// FontAwesomeV5Regular
        /// </summary>
        public static readonly FontImages FontAwesomeV5Regular;

        /// <summary>
        /// FontAwesomeV5Solid
        /// </summary>
        public static readonly FontImages FontAwesomeV5Solid;

        //public const int FontAwesomeV4Count = 786; 
        //public const int ElegantIconsCount = 360;
        //public const int FontAwesomeV5RegularCount = 151;
        //public const int FontAwesomeV5SolidCount = 1001;
        //public const int FontAwesomeV5BrandsCount = 457;
        //public const int LineAwesomeRegularCount = 151;
        //public const int LineAwesomeSolidCount = 960;
        //public const int LineAwesomeBrandsCount = 433;

        /// <summary>
        /// ���캯��
        /// </summary>
        static FontImageHelper()
        {
            FontAwesomeV4 = new FontImages(���뼧.Properties.Resources.FontAwesome);
            ElegantIcons = new FontImages(���뼧.Properties.Resources.ElegantIcons);
            FontAwesomeV5Brands = new FontImages(���뼧.Properties.Resources.fa_brands_400);
            FontAwesomeV5Regular = new FontImages(���뼧.Properties.Resources.fa_regular_400);
            FontAwesomeV5Solid = new FontImages(���뼧.Properties.Resources.fa_solid_900);

        }

        /// <summary>
        /// ��ȡ�����С
        /// </summary>
        /// <param name="graphics">GDI��ͼ</param>
        /// <param name="symbol">�ַ�</param>
        /// <param name="symbolSize">��С</param>
        /// <returns>�����С</returns>
        public static SizeF GetFontImageSize(this Graphics graphics, int symbol, int symbolSize)
        {
            Font font = GetFont(symbol, symbolSize);
            if (font == null)
            {
                return new SizeF(0, 0);
            }

            return graphics.MeasureString(char.ConvertFromUtf32(symbol), font);
        }

        private static UISymbolType GetSymbolType(int symbol)
        {
            return (UISymbolType)symbol.Div(100000);
        }

        private static int GetSymbolValue(int symbol)
        {
            return symbol.Mod(100000);
        }

        /// <summary>
        /// ��������ͼƬ
        /// </summary>
        /// <param name="graphics">GDI��ͼ</param>
        /// <param name="symbol">�ַ�</param>
        /// <param name="symbolSize">��С</param>
        /// <param name="color">��ɫ</param>
        /// <param name="rect">����</param>
        /// <param name="xOffset">����ƫ��</param>
        /// <param name="yOffSet">����ƫ��</param>
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color,
            RectangleF rect, int xOffset = 0, int yOffSet = 0)
        {
            SizeF sf = graphics.GetFontImageSize(symbol, symbolSize);
            graphics.DrawFontImage(symbol, symbolSize, color, rect.Left + ((rect.Width - sf.Width) / 2.0f).RoundEx(),
                rect.Top + ((rect.Height - sf.Height) / 2.0f).RoundEx(), xOffset, yOffSet);
        }

        /// <summary>
        /// ��������ͼƬ
        /// </summary>
        /// <param name="graphics">GDI��ͼ</param>
        /// <param name="symbol">�ַ�</param>
        /// <param name="symbolSize">��С</param>
        /// <param name="color">��ɫ</param>
        /// <param name="left">��</param>
        /// <param name="top">��</param>
        /// <param name="xOffset">����ƫ��</param>
        /// <param name="yOffSet">����ƫ��</param>
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color,
            float left, float top, int xOffset = 0, int yOffSet = 0)
        {
            //����
            Font font = GetFont(symbol, symbolSize);
            if (font == null)
            {
                return;
            }

            var symbolValue = GetSymbolValue(symbol);
            string text = char.ConvertFromUtf32(symbolValue);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.DrawString(text, font, color, left + xOffset, top + yOffSet);
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.InterpolationMode = InterpolationMode.Default;
        }

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="symbol">�ַ�</param>
        /// <param name="size">��С</param>
        /// <param name="color">��ɫ</param>
        /// <returns>ͼƬ</returns>
        public static Image CreateImage(int symbol, int size, Color color)
        {
            Bitmap image = new Bitmap(size, size);

            using (Graphics g = image.Graphics())
            {
                SizeF sf = g.GetFontImageSize(symbol, size);
                g.DrawFontImage(symbol, size, color, (image.Width - sf.Width) / 2.0f, (image.Height - sf.Height) / 2.0f);
            }

            return image;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="symbol">�ַ�</param>
        /// <param name="imageSize">��С</param>
        /// <returns>����</returns>
        public static Font GetFont(int symbol, int imageSize)
        {
            var symbolType = GetSymbolType(symbol);
            var symbolValue = GetSymbolValue(symbol);
            switch (symbolType)
            {
                case UISymbolType.FontAwesomeV4:
                    if (symbol > 0xF000)
                        return FontAwesomeV4.GetFont(symbolValue, imageSize);
                    else
                        return ElegantIcons.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Brands:
                    return FontAwesomeV5Brands.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Regular:
                    return FontAwesomeV5Regular.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Solid:
                    return FontAwesomeV5Solid.GetFont(symbolValue, imageSize);
                default:
                    if (symbol > 0xF000)
                        return FontAwesomeV4.GetFont(symbolValue, imageSize);
                    else
                        return ElegantIcons.GetFont(symbolValue, imageSize);
            }
        }
    }

    /// <summary>
    /// ����ͼ��ͼƬ
    /// </summary>
    public class FontImages
    {
        private readonly PrivateFontCollection ImageFont;
        private readonly Dictionary<int, Font> Fonts = new Dictionary<int, Font>();
        private const int MinFontSize = 8;
        private const int MaxFontSize = 88;
        private readonly IntPtr memoryFont = IntPtr.Zero;

        public FontImages(byte[] buffer)
        {
            ImageFont = new PrivateFontCollection();
            memoryFont = Marshal.AllocCoTaskMem(buffer.Length);
            Marshal.Copy(buffer, 0, memoryFont, buffer.Length);
            ImageFont.AddMemoryFont(memoryFont, buffer.Length);
            Loaded = true;
            LoadDictionary();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="filename">�����ļ���</param>
        public FontImages(string filename)
        {
            if (File.Exists(filename))
            {
                ImageFont = new PrivateFontCollection();
                ImageFont.AddFontFile(filename);
                Loaded = true;
                LoadDictionary();
            }
        }

        /// <summary>
        /// ���������ɱ�־
        /// </summary>
        public bool Loaded
        {
            get;
        }

        private void LoadDictionary()
        {
            int size = MinFontSize;
            for (int i = 0; i <= MaxFontSize - MinFontSize; i++)
            {
                Fonts.Add(size, GetFont(size));
                size += 1;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~FontImages()
        {
            foreach (var font in Fonts.Values)
            {
                font.Dispose();
            }

            if (memoryFont != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(memoryFont);
            }

            Fonts.Clear();
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="iconText">ͼ��</param>
        /// <param name="imageSize">ͼ���С</param>
        /// <returns>����</returns>
        public Font GetFont(int iconText, int imageSize)
        {
            int item = GetFontSize(iconText, imageSize);
            if (Fonts.ContainsKey(item))
            {
                return Fonts[GetFontSize(iconText, imageSize)];
            }

            return null;
        }

        /// <summary>
        /// ��ȡ�����С
        /// </summary>
        /// <param name="symbol">ͼ��</param>
        /// <param name="imageSize">ͼ���С</param>
        /// <returns>�����С</returns>
        public int GetFontSize(int symbol, int imageSize)
        {
            using (Bitmap bitmap = new Bitmap(48, 48))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                return BinarySearch(graphics, MinFontSize, MaxFontSize, symbol, imageSize);
            }
        }

        public int BinarySearch(Graphics graphics, int low, int high, int symbol, int imageSize)
        {
            int mid = (low + high) / 2;
            Font font = Fonts[mid];
            SizeF sf = GetIconSize(symbol, graphics, font);
            if (low >= high)
            {
                return mid;
            }

            if (sf.Width < imageSize && sf.Height < imageSize)
            {
                return BinarySearch(graphics, mid + 1, high, symbol, imageSize);
            }

            return BinarySearch(graphics, low, mid - 1, symbol, imageSize);
        }

        private Size GetIconSize(int iconText, Graphics graphics, Font font)
        {
            string text = char.ConvertFromUtf32(iconText);
            return graphics.MeasureString(text, font).ToSize();
        }

        public Icon ToIcon(Bitmap srcBitmap, int size)
        {
            if (srcBitmap == null)
            {
                throw new ArgumentNullException(nameof(srcBitmap));
            }

            Icon icon;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new Bitmap(srcBitmap, new Size(size, size)).Save(memoryStream, ImageFormat.Png);
                Stream stream = new MemoryStream();
                BinaryWriter binaryWriter = new BinaryWriter(stream);
                if (stream.Length <= 0L)
                {
                    return null;
                }

                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)0);
                binaryWriter.Write((short)32);
                binaryWriter.Write((int)memoryStream.Length);
                binaryWriter.Write(22);
                binaryWriter.Write(memoryStream.ToArray());
                binaryWriter.Flush();
                binaryWriter.Seek(0, SeekOrigin.Begin);
                icon = new Icon(stream);
                stream.Dispose();
            }

            return icon;
        }

        private Font GetFont(float size)
        {
            return Loaded ? new Font(ImageFont.Families[0], size, FontStyle.Regular, GraphicsUnit.Point) : null;
        }
    }
}