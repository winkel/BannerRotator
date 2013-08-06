using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System.Web.UI.WebControls
{
	[DefaultProperty("ID")]
	[ToolboxData("<{0}:FlashControl runat=server></{0}:FlashControl>")]
	public class FlashControl : WebControl
	{
		#region Constants

		private const string classidDefault = "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
		private const string codebaseDefault = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0";
		private const string qualityDefault = "high";
		private const bool renderXhtmlValidDefault = false;
		private const string mimeTypeDefault = "application/x-shockwave-flash";
		private const string pluginspageDefault = "http://www.macromedia.com/go/getflashplayer";
		private const string wmodeDefault = "window";

		#endregion

		#region Fields

		private string classid = classidDefault;
		private string codebase = codebaseDefault;
		private string flashUrl;
		private string quality = qualityDefault;
		private bool renderXhtmlValid = renderXhtmlValidDefault;
		private string mimeType = mimeTypeDefault;
		private string pluginspage = pluginspageDefault;
		private string wmode = wmodeDefault;

		#endregion

		#region Properties

		[Browsable(true), Category("Behavior"), DefaultValue(classidDefault)]
		public string Classid
		{
			get { return classid; }
			set { classid = value; }
		}

		[Browsable(true), Category("Behavior"), DefaultValue(codebaseDefault)]
		public string Codebase
		{
			get { return codebase; }
			set { codebase = value; }
		}

		[Browsable(true), Category("Appearance"), DefaultValue("")]
		public string FlashUrl
		{
			get { return flashUrl; }
			set { flashUrl = value; }
		}

		[Browsable(true), Category("Appearance"), DefaultValue(qualityDefault)]
		public string Quality
		{
			get { return quality; }
			set { quality = value; }
		}

		[Browsable(true), Category("Appearance"), DefaultValue(renderXhtmlValidDefault)]
		public bool RenderXhtmlValid
		{
			get { return renderXhtmlValid; }
			set { renderXhtmlValid = value; }
		}

		[Browsable(true), Category("Behavior"), DefaultValue(mimeTypeDefault)]
		public string MimeType
		{
			get { return mimeType; }
			set { mimeType = value; }
		}

		[Browsable(true), Category("Behavior"), DefaultValue(pluginspageDefault)]
		public string Pluginspage
		{
			get { return pluginspage; }
			set { pluginspage = value; }
		}

		[Browsable(true), Category("Behavior"), DefaultValue(wmodeDefault)]
		public string WMode
		{
			get { return wmode; }
			set { wmode = value; }
		}

		#endregion

		#region Overriden Methods

		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		#endregion

		protected override void RenderContents(HtmlTextWriter output)
		{
			if (this.DesignMode)
			{
				output.RenderBeginTag(HtmlTextWriterTag.Span);
				output.Write(this.ClientID);
				output.RenderEndTag();
				return;
			}

			if (renderXhtmlValid)
			{
				RenderXHtmlValidHtml(output);
			}
			else
			{
				RenderLegacyHtml(output);
			}
		}

		protected virtual void RenderLegacyHtml(HtmlTextWriter output)
		{
			if (String.IsNullOrEmpty(flashUrl))
			{
				return;
			}

			int width = ((int)Width.Value);
			int height = ((int)Height.Value);
			bool nonZeroSize = width > 0 && height > 0;

			string flashUrlResolved = this.ResolveUrl(flashUrl);

			output.AddAttribute("classid", classid, true);
			output.AddAttribute("codebase", codebase, true);
			output.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);

			if (nonZeroSize)
			{
				output.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString());
				output.AddAttribute(HtmlTextWriterAttribute.Height, height.ToString());
			}

			output.RenderBeginTag(HtmlTextWriterTag.Object);

			addParamTag(output, "movie", flashUrlResolved, true);
			addParamTag(output, "wmode", wmode, false);

			output.Write("<embed");

			if (nonZeroSize)
			{
				output.WriteAttribute("width", width.ToString());
				output.WriteAttribute("height", height.ToString());
			}

			output.WriteAttribute("name", "movie");
			output.WriteAttribute("wmode", wmode);
			output.WriteAttribute("quality", Quality);
			output.WriteAttribute("type", MimeType, true);
			output.WriteAttribute("pluginspage", Pluginspage, true);
			output.WriteAttribute("src", flashUrlResolved, true);
			output.Write(">");

			output.RenderEndTag();
		}

		private void addParamTag(HtmlTextWriter output, string name, string value, bool fEncodeValue)
		{
			output.Write("<param");
			output.WriteAttribute("name", name);
			output.WriteAttribute("value", value, fEncodeValue);
			output.Write(">");
		}

		protected virtual void RenderXHtmlValidHtml(HtmlTextWriter output)
		{
			if (String.IsNullOrEmpty(flashUrl))
			{
				return;
			}
		}
	}
}