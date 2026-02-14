# DwdAzureTransmitter
#### Read out /stationOverviewExtended from https://dwd.api.bund.dev/ and transform the crawled json-Files in a proper way to send it to an Azure SQL Server.

### App Architecture
program.cs                 <br> 
│                          <br>
Services/                  <br>
├── ProperyPrinter.cs      <br>
├── Import/                <br>
│   ├── DwdApiClient.cs    <br>
│   └── DwdApiParser.cs    <br>
│                          <br>
├── Transformation/        <br>
│   └── DwdDtoConverter.cs <br>
│                          <br>
├── Export/                <br>
│   └── SqWriter.cs        <br>
│   └── AzureClient.cs     <br>
│                          <br>
Domains/                   <br>
├── DwdApiModels.cs        <br>
└── WeatherRecord.cs       <br>
│                          <br>
Models/                    <br>
├── Meassurements.cs       <br>
└── StationData.cs         <br>
