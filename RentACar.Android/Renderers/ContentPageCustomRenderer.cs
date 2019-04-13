using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using RentACar.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Graphics.Bitmap;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageCustomRenderer))]
namespace RentACar.Droid.Renderers
{
    public class ContentPageCustomRenderer : PageRenderer
    {
        public ContentPageCustomRenderer(Context context) : base(context)
        {
            this.activity = context as Activity;



            if (Element == null || view == null)
            {
                return;
            }
        }

        global::Android.Views.View view;

        Activity activity;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            ContentPage page = (ContentPage)Element;


            if (changed && !string.IsNullOrEmpty(page.BackgroundImage))
            {
                int id = (int)typeof(Resource.Drawable).GetField(page.BackgroundImage).GetValue(null);
                Bitmap originalImage = BitmapFactory.DecodeResource(Forms.Context.Resources, id);




                Bitmap background = Bitmap.CreateBitmap((int)this.Width, (int)this.Height, Config.Argb8888);

                float originalWidth = originalImage.Width;
                float originalHeight = originalImage.Height;

                Canvas canvas = new Canvas(background);

                float scaleX = this.Width / originalWidth;
                float scaleY = this.Height / originalHeight;
                float scale = Math.Max(scaleX, scaleY);

                float xTranslation = (this.Width - originalWidth * scale) / 2.0f;
                float yTranslation = (this.Height - originalHeight * scale) / 2.0f;

                Matrix transformation = new Matrix();
                transformation.PostTranslate(xTranslation, yTranslation);
                transformation.PreScale(scale, scale);

                Paint paint = new Paint();
                //paint.SetFilterBitmap(true);

                canvas.DrawBitmap(originalImage, transformation, paint);

                Background = null;
                SetBackgroundDrawable(new BitmapDrawable(background));
            }
        }
    }
}