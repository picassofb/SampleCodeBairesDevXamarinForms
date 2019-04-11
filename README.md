# Description

Multi platform Movil App to consume [this Api](https://github.com/picassofb/SampleCodeBairesDevAspNetCore)


<table>
	<tr>
		<td>
			<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/1.png" width="200" title="Login">
		</td>
		
		<td>
			<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/2.png" width="200" title="Home Page / Projects">
		</td>
		
		<td>
			<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/3.png" width="200" title="Tasks Page">
		</td>
		
		<td>
			<img src="https://github.com/picassofb/SampleCodeBairesDevXamarinForms/blob/master/SqueakyCleanEnergy/SqueakyCleanEnergy.Android/Resources/drawable/Captures/4.png" width="200" title="Add Project">
		</td>
	</tr>
</table>



# Tecnologies

This was made using ASP.NET Core 2.2, Entity Framework Core to interact with Sql Server. The services on the startup are configure to use Jwt Token and the authentication with lowered security for demonstration purpose only.


# How to Use

First you need to change the appsettings.json file and change the defaultConnection string to yours.

```json
{
  "ConnectionStrings": {
    "defaultConnection": "YourConnectionString"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

After that is necessary to run **update-database** in the package management console to create the database and the tables.



To start using first you need to create a user and login to obtain the JWT Token, with this token you can consume all the endpoints.

Please visit this [webpage](https://documenter.getpostman.com/view/2622970/S1ENxJ6i) for more information about this API.


Thank you!