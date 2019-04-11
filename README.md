# Description

Multi platform Movil App to consume [this](https://github.com/picassofb/SampleCodeBairesDevAspNetCore) Api


<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/1.png" width="200" title="Login"> <img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/2.png" width="200" title="Home Page / Projects">
<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/3.png" width="200" title="Tasks Page"> <img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/4.png" width="200" title="Add Project">


# Tecnologies

This was made using Xaramtin Forms with Microsoft.Net.Http for sending request over HTTP. The Model-View-ViewModel (MVVM) pattern was used to implement this solution.


# How to Use

First you need to change the UrlBase string Key Vale in App.xaml to point to the Web Api.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="SqueakyCleanEnergy.App">
    <Application.Resources>

        <x:String x:Key="UrlBase">YourWebApiServer</x:String>

        <ResourceDictionary>
            <Color x:Key="PrincipalColor">#3a4760</Color>
            <Color x:Key="TextColor">#ffffff</Color>
            <Color x:Key="SecundaryColor">#0a1934</Color>
            <Color x:Key="LogoColor">#23cbcb</Color>
        </ResourceDictionary>

    </Application.Resources>
</Application>

```

Don't forget to create your user in the Api; thats it!


Thank you!