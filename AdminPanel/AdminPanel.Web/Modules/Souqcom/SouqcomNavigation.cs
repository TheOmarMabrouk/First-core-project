using Serenity.Navigation;
using MyPages = AdminPanel.Souqcom.Pages;

[assembly: NavigationLink(int.MaxValue, "Souqcom/Category", typeof(MyPages.CategoryPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Souqcom/Cart", typeof(MyPages.CartPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Souqcom/Product Images", typeof(MyPages.ProductImagesPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Souqcom/Products", typeof(MyPages.ProductsPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Souqcom/Review", typeof(MyPages.ReviewPage), icon: null)]