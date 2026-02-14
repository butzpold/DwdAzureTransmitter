# DwdAzureTransmitter

### App Architecture
program.cs
|
Services/
├── ProperyPrinter.cs
├── Import/
│   ├── DwdApiClient.cs
│   └── DwdApiParser.cs
│
├── Transformation/
│   └── DwdDtoConverter.cs
│
├── Export/
│   └── SqlWeatherWriter.cs
│
Domains/
├── DwdApiModels.cs
└── WeatherRecord.cs
│
Models/
├── Meassurements.cs
└── StationData.cs
