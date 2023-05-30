using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using System;
using Android.Widget;
using Android.Graphics;
using Android.Content;
//using SQLite;

namespace Android2048
{
    [Activity(Label = "2048", Icon = "@drawable/picture2048", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //SQLiteConnection connection;

        ImageView ivPicture;
        EditText etUsername;
        TextView tvError;
        Button btnStart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //var db = new SQLiteConnection("C:\\Users\\test0\\OneDrive\\שולחן העבודה\\personal\\Android2048\\users.db");

            InitViews();
        }

        private void InitViews()
        {
            ivPicture = FindViewById<ImageView>(Resource.Id.ivPicture);
            etUsername = FindViewById<EditText>(Resource.Id.etUsername);
            tvError = FindViewById<TextView>(Resource.Id.tvError);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);

            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.openningPicture);
            ivPicture.SetImageBitmap(bitmap);

            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (etUsername.Text == string.Empty)
            {
                tvError.Text = "Please fill username";
            }
            else
            {
                tvError.Text = string.Empty;
                Intent intent = new Intent(this, typeof(GameActivity));
                intent.PutExtra("fUsername", etUsername.Text);
                StartActivity(intent);
            }

        }
        //private bool DoesUserExist()
        //{
        //    string query = @"SELECT * FROM USERS WHERE USERNAME = '" + etUsername.Text + "'";
        //    return false;
        //}
        private void BtnSignup_Click(object sender, EventArgs e)
        {

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}