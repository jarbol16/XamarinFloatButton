using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloatButton
{
    public partial class MainPage : ContentPage
    {
        FloatButton floatButton;
        public MainPage()
        {
            InitializeComponent();
            SetFloatButton();
        }

        public void SetFloatButton()
        {
            FloatButton.SelectButton select = Selecction;
            floatButton = new FloatButton(ref Float_Button, "house.png", Color.DarkCyan, select,typeof(MainPage));
            floatButton.AddOption("house.png", Color.GreenYellow, "New");
            floatButton.AddOption("house.png", Color.DarkOrange, "Search");
            Float_Button.Content = floatButton.GetFloatButton();
        }


        public void Selecction(string s)
        {
            floatButton.Options(ref Float_Button);
        }
    }
}
