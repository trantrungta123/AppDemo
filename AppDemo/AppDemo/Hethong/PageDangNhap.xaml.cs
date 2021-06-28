using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using YViet_EMR.Model;
namespace AppDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageDangNhap : ContentPage
	{
       // View pageContent;
		public PageDangNhap ()
		{
			InitializeComponent ();
            //NavigationPage.SetHasBackButton(this, false);
           

           
           
        }
        protected override  void OnAppearing()
        {
            base.OnAppearing();
            
        }
        #region Progress
        View viewContent;
        public void ShowProgressDisplay()
        {
            viewContent = Content;
            this.IsEnabled = false;
            var content = Content;

            var grid = new Grid();
            grid.Children.Add(content);
            var gridProgress = new Grid { BackgroundColor = Color.FromHex("#64FFE0B2"), Padding = new Thickness(50) };
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridProgress.SetBinding(VisualElement.IsVisibleProperty, "IsWorking");
            var activity = new ActivityIndicator
            {
                IsEnabled = true,
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsRunning = true
            };
            gridProgress.Children.Add(activity, 0, 1);
            grid.Children.Add(gridProgress);
            Content = grid;

        }
        void CloseProgressDisplay()
        {
            if (viewContent != null) Content = viewContent;
            viewContent = null;
            this.IsEnabled = true;
        }
        #endregion
       

          private async void DangNhap_Clicked(object sender, EventArgs e)
        {
            // CrossLocalNotifications.Current.Show("DrAI", "Hello");

            //ShowImage();
            //return;
            // AddProgressDisplay();
            ShowProgressDisplay();
            TData ob = new TData();
            ob.Ten1 = entry_id.Text;
            ob.Ten2 = entry_pass.Text;
            string url = MainModel.BaseURL + "api/v2/DangNhap/LoginForPatient";
            var kq = MainModel. PostDataAsyncOb<TResult>(url, ob);
            object ketqua = await kq;
            bool dangnhap = false;
            if (ketqua != null)
            {
                TResult t = (TResult)ketqua;
                dangnhap = t.ThanhCong;
            }

            //var v = MainModel.BenhNhanDangNhap(entry_id.Text, entry_pass.Text);
          
            //bool dangnhap = await v;
            if (dangnhap)
            {

                var tb = DisplayAlert("Alert", "Kết quả", "Thanh cong");
                await tb;
                CloseProgressDisplay();
                //await saveGiaoDien;
                //var showHome = Navigation.PushAsync(new PageHome(HubsHelperKeys.KeysHT_GiaoDien.ToList()));

                //await showHome;

            }
            else {
                var tb = DisplayAlert("Alert", "Kết quả", "Thất bại");
                await tb;
                CloseProgressDisplay();
               // Content = pageContent;
            }

            

        }

      

       }
}