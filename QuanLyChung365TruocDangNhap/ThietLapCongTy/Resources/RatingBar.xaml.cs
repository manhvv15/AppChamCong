using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Resources
{
    /// <summary>
    /// Interaction logic for RatingBar.xaml
    /// </summary>
    public partial class RatingBar : UserControl
    {
        public event EventHandler<int> RatingChanged;
        BrushConverter br = new BrushConverter();
        public int rating;
        public RatingBar()
        {
            InitializeComponent();
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            Button clickedStar = sender as Button;

            rating = int.Parse(clickedStar.Name.Replace("Star", ""));

            SetRating(rating);
            OnRatingChanged(rating);
        }

        public void SetRating(int rating)
        {
            for (int i = 1; i <= 5; i++)
            {
                Button star = FindName($"Star{i}") as Button;
                if (star != null)
                {
                    if (i <= rating)
                    {
                        star.Foreground = (Brush)br.ConvertFrom("#FFAB2E");
                    }
                    else
                    {
                        star.Foreground = (Brush)br.ConvertFrom("#AAA"); ;
                    }
                }
            }
        }

        protected virtual void OnRatingChanged(int rating)
        {
            RatingChanged?.Invoke(this, rating);
        }
    }
}
