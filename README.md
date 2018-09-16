# ShopApp
Practicing with ASP.NET Core 2.2 new features + improving Angular 6 skills. Also the app containing html parser.

# Important!
```sh
update-database
```
You must `update-database` in Package Manager Console before starting IIS. It creates the database using migrations.


***

# Tests

## C#
XUnitTests project. To run tests click `Test`->`Run`->`All tests` in Visual Studio. There's 12 tests for ItemsController and HtmlParser service.

## Angular
Tests in process. To run them open cmd in `ClientApp` directory and type:
```sh
ng test
```
There's only 7 simple tests.

***

# Created for
>The app was created for parsing internet-shop and output items main info.
>Also you can view a statistics of price changing for each item.
>EF Core was used.
>It's wasn't made for all internet shops, specifically for "hotline".
>The latest version of Angular Material is used for making beautiful and simply UI.
