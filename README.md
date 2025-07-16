# SwiftConnector pour .NET 8

**SwiftConnector** est un connecteur HTTP asynchrone permettant de se connecter à l'API REST [OpenStack Swift](http://developer.openstack.org/api-ref-objectstorage-v1.html).

Ce projet a été conçu pour interagir avec l’espace de stockage OpenStack Swift d’un client, hébergé par **OVH**.

## 🛠️ Configuration

Pour utiliser ce connecteur, ajoutez la section suivante dans votre fichier `appsettings.json` en remplissant les informations d’authentification :

```json
"SwiftAuthentication": {
  "Authurl": "",
  "Username": "",
  "Password": "",
  "Region": ""
}
```

## 🔧 Enregistrement du service

Enregistrez ensuite le connecteur comme service via la méthode d’extension `AddSwiftClient`, qui nécessite une instance de `IConfiguration` pour accéder aux paramètres :

```csharp
public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
  services.AddSwiftClient(Configuration);
}
```

## 🚀 Utilisation

Une fois la configuration terminée, injectez simplement une instance de `ISwiftClient` pour :

- Lister les conteneurs disponibles
- Parcourir les fichiers stockés
- Ajouter de nouveaux fichiers dans les conteneurs

## 📦 Packages NuGet

| 📁 Package | 🧾 Version | 📥 Installation |
|------------|------------|------------------|
| `Tools.Swift.Connector` | [![NuGet](https://img.shields.io/nuget/v/Tools.Swift.Connector.svg)](https://www.nuget.org/packages/Tools.Swift.Connector) | `dotnet add package Tools.Swift.Connector` |

---
