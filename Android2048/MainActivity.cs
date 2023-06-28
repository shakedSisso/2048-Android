using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;
using System;
using System.Text.RegularExpressions;

namespace Android2048
{
    [Activity(Label = "2048", Icon = "@drawable/picture2048", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //SQLiteConnection connection;

        ImageView ivPicture;
        EditText etUsername, etPassword, etName, etPass;
        TextView tvError, tvPassError;
        Button btnStart, btnSignup, btnCutsomSignup;

        Android.App.Dialog custom_d;

        string dbPath;
        SQLiteConnection db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            this.dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "2048Users.db");
            this.db = new SQLiteConnection(this.dbPath);

            db.CreateTable<User>();
            InitViews();
        }

        private void InitViews()
        {
            ivPicture = FindViewById<ImageView>(Resource.Id.ivPicture);
            etUsername = FindViewById<EditText>(Resource.Id.etUsername);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            tvError = FindViewById<TextView>(Resource.Id.tvError);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnSignup = FindViewById<Button>(Resource.Id.btnSignup);

            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.openningPicture);
            ivPicture.SetImageBitmap(bitmap);

            btnStart.Click += BtnStart_Click;
            btnSignup.Click += BtnSignup_Click;
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                this.db.Close();
                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (etUsername.Text == string.Empty || etPassword.Text == string.Empty)
            {
                tvError.Text = "Please fill all fields";
            }
            else
            {
                if (DoesUserExist(etUsername.Text) && DoesPasswordMatch(etUsername.Text, etPassword.Text))
                {
                    tvError.Text = string.Empty;
                    Intent intent = new Intent(this, typeof(GameActivity));
                    intent.PutExtra("fUsername", etUsername.Text);
                    intent.PutExtra("dbPath", this.dbPath);
                    db.Close();
                    StartActivity(intent);
                }
            }

        }

        private bool DoesPasswordMatch(string username, string password)
        {
            User user = this.db.Table<User>().FirstOrDefault(u => u.Name == username);
            if (user != null)
            {
                if (etPassword.Text != user.Password)
                {
                    tvError.Text = "Password is incorrect";
                    return false;
                }

            }
            return true;
        }

        private bool DoesUserExist(string username)
        {
            User user = this.db.Table<User>().FirstOrDefault(u => u.Name == username);
            if (user == null)
            {
                tvError.Text = "User is not registered";
                return false;
            }
            return true;
        }
        private void BtnSignup_Click(object sender, EventArgs e)
        {
            custom_d = new Android.App.Dialog(this);
            custom_d.SetContentView(Resource.Layout.signup_activity);
            custom_d.SetTitle("Signup");
            custom_d.SetCancelable(true);
            etName = custom_d.FindViewById<EditText>(Resource.Id.etName);
            etPass = custom_d.FindViewById<EditText>(Resource.Id.etPass);
            tvPassError = custom_d.FindViewById<TextView>(Resource.Id.tvPassError);
            btnCutsomSignup = custom_d.FindViewById<Button>(Resource.Id.btnCutsomSignup);
            btnCutsomSignup.Click += BtnCutsomSignup_Click;
            custom_d.Show();
        }

        private void BtnCutsomSignup_Click(object sender, EventArgs e)
        {
            if (DoesUserExist(etName.Text))
            {
                tvPassError.Text = "User already exists";
            }
            else if (!IsPasswordValid(etPass.Text))
            {
                tvPassError.Text = "Password is must be:\n-At least 8 characters long\n-1 upper and 1 lower case letter\n-1 special character\n-At least 1 number";
            }
            else
            {
                User user = new User(etName.Text, etPass.Text);
                db.Insert(user);
                custom_d.Dismiss();
                Intent intent = new Intent(this, typeof(GameActivity));
                intent.PutExtra("fUsername", etName.Text);
                intent.PutExtra("dbPath", this.dbPath);
                db.Close();
                StartActivity(intent);
            }
        }

        private bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$");
            Match match = regex.Match(password);

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}