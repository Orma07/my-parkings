using System;
using System.Diagnostics;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
namespace iconview.Forms
{
    /// <summary>
    /// Author: Amro Abd Elgawwad
    /// </summary>
    public class IconView : SKCanvasView
    {
     

        #region LineColor
        public static readonly BindableProperty LineColorProperty =
        BindableProperty.Create("LineColor",
            typeof(Color),
            typeof(IconView),
            Color.Black,
            propertyChanged: OnLineChange);



        private static void OnLineChange(BindableObject bindable, object oldValue, object newValue)
        {
            var current = (IconView)bindable;
            current.InvalidateSurface();


        }

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        #endregion

        #region LineWidth
        public static readonly BindableProperty LineWidthProperty =
        BindableProperty.Create("LineWidth",
            typeof(float),
            typeof(IconView),
            1f,
            propertyChanged: OnLineChange);


        public float LineWidth
        {
            get { return (float)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }
        #endregion

        #region Source
        public static readonly BindableProperty SourceProperty =
        BindableProperty.Create("Source",
            typeof(string),
            typeof(IconView),
            "",
            propertyChanged: OnLineChange);


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        #endregion

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            var strokeWidth = (float)(info.Height / Height) * LineWidth;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Square,
                Color = LineColor.ToSKColor(),
                StrokeWidth = strokeWidth,
                IsDither = true,
                IsAntialias = true
            };

            var svg = new SkiaSharp.Extended.Svg.SKSvg();

            var type = typeof(IconView).GetTypeInfo();
            var assembly = type.Assembly;
            canvas.Translate(info.Width / 2f, info.Height / 2f);


            try
            {
                using (paint)
                {
                    using (var s = assembly.GetManifestResourceStream($"icon-view.Forms.Resources.{Source}.svg"))
                        svg.Load(s);
                    var svgSize = svg.Picture.CullRect;
                    float svgMax = Math.Max(svgSize.Width, svgSize.Height);
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(LineColor.ToSKColor(), SKBlendMode.SrcIn);

                    // calculate the scaling need to fit
                    float canvasMin = Math.Min(info.Width, info.Height);
                    float scale = (canvasMin / svgMax);
                    var matrix = SKMatrix.MakeScale(scale, scale);
                    matrix.TransX = -scale * svg.Picture.CullRect.Width / 2;

                    matrix.TransY = -(scale * svg.Picture.CullRect.Height / 2);

                    // draw the svg
                    canvas.DrawPicture(svg.Picture, ref matrix, paint);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"IconView: couldn't draw svg ex - {ex}");
            }

        }
      
    }

    
}
