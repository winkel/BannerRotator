BannerRotator
=============

http://anyrest.wordpress.com/2010/02/24/extending-the-adrotator-to-use-flash-banners/


example of a data source

<?xml version="1.0" encoding="utf-8" ?>
<Advertisements xmlns="http://schemas.microsoft.com/AspNet/AdRotator-Schedule-File">
 <Ad>
 <ImageUrl>~/banners/1Test.swf</ImageUrl>
 <NavigateUrl>javascript:window.open('/makebooking.aspx','Book','height=600,width=800,scrollbars,resizable');void(0);</NavigateUrl>
 <AlternateText>Book a Course</AlternateText>
 <Impressions>100</Impressions>
 <Width>229</Width>
 <Height>600</Height>
 </Ad>
 <Ad>
 <ImageUrl>~/banners/2Test.gif</ImageUrl>
 <NavigateUrl>javascript:window.open('/shop.aspx','Shop','');void(0);</NavigateUrl>
 <AlternateText>Shop Now</AlternateText>
 <Impressions>100</Impressions>
 </Ad>

</Advertisements>