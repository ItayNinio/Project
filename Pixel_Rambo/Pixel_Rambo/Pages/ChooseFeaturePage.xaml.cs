
using DataBaseProject;
using DataBaseProject.Models;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChooseFeaturePage : Page
        //דף מחסן לפריטים שבבעלות השחקן
    {
        private List<int> _ownProductsId = null;
        //רשימת הסקינים שבבעלות השחקן
        private List<Product> _allProducts = null;
        //רשימת כל המוצרים שבמשחק
        private List<Product> _chooseProducts = null;
        //רשימת המוצרים של השחקן

        public ChooseFeaturePage()
        {
            this.InitializeComponent();
        }
        //הפעולה מופעלת כאשר הדף נטען

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ownProductsId = Server.GetOwnProductId(GameManager.GameUser);
            _allProducts = GetProducts(); //הפעולה ממלא את רשימת המוצרים שבכל המשחק
           //רשימת הסקינים (לא מספריהם) שבבעלות השחקן
            _chooseProducts = new List<Product>();
            foreach(Product product in _allProducts)
            {
                if (IsExist(_ownProductsId, product.Id))
                {
                    _chooseProducts.Add(product);
                }
            }
            ShowProducts(); //הפעולה מראה את המוצרים של השחקן על המסך

        }
        //הפעולה מראה את המוצרים של השחקן על המסך

        private void ShowProducts()
        {
            Image image;
           foreach(Product product in _chooseProducts)
            {
                image = GetImage(product.Id);
                featureViewList.Items.Add(image);
            }
        }
        //הפעולה מחזירה את התמונה של המוצר לפי מספרו

        private Image GetImage(int id)
        {
            Image image = new Image();
            image.Width = 80;
            image.Height = 80;
            switch (id)
            {

                case 1:
                    image.Source = new BitmapImage( new Uri("ms-appx:///Imgs/vest.png")); break;
                case 2:
                    image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/boots.png")); break;
                case 3:
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rambo/special_bullet3.png")); break;
            }
            return image;
        }
        //הפעולה מחזירה את תמונת הפריט הרצוי לפי מספרו

        private bool IsExist(List<int> ownProductsId, int id)
        {
            if (ownProductsId == null)
            {
                return false;
            }
           foreach (int index in ownProductsId)
            {
                if (index == id)
                {
                    return true;
                }
            }
            return false;
        }
        //הפעולה בודקת אם המוצר קיים ברשימה של השחקן

        private List<Product> GetProducts()
        {
            _allProducts = new List<Product>();
            _allProducts.Add(new Product
            {
                Id = 1,
                Price = 0,

            }
             );
            _allProducts.Add(new Product
            {
                Id = 2,
                Price = 4,

            }
             );
            _allProducts.Add(new Product
            {
                Id = 3,
                Price = 10,

            }
             );
            return _allProducts;
        }
        // פעולה שמחזירה את רשימת כל המוצרים שקיימים במשחק (עם מחירים)
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
        //חזרה לדף התפריט
    }
}
