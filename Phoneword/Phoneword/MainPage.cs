using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        private Entry _entryPhoneNumber;
        private Button _btnTranslate;
        private Button _btnCall;
        private string translatedNumber;

        public MainPage()
        {
            this.Padding = new Thickness(20, Device.OnPlatform<double>(40, 20, 20), 20, 20); //40 = iOS, 20 for Android and Windows

            

            
            

            StackLayout panel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Orientation = StackOrientation.Vertical
            };
            panel.Children.Add(new Label
            {
                Text = "Enter a phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });
            panel.Children.Add(_entryPhoneNumber = new Entry
            {
                Text = "1-855-XAMARIN"
            });
            panel.Children.Add(_btnTranslate = new Button
            {
                Text = "Translate"
            });
            panel.Children.Add(_btnCall = new Button
            {
                Text = "Call",
                IsEnabled = false
            });

            _btnTranslate.Clicked += OnTranslate;
            _btnCall.Clicked += OnCall;

            this.Content = panel;
        }

        private async void OnCall(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (await this.DisplayAlert(
                "Dial a number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"
                ))
            {
                //dial the number
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    dialer.Dial(translatedNumber);
                }
            };
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = _entryPhoneNumber.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            if(!String.IsNullOrWhiteSpace(translatedNumber))
            {
                _btnCall.IsEnabled = true;
                _btnCall.Text = "Call " + translatedNumber;
            }
            else
            {
                _btnCall.IsEnabled = false;
                _btnCall.Text = "Call";
            }

        }
    }
}
