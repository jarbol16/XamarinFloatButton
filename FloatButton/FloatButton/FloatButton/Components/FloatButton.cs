using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloatButton
{
    public class FloatButton: ContentPage
    {
        private Grid grid_main;
        private AbsoluteLayout button_main;
        private int row = 0;
        private List<OPTION> optiones;
        public delegate void SelectButton(string Id);
        SelectButton select;
        public object viewPage;
        public bool seeOptions = true;
        public struct OPTION
        {
            public Label label;
            public AbsoluteLayout absoluteLayout;
        }


        public void Options(ref ContentView view)
        {
            if (seeOptions)
            {
                SeeOptions(ref view);
                seeOptions = false;
            }
            else
            {
                NotSeeOptions(ref view);
                seeOptions = true;
            }
        }

        public void SeeOptions(ref ContentView view)
        {
            view.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, false);
            view.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, false);
            grid_main.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true);
            grid_main.VerticalOptions = new LayoutOptions(LayoutAlignment.End, true);
            view.BackgroundColor = Color.FromHex("#A0000000");
            foreach (OPTION opt in optiones)
            {
                opt.absoluteLayout.IsVisible = true;
                opt.label.IsVisible = true;
            }
            view.Content = GetFloatButton();
        }

        public void NotSeeOptions(ref ContentView view)
        {
            view.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
            view.VerticalOptions = new LayoutOptions(LayoutAlignment.End, false);
            view.BackgroundColor = Color.Transparent;
            view.Content = null;
            foreach (OPTION opt in optiones)
            {
                opt.absoluteLayout.IsVisible = false;
                opt.label.IsVisible = false;
            }
            view.Content = GetFloatButton();
        }

        public FloatButton(ref ContentView contentView, string image_source,Color color, SelectButton Selection, object viewPage)
        {
            grid_main = new Grid();
            AbsoluteLayout.SetLayoutBounds(contentView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(contentView, AbsoluteLayoutFlags.All);
            contentView.Padding = new Thickness(0, 0, 30, 30);
            contentView.HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false);
            contentView.VerticalOptions = new LayoutOptions(LayoutAlignment.End, false);
            select = Selection;
            this.viewPage = viewPage;
            button_main = CreateButtonMain(image_source, color);
            optiones = new List<OPTION>();
        }
        public Grid GetFloatButton()
        {
            row = 0;
            grid_main.Children.Clear();
            foreach (OPTION option in optiones)
            {
                grid_main.Children.Add(option.label, 0, row);
                grid_main.Children.Add(option.absoluteLayout, 1, row);
                row++;
            }
            grid_main.Children.Add(button_main, 1, row);
            return grid_main;
        }

        public List<OPTION> GetOptions()
        {
            return optiones;
        }

        public AbsoluteLayout CreateButtonMain(string image_source,Color color)
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout
            {
                HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true),
            };
           Frame frame = new Frame
            {
                HeightRequest = 25,
                WidthRequest = 25,
                BackgroundColor = color,
                CornerRadius = 35,
                HasShadow = true,
                Content = new Image
                {
                    Source = image_source
                }
            };
            frame.GestureRecognizers.Add(SetEventClik(ref frame,"Main"));
            absoluteLayout.Children.Add(frame);
            return absoluteLayout; 
        }

        public TapGestureRecognizer SetEventClik(ref Frame frame, string id)
        {
            BindableProperty click = BindableProperty.Create("ID", typeof(string), (Type)viewPage, null);
            frame.SetValue(click, id);
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                string _id = (string)((Frame)s).GetValue(click);
                select(_id);
            };
            return tapGestureRecognizer;
        }

        public void AddOption(string image_source,Color color,string text)
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout
            {
                Padding = new Thickness(5, 0, 5, 10),
                HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true),
                IsVisible = false
            };
            Frame frame = new Frame
            {
                HeightRequest = 20,
                WidthRequest = 20,
                BackgroundColor = color,
                CornerRadius = 30,
                HasShadow = true,
                Content = new Image
                {
                    Source = image_source
                }
            };
            frame.GestureRecognizers.Add(SetEventClik(ref frame,text));
            absoluteLayout.Children.Add(frame);
            if(row == 0)
                grid_main.Children.Clear();
            OPTION pTION = new OPTION
            {
                label = TitleOption(text),
                absoluteLayout = absoluteLayout
            };
            optiones.Add(pTION);
        }

        private Label TitleOption(string text)
        {
            Label label = new Label
            {
                Text = text,
                TextColor = Color.White,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.End, false),
                VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                IsVisible = false
            };
            return label;
        }
    }
}
