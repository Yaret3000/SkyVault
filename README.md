# SkyVault

SkyVault is a cloud storage application where users can securely store their valuable files. Similar to popular cloud storage solutions like Google Drive, SkyVault offers a convenient and reliable way to store and access important documents, photos, videos, and more.

## Demo

![](https://github.com/Yaret3000/SkyVault/tree/main/_Media/SkyVaultDemo.gif)

## Technical Details

- Developed using ASP.NET 7 (C#) with Razor Pages and following the Model-View-Controller (MVC) pattern design
- Authentication powered by Azure Active Directory B2C
- File storage using Azure Blob Storage
- Metadata management using Cosmos DB with Entity Framework Core

## Key Features

- Secure and reliable storage: All files are securely stored in Azure Blob Storage, providing high availability and durability.
- Easy-to-use interface: SkyVault has a simple and intuitive interface, making it easy for users to upload, view, and manage their files.
- Integration with Azure Active Directory B2C: SkyVault leverages Azure Active Directory B2C for authentication, providing a secure and seamless experience for users.
- Metadata management: SkyVault uses Cosmos DB with Entity Framework Core to handle the database, allowing for efficient and reliable storage of metadata about each file, including the file name, size, and creation date.

## Development Good Practices

- Clean code architecture: The SkyVault project was developed with clean code and good practices to ensure scalability and maintainability.
- Providers flexibility: The project was designed to make it easy to switch to a different storage or database provider if needed.

## Getting Started

1. Create an account with SkyVault using your email address and a password.
2. Log in to the SkyVault app and navigate to the upload page.
3. Upload your files by selecting them from your device.
4. Your files will be securely stored in Azure Blob Storage and their metadata will be stored in Cosmos DB.
5. To download your files, simply log in to the SkyVault app and navigate to the file management page.

## Instructions

### Requirements

To run SkyVault in your device, you will need the following software:

- Visual Studio 2022 or Visual Studio Code
- .NET SDK (7.0.102) or later installed

### 1. Running the Application with Visual Studio

1. Open Visual Studio and select "Open a project or solution".
2. Navigate to the location of the SkyVault solution file (with a .sln extension) and select it.
3. Once the solution is open, select the SkyVault project as the startup project.
4. Hit the run button or use the keyboard shortcut "F5" to start the application.
5. The application should now be running in your local development environment.

### 2. Running the Application with Visual Studio Code

1. Open Visual Studio Code and select "Open Folder".
2. Navigate to the location of the SkyVault project and select the folder.
3. Once the project is open, open a terminal window in Visual Studio Code.
4. In the terminal, run the command "dotnet run" to start the application.
5. The application should now be running in your local development environment.

### 3. Appsetting JSON Configuration

SkyVault uses Azure services, so setup these services in your Azure directory and then configure the keys, urls and connection strings in the "appsettings.json" file.

AppSetting sections:

- AzureAdB2C: Setup Instance, ClientId and Domain

- AzureBlobConf: Setup ConnectionString and ContainerName.This container has to be created previously.

- CosmosDb: Setup AccountEndPoint, AccountKey and DatabaseName. The database has to be previously created with a collection called 'FilesMetadata' with Id field as primary key.

## Conclusion

SkyVault is a secure and convenient cloud storage solution that makes it easy for users to store, access, and share their valuable files. With its integration with Azure