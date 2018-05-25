using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ConwaysGame.Cells;

namespace ConwaysGame
{
    [Activity(Label = "ConwaysGame", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            var arr = SetupView();

            ExecuteConway(arr);
        }

        public Cell[,] SetupView()
        {
            Cell[,] array = new Cell[10,10];
            foreach(var cel in array)
            {
                cel.Live = RandLive();
            }
            return array;
        }

        public bool RandLive()
        {
            var random = new Random();
            return random.Next(1) == 0;
        }

        public void ExecuteConway(Cell[,] arr)
        {
            for(var i = 0; i < 1000; i++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        SetCellState(arr, x, y);
                    }
                }
                //do **** on the screen with current array state
            }
        }

        public void SetCellState(Cell[,] arr, int x, int y) {
            var count = 0;
            var cell = arr[x, y];
            if (arr[x - 1, y].Live)
                count++;
            if (arr[x - 1, y - 1].Live)
                count++;
            if (arr[x, y - 1].Live)
                count++;
            if (arr[x + 1, y - 1].Live)
                count++;
            if (arr[x + 1, y].Live)
                count++;
            if (arr[x + 1, y + 1].Live)
                count++;
            if (arr[x, y + 1].Live)
                count++;
            if (arr[x - 1, y = 1].Live)
                count++;

            if (cell.Live)
            {
                if (count < 2)
                    cell.ToggleCell();
                else if (count > 4)
                    cell.ToggleCell();
            }
            else
            {
                if (count == 3)
                    cell.ToggleCell();
            }
        }
    }
}

