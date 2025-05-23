# SwiftConnector pour .Net 8

***SwiftConnector*** est un connecteur HTTP asynchrone qui permet de se connecter à l'API REST [OpenStack Swift](http://developer.openstack.org/api-ref-objectstorage-v1.html). 

Ce projet a été utilisé pour se connecter à l'espace de stockage OpenStack Swift d'un client hébergé par OVH. Pour utiliser ce connecteur, il suffit d'ajouter cette section dans votre fichier ***appsettings.json*** en y renseignant les informations d'authentification de votre espace de stockage.

```json
  "SwiftAuthentication": {
    "Authurl": "",
    "Username": "",
    "Password": "",
    "Region": ""
  }
```

Enfin, il suffit d'enregistrer le connecteur comme service dans ***IServiceCollection***.La méthode d'extension ***AddSwiftClient*** requiert comme paramètre toutes les propriétés de configuration d'application ***IConfiguration*** pour obtenir toutes les informations d'authentification.

```cs
public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
  services.AddSwiftClient(Configuration);
}
```
Une fois la configuration précédente effectuée, il suffit d'une instance de ***ISwiftClient*** pour accéder à tous les conteneurs auxquels vous avez accès, ainsi que pour connaître tous les fichiers présents dans ces conteneurs et ajouter de nouveaux fichiers.