
<div align="center">
<br />
  <h1>IOX API</h1>
  <p>
    An API to manage Vehicles and Accounts
  </p>
</div>

### :space_invader: Details

The API...

## :bat: Connection Settings

Set connection to connect to your MS SQL instance in the appsettings.json file.
NB: Entity Framework shall create the required Database and Tables.

```json
 "ConnectionStrings": {
    "databaseContext": "Server=localhost;Database=IOXFleetServicesAPI;User Id=sa;Password=xxxxx;TrustServerCertificate=True;"
  }
```

<br/>

## :eyes: Logging - Serilog

Logging Text/Json file

```html
logs/logs-.txt

logs/logs-.json
```

<br/>

## :fire: Entity Models

### Account
```csharp
public class Account : CoreModel
{
    [Key]
    public int Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public double TotalAmount { get; set; }

    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public List<Quote> Quotes { get; set; } = new List<Quote>();

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public User User { get; set; }

}
```

### Quote
```csharp
public class Quote : CoreModel
{
    [Key]
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? ValidTo { get; set; }

    public string QuoteNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public Vehicle vehicle { get; set; }
}
```

### Transaction
```csharp
public class Transaction : CoreModel
{
    [Key]
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string Type { get; set; } = string.Empty;

    public double Amount { get; set; }

}
```

<br/>

### User
```csharp
public class User : CoreModel
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string IDNumber { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

}
```

<br/>

### Vehicle
```csharp
public class Vehicle : CoreModel
{
    [Key]
    public int Id { get; set; }

    public string VIN { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public string PlateNumber { get; set; } = string.Empty;

    public DateTime? LicenseExpiry { get; set; }

    public string Model { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

}
```

<br/>

## :bird: Util Models

### Custom Response Message
```csharp
public class CustomResponseMessage<T>
{
    public int MessageCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public CustomResponseMessage()
    {
    }
}
```

### Core Model
```csharp
public class CoreModel
{
    public Guid CreatedBy { get; set; } = new Guid("8e4c7313-e3cd-4556-be01-56602945c049");

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid? ModifiedBy { get; set; } = null;

    public DateTime? ModifiedDate { get; set; } = null;

    public bool IsDeleted { get; set; } = false;
}
```

<br/>

## :dart: API Endpoints

#### User
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/user-blue)  

![Static Badge](https://img.shields.io/badge/Request--darkgreeen)
```json
{
  "firstName": "David",
  "lastName": "Hasselhoff",
  "idNumber": "8503175086086",
  "password": "qwe123",
  "email": "carl@carllabuschagne.com",
  "accountNumber": "LAB0001"
}
```
![Static Badge](https://img.shields.io/badge/Created-201-darkgreeen)
```json
{
	"messageCode": 201,
	"message": "User created for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/BadRequest-400-orange)
```json
{
	"messageCode": 400,
	"message": "Account already exists for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

<br />

#### Deposit
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/Account/Deposit-blue)  

REQUEST
```json
{
  "accountNumber": "LAB0001",
  "amount": 200
}
```
![Static Badge](https://img.shields.io/badge/Created-201-darkgreeen)
```json
{
	"messageCode": 201,
	"message": "Deposit created for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Account not found for: LAB001101",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

<br />

#### Quote
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/account/quote-blue)  

REQUEST
```json
{
  "accountNumber": "LAB0001",
  "plateNumber": "AB12DFGP"
}
```
![Static Badge](https://img.shields.io/badge/Created-201-darkgreeen)
```json
{
	"messageCode": 201,
	"message": "Quote created for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Account not found for: LAB001101",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

<br />

#### Renew License
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/Account/renewlicense-blue)  

REQUEST
```json
{
  "accountNumber": "LAB0001",
  "quoteNumber": "AB12DFGP20246612456"
}
```
![Static Badge](https://img.shields.io/badge/OK-201-darkgreeen)
```json
{
	"messageCode": 201,
	"message": "License has been renewed for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/OK-200-green)
```json
{
	"messageCode": 200,
	"message": "Insufficient funds for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Account not found for: LAB001101",
	"data": false
}
```
![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Quote not found for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

<br />

#### Add a Vehicle
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/Vehicle-blue)  

REQUEST
```json
{
  "accountNumber": "LAB0001",
  "vin": "AAAA12388872",
  "licenseNumber": "44441991817",
  "plateNumber": "KL45ZZGP",
  "licenseExpiry": "2025-06-05T14:59:24.911Z",
  "model": "BMW Coupe",
  "color": "White"
}
```
![Static Badge](https://img.shields.io/badge/Created-201-darkgreeen)
```json
{
	"messageCode": 201,
	"message": "Vehicle created for: LAB0001",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Account not found for: LAB001101",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/BadRequest-404-orange)
```json
{
	"messageCode": 400,
	"message": "Vehicle exists for: KL45ZZGP AAAA12388872 44441991817",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

<br />

#### Get Vehicle List
![Static Badge](https://img.shields.io/badge/POST-darkgreeen) ![Static Badge](https://img.shields.io/badge//v1/GetVehicleList-blue)  

REQUEST
```json
{
  "accountNumber": "LAB0001",
  "filter": "blue",
  "pageNumber": 1,
  "pageSize": 10
}
```
![Static Badge](https://img.shields.io/badge/Created-200-darkgreeen)
```json
{
	"messageCode": 200,
	"message": "",
	"data": [
		{
			"id": 1,
			"vin": "3N1AB7AP2DL645733",
			"licenseNumber": "1877265287356",
			"plateNumber": "AB12DFGP",
			"licenseExpiry": "2025-06-06T10:53:16.7159938",
			"model": "VW Bus",
			"color": "Blue",
			"createdBy": "8e4c7313-e3cd-4556-be01-56602945c049",
			"createdDate": "2024-06-05T15:01:44.6112623",
			"modifiedBy": null,
			"modifiedDate": null,
			"isDeleted": false
		}
	]
}
```

![Static Badge](https://img.shields.io/badge/NotFound-404-orange)
```json
{
	"messageCode": 404,
	"message": "Account not found for: LAB001101",
	"data": false
}
```

![Static Badge](https://img.shields.io/badge/InternalServerError-500-darkred)
```json
{
	"messageCode": 500,
	"message": "Error - Timeout has occurred.",
	"data": false
}
```

##
