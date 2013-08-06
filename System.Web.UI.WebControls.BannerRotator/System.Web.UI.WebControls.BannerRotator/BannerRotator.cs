using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Web.UI.WebControls
{
	[DefaultProperty("AdvertisementFile")]
	[ToolboxData("<{0}:BannerRotator runat=server></{0}:BannerRotator>")]
	public class BannerRotator : System.Web.UI.WebControls.AdRotator
	{
		#region Constants

		private const string wmodeDefault = "opaque";

		#endregion

		#region Fields

		private AdCreatedEventArgs selectedAdvertArgs;
		private string widthKeyPropertyName = "Width";
		private string heightKeyPropertyName = "Height";
		private string flashFileExtension = ".swf";
		private bool? isFlashBanner = null;
		private string navigateUrlBase = null;
		private string wmode = wmodeDefault;

		#endregion

		#region Properties

		[Browsable(true), Category("Behavior"), DefaultValue("")]
		public string NavigateUrlBase
		{
			get { return navigateUrlBase; }
			set { navigateUrlBase = value; }
		}

		protected bool IsFlashBanner
		{
			get
			{
				if (isFlashBanner != null) { return (bool)isFlashBanner; }

				isFlashBanner = false;

				if (selectedAdvertArgs == null) { return (bool)isFlashBanner; }

				if (selectedAdvertArgs.ImageUrl == null) { return (bool)isFlashBanner; }

				if (selectedAdvertArgs.ImageUrl.EndsWith(flashFileExtension, true, CultureInfo.InvariantCulture))
				{
					isFlashBanner = true;
					return true;
				}

				return (bool)isFlashBanner;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(wmodeDefault)]
		public string WMode
		{
			get { return wmode; }
			set { wmode = value; }
		}

		#endregion

		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				base.Render(writer);
				return;
			}

			if (IsFlashBanner)
			{
				RenderFlashBanner(writer);
				return;
			}

			base.Render(writer);
		}

		protected override void OnAdCreated(AdCreatedEventArgs e)
		{
			base.OnAdCreated(e);
			selectedAdvertArgs = e;
			ResolveTrackingUrl();
		}

		private void ResolveTrackingUrl()
		{
			if (String.IsNullOrEmpty(navigateUrlBase)) { return; }

			if (selectedAdvertArgs == null) { return; }

			if (String.IsNullOrEmpty(selectedAdvertArgs.NavigateUrl)) { return; }

			if (IsFlashBanner) { return; }

			selectedAdvertArgs.NavigateUrl = String.Format(navigateUrlBase, HttpContext.Current.Server.UrlEncode(selectedAdvertArgs.NavigateUrl));
		}

		private void RenderFlashBanner(HtmlTextWriter writer)
		{
			if (selectedAdvertArgs == null) { return; }

			if (String.IsNullOrEmpty(selectedAdvertArgs.ImageUrl)) { return; }

			FlashControl flash = new FlashControl();
			flash.FlashUrl = selectedAdvertArgs.ImageUrl;

			if (!String.IsNullOrEmpty(wmode))
			{
				flash.WMode = wmode;
			}

			if (!String.IsNullOrEmpty(this.ID))
			{
				flash.ID = this.ClientID;
			}

			if (!this.Enabled)
			{
				flash.Enabled = false;
			}

			if (!SetDimensions(flash))
			{
				flash.Width = this.Width;
				flash.Height = this.Height;
			}

			flash.RenderControl(writer);
		}

		private bool SetDimensions(FlashControl flash)
		{
			if (selectedAdvertArgs.AdProperties == null) { return false; }

			string widthProperty = (string)selectedAdvertArgs.AdProperties[widthKeyPropertyName];
			string heightProperty = (string)selectedAdvertArgs.AdProperties[heightKeyPropertyName];

			int width;
			int height;

			if (!int.TryParse(widthProperty, out width)) { return false; }
			if (!int.TryParse(heightProperty, out height)) { return false; }

			flash.Width = Math.Abs(width);
			flash.Height = Math.Abs(height);

			return true;
		}

	}
}