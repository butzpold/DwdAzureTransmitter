# DwdAzureTransmitter
#### c# Console-App, reading out /stationOverviewExtended from https://dwd.api.bund.dev/ and transforms the crawled json-Files in a proper way to send it to an Azure SQL Server, respectively appends new data to the corresponding table.

### App Architecture
program.cs                 						<br> 
│                          						<br>
Services/                  						<br>
├── ProperyPrinter.cs      						<br>
├── Import/                						<br>
│   ├── DwdApiClient.cs    						<br>
│   └── DwdApiParser.cs    						<br>
│                          						<br>
├── Transformation/        						<br>
│   └── DwdDtoConverter.cs 						<br>
│                          						<br>
├── Export/                						<br>
│   └── AzureDbClient.cs     	(repository/ data access)		<br>
│   └── AzureDbContext.cs     	(mapping data to table-structure)	<br>
│   └── AzureDbExportService.cs (business logic)			<br>
│                          						<br>
Domains/                   						<br>
├── DwdApiModels.cs        						<br>
└── WeatherRecord.cs       						<br>
│                          						<br>
Models/                    						<br>
├── Meassurements.cs       						<br>
└── StationData.cs         						<br>
