# DwdAzureTransmitter

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
│   └── SqlWeatherWriter.cs<br>
│                          <br>
Domains/                   <br>
├── DwdApiModels.cs        <br>
└── WeatherRecord.cs       <br>
│                          <br>
Models/                    <br>
├── Meassurements.cs       <br>
└── StationData.cs         <br>
