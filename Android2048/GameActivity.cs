using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android2048
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        ImageView[,] tiles;
        TextView txtHelloMsg, txtScore, txtBestScore;
        Button btnRestart;

        Board board;
        Score score;
        Bitmap[] pictures;

        enum pictureCode { Pic0, Pic2, Pic4, Pic8, Pic16, Pic32, Pic64, Pic128, Pic256, Pic512, Pic1024, Pic2048 };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);
            InitVIews();
        }

        private void InitVIews()
        {
            board = new Board();
            score = new Score();

            txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            txtBestScore = FindViewById<TextView>(Resource.Id.txtBestScore);
            txtHelloMsg = FindViewById<TextView>(Resource.Id.txtHelloMsg);
            txtHelloMsg.Text = "Hello " + Intent.GetStringExtra("fUsername") + "!";

            tiles = new ImageView[4, 4];
            InitBoard();
            InitPictures();

            btnRestart = FindViewById<Button>(Resource.Id.btnRestart);
            btnRestart.Click += BtnRestart_Click;
            StartGame();
        }

        private void InitPictures()
        {
            pictures = new Bitmap[12];
            pictures[0] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture0);
            pictures[1] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture2);
            pictures[2] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture4);
            pictures[3] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture8);
            pictures[4] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture16);
            pictures[5] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture32);
            pictures[6] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture64);
            pictures[7] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture128);
            pictures[8] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture256);
            pictures[9] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture512);
            pictures[10] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture1024);
            pictures[11] = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.picture2048);
        }

        private void StartGame()
        {
            board.ResetBoard();
            score.ResetScore();
            ChangeColors();
            ChangeScores();
        }

        private void ChangeScores()
        {
            txtScore.Text = score.GetScore().ToString();
            txtBestScore.Text = score.GetBestScore().ToString();
        }

        private void ChangeColors()
        {
            Tiles[,] b = board.gameBoard;
            int size = board.GetBoardSize();
            Bitmap pic;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    switch (b[i, j].GetValue())
                    {
                        default:
                            pic = pictures[(int)pictureCode.Pic0];
                            break;
                        case 2:
                            pic = pictures[(int)pictureCode.Pic2];
                            break;
                        case 4:
                            pic = pictures[(int)pictureCode.Pic4];
                            break;
                        case 8:
                            pic = pictures[(int)pictureCode.Pic8];
                            break;
                        case 16:
                            pic = pictures[(int)pictureCode.Pic16];
                            break;
                        case 32:
                            pic = pictures[(int)pictureCode.Pic32];
                            break;
                        case 64:
                            pic = pictures[(int)pictureCode.Pic64];
                            break;
                        case 128:
                            pic = pictures[(int)pictureCode.Pic128];
                            break;
                        case 256:
                            pic = pictures[(int)pictureCode.Pic256];
                            break;
                        case 512:
                            pic = pictures[(int)pictureCode.Pic512];
                            break;
                        case 1024:
                            pic = pictures[(int)pictureCode.Pic1024];
                            break;
                        case 2048:
                            pic = pictures[(int)pictureCode.Pic2048];
                            break;
                    }
                    tiles[i, j].SetImageBitmap(pic);
                }
            }
            }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void InitBoard()
        {
            tiles[0, 0] = FindViewById<ImageView>(Resource.Id.ivTile0);
            tiles[0, 1] = FindViewById<ImageView>(Resource.Id.ivTile1);
            tiles[0, 2] = FindViewById<ImageView>(Resource.Id.ivTile2);
            tiles[0, 3] = FindViewById<ImageView>(Resource.Id.ivTile3);
            tiles[1, 0] = FindViewById<ImageView>(Resource.Id.ivTile4);
            tiles[1, 1] = FindViewById<ImageView>(Resource.Id.ivTile5);
            tiles[1, 2] = FindViewById<ImageView>(Resource.Id.ivTile6);
            tiles[1, 3] = FindViewById<ImageView>(Resource.Id.ivTile7);
            tiles[2, 0] = FindViewById<ImageView>(Resource.Id.ivTile8);
            tiles[2, 1] = FindViewById<ImageView>(Resource.Id.ivTile9);
            tiles[2, 2] = FindViewById<ImageView>(Resource.Id.ivTile10);
            tiles[2, 3] = FindViewById<ImageView>(Resource.Id.ivTile11);
            tiles[3, 0] = FindViewById<ImageView>(Resource.Id.ivTile12);
            tiles[3, 1] = FindViewById<ImageView>(Resource.Id.ivTile13);
            tiles[3, 2] = FindViewById<ImageView>(Resource.Id.ivTile14);
            tiles[3, 3] = FindViewById<ImageView>(Resource.Id.ivTile15);
        }
    }

}